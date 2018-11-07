using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilModelService;

namespace RegexTester.Services
{
    public class RegexTesterUtilModel : IUtilModelService
    {
        #region IUtilModel Members

        public string GetName()
        {
            return "Regex Tester";
        }

        #endregion
    }
}
