using System;
using System.Drawing;
using System.Runtime.InteropServices; 

namespace Utils.Native
{
    public static class Win32
    {
        public const int SHGFI_ICON = 0x100;
        public const int SHGFI_LARGEICON = 0x0;
        public const int SHGFI_SMALLICON = 0x1;
        public const int SHGFI_USEFILEATTRIBUTES = 0x10;
        public const int SHGFI_SYSICONINDEX = 0x4000;
        public const int SHGFI_LINKOVERLAY = 0x8000;
        public const int SHIL_JUMBO = 0x4;
        public const int SHIL_EXTRALARGE = 0x2;
        public const int FILE_ATTRIBUTE_NORMAL = 0x80;

        [Flags]
        public enum ILD_FLAGS : int
        {
            ILD_NORMAL = 0x0,
            ILD_TRANSPARENT = 0x1,
            ILD_BLEND25 = 0x2,
            ILD_FOCUS = 0x2,
            ILD_BLEND50 = 0x4,
            ILD_SELECTED = 0x4,
            ILD_BLEND = 0x4,
            ILD_MASK = 0x10,
            ILD_IMAGE = 0x20,
            ILD_ROP = 0x40,
            ILD_OVERLAYMASK = 0xF00,
            ILD_PRESERVEALPHA = 0x1000,
            ILD_SCALE = 0x2000,
            ILD_DPISCALE = 0x4000,
            ILD_ASYNC = 0x8000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHFILEINFOW
        {
            public System.IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGELISTDRAWPARAMS
        {
            public int cbSize;
            public IntPtr himl;
            public int i;
            public IntPtr hdcDst;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int xBitmap;
            public int yBitmap;
            public int rgbBk;
            public int rgbFg;
            public int fStyle;
            public int dwRop;
            public int fState;
            public int Frame;
            public int crEffect;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGEINFO
        {
            public IntPtr hbmImage;
            public IntPtr hbmMask;
            public int Unused1;
            public int Unused2;
            public RECT rcImage;
        }

        [ComImport()]
        [Guid("46EB5926-582E-4017-9FDF-E8998DAA0950")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IImageList
        {
            [PreserveSig()]
            int Add(IntPtr hbmImage, IntPtr hbmMask, ref int pi);
            [PreserveSig()]
            int ReplaceIcon(int i, IntPtr hicon, ref int pi);
            [PreserveSig()]
            int SetOverlayImage(int iImage, int iOverlay);
            [PreserveSig()]
            int Replace(int i, IntPtr hbmImage, IntPtr hbmMask);
            [PreserveSig()]
            int AddMasked(IntPtr hbmImage, int crMask, ref int pi);
            [PreserveSig()]
            int Draw(ref IMAGELISTDRAWPARAMS pimldp);
            [PreserveSig()]
            int Remove(int i);
            [PreserveSig()]
            int GetIcon(int i, int flags, ref IntPtr picon);
            [PreserveSig()]
            int GetImageInfo(int i, ref IMAGEINFO pImageInfo);
            [PreserveSig()]
            int Copy(int iDst, IImageList punkSrc, int iSrc, int uFlags);
            [PreserveSig()]
            int Merge(int i1, IImageList punk2, int i2, int dx, int dy, ref Guid riid, ref IntPtr ppv);
            [PreserveSig()]
            int Clone(ref Guid riid, ref IntPtr ppv);
            [PreserveSig()]
            int GetImageRect(int i, ref RECT prc);
            [PreserveSig()]
            int GetIconSize(ref int cx, ref int cy);
            [PreserveSig()]
            int SetIconSize(int cx, int cy);
            [PreserveSig()]
            int GetImageCount(ref int pi);
            [PreserveSig()]
            int SetImageCount(int uNewCount);
            [PreserveSig()]
            int SetBkColor(int clrBk, ref int pclr);
            [PreserveSig()]
            int GetBkColor(ref int pclr);
            [PreserveSig()]
            int BeginDrag(int iTrack, int dxHotspot, int dyHotspot);
            [PreserveSig()]
            int EndDrag();
            [PreserveSig()]
            int DragEnter(IntPtr hwndLock, int x, int y);
            [PreserveSig()]
            int DragLeave(IntPtr hwndLock);
            [PreserveSig()]
            int DragMove(int x, int y);
            [PreserveSig()]
            int SetDragCursorImage(ref IImageList punk, int iDrag, int dxHotspot, int dyHotspot);
            [PreserveSig()]
            int DragShowNolock(int fShow);
            [PreserveSig()]
            int GetDragImage(ref Point ppt, ref Point pptHotspot, ref Guid riid, ref IntPtr ppv);
            [PreserveSig()]
            int GetItemFlags(int i, ref int dwFlags);
            [PreserveSig()]
            int GetOverlayImage(int iOverlay, ref int piIndex);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;
        }

        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT point);

        /// SHGetImageList is not exported correctly in XP.  See KB316931
        ///     ''' http://support.microsoft.com/default.aspx?scid=kb;EN-US;Q316931
        ///     ''' Apparently (and hopefully) ordinal 727 isn't going to change.
        ///     '''
        [DllImport("shell32.dll", EntryPoint = "#727")]
        public static extern int SHGetImageList(int iImageList, ref Guid riid, ref IImageList ppv);

        [DllImport("shell32.dll", EntryPoint = "SHGetFileInfoW", CallingConvention = CallingConvention.StdCall)]
        public static extern int SHGetFileInfoW([MarshalAs(UnmanagedType.LPWStr)] string pszPath, uint dwFileAttributes, ref SHFILEINFOW psfi, int cbFileInfo, uint uFlags);

        [DllImport("shell32.dll", EntryPoint = "SHGetFileInfoW", CallingConvention = CallingConvention.StdCall)]
        public static extern int SHGetFileInfoW(IntPtr pszPath, uint dwFileAttributes, ref SHFILEINFOW psfi, int cbFileInfo, uint uFlags);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        public static extern bool DestroyIcon(IntPtr hIcon);
    }
}
