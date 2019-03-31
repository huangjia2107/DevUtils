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

    public class AddToMineUtilsEvent : PubSubEvent<UtilModel>
    {
    }

    public class DeleteFromMineUtilsEvent : PubSubEvent<UtilModel>
    {

    }

    public class MoveMineUtilsEvent: PubSubEvent<MoveUtilParam>
    {
    }
}
