using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using DevUtils.Datas;
using DevUtils.Models;
using DevUtils.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Theme.Extensions;

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

        private DelegateCommand _settingCommand = null;
        public ICommand SettingCommand
        {
            get
            {
                return _settingCommand ?? (_settingCommand = new DelegateCommand(
                    () =>
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
                   }));
            }
        }

        private DelegateCommand<UtilModel> _openUtilCommand = null;
        public ICommand OpenUtilCommand
        {
            get
            {
                return _openUtilCommand ?? (new DelegateCommand<UtilModel>(
                   util =>
                   {
                       MessageBox.Show(util.Name);
                   }));
            }
        }

        public ObservableCollection<UtilModel> MineUtils
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
    }
}
