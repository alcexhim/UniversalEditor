using WeifenLuo.WinFormsUI.Docking;

namespace WeifenLuo.WinFormsUI.Theming
{
    internal class VS2012WindowSplitterControlFactory : DockPanelExtender.IWindowSplitterControlFactory
    {
        public SplitterBase CreateSplitterControl(ISplitterHost host)
        {
            return new VS2012WindowSplitterControl(host);
        }
    }
}
