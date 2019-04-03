using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UtilModelService;

namespace ColorPix.Services
{
    public class ColorPixUtilModel : IUtilModel
    {
        #region IUtilModel Members 

        public string Token => "361d828e-8831-45e0-ac7a-78ba929c4af5";

        public string Name => "Color Pix";
         
        public string Description => "this is a description about Color Pix";
         
        public UtilType Type => UtilType.Tester;

        public string Location { get; set; } 
         
        public void Run()
        {
            MessageBox.Show("Run ColorPix");
        }

        #endregion
    }
}
