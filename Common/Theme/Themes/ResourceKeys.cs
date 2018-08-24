using System.Windows;

namespace Theme.Themes
{
    public static class ResourceKeys
    {
        #region ButtonBase
        /// <summary>
        /// Style="{DynamicResource/StaticResource {x:Static themes:ResourceKeys.NoBgButtonStyleKey}}"
        /// </summary>
        public const string NoBgButtonBaseStyle = "NoBgButtonBaseStyle";
        public static ComponentResourceKey NoBgButtonBaseStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), NoBgButtonBaseStyle); }
        }

        #endregion

        #region Button

        public const string NormalButtonStyle = "NormalButtonStyle";
        public static ComponentResourceKey NormalButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), NormalButtonStyle); }
        }

        public const string MiniWindowTitlebarButtonStyle = "MiniWindowTitlebarButtonStyle";
        public static ComponentResourceKey MiniWindowTitlebarButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), MiniWindowTitlebarButtonStyle); }
        }

        public const string MiniWindowTitlebarCloseButtonStyle = "MiniWindowTitlebarCloseButtonStyle";
        public static ComponentResourceKey MiniWindowTitlebarCloseButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), MiniWindowTitlebarCloseButtonStyle); }
        }

        public const string MainWindowTitlebarBtnStyle = "MainWindowTitlebarBtnStyle";
        public static ComponentResourceKey MainWindowTitlebarBtnStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), MainWindowTitlebarBtnStyle); }
        }

        public const string MainWindowTitlebarCloseBtnStyle = "MainWindowTitlebarCloseBtnStyle";
        public static ComponentResourceKey MainWindowTitlebarCloseBtnStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), MainWindowTitlebarCloseBtnStyle); }
        }

        public const string MiniWindowShortcutBtnStyle = "MiniWindowShortcutBtnStyle";
        public static ComponentResourceKey MiniWindowShortcutBtnStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), MiniWindowShortcutBtnStyle); }
        }


        #endregion

        #region RadioButton

        public const string UtilRadioButtonStyle = "UtilRadioButtonStyle";
        public static ComponentResourceKey UtilRadioButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), UtilRadioButtonStyle); }
        }

        #endregion

        #region StatusToggle

        public const string StatusToggleStyle = "StatusToggleStyle";
        public static ComponentResourceKey StatusToggleStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), StatusToggleStyle); }
        }

        #endregion

        #region RepeatButton

        public const string NormalRepeatButtonStyle = "NormalRepeatButtonStyle";
        public static ComponentResourceKey NormalRepeatButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), NormalRepeatButtonStyle); }
        }

        #endregion

        #region UserWindow

        public const string NormalUserWindowStyle = "NormalUserWindowStyle";
        public static ComponentResourceKey NormalUserWindowStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), NormalUserWindowStyle); }
        }

        public const string MiniUserWindowStyle = "MiniUserWindowStyle";
        public static ComponentResourceKey MiniUserWindowStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), MiniUserWindowStyle); }
        }

        #endregion

        #region ListBox

        public const string NormalListBoxItemStyle = "NormalListBoxItemStyle";
        public static ComponentResourceKey NormalListBoxItemStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), NormalListBoxItemStyle); }
        }

        public const string HorizontalListBoxItemStyle = "HorizontalListBoxItemStyle";
        public static ComponentResourceKey HorizontalListBoxItemStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), HorizontalListBoxItemStyle); }
        }

        public const string UtilListBoxItemStyle = "UtilListBoxItemStyle";
        public static ComponentResourceKey UtilListBoxItemStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), UtilListBoxItemStyle); }
        }

        public const string UtilDispalyListBoxItemStyle = "UtilDispalyListBoxItemStyle";
        public static ComponentResourceKey UtilDispalyListBoxItemStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), UtilDispalyListBoxItemStyle); }
        }

        #endregion
    }
}
