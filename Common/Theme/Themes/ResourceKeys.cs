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

        public const string NormalListBoxStyle = "NormalListBoxStyle";
        public static ComponentResourceKey NormalListBoxStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), NormalListBoxStyle); }
        }

        public const string HorizontalListBoxStyle = "HorizontalListBoxStyle";
        public static ComponentResourceKey HorizontalListBoxStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), HorizontalListBoxStyle); }
        }

        #endregion
    }
}
