using WeifenLuo.WinFormsUI.Docking;

namespace WeifenLuo.WinFormsUI.Theming
{
    internal class VS2012DockWindowFactory : DockPanelExtender.IDockWindowFactory
    {
        public DockWindow CreateDockWindow(DockPanel dockPanel, DockState dockState)
        {
            return new VS2012DockWindow(dockPanel, dockState);
        }
    }
}
