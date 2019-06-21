using System.IO;
using DevUtils.Datas;
using DevUtils.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;

namespace DevUtils.ViewModels
{
    public class SettingControlViewModel : BindableBase
    {
        private AppData _appData = null;

        private IEventAggregator _eventAggregator = null;
        private RefreshMineUtilsEvent _refreshMineUtilsEvent = null;

        public SettingControlViewModel(IContainerExtension container, IEventAggregator eventAggregator)
        {
            _appData = container.Resolve<AppData>();
            _eventAggregator = eventAggregator;

            _refreshMineUtilsEvent = _eventAggregator.GetEvent<RefreshMineUtilsEvent>();
        }

        public bool AutoRemoveInvalidUtils
        {
            get { return _appData.SettingsData.AutoRemoveInvalidUtils; }
            set
            {
                if (value)
                {
                    _appData.UtilsData.AllUtils.RemoveAll(util => !File.Exists(util.Location));
                    _appData.UtilsData.MineUtils.RemoveAll(token => !_appData.UtilsData.AllUtils.Exists(util => util.Token == token));

                    _refreshMineUtilsEvent.Publish();
                }

                _appData.SettingsData.AutoRemoveInvalidUtils = value;
                RaisePropertyChanged();
            }
        }

        public bool IsStartupWithSystem
        {
            get { return _appData.SettingsData.IsStartupWithSystem; }
            set
            {
                _appData.SettingsData.IsStartupWithSystem = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSystemTrayAfterStartup
        {
            get { return _appData.SettingsData.IsSystemTrayAfterStartup; }
            set
            {
                _appData.SettingsData.IsSystemTrayAfterStartup = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSystemTrayAfterClose
        {
            get { return _appData.SettingsData.IsSystemTrayAfterClose; }
            set
            {
                _appData.SettingsData.IsSystemTrayAfterClose = value;
                RaisePropertyChanged();
            }
        }
    }
}
