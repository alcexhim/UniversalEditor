using System;

using UniversalEditor.Accessors;
using UniversalEditor.UserInterface;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Dialogs;

namespace UniversalEditor.Engines.UWT
{
	public class MainWindow : Window, IHostApplicationWindow
	{
		private TabContainer tbsDocumentTabs = null;

		public MainWindow ()
		{
			UniversalWidgetToolkit.Layouts.BoxLayout layout = new UniversalWidgetToolkit.Layouts.BoxLayout (Orientation.Vertical);
			this.Layout = layout;

			foreach (CommandItem ci in UniversalEditor.UserInterface.Engine.CurrentEngine.MainMenu.Items) {
				UniversalWidgetToolkit.MenuItem mi = LoadMenuItem (ci);
				if (mi == null)
					continue;

				if (mi.Name == "Help") {
					mi.HorizontalAlignment = MenuItemHorizontalAlignment.Right;
				}
				this.MenuBar.Items.Add (mi);
			}

			tbsDocumentTabs = new TabContainer ();
			tbsDocumentTabs.TabPages.Add (new TabPage ("Test Tab"));
			this.Controls.Add (tbsDocumentTabs, new UniversalWidgetToolkit.Layouts.BoxLayout.Constraints (true, true, 0, UniversalWidgetToolkit.Layouts.BoxLayout.PackType.End));

			this.Bounds = new UniversalWidgetToolkit.Drawing.Rectangle (0, 0, 600, 400);

			this.Text = "Universal Editor";
		}

		private void MainWindow_MenuBar_Item_Click(object sender, EventArgs e)
		{
			CommandMenuItem mi = (sender as CommandMenuItem);
			if (mi == null)
				return;

			Command cmd = UniversalEditor.UserInterface.Engine.CurrentEngine.Commands [mi.Name];
			if (cmd == null) {
				Console.WriteLine ("unknown cmd '" + mi.Name + "'");
				return;
			}

			cmd.Execute ();
		}

		public override void OnActivate (EventArgs e)
		{
			Console.WriteLine ("Window activated");
		}

		public override void OnClosed(EventArgs e)
		{
			UniversalWidgetToolkit.Application.Stop ();
		}

		private UniversalWidgetToolkit.MenuItem LoadMenuItem(CommandItem ci)
		{
			if (ci is CommandReferenceCommandItem) {
				CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);

				Command cmd = UniversalEditor.UserInterface.Engine.CurrentEngine.Commands [crci.CommandID];
				if (cmd != null) {
					CommandMenuItem mi = new CommandMenuItem (cmd.Title);
					mi.Name = cmd.ID;
					if (cmd.Items.Count > 0) {
						foreach (CommandItem ci1 in cmd.Items) {
							UniversalWidgetToolkit.MenuItem mi1 = LoadMenuItem (ci1);
							mi.Items.Add (mi1);
						}
					}
					else {
						mi.Click += MainWindow_MenuBar_Item_Click;
					}
					return mi;
				} else {
					Console.WriteLine ("attempted to load unknown cmd '" + crci.CommandID + "'");
				}
				return null;
			} else if (ci is SeparatorCommandItem) {
				return new UniversalWidgetToolkit.SeparatorMenuItem ();
			}
			return null;
		}

		#region IHostApplicationWindow implementation

		public void NewFile ()
		{
			throw new NotImplementedException ();
		}

		public void NewProject (bool combineObjects = false)
		{
			throw new NotImplementedException ();
		}

		public void OpenFile ()
		{
			FileDialog dlg = new FileDialog ();
			dlg.Mode = FileDialogMode.Open;
			dlg.MultiSelect = true;
			if (dlg.ShowDialog () == CommonDialogResult.OK) {
				OpenFile (dlg.SelectedFileNames.ToArray ());
			}
		}

		public void OpenFile (params string[] fileNames)
		{
			Document[] documents = new Document[fileNames.Length];
			for (int i = 0; i < documents.Length; i++) {
				FileAccessor fa = new FileAccessor (fileNames [i]);
				documents [i] = new Document (fa);
			}
			OpenFile (documents);
		}

		public void OpenFile (params Document[] documents)
		{
			foreach (Document doc in documents) {
				TabPage tab = new TabPage ();
				tab.Text = doc.Title;
				tbsDocumentTabs.TabPages.Add (tab);
			}
		}

		public void OpenProject (bool combineObjects = false)
		{
			throw new NotImplementedException ();
		}

		public void OpenProject (string FileName, bool combineObjects = false)
		{
			throw new NotImplementedException ();
		}

		public void SaveFile ()
		{
			throw new NotImplementedException ();
		}

		public void SaveFileAs ()
		{
			throw new NotImplementedException ();
		}

		public void SaveFileAs (string FileName, DataFormat df)
		{
			throw new NotImplementedException ();
		}

		public void SaveProject ()
		{
			throw new NotImplementedException ();
		}

		public void SaveProjectAs ()
		{
			throw new NotImplementedException ();
		}

		public void SaveProjectAs (string FileName, DataFormat df)
		{
			throw new NotImplementedException ();
		}

		public void SaveAll ()
		{
			throw new NotImplementedException ();
		}

		public void SwitchPerspective (int index)
		{
			throw new NotImplementedException ();
		}

		public void CloseFile ()
		{
			throw new NotImplementedException ();
		}

		public void CloseProject ()
		{
			throw new NotImplementedException ();
		}

		public void CloseWindow ()
		{
			throw new NotImplementedException ();
		}

		public IEditorImplementation GetCurrentEditor ()
		{
			throw new NotImplementedException ();
		}

		public bool ShowOptionsDialog ()
		{
			throw new NotImplementedException ();
		}

		public void ToggleMenuItemEnabled (string menuItemName, bool enabled)
		{
			throw new NotImplementedException ();
		}

		public void RefreshCommand (object nativeCommandObject)
		{
			throw new NotImplementedException ();
		}

		public void UpdateStatus (string statusText)
		{
			throw new NotImplementedException ();
		}

		public void UpdateProgress (bool visible)
		{
			throw new NotImplementedException ();
		}

		public void UpdateProgress (int minimum, int maximium, int value)
		{
			throw new NotImplementedException ();
		}

		public void ActivateWindow ()
		{
			throw new NotImplementedException ();
		}

		public void ShowStartPage ()
		{
			throw new NotImplementedException ();
		}

		public void SetWindowListVisible (bool visible, bool modal)
		{
			throw new NotImplementedException ();
		}

		public event EventHandler WindowClosed;

		public bool FullScreen { get; set; }

		#endregion
	}
}

