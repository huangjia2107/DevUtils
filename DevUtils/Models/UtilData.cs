using System;
using System.Collections.ObjectModel; 

namespace DevUtils.Models
{
    [Serializable]
    public class UtilData
    {
        public ObservableCollection<UtilModel> AllUtils { get; set; }
        public ObservableCollection<UtilModel> MineUtils { get; set; }
    }
}
