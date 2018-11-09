using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; 
using DevUtils.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace DevUtils.ViewModels
{
    public class UtilControlViewModel : BindableBase
    {
        private UtilData _utilData = null;

        public DelegateCommand<UtilViewModel> DeleteFromMineCommand { get; set; }
        public DelegateCommand<UtilViewModel> DeleteFromAllCommand { get; set; }
        public DelegateCommand<UtilViewModel> AddToMineCommand { get; set; }
        public DelegateCommand AddUtilCommand { get; set; }

        public UtilControlViewModel(UtilData utilData)
        {
            _utilData = utilData;

            DeleteFromMineCommand = new DelegateCommand<UtilViewModel>(DeleteUtilModelFromMine);
            DeleteFromAllCommand = new DelegateCommand<UtilViewModel>(DeleteUtilModelFromAll);

            AddToMineCommand = new DelegateCommand<UtilViewModel>(AddToMine);
            AddUtilCommand=new DelegateCommand(AddUtil);
        }

        public IEnumerable<ClassifiedUtil> ClassifiedUtils
        {
            get { return _utilData.ClassifiedUtils; }
        }

        public ObservableCollection<UtilViewModel> MineUtils
        {
            get { return _utilData.MineUtils; }
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

        private bool _isEdited;
        public bool IsEdited
        {
            get { return _isEdited; }
            set { SetProperty(ref _isEdited, value); }
        }

        private void DeleteUtilModelFromAll(UtilViewModel utilViewModel)
        {
            var classifiedUtil = _utilData.ClassifiedUtils.FirstOrDefault(utils => utils.Type == utilViewModel.Type);
            if (classifiedUtil == null || !classifiedUtil.Utils.Contains(utilViewModel))
                return;

            classifiedUtil.Utils.Remove(utilViewModel);
            DeleteUtilModelFromMine(utilViewModel);

            if (classifiedUtil.Utils.Count == 0)
            {
                _utilData.ClassifiedUtils.Remove(classifiedUtil);

                //TDO: notify ui to update selection status
            }
        }

        private void DeleteUtilModelFromMine(UtilViewModel utilViewModel)
        {
            if (_utilData.MineUtils.Contains(utilViewModel))
            {
                utilViewModel.IsMine = false;
                _utilData.MineUtils.Remove(utilViewModel);
            }
        }

        private void AddToMine(UtilViewModel utilViewModel)
        {
            if (!_utilData.MineUtils.Contains(utilViewModel))
            {
                utilViewModel.IsMine = true;
                _utilData.MineUtils.Add(utilViewModel);
            }
        }

        private void AddUtil()
        {
            
        }
    }
}
