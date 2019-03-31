using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UtilModelService;

namespace RegexTester.Services
{ 
    public class RegexTesterUtilModel : UtilModel
    {
        #region IUtilModel Members 

        public override string Name => "Regex Tester"; 

        public override string Description => "this is a description about Regex Tester"; 

        public override UtilType Type => UtilType.Tester;

        public override string Location { get; set; } 
         
        public override void Run()
        {
            MessageBox.Show("Run RegexTester");
        }

        #endregion
    }
}
