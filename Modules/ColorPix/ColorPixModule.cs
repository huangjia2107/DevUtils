using ColorPix.Services;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using UtilModelService;

namespace ColorPix
{
    public class ColorPixModule : IModule
    {
        private IRegionManager _regionManager = null;

        #region IModule Members

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IUtilModel, ColorPixUtilModel>("ColorPixModule");
        }

        #endregion
    }
}
