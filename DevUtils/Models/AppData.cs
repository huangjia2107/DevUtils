using System;

namespace DevUtils.Models
{
    [Serializable]
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
