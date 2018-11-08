using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DevUtils.Datas;
using DevUtils.Models;
using DevUtils.Views;
using Prism.Commands;
using Prism.Mvvm;
using UtilModelService;

namespace DevUtils.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "我的工具";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<IUtilModel> MineUtils
        {
            get { return DataManager.Instance().CurAppData.UtilsData.MineUtils; }
        }

        private Action<int, int> _moveMineUtilPosAction = null;
        public Action<int, int> MoveMineUtilPosAction
        {
            get
            {
                if (_moveMineUtilPosAction == null)
                    _moveMineUtilPosAction = (si, ti) => MineUtils.Move(si, ti);

                return _moveMineUtilPosAction;
            }
        }

        public DelegateCommand SettingCommand { get; set; }
        public DelegateCommand UtilsCommand { get; set; }
        public DelegateCommand<IUtilModel> RunUtilCommand { get; set; }

        public MainWindowViewModel()
        {
            SettingCommand = new DelegateCommand(OpenSetting);
            UtilsCommand = new DelegateCommand(OpenUtils);
            RunUtilCommand = new DelegateCommand<IUtilModel>(RunUtil);
        }

        private void OpenSetting()
        {
            (new CommonWindow
            {
                Title = "设置",
                Height = 385,
                Width = 550,
                Content = new SettingControl
                {
                    DataContext = new SettingControlViewModel(DataManager.Instance().CurAppData.SettingsData)
                }
            }).ShowDialog();
        }

        private void OpenUtils()
        {
            (new CommonWindow
            {
                Title = "工具",
                Height = 550,
                Width = 920,
                Content = new UtilControl
                {
                    DataContext = new UtilControlViewModel(DataManager.Instance().CurAppData.UtilsData)
                }
            }).ShowDialog();
        }

        private void RunUtil(IUtilModel util)
        {
            if (util != null)
                util.Run();
        }
    }
}
