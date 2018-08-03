using System;

namespace DevUtils.Models
{
    [Serializable]
    public class SettingModel
    {
        public SettingModel()
        {
            ShortcutCount = 5;
        }

        public int ShortcutCount { get; set; }
        public bool IsStartupWithSystem { get; set; }
        public bool IsSystemTrayAfterStartup { get; set; }
        public bool IsSystemTrayAfterClose { get; set; }
    }
}
