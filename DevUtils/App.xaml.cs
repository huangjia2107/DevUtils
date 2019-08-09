using System;
using System.Collections.ObjectModel;
using System.IO;
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
using CommandService;
using Utils.IO;
using DevUtils.Models;

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
            var localAppData = FileHelper.LoadFromJsonFile<AppData>(ResourcesMap.LocationDictionary[Location.SettingFile]);
            if (localAppData != null)
                containerRegistry.RegisterInstance(localAppData);
            else
                containerRegistry.RegisterSingleton<AppData>();

            //containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
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

            if (appData == null)
                return;

            //check AllUtils
            if(appData.SettingsData.AutoRemoveInvalidUtils)
            {
                appData.UtilsData.AllUtils.RemoveAll(util => !File.Exists(util.Location));
            }

            //merge utilModels
            if (utilModels != null && utilModels.Length > 0)
            {
                Array.ForEach(utilModels, util =>
                {
                    if (!appData.UtilsData.AllUtils.Exists(u => u.Token == util.Token))
                    {
                        appData.UtilsData.AllUtils.Add(new UtilModel(util.Token, util.Name, util.Description, util.Location, util.Type));
                    }
                });  
            }

            //check MineUtils
            appData.UtilsData.MineUtils.RemoveAll(token => !appData.UtilsData.AllUtils.Exists(util => util.Token == token));
        }
    }
}
