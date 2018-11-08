using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
using UtilModelService;

namespace DevUtils.Models
{
    [Serializable]
    public class UtilData
    {
        public UtilData()
        {
            AllUtils = new ObservableCollection<IUtilModel>();
            MineUtils = new ObservableCollection<IUtilModel>(); 
        }

        [NonSerialized]
        private IEnumerable<ClassifiedUtil> _classifiedUtils = null;
        public IEnumerable<ClassifiedUtil> ClassifiedUtils
        {
            get
            {
                if (_classifiedUtils == null)
                    _classifiedUtils =
                        AllUtils.GroupBy(u => u.Type).OrderBy(g => g.Key).Select(g => new ClassifiedUtil { Type = g.Key, Utils = g.ToList() });

                return _classifiedUtils;
            }
        }

        public ObservableCollection<IUtilModel> AllUtils { get; set; }
        public ObservableCollection<IUtilModel> MineUtils { get; set; }
    }
}
