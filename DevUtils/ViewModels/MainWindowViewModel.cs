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
using System.Linq;
using Prism.Events;
using DevUtils.Events;
using System.ComponentModel;
using Utils.IO;

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
         
        private ObservableCollection<UtilViewModel> _mineUtils;
        public ObservableCollection<UtilViewModel> MineUtils
        {
            get
            {
                if (_mineUtils == null)
                    _mineUtils = new ObservableCollection<UtilViewModel>(_appData.UtilsData.MineUtils.Select(u => new UtilViewModel(u)));

                return _mineUtils;
            }
        }

        private Action<int, int> _moveMineUtilPosAction = null;
        public Action<int, int> MoveMineUtilPosAction
        {
            get
            {
                if (_moveMineUtilPosAction == null)
                    _moveMineUtilPosAction = (si, ti) =>
                    { 
                        if (si == ti)
                            return;

                        _mineUtils.Move(si, ti);

                        var util = _appData.UtilsData.MineUtils[si];

                        _appData.UtilsData.MineUtils.RemoveAt(si);
                        _appData.UtilsData.MineUtils.Insert(si < ti ? Math.Max(0, ti - 1) : ti, util);

                        _moveMineUtilsEvent.Publish(new MoveUtilParam { Token = 1, SourceIndex = si, TargetIndex = ti });
                    };

                return _moveMineUtilPosAction;
            }
        }

        private IEventAggregator _eventAggregator = null;
        private MoveMineUtilsEvent _moveMineUtilsEvent = null;
        private AppData _appData = null; 

        public DelegateCommand SettingCommand { get; private set; }
        public DelegateCommand UtilsCommand { get; private set; }
        public DelegateCommand<UtilViewModel> RunUtilCommand { get; private set; }

        public DelegateCommand ExpandOrCollapseCommand { get; private set; }
        public DelegateCommand CollapseCommand { get; private set; }

        public DelegateCommand ClosingCommand { get; private set; }

        public MainWindowViewModel(IContainerExtension container, IEventAggregator eventAggregator)
        {
            _appData = container.Resolve<AppData>();
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AddToMineUtilsEvent>().Subscribe(AddToMineUtils);
            _eventAggregator.GetEvent<DeleteFromMineUtilsEvent>().Subscribe(DeleteFromMineUtils);

            _moveMineUtilsEvent = _eventAggregator.GetEvent<MoveMineUtilsEvent>();
            _moveMineUtilsEvent.Subscribe(MoveMineUtils, ThreadOption.PublisherThread, false, p => p.Token == 0);

            SettingCommand = new DelegateCommand(OpenSetting);
            UtilsCommand = new DelegateCommand(OpenUtils);
            RunUtilCommand = new DelegateCommand<UtilViewModel>(RunUtil);

            ExpandOrCollapseCommand = new DelegateCommand(ExpandOrCollapse);
            CollapseCommand = new DelegateCommand(Collapse);

            ClosingCommand = new DelegateCommand(Closing);

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

        private void Closing()
        {
            //FileHelper.SaveToXmlFile<AppData>(AppDomain.CurrentDomain.BaseDirectory + "settings.xml",_appData);
        }

        #region IEventAggregator

        public void AddToMineUtils(UtilModel utilModel)
        {
            var utilViewModel = _mineUtils.FirstOrDefault(vm => vm.Model == utilModel);
            if (utilViewModel != null)
                return;

            _mineUtils.Add(new UtilViewModel(utilModel));
        }

        public void DeleteFromMineUtils(UtilModel utilModel)
        {
            var utilViewModel = _mineUtils.FirstOrDefault(vm => vm.Model == utilModel);
            if (utilViewModel == null)
                return;

            _mineUtils.Remove(utilViewModel);
        }

        public void MoveMineUtils(MoveUtilParam param)
        {
            if (param.SourceIndex >= _mineUtils.Count || param.TargetIndex >= _mineUtils.Count)
                return;

            _mineUtils.Move(param.SourceIndex, param.TargetIndex);
        }

        #endregion
    }    
}        
