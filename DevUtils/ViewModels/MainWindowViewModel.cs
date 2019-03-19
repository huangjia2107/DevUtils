using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DevUtils.Datas;
using DevUtils.Models;
using DevUtils.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using UtilModelService;
using System.Windows.Controls;

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

        private double _height = 0d;
        public double Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        private SizeToContent _sizeToContent = SizeToContent.Manual;
        public SizeToContent SizeToContent
        {
            get { return _sizeToContent; }
            set { SetProperty(ref _sizeToContent, value); }
        } 

        private ScrollBarVisibility _verticalScrollBarVisibility= ScrollBarVisibility.Disabled;
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return _verticalScrollBarVisibility; }
            set { SetProperty(ref _verticalScrollBarVisibility, value); }
        } 

        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }

        public ObservableCollection<UtilViewModel> MineUtils
        {
            get { return _appData.UtilsData.MineUtils; }
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

        private AppData _appData = null; 

        public DelegateCommand SettingCommand { get; set; }
        public DelegateCommand UtilsCommand { get; set; }
        public DelegateCommand<UtilViewModel> RunUtilCommand { get; set; }

        public DelegateCommand ExpandOrCollapseCommand { get; set; }
        public DelegateCommand CollapseCommand { get; set; }

        public MainWindowViewModel(IContainerExtension container)
        { 
            _appData = container.Resolve<AppData>();

            SettingCommand = new DelegateCommand(OpenSetting);
            UtilsCommand = new DelegateCommand(OpenUtils);
            RunUtilCommand = new DelegateCommand<UtilViewModel>(RunUtil);
            ExpandOrCollapseCommand = new DelegateCommand(ExpandOrCollapse);
            CollapseCommand = new DelegateCommand(Collapse);
        }

        private void OpenSetting()
        {
            (new CommonWindow
            {
                Title = "设置",
                Height = 385,
                Width = 550,
                Content = new SettingControl()
            }).ShowDialog();
        }

        private void OpenUtils()
        {
            (new CommonWindow
            {
                Title = "工具",
                Height = 550,
                Width = 920,
                Content = new UtilControl()
            }).ShowDialog();
        }

        private void RunUtil(UtilViewModel utilViewModel)
        {
            if (utilViewModel != null)
                utilViewModel.Run();
        }

        private void ExpandOrCollapse()
        {
            ExpandOrCollapse(IsExpanded); 
        }

        private void Collapse()
        {
            IsExpanded = false;
            ExpandOrCollapse();
        }

        private void ExpandOrCollapse(bool isExpand)
        {
            if (isExpand)
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                SizeToContent = SizeToContent.Height;
            }
            else
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                SizeToContent = SizeToContent.Manual;
                Height = 88;
            }
        }
    }
}
