using System.Windows;
using DevUtils.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using UtilModelService;

namespace DevUtils
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            //get module service implementations
            var utilModelServices = Container.Resolve<IUtilModelService[]>();
        }
    }
}
