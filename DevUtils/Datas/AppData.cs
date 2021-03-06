﻿using System;
using DevUtils.Models;

namespace DevUtils.Datas
{ 
    public class AppData
    {
        public AppData()
        {
            SettingsData = new SettingModel();
            UtilsData = new UtilData();
        }

        public SettingModel SettingsData { get; set; }
        public UtilData UtilsData { get; set; }
    }
}
