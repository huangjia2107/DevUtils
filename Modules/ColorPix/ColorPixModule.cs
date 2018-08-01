using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ColorPix
{
    public class ColorPixModule : IModule
    {
        private IRegionManager _regionManager = null;

        public ColorPixModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region IModule Members

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        #endregion
    }
}
