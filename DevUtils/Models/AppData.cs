using System;

namespace DevUtils.Models
{
    [Serializable]
    public class AppData
    {
        public SettingModel SettingsData { get; set; }
        public UtilData UtilsData { get; set; }
    }
}
