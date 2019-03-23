using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevUtils.Datas;
using DevUtils.ViewModels;
using DevUtils.Views;
using Prism.Ioc;
using Prism.Mvvm;
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
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //for view-viewmodel map
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            ViewModelLocationProvider.Register<SettingControl, SettingControlViewModel>();
            ViewModelLocationProvider.Register<UtilControl, UtilControlViewModel>();
            //ViewModelLocationProvider.Register<AddUtilControl, AddUtilControlViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<AppData>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var appData = Container.Resolve<AppData>();
            var utilModels = Container.Resolve<IUtilModel[]>();

            if (appData == null || utilModels == null || utilModels.Length == 0)
                return;

            appData.UtilsData.AllUtils.AddRange(utilModels);
        }
    }
}
