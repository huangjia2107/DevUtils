using System.Collections.ObjectModel; 
using DevUtils.ViewModels;
using UtilModelService;

namespace DevUtils.Models
{
    public class ClassifiedUtil
    {
        public int Index { get; set; }
        public UtilType Type { get; set; }
        public ObservableCollection<UtilViewModel> Utils { get; set; }
    }  
}
