using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UtilModelService;

namespace RegexTester.Services
{ 
    public class RegexTesterUtilModel : IUtilModel
    {
        #region IUtilModel Members 

        public string Token => "5586f330-55e7-4e63-b8c2-cb95919ea192";

        public string Name => "Regex Tester"; 

        public string Description => "this is a description about Regex Tester"; 

        public UtilType Type => UtilType.Tester;

        public string Location { get; set; } 
         
        public void Run()
        {
            MessageBox.Show("Run RegexTester");
        }

        #endregion
    }
}
