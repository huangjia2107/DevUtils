using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;

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
                new UtilModel{Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Name="查找大文件",Discription = "一键查找并清理重复文件"},

                new UtilModel{Type=UtilType.File, Name="颜色拾取",Discription = "颜色选择器，一目了然"},
                new UtilModel{Type=UtilType.File,Name="系统备份还原",Discription = "一键备份还原系统，方便安全"},
                new UtilModel{Type=UtilType.File,Name="急救盘",Discription = "一盘在手，系统无忧"},
                new UtilModel{Type=UtilType.File,Name="任务管理器",Discription = "找出当前占用资源的程序"},
                new UtilModel{Type=UtilType.File,Name="鲁大师",Discription = "辨别硬件真伪，实时监控温度"},
                new UtilModel{Type=UtilType.File,Name="默认软件",Discription = "帮您设置常用的默认软件"},
                new UtilModel{Type=UtilType.File,Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Type=UtilType.File,Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Type=UtilType.File,Name="查找大文件",Discription = "一键查找并清理重复文件"},

                new UtilModel{Type=UtilType.Test, Name="颜色拾取",Discription = "颜色选择器，一目了然"},
                new UtilModel{Type=UtilType.Test,Name="系统备份还原",Discription = "一键备份还原系统，方便安全"},
                new UtilModel{Type=UtilType.Test,Name="急救盘",Discription = "一盘在手，系统无忧"},
                new UtilModel{Type=UtilType.Test,Name="任务管理器",Discription = "找出当前占用资源的程序"},
                new UtilModel{Type=UtilType.Test,Name="鲁大师",Discription = "辨别硬件真伪，实时监控温度"},
                new UtilModel{Type=UtilType.Test,Name="默认软件",Discription = "帮您设置常用的默认软件"},
                new UtilModel{Type=UtilType.Test,Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Type=UtilType.Test,Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Type=UtilType.Test,Name="查找大文件",Discription = "一键查找并清理重复文件"},

                new UtilModel{Type=UtilType.Misc, Name="颜色拾取",Discription = "颜色选择器，一目了然"},
                new UtilModel{Type=UtilType.Misc,Name="系统备份还原",Discription = "一键备份还原系统，方便安全"},
                new UtilModel{Type=UtilType.Misc,Name="急救盘",Discription = "一盘在手，系统无忧"},
                new UtilModel{Type=UtilType.Misc,Name="任务管理器",Discription = "找出当前占用资源的程序"},
                new UtilModel{Type=UtilType.Misc,Name="鲁大师",Discription = "辨别硬件真伪，实时监控温度"},
                new UtilModel{Type=UtilType.Misc,Name="默认软件",Discription = "帮您设置常用的默认软件"},
                new UtilModel{Type=UtilType.Misc,Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Type=UtilType.Misc,Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Type=UtilType.Misc,Name="查找大文件",Discription = "一键查找并清理重复文件"}, 

            };
            MineUtils = new ObservableCollection<UtilModel>
            {
                new UtilModel{Name="颜色拾取",Discription = "颜色选择器，一目了然"},
                new UtilModel{Name="系统备份还原",Discription = "一键备份还原系统，方便安全"},
                new UtilModel{Name="急救盘",Discription = "一盘在手，系统无忧"},
                new UtilModel{Name="任务管理器",Discription = "找出当前占用资源的程序"},
                new UtilModel{Name="鲁大师",Discription = "辨别硬件真伪，实时监控温度"},
                new UtilModel{Name="默认软件",Discription = "帮您设置常用的默认软件"},
                new UtilModel{Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Name="查找大文件",Discription = "一键查找并清理重复文件"},
                new UtilModel{Name="查找大文件",Discription = "一键查找并清理重复文件"},
            };
        }

        [NonSerialized]
        private IEnumerable<ClassifiedUtil> _classifiedUtils = null;
        public IEnumerable<ClassifiedUtil> ClassifiedUtils
        {
            get
            {
                if (_classifiedUtils == null)
                    _classifiedUtils =
                        AllUtils.GroupBy(u => u.Type).Select(g => new ClassifiedUtil { Type = g.Key, Utils = g.ToList() });

                return _classifiedUtils;
            }
        }

        public IEnumerable<UtilModel> AllUtils { get; set; }
        public ObservableCollection<UtilModel> MineUtils { get; set; }
    }
}
