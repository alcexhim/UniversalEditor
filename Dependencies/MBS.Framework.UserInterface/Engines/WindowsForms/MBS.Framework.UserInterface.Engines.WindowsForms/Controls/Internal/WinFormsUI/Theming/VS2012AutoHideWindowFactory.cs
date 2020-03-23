using WeifenLuo.WinFormsUI.Docking;

namespace WeifenLuo.WinFormsUI.Theming
{
    internal class VS2012AutoHideWindowFactory : DockPanelExtender.IAutoHideWindowFactory
    {
        public DockPanel.AutoHideWindowControl CreateAutoHideWindow(DockPanel panel)
        {
            return new VS2012AutoHideWindowControl(panel);
        }
    }
}
