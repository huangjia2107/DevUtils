﻿using System;

namespace DevUtils.Models
{
    [Serializable]
    public class SettingModel
    {  
        public bool AutoRemoveInvalidUtils { get; set; }
        public bool IsStartupWithSystem { get; set; }
        public bool IsSystemTrayAfterStartup { get; set; }
        public bool IsSystemTrayAfterClose { get; set; }
    }
}
