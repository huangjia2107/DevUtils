using System;
using System.Collections.ObjectModel;

namespace DevUtils.Models
{
    [Serializable]
    public class UtilData
    {
        public UtilData()
        {
            AllUtils = new ObservableCollection<UtilModel>();
            MineUtils = new ObservableCollection<UtilModel>();
        }
        public ObservableCollection<UtilModel> AllUtils { get; set; }
        public ObservableCollection<UtilModel> MineUtils { get; set; }
    }
}
