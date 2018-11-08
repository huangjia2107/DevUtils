using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;
using UtilModelService;

namespace DevUtils.Models
{ 
    public class UtilDisplay
    {
        public UtilDisplay()
        {
            Width = 18;
            Height = 18;
        }

        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Data { get; set; }
    } 

    public class ClassifiedUtil
    {
        public UtilType Type { get; set; }
        public List<IUtilModel> Utils { get; set; }
    }
}
