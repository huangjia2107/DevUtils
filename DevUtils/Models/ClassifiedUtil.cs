using System.Collections.ObjectModel;
using DevUtils.ViewModels;
using UtilModelService;
using Prism.Mvvm;

namespace DevUtils.Models
{
    public class ClassifiedUtil : BindableBase
    {
        public int Index { get; set; }
        public UtilType Type { get; set; }

        private ObservableCollection<UtilViewModel> _utils;
        public ObservableCollection<UtilViewModel> Utils
        {
            get { return _utils; }
            set
            {
                if (_utils != value)
                {
                    _utils = value;
                    RefreshIsEmpty();
                }
            }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { SetProperty(ref _isEmpty, value); }
        }

        public void RefreshIsEmpty()
        {
            IsEmpty = _utils == null || _utils.Count == 0;
        }

    }
}
