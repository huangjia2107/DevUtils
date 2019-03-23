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
            AllUtils = new List<IUtilModel>();
            MineUtils = new List<IUtilModel>();
        }

        public List<IUtilModel> AllUtils { get; set; }
        public List<IUtilModel> MineUtils { get; set; }
    }
}
