using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utils.Native;

namespace Utils.Media
{
    public enum IconSize
    {
        Small,
        Large,
        ExtraLarge,
        Jumbo
    }

    public static class ImageHelper
    {
        public static Icon GetIcon(string fileName)
        {
            return Icon.ExtractAssociatedIcon(fileName);
        }

        public static Icon GetIcon(string fileName, IconSize thisSize)
        {
            var shinfo = new Win32.SHFILEINFOW();
            Icon myIcon;
            int flags, res;

            switch (thisSize)
            {
                case IconSize.Small:
                case IconSize.Large:
                    {
                        if (thisSize == IconSize.Small)
                            flags = Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON;
                        else
                            flags = Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON;

                        flags = flags | Win32.SHGFI_USEFILEATTRIBUTES;

                        res = Win32.SHGetFileInfoW(fileName, 0, ref shinfo, Marshal.SizeOf(shinfo), (uint)flags);
                        if (res == 0)
                            throw (new System.IO.FileNotFoundException());

                        myIcon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
                        Win32.DestroyIcon(shinfo.hIcon);
                        break;
                    }

                case IconSize.ExtraLarge:
                case IconSize.Jumbo:
                    {
                        flags = Win32.SHGFI_SYSICONINDEX;
                        flags = flags | Win32.SHGFI_USEFILEATTRIBUTES;

                        res = Win32.SHGetFileInfoW(fileName, Win32.FILE_ATTRIBUTE_NORMAL, ref shinfo, Marshal.SizeOf(shinfo), (uint)flags);
                        if (res == 0)
                            throw new System.IO.FileNotFoundException();

                        var iconIndex = shinfo.iIcon;
                        var iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
                        Win32.IImageList iml = null;

                        int size;
                        if (thisSize == IconSize.ExtraLarge)
                            size = System.Convert.ToInt32(Win32.SHIL_EXTRALARGE);
                        else
                            size = System.Convert.ToInt32(Win32.SHIL_JUMBO);

                        Win32.SHGetImageList(size, ref iidImageList, ref iml);

                        var hIcon = IntPtr.Zero;
                        iml.GetIcon(iconIndex, (int)Win32.ILD_FLAGS.ILD_IMAGE, ref hIcon);

                        myIcon = (Icon)Icon.FromHandle(hIcon).Clone();

                        Win32.DestroyIcon(hIcon);
                        var bmp = myIcon.ToBitmap();
                        if (bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                        {
                            myIcon.Dispose();
                            myIcon = null/* TODO Change to default(_) if this is not a reference type */;
                            iml.GetIcon(iconIndex, (int)Win32.ILD_FLAGS.ILD_TRANSPARENT, ref hIcon);
                            myIcon = (Icon)Icon.FromHandle(hIcon).Clone();
                            Win32.DestroyIcon(hIcon);
                        }

                        break;
                    }

                default:
                    {
                        return null/* TODO Change to default(_) if this is not a reference type */;
                    }
            }

            return myIcon;
        }
    }
}
