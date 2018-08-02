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

        public const string TitlebarButtonStyle = "TitlebarButtonStyle";
        public static ComponentResourceKey TitlebarButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), TitlebarButtonStyle); }
        }

        public const string TitlebarCloseButtonStyle = "TitlebarCloseButtonStyle";
        public static ComponentResourceKey TitlebarCloseButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), TitlebarCloseButtonStyle); }
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
    }
}
