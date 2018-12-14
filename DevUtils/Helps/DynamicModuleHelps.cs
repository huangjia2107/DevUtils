using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Threading;
using CommonServiceLocator;
using Prism.Modularity;

namespace DevUtils.Helps
{
    /// <summary>
    /// https://www.infragistics.com/community/blogs/b/blagunas/posts/prism-dynamically-discover-and-load-modules-at-runtime
    /// </summary>
    public class DynamicModuleHelps
    {
        SynchronizationContext _context;

        private static DynamicModuleHelps _dynamicModuleHelps = new DynamicModuleHelps(); 
        public static DynamicModuleHelps Instance()
        {
            return _dynamicModuleHelps;
        }

        public DynamicModuleHelps()
        {
            _context = SynchronizationContext.Current;
        }

        /// <summary>
        /// build the child domain and search for the assemblies.
        /// </summary>
        public void LoadModule(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new InvalidOperationException("Path cannot be null.");

            if (!File.Exists(path))
                throw new InvalidOperationException(string.Format("File {0} could not be found.", path));

            var childDomain = BuildChildDomain(AppDomain.CurrentDomain);
            try
            {
                var loadedAssemblies = (from Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()
                                        where !(assembly is System.Reflection.Emit.AssemblyBuilder)
                                              && assembly.GetType().FullName != "System.Reflection.Emit.InternalAssemblyBuilder"
                                              && !String.IsNullOrEmpty(assembly.Location)
                                        select assembly.Location);

                var loaderType = typeof(InnerModuleInfoLoader);

                var loader = (InnerModuleInfoLoader)childDomain.CreateInstanceFrom(loaderType.Assembly.Location, loaderType.FullName).Unwrap();
                loader.LoadAssemblies(loadedAssemblies);

                //get the ModuleInfo
                var module = loader.GetModuleInfo(path);

                //load
                ActivateModule(module);
            }
            finally
            {
                AppDomain.Unload(childDomain);
            }
        }

        public void UnloadModule()
        {
            
        }

        /// <summary>
        /// Creates a new child domain and copies the evidence from a parent domain.
        /// </summary>
        /// <param name="parentDomain">The parent domain.</param>
        /// <returns>The new child domain.</returns>
        /// <remarks>
        /// Grabs the <paramref name="parentDomain"/> evidence and uses it to construct the new
        /// <see cref="AppDomain"/> because in a ClickOnce execution environment, creating an
        /// <see cref="AppDomain"/> will by default pick up the partial trust environment of 
        /// the AppLaunch.exe, which was the root executable. The AppLaunch.exe does a 
        /// create domain and applies the evidence from the ClickOnce manifests to 
        /// create the domain that the application is actually executing in. This will 
        /// need to be Full Trust for Composite Application Library applications.
        /// </remarks>
        /// <exception cref="ArgumentNullException">An <see cref="ArgumentNullException"/> is thrown if <paramref name="parentDomain"/> is null.</exception>
        private AppDomain BuildChildDomain(AppDomain parentDomain)
        {
            if (parentDomain == null)
                throw new ArgumentNullException("parentDomain");

            var evidence = new Evidence(parentDomain.Evidence);
            return AppDomain.CreateDomain("DiscoveryRegion", evidence, parentDomain.SetupInformation);
        }

        /// <summary>
        /// Uses the IModuleManager to load the modules into memory
        /// </summary>
        /// <param name="module"></param>
        private void ActivateModule(ModuleInfo module)
        {
            if (_context == null || module == null)
                return;

            var catalog = ServiceLocator.Current.GetInstance<IModuleCatalog>();
            var manager = ServiceLocator.Current.GetInstance<IModuleManager>();

            _context.Send((delegate
            {
                //add the ModuleInfo to the catalog so it can be loaded
                catalog.AddModule(module);
                //load
                manager.LoadModule(module.ModuleName);
            }), null);
        }

        private class InnerModuleInfoLoader : MarshalByRefObject
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
            internal ModuleInfo GetModuleInfo(string path)
            {
                var moduleReflectionOnlyAssembly =
                    AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().First(
                        asm => asm.FullName == typeof(IModule).Assembly.FullName);

                var moduleType = moduleReflectionOnlyAssembly.GetType(typeof(IModule).FullName);
                var info = new FileInfo(path);

                ResolveEventHandler resolveEventHandler = (sender, args) => OnReflectionOnlyResolve(args);

                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += resolveEventHandler;
                var modules = GetNotAllReadyLoadedModuleInfos(info, moduleType);
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= resolveEventHandler;

                if (modules != null)
                    return modules.First();

                return null;
            }

            private static IEnumerable<ModuleInfo> GetNotAllReadyLoadedModuleInfos(FileInfo fileInfo, Type moduleType)
            {
                if (fileInfo == null)
                    throw new ArgumentNullException("fileInfo");

                var alreadyLoadedAssemblies = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies();

                if (alreadyLoadedAssemblies.FirstOrDefault(assembly => string.Compare(Path.GetFileName(assembly.Location), fileInfo.Name, StringComparison.OrdinalIgnoreCase) == 0) == null)
                {
                    //look for all the classes that implement IModule in our assembly and create a ModuleInfo class from it
                    return Assembly.ReflectionOnlyLoadFrom(fileInfo.FullName).GetExportedTypes()
                                     .Where(moduleType.IsAssignableFrom)
                                     .Where(t => t != moduleType)
                                     .Where(t => !t.IsAbstract).Select(t => CreateModuleInfo(t));
                }

                return null;
            }

            private static Assembly OnReflectionOnlyResolve(ResolveEventArgs args)
            {
                return AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().FirstOrDefault(
                    asm => string.Equals(asm.FullName, args.Name, StringComparison.OrdinalIgnoreCase));
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
            internal void LoadAssemblies(IEnumerable<string> assemblies)
            {
                foreach (var assemblyPath in assemblies)
                {
                    try
                    {
                        Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                    }
                    catch (FileNotFoundException)
                    {
                        // Continue loading assemblies even if an assembly can not be loaded in the new AppDomain
                    }
                }
            }

            private static ModuleInfo CreateModuleInfo(Type type)
            {
                var onDemand = false;
                var moduleName = type.Name;
                var moduleAttribute = CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleAttribute).FullName);

                if (moduleAttribute != null)
                {
                    foreach (var argument in moduleAttribute.NamedArguments)
                    {
                        var argumentName = argument.MemberInfo.Name;
                        switch (argumentName)
                        {
                            case "ModuleName":
                                moduleName = (string)argument.TypedValue.Value;
                                break;

                            case "OnDemand":
                                onDemand = (bool)argument.TypedValue.Value;
                                break;

                            case "StartupLoaded":
                                onDemand = !((bool)argument.TypedValue.Value);
                                break;
                        }
                    }
                }

                var moduleDependencyAttributes = CustomAttributeData.GetCustomAttributes(type).Where(cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleDependencyAttribute).FullName);
                var dependsOn = moduleDependencyAttributes.Select(cad => (string)cad.ConstructorArguments[0].Value).ToList();

                var moduleInfo = new ModuleInfo(moduleName, type.AssemblyQualifiedName)
                {
                    InitializationMode = onDemand ? InitializationMode.OnDemand : InitializationMode.WhenAvailable,
                    Ref = type.Assembly.CodeBase,
                };
                moduleInfo.DependsOn.AddRange(dependsOn);

                return moduleInfo;
            }
        }
    }
}
