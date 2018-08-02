using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Setting
{
    public class SettingModule : IModule
    {
        private IRegionManager _regionManager = null;

        public SettingModule(IRegionManager regionManager)
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
