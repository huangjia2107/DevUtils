using System;
using System.Collections.Generic;
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
    }
}
