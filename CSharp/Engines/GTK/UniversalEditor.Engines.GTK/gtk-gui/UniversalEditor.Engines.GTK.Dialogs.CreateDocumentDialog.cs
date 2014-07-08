
namespace UniversalEditor.Engines.GTK.Dialogs
{
	public partial class CreateDocumentDialog
	{
		private Gtk.Button buttonCancel;
		private Gtk.Button buttonOk;
		private Gtk.TreeView tvDocumentTypes;
		private Gtk.IconView lvDocumentTemplates;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget UniversalEditor.Engines.GTK.Dialogs.CreateDocumentDialog
			this.UIManager = new global::Gtk.UIManager ();
			this.buttonCancel = ((global::Gtk.Button)(this.UIManager.GetWidget ("/buttonCancel")));
			this.buttonOk = ((global::Gtk.Button)(this.UIManager.GetWidget ("/buttonOk")));
			this.tvDocumentTypes = ((global::Gtk.TreeView)(this.UIManager.GetWidget ("/tvDocumentTypes")));
			this.lvDocumentTemplates = ((global::Gtk.IconView)(this.UIManager.GetWidget ("/lvDocumentTemplates")));
		}
	}
}
