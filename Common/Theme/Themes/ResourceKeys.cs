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

        public const string FgWhiteCloseBtnStyle = "FgWhiteCloseBtnStyle";
        public static ComponentResourceKey FgWhiteCloseBtnStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), FgWhiteCloseBtnStyle); }
        }

        public const string FgRedCloseBtnStyle = "FgRedCloseBtnStyle";
        public static ComponentResourceKey FgRedCloseBtnStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), FgRedCloseBtnStyle); }
        }

        #endregion

        #region RepeatButton

        public const string NormalRepeatButtonStyle = "NormalRepeatButtonStyle";
        public static ComponentResourceKey NormalRepeatButtonStyleKey
        {
            get { return new ComponentResourceKey(typeof(ResourceKeys), NormalRepeatButtonStyle); }
        }

        #endregion
    }
}
