using System;

using UniversalEditor.Accessors;
using UniversalEditor.UserInterface;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Controls.Docking;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.Input.Keyboard;

// TODO: We need to work on UWT signaling to native objects...

namespace UniversalEditor.Engines.UWT
{
	public class MainWindow : Window, IHostApplicationWindow
	{
		private DockingContainer tbsDocumentTabs = null;

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

			tbsDocumentTabs = new DockingContainer ();

			InitStartPage();
			InitEditorPage("test.txt");

			this.Controls.Add(tbsDocumentTabs, new UniversalWidgetToolkit.Layouts.BoxLayout.Constraints(true, true, 0, UniversalWidgetToolkit.Layouts.BoxLayout.PackType.End));

			this.Bounds = new UniversalWidgetToolkit.Drawing.Rectangle (0, 0, 600, 400);

			this.Text = "Universal Editor";
		}

		private void InitEditorPage(string title)
		{
			TextBox txt = new TextBox(); 
			txt.Text = "Testing for " + title;
			txt.Multiline = true;

			InitDocTab(title, txt);
		}
		private void InitStartPage()
		{
			Label lblStartPage = new Label();
			lblStartPage.Text = "this is a start page";
			InitDocTab("Start Page", lblStartPage);
		}

		private void InitDocTab(string title, Control content)
		{
			DockingItem item = new DockingItem(title, content);
			tbsDocumentTabs.Items.Add(item);
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

		private Shortcut CommandShortcutKeyToUWTShortcut(CommandShortcutKey shortcutKey)
		{
			KeyboardKey key = KeyboardKey.None;

			switch (shortcutKey.Value) {
				case CommandShortcutKeyValue.A:
				{
					key = KeyboardKey.A;
					break;
				}
				case CommandShortcutKeyValue.B:
				{
					key = KeyboardKey.B;
					break;
				}
				case CommandShortcutKeyValue.C:
				{
					key = KeyboardKey.C;
					break;
				}
				case CommandShortcutKeyValue.D:
				{
					key = KeyboardKey.D;
					break;
				}
				case CommandShortcutKeyValue.E:
				{
					key = KeyboardKey.E;
					break;
				}
				case CommandShortcutKeyValue.F:
				{
					key = KeyboardKey.F;
					break;
				}
				case CommandShortcutKeyValue.G:
				{
					key = KeyboardKey.G;
					break;
				}
				case CommandShortcutKeyValue.H:
				{
					key = KeyboardKey.H;
					break;
				}
				case CommandShortcutKeyValue.I:
				{
					key = KeyboardKey.I;
					break;
				}
				case CommandShortcutKeyValue.J:
				{
					key = KeyboardKey.J;
					break;
				}
				case CommandShortcutKeyValue.K:
				{
					key = KeyboardKey.K;
					break;
				}
				case CommandShortcutKeyValue.L:
				{
					key = KeyboardKey.L;
					break;
				}
				case CommandShortcutKeyValue.M:
				{
					key = KeyboardKey.M;
					break;
				}
				case CommandShortcutKeyValue.N:
				{
					key = KeyboardKey.N;
					break;
				}
				case CommandShortcutKeyValue.O:
				{
					key = KeyboardKey.O;
					break;
				}
				case CommandShortcutKeyValue.P:
				{
					key = KeyboardKey.P;
					break;
				}
				case CommandShortcutKeyValue.Q:
				{
					key = KeyboardKey.Q;
					break;
				}
				case CommandShortcutKeyValue.R:
				{
					key = KeyboardKey.R;
					break;
				}
				case CommandShortcutKeyValue.S:
				{
					key = KeyboardKey.S;
					break;
				}
				case CommandShortcutKeyValue.T:
				{
					key = KeyboardKey.T;
					break;
				}
				case CommandShortcutKeyValue.U:
				{
					key = KeyboardKey.U;
					break;
				}
				case CommandShortcutKeyValue.V:
				{
					key = KeyboardKey.V;
					break;
				}
				case CommandShortcutKeyValue.W:
				{
					key = KeyboardKey.W;
					break;
				}
				case CommandShortcutKeyValue.X:
				{
					key = KeyboardKey.X;
					break;
				}
				case CommandShortcutKeyValue.Y:
				{
					key = KeyboardKey.Y;
					break;
				}
				case CommandShortcutKeyValue.Z:
				{
					key = KeyboardKey.Z;
					break;
				}
			}

			KeyboardModifierKey modifierKeys = KeyboardModifierKey.None;

			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Alt) == CommandShortcutKeyModifiers.Alt) modifierKeys |= KeyboardModifierKey.Alt;
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Control) == CommandShortcutKeyModifiers.Control) modifierKeys |= KeyboardModifierKey.Control;
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Hyper) == CommandShortcutKeyModifiers.Hyper) modifierKeys |= KeyboardModifierKey.Hyper;
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Shift) == CommandShortcutKeyModifiers.Shift) modifierKeys |= KeyboardModifierKey.Shift;
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Super) == CommandShortcutKeyModifiers.Super) modifierKeys |= KeyboardModifierKey.Super;

			return new Shortcut (key, modifierKeys);
		}

		private UniversalWidgetToolkit.MenuItem LoadMenuItem(CommandItem ci)
		{
			if (ci is CommandReferenceCommandItem) {
				CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);

				Command cmd = UniversalEditor.UserInterface.Engine.CurrentEngine.Commands [crci.CommandID];
				if (cmd != null) {
					CommandMenuItem mi = new CommandMenuItem (cmd.Title);
					mi.Name = cmd.ID;
					mi.Shortcut = CommandShortcutKeyToUWTShortcut (cmd.ShortcutKey);
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
			if (dlg.ShowDialog () == DialogResult.OK) {
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
				InitEditorPage(doc.Title);
			}
		}

		public void OpenProject (bool combineObjects = false)
		{
			FileDialog dlg = new FileDialog ();
			dlg.FileNameFilters.Add ("Project files", "*.ueproj");
			dlg.FileNameFilters.Add ("Solution files", "*.uesln");
			dlg.Title = "Open Project or Solution";
			if (dlg.ShowDialog () == DialogResult.OK) {

			}
		}

		public void OpenProject (string FileName, bool combineObjects = false)
		{
			throw new NotImplementedException ();
		}

		public void SaveFile ()
		{
			IEditorImplementation currentEditor = GetCurrentEditor();
			if (currentEditor != null)
			{
				FileDialog fd = new FileDialog();
				fd.Mode = FileDialogMode.Save;
				if (fd.ShowDialog() == DialogResult.OK)
				{

				}
			}
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

		private System.Collections.Generic.List<Window> Windows = new System.Collections.Generic.List<Window>();
		public void CloseFile ()
		{
			if (tbsDocumentTabs.CurrentItem != null)
			{
				tbsDocumentTabs.Items.Remove(tbsDocumentTabs.CurrentItem);
			}
			if (this.Windows.Count == 0)
			{
				this.Destroy();
			}
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
			DockingItem curitem = tbsDocumentTabs.CurrentItem;
			if (curitem == null) return null;

			Editor editor = (curitem.ChildControl as Editor);
			if (editor == null) return null;

			return editor;
		}

		public bool ShowOptionsDialog ()
		{
			OptionsDialog dlg = new OptionsDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				return true;
			}
			return false;
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
			InitStartPage();
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

