using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;
using UtilModelService;

namespace DevUtils.Models
{
    public enum UtilType
    {
        Color,
        File,
        Debug,
        Test,
        Misc
    }

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

    [Serializable]
    public class UtilModel
    {
        public UtilType Type { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string IconPath { get; set; }
    }

    public class ClassifiedUtil
    {
        public UtilType Type { get; set; }
        public List<UtilModel> Utils { get; set; }
    }
}
