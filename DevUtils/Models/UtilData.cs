using System;
using System.Collections.ObjectModel;

namespace DevUtils.Models
{
    [Serializable]
    public class UtilData
    {
        public UtilData()
        {
            AllUtils = new ObservableCollection<UtilModel>
            {
                new UtilModel{Name="颜色拾取",Discription = "颜色选择器，一目了然"},
                new UtilModel{Name="系统备份还原",Discription = "一键备份还原系统，方便安全"},
                new UtilModel{Name="急救盘",Discription = "一盘在手，系统无忧"},
                new UtilModel{Name="任务管理器",Discription = "找出当前占用资源的程序"},
                new UtilModel{Name="鲁大师",Discription = "辨别硬件真伪，实时监控温度"},
                new UtilModel{Name="默认软件",Discription = "帮您设置常用的默认软件"},
                new UtilModel{Name="查找大文件",Discription = "一键查找并清理重复文件"},
            };
            MineUtils = new ObservableCollection<UtilModel>();
        }
        public ObservableCollection<UtilModel> AllUtils { get; set; }
        public ObservableCollection<UtilModel> MineUtils { get; set; }
    }
}
