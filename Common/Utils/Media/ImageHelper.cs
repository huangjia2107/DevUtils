using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Media
{
    public static class ImageHelper
    {
        public static Icon GetIcon(string fileName)
        {
            return Icon.ExtractAssociatedIcon(fileName);
        }
    }
}
