using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Media;
using Utils.Media;

namespace DevUtils.Helps
{
    public static class Utils
    {
        public static ImageSource GetIcon(string fileName)
        {
            var icon = ImageHelper.GetIcon(fileName, IconSize.ExtraLarge);
            if (icon == null)
                return null;

            return Imaging.CreateBitmapSourceFromHIcon(
                        icon.Handle,
                        new Int32Rect(0, 0, icon.Width, icon.Height),
                        BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
