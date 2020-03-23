using System.Drawing;

namespace WeifenLuo.WinFormsUI.Docking
{
    using Theming;
    
    /// <summary>
    /// Visual Studio 2012 Light theme.
    /// </summary>
    public class VS2012BlueTheme : VS2012ThemeBase
    {
        public VS2012BlueTheme()
            : base(Theming.Resources.vs2012blue_vstheme, new VS2012DockPaneSplitterControlFactory(), new VS2012WindowSplitterControlFactory())
        {
            ColorPalette.TabSelectedInactive.Background = ColorTranslator.FromHtml("#FF3D5277");// TODO: from theme .FromHtml("#FF4D6082");
        }
    }
}