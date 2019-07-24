using System;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.Solution;
using UniversalEditor.ObjectModels.Text.Plain;

using UniversalEditor.UserInterface.Dialogs;
using UniversalEditor.UserInterface.Panels;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Controls.Docking;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.DragDrop;
using UniversalWidgetToolkit.Input.Keyboard;
using UniversalWidgetToolkit.Input.Mouse;

using UniversalWidgetToolkit.Drawing;
using MBS.Framework.Drawing;

// TODO: We need to work on UWT signaling to native objects...

namespace UniversalEditor.UserInterface
{
	public class MainWindow : Window, IHostApplicationWindow
	{
		private DockingContainer dckContainer = null;
		private TabContainer tbsDocumentTabs = new TabContainer();

		private ErrorListPanel pnlErrorList = new ErrorListPanel();
		private SolutionExplorerPanel pnlSolutionExplorer = new SolutionExplorerPanel();

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
					Command cmd = Engine.CurrentEngine.Commands[crci.CommandID];
					if (cmd == null) continue;
					
					ToolbarItemButton tsb = new ToolbarItemButton(cmd.ID, (StockType)cmd.StockType);
					tsb.SetExtraData<CommandReferenceCommandItem>("crci", crci);
					tsb.Click += tsbCommand_Click;
					tb.Items.Add(tsb);
				}
			}
			return tb;
		}

		private void tsbCommand_Click(object sender, EventArgs e)
		{
			ToolbarItemButton tsb = (sender as ToolbarItemButton);
			CommandReferenceCommandItem crci = tsb.GetExtraData<CommandReferenceCommandItem>("crci");
			Command cmd = Engine.CurrentEngine.Commands[crci.CommandID];
			cmd.Execute();
		}

		public MainWindow()
		{
			UniversalWidgetToolkit.Layouts.BoxLayout layout = new UniversalWidgetToolkit.Layouts.BoxLayout(Orientation.Vertical);
			this.Layout = layout;
			this.IconName = "universal-editor";

			foreach (CommandItem ci in Engine.CurrentEngine.MainMenu.Items)
			{
				UniversalWidgetToolkit.MenuItem mi = LoadMenuItem(ci);
				if (mi == null)
					continue;

				if (mi.Name == "Help")
				{
					mi.HorizontalAlignment = MenuItemHorizontalAlignment.Right;
				}
				this.MenuBar.Items.Add(mi);
			}
			foreach (CommandBar cb in Engine.CurrentEngine.CommandBars)
			{
				Toolbar tb = LoadCommandBar(cb);
				if (tb == null) continue;
				
				Controls.Add(tb);
			}

			dckContainer = new DockingContainer();
			tbsDocumentTabs = new TabContainer();

			InitStartPage();

			AddPanel("Toolbox", DockingItemPlacement.Left);

			AddPanel("Solution Explorer", DockingItemPlacement.Right, pnlSolutionExplorer);
			AddPanel("Properties", DockingItemPlacement.Right);


			AddPanel("Error List", DockingItemPlacement.Bottom, pnlErrorList);


			this.Controls.Add(dckContainer, new UniversalWidgetToolkit.Layouts.BoxLayout.Constraints(true, true, 0, UniversalWidgetToolkit.Layouts.BoxLayout.PackType.End));

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

		public void NewFile()
		{
			NewDialog2 dlg2 = new NewDialog2();
			dlg2.ShowDialog();


			NewDialog dlg = new NewDialog();
			dlg.Mode = NewDialogMode.File;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				DocumentTemplate template = (dlg.SelectedItem as DocumentTemplate);
				if (template == null) return;

				Pages.EditorPage page = new Pages.EditorPage();
				page.DocumentEdited += page_DocumentEdited;
				page.Title = "<untitled>";

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
			}
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
					ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(doc.Accessor);
					if (omrs.Length < 1)
					{
						Console.WriteLine("Object model not found for data format " + dfrs[0].Title + " ; using default editor");
						OpenDefaultEditor(doc.Accessor.GetFileName());
						return;
					}
					
					ObjectModelReference omr = omrs[0];
					ObjectModel om = omr.Create();

					doc.DataFormat = dfrs[0].Create();
					doc.ObjectModel = om;
				}
			}
			
			EditorReference[] editors = Common.Reflection.GetAvailableEditors(doc.ObjectModel.MakeReference());
			Console.WriteLine("found {0} editors for object model {1}", editors.Length.ToString(), doc.ObjectModel.ToString());
			if (editors.Length > 0)
			{
				doc.Accessor.Open();
				doc.Load();
				//doc.Close();

				Editor editor = editors[0].Create();
				InitDocTab(doc.Title, editor);

				editor.ObjectModel = doc.ObjectModel;
			}
			else
			{
				Console.Error.WriteLine("Editor not found for object model " + doc.ObjectModel.MakeReference().Title + " ; using default editor");
				OpenDefaultEditor(doc.Accessor.GetFileName());
			}
		}

		private EditorReference DefaultEditor = new EditorReference(typeof(Editors.Text.Plain.PlainTextEditor));
		private void OpenDefaultEditor(string filename)
		{
			if (DefaultEditor == null) return;

			Editor ed = DefaultEditor.Create();

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

			InitDocTab(System.IO.Path.GetFileName(filename)
			, ed);
		}

		[ContainerLayout("~/Panels/StartPage.glade")]
		private class StartPageDialog : Dialog
		{
		}

		private void InitStartPage()
		{
			// StartPagePanel lblStartPage = new StartPagePanel();
			// StartPageDialog dlg = new StartPageDialog();
			// dlg.ShowDialog();
			// InitDocTab("Start Page", lblStartPage);
		}

		private void InitDocTab(string title, Control content)
		{
			DockingItem item = new DockingItem(title, content);
			dckContainer.Items.Add(item);
		}

		private void MainWindow_MenuBar_Item_Click(object sender, EventArgs e)
		{
			CommandMenuItem mi = (sender as CommandMenuItem);
			if (mi == null)
				return;

			Command cmd = UniversalEditor.UserInterface.Engine.CurrentEngine.Commands[mi.Name];
			if (cmd == null)
			{
				Console.WriteLine("unknown cmd '" + mi.Name + "'");
				return;
			}

			cmd.Execute();
		}

		protected override void OnClosed(EventArgs e)
		{
			UniversalWidgetToolkit.Application.Stop();
		}
		protected override void OnCreated(EventArgs e)
		{
			this.RegisterDropTarget(new DragDropTarget[]
			{
				new DragDropTarget("text/uri-list", DragDropTargetFlags.SameApplication | DragDropTargetFlags.OtherApplication, 0x1)
			}, DragDropEffect.Copy, MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey.None);
		}

		private Shortcut CommandShortcutKeyToUWTShortcut(CommandShortcutKey shortcutKey)
		{
			KeyboardKey key = KeyboardKey.None;

			switch (shortcutKey.Value)
			{
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

			return new Shortcut(key, modifierKeys);
		}

		private UniversalWidgetToolkit.MenuItem LoadMenuItem(CommandItem ci)
		{
			if (ci is CommandReferenceCommandItem)
			{
				CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);

				Command cmd = UniversalEditor.UserInterface.Engine.CurrentEngine.Commands[crci.CommandID];
				if (cmd != null)
				{
					CommandMenuItem mi = new CommandMenuItem(cmd.Title);
					mi.Name = cmd.ID;
					mi.Shortcut = CommandShortcutKeyToUWTShortcut(cmd.ShortcutKey);
					if (cmd.Items.Count > 0)
					{
						foreach (CommandItem ci1 in cmd.Items)
						{
							UniversalWidgetToolkit.MenuItem mi1 = LoadMenuItem(ci1);
							mi.Items.Add(mi1);
						}
					}
					else
					{
						mi.Click += MainWindow_MenuBar_Item_Click;
					}
					return mi;
				}
				else
				{
					Console.WriteLine("attempted to load unknown cmd '" + crci.CommandID + "'");
				}
				return null;
			}
			else if (ci is SeparatorCommandItem)
			{
				return new UniversalWidgetToolkit.SeparatorMenuItem();
			}
			return null;
		}

		#region IHostApplicationWindow implementation
		public void OpenFile()
		{
			Console.WriteLine("in OpenFile()");
			using (DocumentPropertiesDialog dlg = new DocumentPropertiesDialog())
			{
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					Console.WriteLine("====== DOCUMENT PROPERTIES DIALOG RESULTS ======");
					Console.WriteLine("    ObjectModel:  {0}", dlg.ObjectModel);
					Console.WriteLine("    DataFormat:   {0}", dlg.DataFormat);
					Console.WriteLine("    Accessor:     {0}", dlg.Accessor);
					Document doc = new Document(dlg.ObjectModel, dlg.DataFormat, dlg.Accessor);
					OpenFile(doc);
				}
			}
		}

		public void OpenFile(params string[] fileNames)
		{
			Document[] documents = new Document[fileNames.Length];
			for (int i = 0; i < documents.Length; i++)
			{
				FileAccessor fa = new FileAccessor(fileNames[i]);
				documents[i] = new Document(fa);
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
			Editor currentEditor = GetCurrentEditor();
			if (currentEditor != null)
			{
				FileDialog fd = new FileDialog();
				fd.Mode = FileDialogMode.Save;
				if (fd.ShowDialog() == DialogResult.OK)
				{

				}
			}
		}

		public void SaveFileAs()
		{
			throw new NotImplementedException();
		}

		public void SaveFileAs(string FileName, DataFormat df)
		{
			throw new NotImplementedException();
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
			}
			if (this.Windows.Count == 0)
			{
				this.Destroy();
			}
		}

		public void CloseProject()
		{
			throw new NotImplementedException();
		}

		public void CloseWindow()
		{
			throw new NotImplementedException();
		}

		public Editor GetCurrentEditor()
		{
			DockingItem curitem = dckContainer.CurrentItem;
			if (curitem == null) return null;

			Editor editor = (curitem.ChildControl as Editor);
			if (editor == null) return null;

			return editor;
		}

		public bool ShowOptionsDialog()
		{
			OptionsDialog dlg = new OptionsDialog();
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
			Command mnuFileRecentFiles = Engine.CurrentEngine.Commands["FileRecentFiles"];

			Command mnuFileRecentFile = new Command();
			mnuFileRecentFile.ID = "FileRecentFile_" + FileName;
			mnuFileRecentFile.Title = System.IO.Path.GetFileName(FileName);
			// mnuFileRecentFile.ToolTipText = FileName;
			Engine.CurrentEngine.Commands.Add(mnuFileRecentFile);

			CommandReferenceCommandItem tsmi = new CommandReferenceCommandItem("FileRecentFile_" + FileName);
			mnuFileRecentFiles.Items.Add(tsmi);
		}
		private void RefreshRecentFilesList()
		{
			Command mnuFileRecentFiles = Engine.CurrentEngine.Commands["FileRecentFiles"];
			mnuFileRecentFiles.Items.Clear();
			foreach (string fileName in Engine.CurrentEngine.RecentFileManager.FileNames)
			{
				AddRecentMenuItem(fileName);
			}

			Command mnuFileRecentProjects = Engine.CurrentEngine.Commands["FileRecentProjects"];

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
			/*
			if (modal)
			{
				dcc.DisplayWindowListDialog();
			}
			else
			{
				if (visible)
				{
					dcc.ShowWindowListPopupDialog();
				}
				else
				{
					dcc.HideWindowListPopupDialog();
				}
			}
			*/
		}

		public event EventHandler WindowClosed;

		public bool FullScreen { get; set; }

		public SolutionObjectModel CurrentSolution { get; set; }

		#endregion
	}
}
