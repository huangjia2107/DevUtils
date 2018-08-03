using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevUtils.Models;
using Prism.Mvvm;

namespace DevUtils.ViewModels
{
    public class SettingControlViewModel : BindableBase
    {
        private SettingModel _settingModel = null;

        public SettingControlViewModel(SettingModel settingModel)
        {
            _settingModel = settingModel;
        }

        public int ShortcutCount
        {
            get { return _settingModel.ShortcutCount; }
            set
            {
                _settingModel.ShortcutCount = value;
                RaisePropertyChanged();
            }
        }

        public bool IsStartupWithSystem
        {
            get { return _settingModel.IsStartupWithSystem; }
            set
            {
                _settingModel.IsStartupWithSystem = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSystemTrayAfterStartup
        {
            get { return _settingModel.IsSystemTrayAfterStartup; }
            set
            {
                _settingModel.IsSystemTrayAfterStartup = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSystemTrayAfterClose
        {
            get { return _settingModel.IsSystemTrayAfterClose; }
            set
            {
                _settingModel.IsSystemTrayAfterClose = value;
                RaisePropertyChanged();
            }
        }
    }
}
