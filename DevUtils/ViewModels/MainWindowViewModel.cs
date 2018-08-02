using System.Windows.Controls.Primitives;
using System.Windows.Input;
using DevUtils.Views;
using Prism.Commands;
using Prism.Mvvm;
using Theme.Extensions;

namespace DevUtils.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "快捷工具";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand _settingCommand = null;
        public ICommand SettingCommand
        {
            get
            {
                return _settingCommand ?? (_settingCommand = new DelegateCommand(
                    () =>
                    {
                        (new CommonWindow() { Width = 200, Height = 200 }).ShowDialog();
                    }));
            }
        }

        private DelegateCommand _utilsCommand = null;
        public ICommand UtilsCommand
        {
            get
            {
                return _utilsCommand ?? (new DelegateCommand(
                   () =>
                   {

                   }));
            }
        }
    }
}
