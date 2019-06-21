using DevUtils.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilModelService;

namespace DevUtils.Events
{
    public struct MoveUtilParam
    {
        public int Token { get; set; }
        public int SourceIndex { get; set; }
        public int TargetIndex { get; set; }
    }

    public class AddToMineUtilsEvent : PubSubEvent<IUtilModel>
    {
    }

    public class DeleteFromMineUtilsEvent : PubSubEvent<IUtilModel>
    {

    }

    public class MoveMineUtilsEvent : PubSubEvent<MoveUtilParam>
    {
    }

    public class RefreshMineUtilsEvent : PubSubEvent
    {

    }
}
