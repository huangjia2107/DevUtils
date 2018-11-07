using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilModelService;

namespace ColorPix.Services
{
    public class ColorPixUtilModel : IUtilModelService
    {
        #region IUtilModel Members

        public string GetName()
        {
            return "Color Pix";
        }

        #endregion
    }
}
