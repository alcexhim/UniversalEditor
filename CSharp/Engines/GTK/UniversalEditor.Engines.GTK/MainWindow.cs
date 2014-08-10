using System;
using Gtk;

using UniversalEditor;
using UniversalEditor.UserInterface;
using UniversalEditor.Accessors;
using UniversalEditor.Engines.GTK.Dialogs;

namespace UniversalEditor.Engines.GTK
{
	public partial class MainWindow: Gtk.Window, IHostApplicationWindow
	{	
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
			InitializeMenuBar();
			tbsDocumentTabs.RemovePage(0);
			
			RefreshEditor();
		}
		
		protected override void OnShown()
		{
			base.OnShown();
			this.Maximize();
		}
		
		public void ShowStartPage()
		{
			// AddDocumentTab(new Panels.StartPage(), "Start Page", null);
		}
		public void SwitchPerspective(int perspective)
		{
			Gtk.MessageDialog dlg = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Switch to perspective " + perspective.ToString () + " not implemented!");
			dlg.Run ();
			dlg.Destroy();
		}
		
		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Engine.CurrentEngine.Windows.Remove (this);
			if (Engine.CurrentEngine.Windows.Count == 0) Application.Quit();
			return base.OnDeleteEvent (evnt);
		}
		
		public IEditorImplementation GetCurrentEditor()
		{
			if (tbsDocumentTabs.Page < 0 || tbsDocumentTabs.Page >= tbsDocumentTabs.NPages) return null;
			
			Gtk.Widget currentEditor = tbsDocumentTabs.GetNthPage(tbsDocumentTabs.Page);
			return (currentEditor as IEditorImplementation);
		}
		
		protected override bool OnFocused (DirectionType direction)
		{
			Engine.CurrentEngine.LastWindow = this;
			return base.OnFocused (direction);
		}
		
		private bool mvarFullScreen = false;
		public bool FullScreen
		{
			get { return mvarFullScreen; }
			set
			{
				mvarFullScreen = value;
				if (mvarFullScreen)
				{
					base.Fullscreen ();
				}
				else
				{
					base.Unfullscreen ();
				}
			}
		}
		
		private void InitializeMenuBar()
		{
			foreach (CommandItem item in Engine.CurrentEngine.MainMenu.Items)
			{
				CreateCommandItem(item, null);
			}
			menubar1.ShowAll ();
		}

		private AccelKey AccelKeyFromCommandShortcutKey (CommandShortcutKey shortcutKey)
		{
			AccelKey ak = new AccelKey();
			switch (shortcutKey.Value)
			{
				case CommandShortcutKeyValue.A: ak.Key = Gdk.Key.A; break;
				case CommandShortcutKeyValue.B: ak.Key = Gdk.Key.B; break;
			}
			if (((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Alt) == CommandShortcutKeyModifiers.Alt)
			    || ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Meta) == CommandShortcutKeyModifiers.Meta))
			{
				ak.AccelMods |= Gdk.ModifierType.MetaMask;
			}
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Control) == CommandShortcutKeyModifiers.Control)
			{
				ak.AccelMods |= Gdk.ModifierType.ControlMask;
			}
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Shift) == CommandShortcutKeyModifiers.Shift)
			{
				ak.AccelMods |= Gdk.ModifierType.ShiftMask;
			}
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Super) == CommandShortcutKeyModifiers.Super)
			{
				ak.AccelMods |= Gdk.ModifierType.SuperMask;
			}
			if ((shortcutKey.Modifiers & CommandShortcutKeyModifiers.Hyper) == CommandShortcutKeyModifiers.Hyper)
			{
				ak.AccelMods |= Gdk.ModifierType.HyperMask;
			}
			return ak;
		}

		private void CreateCommandItem(CommandItem item, Menu parentMenu)
		{
			Gtk.MenuItem menuItem = null;
			
			if (item is CommandReferenceCommandItem)
			{
				CommandReferenceCommandItem crci = (item as CommandReferenceCommandItem);
				Command cmd = Engine.CurrentEngine.Commands[crci.CommandID];
				if (cmd == null)
				{
					HostApplication.Messages.Add(HostApplicationMessageSeverity.Warning, "The command '" + crci.CommandID + "' was not found");
					return;
				}
				
				menuItem = new Gtk.MenuItem(cmd.Title);
				menuItem.AddAccelerator("signal", new AccelGroup(), AccelKeyFromCommandShortcutKey(cmd.ShortcutKey));
				if (cmd.Items.Count > 0)
				{
					Menu submenu = CreateCommandItemSubmenu(cmd);
					menuItem.Submenu = submenu;
				}
			}
			else if (item is SeparatorCommandItem)
			{
				menuItem = new Gtk.SeparatorMenuItem();
			}
			menuItem.Data.Add ("CommandItem", item);
			menuItem.Activated += menuItem_Activated;
			
			if (menuItem != null)
			{
				if (parentMenu == null)
				{
					menubar1.Append(menuItem);
				}
				else
				{
					parentMenu.Append(menuItem);
				}
			}
		}
	
		void menuItem_Activated (object sender, EventArgs e)
		{
			Gtk.MenuItem mi = (sender as Gtk.MenuItem);
			CommandItem ci = (mi.Data["CommandItem"] as CommandItem);
			if (ci is CommandReferenceCommandItem)
			{
				CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);
				Command cmd = Engine.CurrentEngine.Commands[crci.CommandID];
				if (cmd == null)
				{
					MessageDialog dlg = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Could not find the command " + crci.CommandID);
					dlg.Run ();
				}
				else
				{
					cmd.Execute ();
				}
			}
		}
		private Menu CreateCommandItemSubmenu(Command cmd)
		{
			Menu menu = new Menu();
			if (Engine.CurrentEngine.MainMenu.EnableTearoff && cmd.EnableTearoff)
			{
				menu.Append(new TearoffMenuItem());
			}
			foreach (CommandItem item in cmd.Items)
			{
				CreateCommandItem(item, menu);
			}
			return menu;
		}
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
		
		protected override bool OnFocusInEvent (Gdk.EventFocus evnt)
		{
			Engine.CurrentEngine.LastWindow = this;
			return base.OnFocusInEvent(evnt);
		}
		
		#region IHostApplicationWindow implementation
		public event EventHandler WindowClosed;
	
		public void NewFile ()
		{
			CreateDocumentDialog dlg = new CreateDocumentDialog();
			ResponseType result = (ResponseType)dlg.Run ();
			if (result == ResponseType.Ok)
			{
				
			}
		}
	
		public void NewProject (bool combineObjects)
		{
			throw new System.NotImplementedException ();
		}
	
		public void OpenFile ()
		{
			FileChooserDialog dlg = new FileChooserDialog("Open File", this, FileChooserAction.Open, "Open", Gtk.ResponseType.Ok, "Cancel", Gtk.ResponseType.Cancel);
			ResponseType result = (ResponseType) dlg.Run ();
			
			dlg.Hide();
			
			if (result != ResponseType.Ok)
			{
				return;
			}
			
			OpenFile (dlg.Filenames);
			
			dlg.Destroy();
		}
	
		public void OpenFile (params string[] FileNames)
		{
			foreach (string FileName in FileNames)
			{
				OpenFileInternal(FileName);
			}
		}
		
		private void OpenFileInternal(string FileName)
		{
			DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(FileName);
			DataFormat df = dfrs[0].Create ();
			
			FileAccessor fa = new FileAccessor(FileName);
			
			ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(df.MakeReference ());
			foreach (ObjectModelReference omr in omrs)
			{
				ObjectModel om = omr.Create ();
				IEditorImplementation[] ieditors = UniversalEditor.UserInterface.Common.Reflection.GetAvailableEditors(om.MakeReference ());
				if (ieditors.Length == 1)
				{
					Editor editor = (ieditors[0] as Editor);
					if (editor == null) continue;
					
					Document doc = new Document(om, df, fa);
					doc.InputAccessor.Open ();
					doc.Load ();
					
					editor.ObjectModel = om;
					
					AddDocumentTab(editor, FileName, doc);
					break;
				}
				else if (ieditors.Length > 1)
				{
					Notebook tbsEditors = new Notebook();
					foreach (IEditorImplementation ieditor in ieditors)
					{
						Editor editor = (ieditor as Editor);
						if (editor == null) continue;
						editor.ObjectModel = om;
						
						Label tabLabel = new Label();
						tabLabel.LabelProp = editor.Title;
						tbsEditors.InsertPage(editor, tabLabel, -1);
					}
					AddDocumentTab(tbsEditors, FileName);
				}
				else
				{
					// AddDocumentTab(widget, FileName);
				}
			}
		}
		
		private void RefreshEditor()
		{
			Editor editor = (GetCurrentEditor() as Editor);
			string WindowTitle = Engine.CurrentEngine.DefaultLanguage.GetStringTableEntry("ApplicationTitle", "Universal Editor");
			if (editor != null)
			{
				
			}
			else
			{
			}
			this.Title = WindowTitle;
		}
		
		private void AddDocumentTab(Widget child, string tabTitle, Document doc = null)
		{
			child.Data.Add("Document", doc);
			
			Label tabLabel = new Label();
			if (System.IO.File.Exists (tabTitle))
			{
				tabLabel.LabelProp = System.IO.Path.GetFileName(tabTitle);
			}
			else
			{
				tabLabel.LabelProp = tabTitle;
			}
			tabLabel.TooltipText = tabTitle;
			
			Button btnClose = new Button();
			btnClose.Image = new Image("gtk-close", IconSize.Menu);
			btnClose.Relief = ReliefStyle.None;
			
			Image imgIcon = new Image("gtk-new", IconSize.Menu);
			
			HBox hboxLabelAndCloseButton = new HBox();
			hboxLabelAndCloseButton.PackStart (imgIcon, false, false, 0);
			hboxLabelAndCloseButton.PackStart (tabLabel, true, true, 0);
			hboxLabelAndCloseButton.PackEnd (btnClose, false, false, 0);
			hboxLabelAndCloseButton.ShowAll ();
			
			tbsDocumentTabs.InsertPage(child, hboxLabelAndCloseButton, -1);
			tbsDocumentTabs.Page = tbsDocumentTabs.NPages - 1;
			tbsDocumentTabs.ShowAll ();
		}
	
		public void OpenProject (bool combineObjects)
		{
			throw new System.NotImplementedException ();
		}
	
		public void OpenProject (string FileName, bool combineObjects)
		{
			throw new System.NotImplementedException ();
		}
	
		public void SaveFile ()
		{
			throw new System.NotImplementedException ();
		}
	
		public void SaveFileAs ()
		{
			throw new System.NotImplementedException ();
		}
	
		public void SaveFileAs (string FileName, UniversalEditor.DataFormat df)
		{
			throw new System.NotImplementedException ();
		}
	
		public void SaveProject ()
		{
			throw new System.NotImplementedException ();
		}
	
		public void SaveProjectAs ()
		{
			throw new System.NotImplementedException ();
		}
	
		public void SaveProjectAs (string FileName, UniversalEditor.DataFormat df)
		{
			throw new System.NotImplementedException ();
		}
	
		public void SaveAll ()
		{
			throw new System.NotImplementedException ();
		}
	
		public void CloseFile ()
		{
			throw new System.NotImplementedException ();
		}
	
		public void CloseWindow ()
		{
			throw new System.NotImplementedException ();
		}
	
		public bool ShowOptionsDialog ()
		{
			throw new System.NotImplementedException ();
		}
	
		public void ToggleMenuItemEnabled (string menuItemName, bool enabled)
		{
			throw new System.NotImplementedException ();
		}
	
		public void RefreshCommand (object nativeCommandObject)
		{
			throw new System.NotImplementedException ();
		}
	
		public void UpdateStatus (string statusText)
		{
			throw new System.NotImplementedException ();
		}
	
		public void UpdateProgress (bool visible)
		{
			throw new System.NotImplementedException ();
		}
	
		public void UpdateProgress (int minimum, int maximium, int value)
		{
			throw new System.NotImplementedException ();
		}
	
		public void ActivateWindow ()
		{
			throw new System.NotImplementedException ();
		}
		#endregion
	}
}