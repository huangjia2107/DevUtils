using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevUtils.Models;
using Prism.Mvvm;

namespace DevUtils.ViewModels
{
    public class UtilControlViewModel : BindableBase
    {
        private UtilData _utilData = null;

        public UtilControlViewModel(UtilData utilData)
        {
            _utilData = utilData;
        }

        public ObservableCollection<UtilModel> AllUtils
        {
            get { return _utilData.AllUtils; }
        }

        public ObservableCollection<UtilModel> MineUtils
        {
            get { return _utilData.MineUtils; }
        }

        private Action<int, int> _moveMineUtilPosAction = null;
        public Action<int, int> MoveMineUtilPosAction
        {
            get
            {
                if(_moveMineUtilPosAction == null)
                    _moveMineUtilPosAction = (si, ti) => AllUtils.Move(si, ti); 

                return _moveMineUtilPosAction;
            }
        }
    }
}
