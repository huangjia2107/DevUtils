using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UtilModelService;

namespace ColorPix.Services
{
    public class ColorPixUtilModel : UtilModel
    {
        #region IUtilModel Members 

        public override string Name => "Color Pix";
         
        public override string Description => "this is a description about Color Pix";
         
        public override UtilType Type => UtilType.Tester;

        public override string Location { get; set; } 
         
        public override void Run()
        {
            MessageBox.Show("Run ColorPix");
        }

        #endregion
    }
}
