using System.Windows;
using DevUtils.Datas;
using DevUtils.Views;
using Prism.Autofac;
using Prism.Ioc;
using Prism.Modularity;

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
    }
}
