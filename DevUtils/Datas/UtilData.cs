using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevUtils.Models;
using DevUtils.ViewModels;
using UtilModelService;

namespace DevUtils.Datas
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
                {
                    _classifiedUtils = new ObservableCollection<ClassifiedUtil>(
                          AllUtils.GroupBy(u => u.Type)
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

        public ObservableCollection<UtilViewModel> AllUtils { get; set; }
        public ObservableCollection<UtilViewModel> MineUtils { get; set; }
    }
}
