using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace RegexTester
{
    public class RegexTesterModule : IModule
    {
        private IRegionManager _regionManager = null;

        public RegexTesterModule(IRegionManager regionManager)
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
