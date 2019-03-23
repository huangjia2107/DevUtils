using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevUtils.Datas;
using DevUtils.Models;
using DevUtils.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using UtilModelService;
using Prism.Events;
using DevUtils.Events;
using Prism.Interactivity.InteractionRequest;

namespace DevUtils.ViewModels
{
    public class UtilControlViewModel : BindableBase
    { 
        private ObservableCollection<ClassifiedUtil> _classifiedUtils;
        public ObservableCollection<ClassifiedUtil> ClassifiedUtils
        {
            get
            {
                if (_classifiedUtils == null)
                {
                    _classifiedUtils = new ObservableCollection<ClassifiedUtil>(
                          _utilData.AllUtils.Select(u=>new UtilViewModel(u))
                                            .GroupBy(u => u.Type)
                                            .OrderBy(g => g.Key)
                                            .Select((g, i) => new ClassifiedUtil { Index = i, Type = g.Key, Utils = new ObservableCollection<UtilViewModel>(g) }));

                    var array = Enum.GetValues(typeof(UtilType));
                    if (array.Length == _classifiedUtils.Count)
                        return _classifiedUtils;

                    int index = 0;
                    foreach (UtilType type in array)
                    {
                        var classifiedUtil = _classifiedUtils.FirstOrDefault(cu => cu.Type == type);
                        if (classifiedUtil == null)
                        {
                            var newClassifiedUtil = new ClassifiedUtil
                            {
                                Index = index,
                                Type = type,
                                Utils = new ObservableCollection<UtilViewModel>()
                            };

                            if (index < array.Length - 1)
                                _classifiedUtils.Insert(index, newClassifiedUtil);
                            else
                                _classifiedUtils.Add(newClassifiedUtil);
                        }
                        else
                            classifiedUtil.Index = index;

                        index++;
                    }
                }

                return _classifiedUtils;
            }
        }

        private ObservableCollection<UtilViewModel> _mineUtils;
        public ObservableCollection<UtilViewModel> MineUtils
        {
            get
            {
                if (_mineUtils == null)
                    _mineUtils = new ObservableCollection<UtilViewModel>(_utilData.MineUtils.Select(u => new UtilViewModel(u)));

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

                        var util = _utilData.MineUtils[si];

                        _utilData.MineUtils.RemoveAt(si);
                        _utilData.MineUtils.Insert(si < ti ? Math.Max(0, ti-1) : ti, util);
                         
                       _moveMineUtilsEvent.Publish(new MoveUtilParam { Token = 0, SourceIndex = si, TargetIndex = ti});
                    };

                return _moveMineUtilPosAction;
            }
        }

        private bool _isEdited;
        public bool IsEdited
        {
            get { return _isEdited; }
            set { SetProperty(ref _isEdited, value); }
        } 

        private IEventAggregator _eventAggregator = null;
        private AddToMineUtilsEvent _addToMineUtilsEvent = null;
        private DeleteFromMineUtilsEvent _deleteFromMineUtilsEvent = null;
        private MoveMineUtilsEvent _moveMineUtilsEvent = null;

        private UtilData _utilData = null;

        public DelegateCommand<UtilViewModel> DeleteFromMineCommand { get; set; }
        public DelegateCommand<UtilViewModel> DeleteFromAllCommand { get; set; }
        public DelegateCommand<UtilViewModel> AddToMineCommand { get; set; }

        public DelegateCommand<UtilType?> OpenAddUtilByTypeCommand { get; set; }
        public DelegateCommand OpenAddUtilCommand { get; set; }

        public UtilControlViewModel(IContainerExtension container, IEventAggregator eventAggregator)
        {
            _utilData = container.Resolve<AppData>().UtilsData;
            _eventAggregator = eventAggregator; 

            _eventAggregator.GetEvent<AddUtilEvent>().Subscribe(AddUtil);

            _addToMineUtilsEvent = _eventAggregator.GetEvent<AddToMineUtilsEvent>();
            _deleteFromMineUtilsEvent = _eventAggregator.GetEvent<DeleteFromMineUtilsEvent>();

            _moveMineUtilsEvent = _eventAggregator.GetEvent<MoveMineUtilsEvent>();
            _moveMineUtilsEvent.Subscribe(MoveMineUtils, ThreadOption.PublisherThread, false, p => p.Token == 1);



            DeleteFromMineCommand = new DelegateCommand<UtilViewModel>(DeleteUtilModelFromMine);
            DeleteFromAllCommand = new DelegateCommand<UtilViewModel>(DeleteUtilModelFromAll);

            AddToMineCommand = new DelegateCommand<UtilViewModel>(AddToMine);
            OpenAddUtilByTypeCommand = new DelegateCommand<UtilType?>(OpenAddUtilByType);
            OpenAddUtilCommand = new DelegateCommand(OpenAddUtil);
        }

        private void DeleteUtilModelFromAll(UtilViewModel utilViewModel)
        { 
            //popup confirm dialog 
            var classifiedUtil = _classifiedUtils.FirstOrDefault(utils => utils.Type == utilViewModel.Type);
            if (classifiedUtil == null || !classifiedUtil.Utils.Contains(utilViewModel))
                return;

            classifiedUtil.Utils.Remove(utilViewModel);
            classifiedUtil.RefreshIsEmpty();

            DeleteUtilModelFromMine(utilViewModel);

            if (_utilData.AllUtils.Contains(utilViewModel.Model))
                _utilData.AllUtils.Remove(utilViewModel.Model);
        }

        private void DeleteUtilModelFromMine(UtilViewModel utilViewModel)
        {
            utilViewModel.IsMine = false;

            if (_mineUtils.Contains(utilViewModel))
                _mineUtils.Remove(utilViewModel);

            _deleteFromMineUtilsEvent.Publish(utilViewModel.Model);
        }

        private void AddToMine(UtilViewModel utilViewModel)
        {
            utilViewModel.IsMine = true;

            if (!_mineUtils.Contains(utilViewModel)) 
                _mineUtils.Add(utilViewModel);  

            if (!_utilData.MineUtils.Contains(utilViewModel.Model))
                _utilData.MineUtils.Add(utilViewModel.Model);

            _addToMineUtilsEvent.Publish(utilViewModel.Model);
        }

        private void OpenAddUtilByType(UtilType? utilType)
        {
            (new CommonWindow
            {
                Title = "自定义",
                Height = 385,
                Width = 550,
                Content = new AddUtilControl() { DataContext = new AddUtilControlViewModel(utilType.Value) }
            }).ShowDialog();
        }

        private void OpenAddUtil()
        {
            (new CommonWindow
            {
                Title = "自定义",
                Height = 385,
                Width = 550,
                Content = new AddUtilControl() { DataContext = new AddUtilControlViewModel(UtilType.Coder) }
            }).ShowDialog();
        }

        #region IEventAggregator

        private void AddUtil(IUtilModel utilModel)
        {
            if (utilModel == null)
                return; 

            var classifiedUtil = _classifiedUtils.FirstOrDefault(u=>u.Type== utilModel.Type);
            if(classifiedUtil != null)
            {
                classifiedUtil.Utils.Add(new UtilViewModel(utilModel));
                classifiedUtil.RefreshIsEmpty(); 
            } 
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
