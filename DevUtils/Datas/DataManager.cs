using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using DevUtils.Models;

namespace DevUtils.Datas
{
    public class DataManager
    {
        private static DataManager _dataManager = new DataManager();
        public AppData CurAppData { get; set; }

        private DataManager()
        {
            CurAppData = new AppData();
        }

        public static DataManager Instance()
        {
            return _dataManager;
        }

        public void SaveData()
        { }

        public void LoadData()
        { }

    }
}
