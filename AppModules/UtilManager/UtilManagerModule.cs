using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace UtilManager
{
    public class UtilManagerModule : IModule
    {
        private IRegionManager _regionManager = null;

        public UtilManagerModule(IRegionManager regionManager)
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
