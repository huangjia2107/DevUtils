using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevUtils.ViewModels;

namespace DevUtils.Models
{
    [Serializable]
    public class UtilData
    {
        public UtilData()
        {
            AllUtils = new ObservableCollection<UtilViewModel>();
            MineUtils = new ObservableCollection<UtilViewModel>();
        }

        [NonSerialized]
        private ObservableCollection<ClassifiedUtil> _classifiedUtils = null;
        public ObservableCollection<ClassifiedUtil> ClassifiedUtils
        {
            get
            {
                if (_classifiedUtils == null)
                    _classifiedUtils = new ObservableCollection<ClassifiedUtil>(
                        AllUtils.GroupBy(u => u.Type)
                                .OrderBy(g => g.Key)
                                .Select((g, i) => new ClassifiedUtil { Index = i, Type = g.Key, Utils = new ObservableCollection<UtilViewModel>(g) }));

                return _classifiedUtils;
            }
        }

        public ObservableCollection<UtilViewModel> AllUtils { get; set; }
        public ObservableCollection<UtilViewModel> MineUtils { get; set; }
    }
}
