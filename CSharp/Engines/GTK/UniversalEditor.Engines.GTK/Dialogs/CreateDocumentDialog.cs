using System;
namespace UniversalEditor.Engines.GTK.Dialogs
{
	public partial class CreateDocumentDialog : Gtk.Dialog
	{
		public CreateDocumentDialog ()
		{
			this.Build ();
			Gtk.TreeStore ts = new Gtk.TreeStore(typeof(ObjectModelReference), typeof(string), typeof(string));
			ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels ();
			foreach (ObjectModelReference omr in omrs)
			{
				ts.AppendValues(omr, omr.Title, omr.Description);
			}
			tvDocumentTypes.Model = ts;
		}
	}
}

