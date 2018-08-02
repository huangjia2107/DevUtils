using System;

namespace DevUtils.Models
{
    [Serializable]
    public class SettingModel
    {
        public SettingModel()
        {
            ShortcutCountPerRow = 5;
        }

        public int ShortcutCountPerRow { get; set; }


    }
}
