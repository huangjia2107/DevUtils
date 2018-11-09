using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevUtils.Datas;
using DevUtils.ViewModels;
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
            var utilModelServices = Container.Resolve<IUtilModel[]>();

            DataManager.Instance().CurAppData.UtilsData.AllUtils.AddRange(utilModelServices.Select(u => new UtilViewModel(u)));
            //DataManager.Instance().CurAppData.UtilsData.MineUtils.AddRange(utilModelServices);
        }
    }
}
