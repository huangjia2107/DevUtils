using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using RegexTester.Services;
using UtilModelService;

namespace RegexTester
{
    public class RegexTesterModule : IModule
    {
        private IRegionManager _regionManager = null;

        #region IModule Members

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IUtilModel, RegexTesterUtilModel>("RegexTesterModule");
        }

        #endregion
    }
}
