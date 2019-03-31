using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevUtils.Models;
using DevUtils.ViewModels;
using UtilModelService;
using System.Collections.Generic;

namespace DevUtils.Datas
{
    [Serializable]
    public class UtilData
    {
        public UtilData()
        {
            AllUtils = new List<UtilModel>();
            MineUtils = new List<UtilModel>();
        }

        public List<UtilModel> AllUtils { get; set; }
        public List<UtilModel> MineUtils { get; set; }
    }
}
