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

        public string Name
        {
            get { return "Regex Tester"; }
        }

        public string Discription
        {
            get { return "this is a discription about Regex Tester"; }
        }

        public UtilType Type
        {
            get { return UtilType.Test; }
        }

        public void Run()
        {
            MessageBox.Show("Run RegexTester");
        }

        #endregion
    }
}
