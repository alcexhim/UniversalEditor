using System;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.Solution;
using UniversalEditor.ObjectModels.Text.Plain;

using UniversalEditor.UserInterface.Dialogs;
using UniversalEditor.UserInterface.Panels;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Docking;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.Drawing;

// TODO: We need to work on UWT signaling to native objects...
using MBS.Framework.UserInterface.Layouts;
using MBS.Framework.UserInterface.Controls.Ribbon;
using UniversalEditor.Printing;
using MBS.Framework.UserInterface.Printing;
using UniversalEditor.UserInterface.Pages;
using UniversalEditor.ObjectModels.Binary;
using UniversalEditor.DataFormats.Binary;
using System.Collections.Generic;

namespace UniversalEditor.UserInterface
{
	public class MainWindow : Window, IHostApplicationWindow
	{
		private DockingContainer dckContainer = null;
		private TabContainer tbsDocumentTabs = new TabContainer();

		private ErrorListPanel pnlErrorList = new ErrorListPanel();
		private SolutionExplorerPanel pnlSolutionExplorer = new SolutionExplorerPanel();

		private RibbonTab LoadRibbonBar(CommandBar cb)
		{
			RibbonTab tab = new RibbonTab ();

			RibbonTabGroup rtgClipboard = new RibbonTabGroup ();
			rtgClipboard.Title = "Clipboard";

			rtgClipboard.Items.Add (new RibbonCommandItemButton ("EditPaste"));
			(rtgClipboard.Items[0] as RibbonCommandItemButton).IsImportant = true;

			rtgClipboard.Items.Add (new RibbonCommandItemButton ("EditCut"));
			rtgClipboard.Items.Add (new RibbonCommandItemButton ("EditCopy"));
			rtgClipboard.Items.Add (new RibbonCommandItemButton ("EditDelete"));

			tab.Groups.Add (rtgClipboard);

			RibbonTabGroup rtgNew = new RibbonTabGroup ();
			rtgNew.Title = "New";

			rtgNew.Items.Add (new RibbonCommandItemButton ("FileNewDocument"));
			(rtgNew.Items [0] as RibbonCommandItemButton).IsImportant = true;
			rtgNew.Items.Add (new RibbonCommandItemButton ("FileNewProject"));
			(rtgNew.Items [1] as RibbonCommandItemButton).IsImportant = true;

			tab.Groups.Add (rtgNew);

			RibbonTabGroup rtgSelect = new RibbonTabGroup ();
			rtgSelect.Title = "Select";

			rtgSelect.Items.Add (new RibbonCommandItemButton ("EditSelectAll"));
			rtgSelect.Items.Add (new RibbonCommandItemButton ("EditInvertSelection"));

			tab.Groups.Add (rtgSelect);

			/*
			Container ctFont = new Container ();
			ctFont.Layout = new BoxLayout (Orientation.Vertical);
			Container ctFontFace = new Container ();
			ctFontFace.Layout = new BoxLayout (Orientation.Horizontal);
			TextBox txtFontFace = new TextBox ();
			txtFontFace.Text = "Calibri (Body)";
			ctFontFace.Controls.Add (txtFontFace);
			ctFont.Controls.Add (ctFontFace);

			RibbonTabGroup rtgFont = LoadRibbonTabGroup ("Font", ctFont);
			tab.Groups.Add (rtgFont);

			Toolbar tb = LoadCommandBar (cb);
			RibbonTabGroup rtg2 = LoadRibbonTabGroup ("General", tb);

			tab.Groups.Add (rtg2);
			*/
			return tab;
		}

		private Toolbar LoadCommandBar(CommandBar cb)
		{
			Toolbar tb = new Toolbar();	
			foreach (CommandItem ci in cb.Items)
			{
				if (ci is SeparatorCommandItem)
				{
					tb.Items.Add(new ToolbarItemSeparator());
				}
				else if (ci is CommandReferenceCommandItem)
				{
					CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);
					Command cmd = Application.Commands[crci.CommandID];
					if (cmd == null) continue;
					
					ToolbarItemButton tsb = new ToolbarItemButton(cmd.ID, (StockType)cmd.StockType);
					tsb.SetExtraData<CommandReferenceCommandItem>("crci", crci);
					tsb.Click += tsbCommand_Click;
					tsb.Title = cmd.Title;
					tb.Items.Add(tsb);
				}
			}
			return tb;
		}

		private void tsbCommand_Click(object sender, EventArgs e)
		{
			ToolbarItemButton tsb = (sender as ToolbarItemButton);
			CommandReferenceCommandItem crci = tsb.GetExtraData<CommandReferenceCommandItem>("crci");
			Command cmd = Application.Commands[crci.CommandID];
			cmd.Execute();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			// we have to process key shortcuts manually if we do not use a traditional menu bar
			foreach (Command cmd in Application.Commands) {
				if (cmd.Shortcut == null) continue;

				if (cmd.Shortcut.Key == e.Key && cmd.Shortcut.ModifierKeys == e.ModifierKeys) {
					Application.ExecuteCommand (cmd.ID);
					e.Cancel = true;
					break;
				}
			}
		}

		public MainWindow()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			this.IconName = "universal-editor";

			this.CommandDisplayMode = CommandDisplayMode.CommandBar;

			foreach (CommandItem ci in Engine.CurrentEngine.MainMenu.Items)
			{
				MBS.Framework.UserInterface.MenuItem mi = MBS.Framework.UserInterface.MenuItem.LoadMenuItem(ci, MainWindow_MenuBar_Item_Click);
				if (mi == null)
					continue;

				if (mi.Name == "Help")
				{
					mi.HorizontalAlignment = MenuItemHorizontalAlignment.Right;
				}
				this.MenuBar.Items.Add(mi);
			}

			if (this.CommandDisplayMode == CommandDisplayMode.Ribbon || this.CommandDisplayMode == CommandDisplayMode.Both) {
				foreach (CommandBar cb in Engine.CurrentEngine.CommandBars) {
					RibbonTab ribbonTabHome = LoadRibbonBar (cb);
					ribbonTabHome.Title = "Home";
					this.Ribbon.Tabs.Add (ribbonTabHome);
				}
			}
			if (this.CommandDisplayMode == CommandDisplayMode.CommandBar || this.CommandDisplayMode == CommandDisplayMode.Both) {
				foreach (CommandBar cb in Engine.CurrentEngine.CommandBars) {
					this.Controls.Add (LoadCommandBar(cb));
				}
			}
			dckContainer = new DockingContainer();
			dckContainer.SelectionChanged += dckContainer_SelectionChanged;
			Controls.Add (dckContainer, new BoxLayout.Constraints(true, true, 0, BoxLayout.PackType.End));

			tbsDocumentTabs = new TabContainer();

			InitStartPage();

			AddPanel("Toolbox", DockingItemPlacement.Left);

			AddPanel("Solution Explorer", DockingItemPlacement.Right, pnlSolutionExplorer);
			AddPanel("Properties", DockingItemPlacement.Right);


			AddPanel("Error List", DockingItemPlacement.Bottom, pnlErrorList);

			this.Bounds = new Rectangle(0, 0, 600, 400);
			this.Size = new Dimension2D(800, 600);
			this.Text = "Universal Editor";
		}

		#region Editor Page Events
		private void page_DocumentEdited(object sender, EventArgs e)
		{
			Pages.EditorPage page = (sender as Pages.EditorPage);
			// AwesomeControls.DockingWindows.DockingWindow doc = dcc.Windows[page];
			// if (doc == null) return;

			if (String.IsNullOrEmpty(page.Document.Title))
			{
				// doc.Title = "<untitled> (*)";
			}
			else
			{
				// doc.Title = System.IO.Path.GetFileName(page.Document.Title) + " (*)";
			}
			page.Document.IsChanged = true;
		}
		/*
		private void page_Navigate(object sender, NavigateEventArgs e)
		{
			// OpenFile(e.FileName, ((Control.ModifierKeys & Keys.Alt) != Keys.Alt));
		}
		*/
		#endregion

		private int iUntitledDocCount = 0;

		public void NewFile()
		{
			/*
			NewDialog2 dlg2 = new NewDialog2();
			dlg2.ShowDialog();
			*/

			NewDialog dlg = new NewDialog();
			dlg.Mode = NewDialogMode.File;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				iUntitledDocCount++;

				DocumentTemplate template = (dlg.SelectedItem as DocumentTemplate);
				if (template == null) return;

				Pages.EditorPage page = new Pages.EditorPage();
				page.DocumentEdited += page_DocumentEdited;
				page.Title = String.Format("<untitled{0}>", iUntitledDocCount);

				ObjectModel objm = template.ObjectModelReference.Create();
				if (objm == null)
				{
					MessageDialog.ShowDialog("Failed to create an ObjectModel for the type \"" + template.ObjectModelReference.TypeName + "\"", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
					return;
				}

				page.Document = new Document(objm, null, null);
				/*
				DockingWindow dwNewDocument = dcc.Windows.Add("<untitled>", "<untitled>", page);
				dwNewDocument.Behavior = DockBehavior.Dock;

				dcc.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows.Add(dwNewDocument);
				*/
				/*
				Glue.ApplicationEventEventArgs ae = new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.AfterCreateFile,
					new KeyValuePair<string, object>("ObjectModel", objm)
				);

				Glue.Common.Methods.SendApplicationEvent(ae);
				*/
				InitDocTab (page.Title, page);
			}
		}

		private Editor _prevEditor = null;
		private List<Command> _editorScopedCommands = new List<Command>();
		private List<MBS.Framework.UserInterface.MenuItem> _editorScopedMenuItems = new List<MBS.Framework.UserInterface.MenuItem>();

		private void dckContainer_SelectionChanged(object sender, EventArgs e)
		{
			Editor editor = null;
			try
			{
				editor = GetCurrentEditor();
			}
			catch (Exception ex)
			{
			}

			if (editor != _prevEditor)
			{
				foreach (MBS.Framework.UserInterface.MenuItem mi in _editorScopedMenuItems)
				{
					MenuBar.Items.Remove(mi);
				}
				_editorScopedMenuItems.Clear();

				foreach (Command cmd in _editorScopedCommands)
				{
					Application.Commands.Remove(cmd);
				}
				_editorScopedCommands.Clear();
			}

			if (editor == null) {
				Console.WriteLine ("Editor is null");
			} else {
				Console.WriteLine ("Editor is NOT null");

				EditorReference er = editor.MakeReference();
				foreach (Command cmd in er.Commands)
				{
					Application.Commands.Add(cmd);
					_editorScopedCommands.Add(cmd);
				}

				foreach (CommandItem ci in er.MenuBar.Items)
				{
					MBS.Framework.UserInterface.MenuItem mi = MBS.Framework.UserInterface.MenuItem.LoadMenuItem(ci, MainWindow_MenuBar_Item_Click);
					MenuBar.Items.Add(mi);
					_editorScopedMenuItems.Add(mi);
				}
			}
			_prevEditor = editor;
		}

		public void NewProject(bool combineObjects = false)
		{
			NewDialog dlg = new NewDialog();
			dlg.Mode = NewDialogMode.Project;
			dlg.CombineObjects = combineObjects;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				SolutionObjectModel solution = null;

				ProjectTemplate pjt = (dlg.SelectedItem as ProjectTemplate);
				if (dlg.CombineObjects)
				{
					solution = CurrentSolution;
				}
				else
				{
					// Create the project
					solution = new SolutionObjectModel();
					solution.Title = dlg.SolutionTitle;
				}

				ProjectObjectModel project = pjt.Create();
				project.ID = Guid.NewGuid();
				project.Title = dlg.ProjectTitle;
				solution.Projects.Add(project);

				CurrentSolution = solution;
			}
		}



		private void AddPanel(string title, DockingItemPlacement placement, Control control = null)
		{
			if (control == null)
			{
				Label lblErrorList = new Label(title);
				control = lblErrorList;
			}

			DockingItem dkiErrorList = new DockingItem(title, control);
			dkiErrorList.Placement = placement;

			dckContainer.Items.Add(dkiErrorList);
		}

		private void InitEditorPage(Document doc)
		{
			if (doc.DataFormat == null)
			{
				Console.WriteLine("InitEditorPage: DataFormat unspecified for Document");
				
				DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(doc.Accessor);
				Console.WriteLine("found {0} DataFormats for Accessor {1}", dfrs.Length.ToString(), doc.Accessor.ToString());
				
				if (dfrs.Length > 0)
				{
					bool found = false;
					foreach (DataFormatReference dfr in dfrs) {
						ObjectModelReference [] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels (dfr);
						if (omrs.Length < 1) {
							Console.WriteLine ("Object model not found for data format " + dfr.Title + " ; using default editor");
						}

						ObjectModelReference omr = omrs [0];
						ObjectModel om = omr.Create ();

						doc.DataFormat = dfr.Create ();
						doc.ObjectModel = om;

						try {
							doc.Accessor.Open ();
							doc.Load ();
						} catch (InvalidDataFormatException ex) {
							doc.Accessor.Close ();
							continue;
						}

						found = true;
						break;
					}
					if (!found) {
						OpenDefaultEditor (doc.Accessor.GetFileName ());
						return;
					}
				}
			}

			if (doc.ObjectModel != null)
			{
				EditorReference[] editors = Common.Reflection.GetAvailableEditors(doc.ObjectModel.MakeReference());
				Console.WriteLine("found {0} editors for object model {1}", editors.Length.ToString(), doc.ObjectModel.ToString());
				if (editors.Length > 0)
				{
					// no need to open and load file, it's already been done
					Editor editor = editors[0].Create();
					EditorPage page = new EditorPage();
					page.Controls.Add(editor, new BoxLayout.Constraints(true, true));

					InitDocTab(doc.Title, page);

					editor.ObjectModel = doc.ObjectModel;
				}
				else
				{
					Console.Error.WriteLine("Editor not found for object model " + doc.ObjectModel.MakeReference().Title + " ; using default editor");
					OpenDefaultEditor(doc.Accessor.GetFileName());
				}
			}
			else
			{
				Console.Error.WriteLine("ObjectModel not specified for accessor " + doc.Accessor.ToString() + " ; using default editor");
				OpenDefaultEditor(doc.Accessor.GetFileName());
			}
		}

		private EditorReference DefaultEditor = new EditorReference(typeof(Editors.Binary.BinaryEditor));
		private void OpenDefaultEditor(string filename)
		{
			if (DefaultEditor == null) return;

			Editor ed = DefaultEditor.Create();

			/*
			PlainTextObjectModel om1 = new PlainTextObjectModel();
			if (System.IO.File.Exists(filename))
			{
				System.IO.FileInfo fi = new System.IO.FileInfo(filename);
				if (fi.Length < Math.Pow(1024, 2))
				{
					String content = System.IO.File.ReadAllText(filename);
					om1.Text = content;
				}
			}
			ed.ObjectModel = om1;
			*/

			BinaryObjectModel om1 = new BinaryObjectModel();
			if (System.IO.File.Exists(filename))
			{
				System.IO.FileInfo fi = new System.IO.FileInfo(filename);
				if (fi.Length < Math.Pow(1024, 4))
				{
					byte[] content = System.IO.File.ReadAllBytes(filename);
					om1.Data = content;
				}
			}
			ed.ObjectModel = om1;

			EditorPage page = new EditorPage();
			page.Controls.Add(ed, new BoxLayout.Constraints(true, true));

			InitDocTab(System.IO.Path.GetFileName(filename), page);
		}

		[ContainerLayout("~/Panels/StartPage.glade")]
		private class StartPageDialog : Dialog
		{
		}

		private void InitStartPage()
		{
			// StartPageDialog dlg = new StartPageDialog();
			// dlg.ShowDialog();

			StartPagePanel lblStartPage = new StartPagePanel();
			InitDocTab("Start Page", lblStartPage);
		}

		private int documentWindowCount = 0;
		private void InitDocTab(string title, Control content)
		{
			DockingItem item = new DockingItem(title, content);
			dckContainer.Items.Add(item);

			documentWindowCount++;

			// HACK: until we can properly figure out when a docking container has its current window changed
			dckContainer_SelectionChanged(this, EventArgs.Empty);
		}

		private void MainWindow_MenuBar_Item_Click(object sender, EventArgs e)
		{
			CommandMenuItem mi = (sender as CommandMenuItem);
			if (mi == null)
				return;

			Command cmd = Application.Commands[mi.Name];
			if (cmd == null)
			{
				Console.WriteLine("unknown cmd '" + mi.Name + "'");
				return;
			}

			cmd.Execute();
		}

		protected override void OnClosed(EventArgs e)
		{
			MBS.Framework.UserInterface.Application.Stop();
		}
		protected override void OnCreated(EventArgs e)
		{
			this.RegisterDropTarget(new DragDropTarget[]
			{
				new DragDropTarget("text/uri-list", DragDropTargetFlags.SameApplication | DragDropTargetFlags.OtherApplication, 0x1)
			}, DragDropEffect.Copy, MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey.None);
		}

		#region IHostApplicationWindow implementation
		public void OpenFile()
		{
			using (DocumentPropertiesDialogV2 dlg = new DocumentPropertiesDialogV2 ())
			{
				DialogResult result = dlg.ShowDialog ();
				if (result == DialogResult.OK)
				{
					if (dlg.Accessor == null) {
						return;
					}

					Document doc = new Document(null, null, dlg.Accessor);
					OpenFile(doc);
				}
			}
			/*
			using (DocumentPropertiesDialog dlg = new DocumentPropertiesDialog())
			{
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					Document doc = new Document(dlg.ObjectModel, dlg.DataFormat, dlg.Accessor);
					OpenFile(doc);
				}
			}
			*/
		}

		public void OpenFile(params string[] fileNames)
		{
			Document[] documents = new Document[fileNames.Length];
			for (int i = 0; i < documents.Length; i++)
			{
				AccessorReference[] accs = UniversalEditor.Common.Reflection.GetAvailableAccessors(fileNames[i]);
				if (accs.Length > 0)
				{
					Accessor fa = accs[0].Create();
					documents[i] = new Document(fa);
				}
			}
			OpenFile(documents);
		}

		public void OpenFile(params Document[] documents)
		{
			foreach (Document doc in documents)
			{
				InitEditorPage(doc);
			}
		}

		public void OpenProject(bool combineObjects = false)
		{
			FileDialog dlg = new FileDialog();
			dlg.FileNameFilters.Add("Project files", "*.ueproj");
			dlg.FileNameFilters.Add("Solution files", "*.uesln");
			dlg.Text = "Open Project or Solution";
			if (dlg.ShowDialog() == DialogResult.OK)
			{

			}
		}

		public void OpenProject(string FileName, bool combineObjects = false)
		{
			throw new NotImplementedException();
		}

		public void SaveFile()
		{
			Pages.EditorPage currentEditorPage = GetCurrentEditorPage();
			if (currentEditorPage != null)
			{
				if (currentEditorPage.Document.IsSaved) {
					// save it again
				} else {
					SaveFileAs ();
				}
			}
		}

		public void SaveFileAs()
		{
			Editor currentEditor = GetCurrentEditor();
			if (currentEditor != null)
			{
				using (DocumentPropertiesDialog dlg = new DocumentPropertiesDialog ())
				{
					dlg.Mode = DocumentPropertiesDialogMode.Save;
					if (dlg.ShowDialog () == DialogResult.OK)
					{
						DataFormat df = dlg.DataFormat;
						if (df == null && currentEditor.ObjectModel is BinaryObjectModel)
						{
							df = new BinaryDataFormat();
						}
						SaveFileAs(dlg.Accessor.GetFileName(), df);
					}
				}
			}
		}

		public void SaveFileAs(string FileName, DataFormat df)
		{
			Editor ed = GetCurrentEditor();
			Document.Save(ed.ObjectModel, df, new FileAccessor(FileName, true, true, true));
		}

		public void SaveProject()
		{
			throw new NotImplementedException();
		}

		public void SaveProjectAs()
		{
			throw new NotImplementedException();
		}

		public void SaveProjectAs(string FileName, DataFormat df)
		{
			throw new NotImplementedException();
		}

		public void SaveAll()
		{
			throw new NotImplementedException();
		}

		public void SwitchPerspective(int index)
		{
			throw new NotImplementedException();
		}

		private System.Collections.Generic.List<Window> Windows = new System.Collections.Generic.List<Window>();
		public void CloseFile()
		{
			if (dckContainer.CurrentItem != null)
			{
				dckContainer.Items.Remove(dckContainer.CurrentItem);
				documentWindowCount--;
			}
			if (documentWindowCount == 0)
			{
				CloseWindow ();
			}
		}

		public void CloseProject()
		{
			throw new NotImplementedException();
		}

		public void CloseWindow()
		{
			this.Destroy ();
		}

		public void PrintDocument()
		{
			Editor editor = GetCurrentEditor ();
			if (editor != null) {
				PrintHandlerReference[] phrs = UniversalEditor.Printing.Reflection.GetAvailablePrintHandlers(editor.ObjectModel);
				if (phrs.Length > 0)
				{
					PrintDialog dlg = new PrintDialog();
					if (dlg.ShowDialog(this) == DialogResult.OK)
					{
						PrintHandler ph = phrs[0].Create();
						if (ph != null)
						{
							PrintJob job = new PrintJob(editor.Title, dlg.SelectedPrinter, dlg.Settings);
							job.BeginPrint += Job_BeginPrint;
							job.DrawPage += Job_DrawPage;
							job.SetExtraData<PrintHandler>("ph", ph);
							job.SetExtraData<ObjectModel>("om", editor.ObjectModel);

							job.Send();
						}
					}
				}
			}
		}

		void Job_DrawPage(object sender, PrintEventArgs e)
		{
			PrintJob job = (sender as PrintJob);
			PrintHandler ph = job.GetExtraData<PrintHandler>("ph");
			ObjectModel om = job.GetExtraData<ObjectModel>("om");

			ph.Print(om, e.Graphics);
		}


		void Job_BeginPrint(object sender, PrintEventArgs e)
		{
			PrintJob job = (sender as PrintJob);

			PrintHandler ph = job.GetExtraData<PrintHandler>("ph");
			ObjectModel om = job.GetExtraData<ObjectModel>("om");
		}


		public Editor GetCurrentEditor()
		{
			Pages.EditorPage page = GetCurrentEditorPage ();
			if (page == null)
				return null;

			if (page.Controls.Count > 0)
				return (page.Controls[0] as Editor);
			return null;
		}
		public Pages.EditorPage GetCurrentEditorPage()
		{
			DockingItem curitem = dckContainer.CurrentItem;
			if (curitem == null) return null;

			Pages.EditorPage editorPage = (curitem.ChildControl as Pages.EditorPage);
			if (editorPage == null) return null;

			return editorPage;
		}

		public bool ShowOptionsDialog()
		{
			SettingsDialog dlg = new SettingsDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				return true;
			}
			return false;
		}

		public void ToggleMenuItemEnabled(string menuItemName, bool enabled)
		{
			throw new NotImplementedException();
		}

		public void RefreshCommand(object nativeCommandObject)
		{
			throw new NotImplementedException();
		}

		private void AddRecentMenuItem(string FileName)
		{
			Command mnuFileRecentFiles = Application.Commands["FileRecentFiles"];

			Command mnuFileRecentFile = new Command();
			mnuFileRecentFile.ID = "FileRecentFile_" + FileName;
			mnuFileRecentFile.Title = System.IO.Path.GetFileName(FileName);
			// mnuFileRecentFile.ToolTipText = FileName;
			Application.Commands.Add(mnuFileRecentFile);

			CommandReferenceCommandItem tsmi = new CommandReferenceCommandItem("FileRecentFile_" + FileName);
			mnuFileRecentFiles.Items.Add(tsmi);
		}
		private void RefreshRecentFilesList()
		{
			Command mnuFileRecentFiles = Application.Commands["FileRecentFiles"];
			mnuFileRecentFiles.Items.Clear();
			foreach (string fileName in Engine.CurrentEngine.RecentFileManager.FileNames)
			{
				AddRecentMenuItem(fileName);
			}

			Command mnuFileRecentProjects = Application.Commands["FileRecentProjects"];

			mnuFileRecentFiles.Visible = (mnuFileRecentFiles.Items.Count > 0);
			mnuFileRecentProjects.Visible = (mnuFileRecentProjects.Items.Count > 0);
			// mnuFileSep3.Visible = ((mnuFileRecentFiles.DropDownItems.Count > 0) || (mnuFileRecentProjects.DropDownItems.Count > 0));
		}

		public void UpdateStatus(string statusText)
		{
			throw new NotImplementedException();
		}

		public void UpdateProgress(bool visible)
		{
			throw new NotImplementedException();
		}

		public void UpdateProgress(int minimum, int maximium, int value)
		{
			throw new NotImplementedException();
		}

		public void ActivateWindow()
		{
			throw new NotImplementedException();
		}

		public void ShowStartPage()
		{
			InitStartPage();
		}

		public void SetWindowListVisible(bool visible, bool modal)
		{
			// this calls out to the DockingContainerControl in WF
			if (modal)
			{
				// dckContainer.ShowWindowListPopupDialog();
			}
			else
			{
				if (visible)
				{
					// dckContainer.ShowWindowListPopup();
				}
				else
				{
					// dckContainer.HideWindowListPopup();
				}
			}
		}

		public event EventHandler WindowClosed;

		public bool FullScreen { get; set; }

		public SolutionObjectModel CurrentSolution { get; set; }

		#endregion


		public void ShowDocumentPropertiesDialog()
		{
			MessageDialog.ShowDialog("TODO: Implement Document Properties dialog!", "Not Implemented", MessageDialogButtons.OK, MessageDialogIcon.Error);
		}
	}
}
	