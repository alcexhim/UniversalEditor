﻿using System;

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
using System.Text;
using MBS.Framework.UserInterface.Controls.ListView;

namespace UniversalEditor.UserInterface
{
	public class MainWindow : MBS.Framework.UserInterface.MainWindow, IHostApplicationWindow
	{
		private DockingContainerControl dckContainer = null;

		private ErrorListPanel pnlErrorList = new ErrorListPanel();
		private SolutionExplorerPanel pnlSolutionExplorer = new SolutionExplorerPanel();
		private PropertyListPanel pnlPropertyList = new PropertyListPanel();
		private DocumentExplorerPanel pnlDocumentExplorer = new DocumentExplorerPanel();
		public DocumentExplorerPanel DocumentExplorerPanel { get { return pnlDocumentExplorer; } }

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
		
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			UpdateSuperDuperButtonBar();
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
			UpdateSuperDuperButtonBar(e.KeyAsModifier);
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			UpdateSuperDuperButtonBar(KeyboardModifierKey.None);
		}

		private void UpdateSuperDuperButtonBar(KeyboardModifierKey modifierKeys = KeyboardModifierKey.None)
		{
			for (int i = 0; i < SuperButtons.Length; i++)
			{
				SuperButtons[i].Text = ((KeyboardKey)((int)KeyboardKey.F1 + i)).ToString() + "    ";
				SuperButtons[i].SetExtraData<Command>("command", null);
			}
			for (int i = 0; i < Application.Contexts.Count; i++)
			{
				for (int j = 0; j < Application.Contexts[i].KeyBindings.Count; j++)
				{
					if (((int)Application.Contexts[i].KeyBindings[j].Key >= (int)KeyboardKey.F1 && (int)Application.Contexts[i].KeyBindings[j].Key <= (int)KeyboardKey.F12)
					&& Application.Contexts[i].KeyBindings[j].ModifierKeys == modifierKeys)
					{
						int q = (int)Application.Contexts[i].KeyBindings[j].Key - (int)KeyboardKey.F1;
						SuperButtons[q].Text = Application.Contexts[i].KeyBindings[j].Key.ToString() + "    " + Application.Contexts[i].KeyBindings[j].Command?.Title;
						SuperButtons[q].SetExtraData<Command>("command", Application.Contexts[i].KeyBindings[j].Command);
					}
				}
			}
		}

		private Button[] SuperButtons = new Button[]
		{
			new Button("F1    ", SuperButton_Click),
			new Button("F2    ", SuperButton_Click),
			new Button("F3    ", SuperButton_Click),
			new Button("F4    ", SuperButton_Click),
			new Button("F5    ", SuperButton_Click),
			new Button("F6    ", SuperButton_Click),
			new Button("F7    ", SuperButton_Click),
			new Button("F8    ", SuperButton_Click),
			new Button("F9    ", SuperButton_Click),
			new Button("F10   ", SuperButton_Click),
			new Button("F11   ", SuperButton_Click),
			new Button("F12   ", SuperButton_Click)
		};

		private static void SuperButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			Command cmd = btn.GetExtraData<Command>("command");
			if (cmd != null)
				Application.ExecuteCommand(cmd.ID);
		}

		private DefaultTreeModel tmToolbox = new DefaultTreeModel(new Type[] { typeof(string) });

		public MainWindow()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			this.IconName = "universal-editor";

			this.CommandDisplayMode = CommandDisplayMode.CommandBar;

			if (this.CommandDisplayMode == CommandDisplayMode.Ribbon || this.CommandDisplayMode == CommandDisplayMode.Both) {
				foreach (CommandBar cb in Application.CommandBars) {
					RibbonTab ribbonTabHome = LoadRibbonBar (cb);
					ribbonTabHome.Title = "Home";
					this.Ribbon.Tabs.Add (ribbonTabHome);
				}
			}
			dckContainer = new DockingContainerControl();
			dckContainer.SelectionChanged += dckContainer_SelectionChanged;
			Controls.Add (dckContainer, new BoxLayout.Constraints(true, true, 0, BoxLayout.PackType.Start));

			InitStartPage();

			ListViewControl lvToolbox = new ListViewControl();
			lvToolbox.RowActivated += LvToolbox_RowActivated;
			lvToolbox.Model = tmToolbox;
			lvToolbox.Columns.Add(new ListViewColumnText(tmToolbox.Columns[0], "Item"));
			lvToolbox.HeaderStyle = ColumnHeaderStyle.None;
			AddPanel("Toolbox", DockingItemPlacement.Left, lvToolbox);

			AddPanel("Document Explorer", DockingItemPlacement.Bottom, pnlDocumentExplorer);

			DockingContainer dcExplorerProperties = AddPanelContainer(DockingItemPlacement.Right, null);
			AddPanel("Solution Explorer", DockingItemPlacement.Top, pnlSolutionExplorer, dcExplorerProperties);
			AddPanel("Properties", DockingItemPlacement.Bottom, pnlPropertyList, dcExplorerProperties);

			AddPanel("Error List", DockingItemPlacement.Bottom, pnlErrorList);

			Container pnlButtons = new Container();
			pnlButtons.Layout = new BoxLayout(Orientation.Horizontal);
			for (int i = 0; i < SuperButtons.Length; i++)
			{
				pnlButtons.Controls.Add(SuperButtons[i], new BoxLayout.Constraints(true, true));
			}
			pnlButtons.Visible = false;
			Controls.Add(pnlButtons, new BoxLayout.Constraints(false, false, 0, BoxLayout.PackType.Start));

			this.Bounds = new Rectangle(0, 0, 600, 400);
			this.Size = new Dimension2D(800, 600);
			this.Text = Application.Title;

			Application.ContextAdded += Application_ContextChanged;
			Application.ContextRemoved += Application_ContextChanged;

			UpdateSuperDuperButtonBar();
		}

		void LvToolbox_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			Editor ed = GetCurrentEditor();
			if (ed != null)
			{
				ed.ActivateToolboxItem(e.Row.GetExtraData<ToolboxItem>("item"));
			}
		}


		void Application_ContextChanged(object sender, ContextChangedEventArgs e)
		{
			UpdateSuperDuperButtonBar();
		}


		#region Editor Page Events
		private void page_DocumentEdited(object sender, EventArgs e)
		{
			Pages.EditorPage page = (sender as Pages.EditorPage);
			DockingWindow di = dckContainer.Items[page] as DockingWindow;
			if (di == null) return;

			if (String.IsNullOrEmpty(page.Document.Title))
			{
				if (di.Name.StartsWith("<untitled", StringComparison.Ordinal))
				{
					di.Title = di.Name + " (*)";
				}
				else if (page.Document.Accessor != null)
				{
					di.Title = System.IO.Path.GetFileName(page.Document.Accessor.GetFileName()) + " (*)";
				}
				else
				{
					di.Title = System.IO.Path.GetFileName(di.Name) + " (*)";
				}
			}
			else
			{
				di.Title = System.IO.Path.GetFileName(page.Document.Title) + " (*)";
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

		public Document NewFile()
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
				if (template == null) return null;

				string filename = "<untitled{0}>";
				if (!String.IsNullOrEmpty(dlg.ProjectTitle))
				{
					filename = dlg.ProjectTitle;
				}

				Pages.EditorPage page = new Pages.EditorPage();
				page.DocumentEdited += page_DocumentEdited;
				page.Title = String.Format(filename, iUntitledDocCount);

				ObjectModel objm = template.ObjectModelReference.Create();
				if (objm == null)
				{
					MessageDialog.ShowDialog("Failed to create an ObjectModel for the type \"" + template.ObjectModelReference.TypeName + "\"", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
					return null;
				}

				page.Document = new Document(objm, null, null);
				page.Document.Title = String.Format(filename, iUntitledDocCount);

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
				InitDocTab(String.Format(filename, iUntitledDocCount), page.Title, page);
				return page.Document;
			}
			return null;
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
				if (_prevEditor != null)
					Application.Contexts.Remove(_prevEditor.Context);

				if (editor != null)
				{
					Application.Contexts.Add(editor.Context);

					// initialize toolbox items
					EditorReference er = editor.MakeReference();
					for (int i = 0; i < er.Toolbox.Items.Count; i++)
					{
						TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[] { new TreeModelRowColumn(tmToolbox.Columns[0], er.Toolbox.Items[i].Name) });
						row.SetExtraData<ToolboxItem>("item", er.Toolbox.Items[i]);
						tmToolbox.Rows.Add(row);
					}
					DocumentFileName = dckContainer.CurrentItem.Name;
				}
				else
				{
					DocumentFileName = null;
					tmToolbox.Rows.Clear();
				}
				pnlDocumentExplorer.CurrentEditor = editor;
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

		private DockingContainer AddPanelContainer(DockingItemPlacement placement, DockingContainer parent = null)
		{
			DockingContainer dc = new DockingContainer();
			dc.Placement = placement;
			if (parent != null)
			{
				parent.Items.Add(dc);
			}
			else
			{
				dckContainer.Items.Add(dc);
			}
			return dc;
		}
		private void AddPanel(string title, DockingItemPlacement placement, Control control = null, DockingContainer parent = null)
		{
			if (control == null)
			{
				Label lblErrorList = new Label(title);
				control = lblErrorList;
			}

			DockingWindow dw = new DockingWindow(title.Replace("_", "__"), control);
			dw.Placement = placement;

			if (parent != null)
			{
				parent.Items.Add(dw);
			}
			else
			{
				dckContainer.Items.Add(dw);
			}
		}

		private void InitEditorPage(Document doc)
		{
			if (doc == null) return;

			long initpos = 0;
			bool first = true;

			bool loaded = false;
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
							continue;
						}

						ObjectModelReference omr = omrs [0];
						ObjectModel om = omr.Create ();

						doc.DataFormat = dfr.Create ();
						doc.ObjectModel = om;

						try
						{
							doc.Accessor.Open();
							if (first)
							{
								initpos = doc.Accessor.Position;
								first = false;
							}
							else
							{
								doc.Accessor.Position = initpos;
							}

							doc.Load ();
							doc.IsSaved = true;
							loaded = true;
						}
						catch (InvalidDataFormatException ex)
						{
							doc.Accessor.Close ();
							continue;
						}
						catch (UnauthorizedAccessException ex)
						{
							if (doc.Accessor is FileAccessor)
							{
								(doc.Accessor as FileAccessor).AllowWrite = false;
								doc.Load();
								doc.IsSaved = true;
								loaded = true;
							}
						}

						found = true;
						break;
					}
					if (!found) {
						OpenDefaultEditor(doc);
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
					while (!loaded)
					{
						if (doc.Accessor != null)
						{
							try
							{
								doc.Accessor.Open();
								doc.Load();
								doc.IsSaved = true;
								loaded = true;
							}
							catch (ObjectModelNotSupportedException ex)
							{
								// we're catching this one because there's nothing anyone (not even the developer) can do about it if the DF throws ObjectModelNotSupported
								DialogResult result = MessageDialog.ShowDialog(String.Format("The object model you specified is not supported by the selected DataFormat.\r\n\r\n{0}", ex.Message), "Error", MessageDialogButtons.RetryCancel, MessageDialogIcon.Error);
								if (result == DialogResult.Retry)
								{
									DocumentPropertiesDialog dlg = new DocumentPropertiesDialog();
									dlg.DataFormat = doc.DataFormat;
									dlg.ObjectModel = doc.ObjectModel;
									dlg.Accessor = doc.Accessor;
									if (dlg.ShowDialog() == DialogResult.OK)
									{
										doc.DataFormat = dlg.DataFormat;
										doc.ObjectModel = dlg.ObjectModel;
										doc.Accessor = dlg.Accessor;
									}

									// try loading it again
									continue;
								}
								return;
							}
							catch (InvalidDataFormatException ex)
							{
								doc.Accessor.Close();

								// we're catching this one because there's nothing anyone (not even the developer) can do about it if the DF throws ObjectModelNotSupported
								// TODO: For DataFormats that support it (i.e. Layout-based) we should be able to "debug" the DataFormat to find out exactly where it failed
								DialogResult result = MessageDialog.ShowDialog(String.Format("The data format you specified could not load the file.\r\n\r\n{0}", ex.Message), "Error", MessageDialogButtons.RetryCancel, MessageDialogIcon.Error);
								if (result == DialogResult.Retry)
								{
									DocumentPropertiesDialog dlg = new DocumentPropertiesDialog();
									dlg.DataFormat = doc.DataFormat;
									dlg.ObjectModel = doc.ObjectModel;
									dlg.Accessor = doc.Accessor;

									if (dlg.ShowDialog() == DialogResult.OK)
									{
										doc.DataFormat = dlg.DataFormat;
										doc.ObjectModel = dlg.ObjectModel;
										doc.Accessor = dlg.Accessor;
									}

									// try loading it again
									continue;
								}
								return;
							}
#if !DEBUG
						catch (Exception ex)
						{
							if (!System.Diagnostics.Debugger.IsAttached)
							{
								MessageDialog.ShowDialog("could not load file: " + ex.GetType().Name + "\r\n" + ex.Message, "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
								return;
							}
							else
							{
								// fk it
								throw ex;
							}
						}
#endif
						}
						else
						{
							loaded = true;
						}
					}

					EditorPage page = new EditorPage();
					page.Document = doc;
					page.DocumentEdited += page_DocumentEdited;

					string filename = doc.Accessor?.GetFileName();
					if (filename == null) filename = doc.Title;
					InitDocTab(filename, doc.Title, page);
				}
				else
				{
					Console.Error.WriteLine("Editor not found for object model " + doc.ObjectModel.MakeReference().Title + " ; using default editor");
					OpenDefaultEditor(doc);
				}
			}
			else
			{
				Console.Error.WriteLine("ObjectModel not specified for accessor " + doc.Accessor.ToString() + " ; using default editor");
				OpenDefaultEditor(doc);
			}
		}

		public bool ConfirmExit(EditorPage page = null)
		{
			EditorPage[] pages = null;

			if (page != null)
			{
				pages = new EditorPage[] { page };
			}
			else
			{
				pages = GetEditorPages();
			}

			if (pages.Length == 0)
				return true;

			SaveConfirmationDialog dlg = new SaveConfirmationDialog();
			List<int> indices = new List<int>();
			for (int i = 0; i < pages.Length; i++)
			{
				if (pages[i].Document == null)
					continue;

				if (!pages[i].Document.IsChanged)
					continue;

				string filename = null;
				if (pages[i].Document.Accessor != null)
				{
					filename = pages[i].Document.Accessor.GetFileName();
				}
				else
				{
					filename = pages[i].Title;
				}
				dlg.FileNames.Add(filename);
				indices.Add(i);
			}

			if (dlg.FileNames.Count == 0)
			{
				// nothing to save, so we'll just say we're good
				return true;
			}

			DialogResult result = dlg.ShowDialog();
			switch (result)
			{
				case DialogResult.Yes:
				{
					for (int i = 0; i < dlg.FileNames.Count; i++)
					{
						if (dlg.FileNames[i].Selected)
						{
							if (!SaveFile(pages[indices[i]].Document))
								return false;
						}
					}
					break;
				}
				case DialogResult.No:
				{
					// we don't save
					break;
				}
				case DialogResult.None:
				case DialogResult.Cancel:
				{
					// prevent the window from closing
					// for some reason GTK gives us 'None' when we hit Escape ... that should be interpreted as 'Cancel'
					return false;
				}
			}
			return true;
		}

		private bool _UserClosed = false;
		protected override void OnClosing(WindowClosingEventArgs e)
		{
			base.OnClosing(e);

			if (e.CloseReason == WindowCloseReason.UserClosing || e.CloseReason == WindowCloseReason.ApplicationStop)
			{
				if (e.CloseReason == WindowCloseReason.UserClosing)
				{
					_UserClosed = true;
				}
				else if (e.CloseReason == WindowCloseReason.ApplicationStop && _UserClosed)
				{
					return;
				}
				if (!ConfirmExit())
				{
					_UserClosed = false;
					e.Cancel = true;
				}
			}
		}

		private void OpenDefaultEditor(Document doc)
		{
			EditorPage page = new EditorPage();
			page.Document = doc;
			page.DocumentEdited += page_DocumentEdited;

			InitDocTab(doc.Accessor.GetFileName(), System.IO.Path.GetFileName(doc.Accessor.GetFileName()), page);
		}

		[ContainerLayout("~/Panels/StartPage.glade")]
		private class StartPageDialog : Dialog
		{
		}

		private void InitStartPage()
		{
			StartPagePanel lblStartPage = new StartPagePanel();
			InitDocTab("Start Page", "Start Page", lblStartPage);
		}

		private int documentWindowCount = 0;
		private void InitDocTab(string name, string title, Control content)
		{
			DockingWindow item = new DockingWindow(name, title.Replace("_", "__"), content);
			item.Placement = DockingItemPlacement.Center;
			dckContainer.Items.Add(item);

			dckContainer.CurrentItem = item;

			documentWindowCount++;

			// HACK: until we can properly figure out when a docking container has its current window changed
			dckContainer_SelectionChanged(this, EventArgs.Empty);
		}

		protected override void OnClosed(EventArgs e)
		{
			Engine.CurrentEngine.Windows.Remove(this);
			if (Engine.CurrentEngine.Windows.Count <= 0)
			{
				MBS.Framework.UserInterface.Application.Stop();
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			this.RegisterDropTarget(new DragDropTarget[]
			{
				new DragDropTarget(DragDropTargetTypes.FileList, DragDropTargetFlags.SameApplication | DragDropTargetFlags.OtherApplication, 0x1)
			}, DragDropEffect.Copy, MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey.None);

			Engine.CurrentEngine.Windows.Add(this);
			Engine.CurrentEngine.LastWindow = this;
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			Engine.CurrentEngine.LastWindow = this;
		}

		#region IHostApplicationWindow implementation
		public void OpenFile()
		{
			/*
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
			*/
			using (DocumentPropertiesDialog dlg = new DocumentPropertiesDialog())
			{
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					Document doc = new Document(dlg.ObjectModel, dlg.DataFormat, dlg.Accessor);
					OpenFile(doc);
				}
			}
		}

		public void OpenFile(params string[] fileNames)
		{
			Document[] documents = new Document[fileNames.Length];

			List<string> failedFiles = new List<string>();
			for (int i = 0; i < documents.Length; i++)
			{
				AccessorReference[] accs = UniversalEditor.Common.Reflection.GetAvailableAccessors(fileNames[i]);
				if (accs.Length > 0)
				{
					Accessor fa = accs[0].Create();
					fa.OriginalUri = new Uri(fileNames[i]);
					documents[i] = new Document(fa);
				}
				else if (System.IO.File.Exists(fileNames[i]))
				{
					FileAccessor fa = new FileAccessor(fileNames[i], false, false, false);
					documents[i] = new Document(fa);
				}
				else
				{
					failedFiles.Add(fileNames[i]);
				}
			}
			if (failedFiles.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				if (failedFiles.Count == 1)
				{
					sb.Append(String.Format("The file '{0}' could not be found.", failedFiles[0]));
				}
				else
				{
					sb.AppendLine("The following files could not be found:");
					sb.AppendLine();
					for (int i = 0; i < failedFiles.Count; i++)
					{
						sb.AppendLine(failedFiles[i]);
					}
				}

				MessageDialog.ShowDialog(sb.ToString(), "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
			}
			OpenFile(documents);
		}

		public void OpenFile(params Document[] documents)
		{
			foreach (Document doc in documents)
			{
				try
				{
					InitEditorPage(doc);
				}
				catch (System.UnauthorizedAccessException ex)
				{
					MessageDialog.ShowDialog(ex.Message, "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				}
			}
			Present(DateTime.Now);
		}

		public ProjectObjectModel ShowOpenProjectDialog()
		{
			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;

			Association[] projectAssocs = Association.FromObjectModelOrDataFormat((new ProjectObjectModel()).MakeReference());
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			dlg.AddFileNameFilterFromAssociations("Project files", projectAssocs);

			dlg.Text = "Open Project";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ProjectObjectModel proj = new ProjectObjectModel();

				FileAccessor fa = new FileAccessor(dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1]);
				Association[] assocs = Association.FromAccessor(fa);
				DataFormat df = assocs[0].DataFormats[0].Create();

				Document.Load(proj, df, fa);
				return proj;
			}
			return null;
		}

		public void OpenProject(bool combineObjects = false)
		{
			FileDialog dlg = new FileDialog();

			Association[] projectAssocs = Association.FromObjectModelOrDataFormat((new ProjectObjectModel()).MakeReference());
			Association[] solutionAssocs = Association.FromObjectModelOrDataFormat((new SolutionObjectModel()).MakeReference());

			System.Text.StringBuilder sbProject = new System.Text.StringBuilder();
			foreach (Association projectAssoc in projectAssocs)
			{
				for (int i = 0; i < projectAssoc.Filters.Count; i++)
				{
					for (int j = 0; j < projectAssoc.Filters[i].FileNameFilters.Count; j++)
					{
						sbProject.Append(projectAssoc.Filters[i].FileNameFilters[j]);
						if (j < projectAssoc.Filters[i].FileNameFilters.Count - 1)
							sbProject.Append("; ");
					}

					if (i < projectAssoc.Filters.Count - 1)
						sbProject.Append("; ");
				}
			}
			System.Text.StringBuilder sbSolution = new System.Text.StringBuilder();
			foreach (Association solutionAssoc in solutionAssocs)
			{
				for (int i = 0; i < solutionAssoc.Filters.Count; i++)
				{
					for (int j = 0; j < solutionAssoc.Filters[i].FileNameFilters.Count; j++)
					{
						sbSolution.Append(solutionAssoc.Filters[i].FileNameFilters[j]);
						if (j < solutionAssoc.Filters[i].FileNameFilters.Count - 1)
							sbSolution.Append("; ");
					}

					if (i < solutionAssoc.Filters.Count - 1)
						sbSolution.Append("; ");
				}
			}
			dlg.FileNameFilters.Add("Project or solution files", sbProject.ToString() + ';' + sbSolution.ToString());
			dlg.FileNameFilters.Add("Project files", sbProject.ToString());
			dlg.FileNameFilters.Add("Solution files", sbSolution.ToString());

			dlg.Text = "Open Project or Solution";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				OpenProject(dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1], combineObjects);
			}
		}

		public void OpenProject(string FileName, bool combineObjects = false)
		{
			if (!combineObjects)
				CurrentSolution = new SolutionObjectModel();

			FileAccessor fa = new FileAccessor(FileName);
			Association[] assocs = Association.FromAccessor(fa);
			DataFormat df = assocs[0].DataFormats[0].Create();

			Document.Load(_CurrentSolution, df, fa);
			CurrentSolution = _CurrentSolution; // to reset the UI
		}

		public bool SaveFile()
		{
			Pages.EditorPage currentEditorPage = GetCurrentEditorPage();
			if (currentEditorPage != null)
			{
				if (!GetCurrentEditor().NotifySaving())
					return false;

				return SaveFile(currentEditorPage.Document);
			}
			return false;
		}

		public bool SaveFile(Document document)
		{
			if (document.IsSaved)
			{
				if (document.InputAccessor != null && document.InputAccessor.IsOpen)
					document.InputAccessor.Close();

				if (document.OutputAccessor is FileAccessor)
				{
					// FIXME: ewww
					(document.OutputAccessor as FileAccessor).AllowWrite = true;
					(document.OutputAccessor as FileAccessor).ForceOverwrite = true;
				}
				document.OutputAccessor.Open();
				document.Save();
				document.OutputAccessor.Close();

				DockingWindow di = dckContainer.Items[GetCurrentEditorPage()] as DockingWindow;
				if (di != null)
				{
					di.Name = document.OutputAccessor.GetFileName();
					di.Title = System.IO.Path.GetFileName(document.OutputAccessor.GetFileName());
				}

				GetCurrentEditor().Document = document;
				return true;
			}
			else
			{
				return SaveFileAs(document);
			}
		}
		public bool SaveFileAs(Document document)
		{
			using (DocumentPropertiesDialog dlg = new DocumentPropertiesDialog())
			{
				dlg.Mode = DocumentPropertiesDialogMode.Save;
				dlg.DataFormat = document.DataFormat;
				dlg.ObjectModel = document.ObjectModel;
				dlg.Accessor = document.Accessor;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					DataFormat df = dlg.DataFormat;
					if (df == null && document.ObjectModel is BinaryObjectModel)
					{
						df = new BinaryDataFormat();
					}
					bool result = SaveFileAs(dlg.Accessor, df, document.ObjectModel);
					if (!result)
						return false;

					document.OutputAccessor = dlg.Accessor;
					document.OutputDataFormat = df;
					document.IsSaved = true;
					document.IsChanged = false;

					return result;
				}
				return false;
			}
		}

		public bool SaveFileAs()
		{
			Editor currentEditor = GetCurrentEditor();
			if (currentEditor != null)
			{
				using (DocumentPropertiesDialog dlg = new DocumentPropertiesDialog ())
				{
					dlg.Mode = DocumentPropertiesDialogMode.Save;
					dlg.ObjectModel = GetCurrentEditorPage().Document.ObjectModel;
					dlg.DataFormat = GetCurrentEditorPage().Document.DataFormat;
					dlg.Accessor = GetCurrentEditorPage().Document.Accessor;

					if (dlg.ShowDialog () == DialogResult.OK)
					{
						DataFormat df = dlg.DataFormat;
						if (df == null && currentEditor.ObjectModel is BinaryObjectModel)
						{
							df = new BinaryDataFormat();
						}

						return SaveFileAs(dlg.Accessor, df, currentEditor.ObjectModel);
					}
				}
			}
			return false;
		}

		public bool SaveFileAs(Accessor accessor, DataFormat df, ObjectModel om)
		{
			EditorPage page = GetCurrentEditorPage();
			if (page == null) return false;

			if (page.Document == null)
			{
				page.Document = new Document(om, df, accessor);
			}
			else
			{
				page.Document.ObjectModel = om;
				page.Document.DataFormat = df;
				page.Document.Accessor = accessor;
			}
			page.Document.Save();
			GetCurrentEditor().Document = page.Document;

			DockingWindow di = dckContainer.Items[page] as DockingWindow;
			if (di != null)
			{
				di.Name = accessor.GetFileName();
				di.Title = System.IO.Path.GetFileName(accessor.GetFileName());
			}
			return true;
		}
		public bool SaveFileAs(Accessor accessor, DataFormat df)
		{
			return SaveFileAs(accessor, df, GetCurrentEditor()?.ObjectModel);
		}

		public void SaveProject()
		{
			if (_CurrentSolutionDocument != null && _CurrentSolutionDocument.IsSaved)
			{
				MessageDialog.ShowDialog("TODO: overwrite current solution in-place", "Implement this!", MessageDialogButtons.OK);
			}
			else
			{
				SaveProjectAs();
			}
		}

		public void SaveProjectAs()
		{
			if (CurrentSolution == null)
				return;

			Association[] assocs = Association.FromObjectModelOrDataFormat(CurrentSolution.MakeReference());

			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Save;

			System.Text.StringBuilder sbFilter = new System.Text.StringBuilder();

			foreach (Association assoc in assocs)
			{
				foreach (DataFormatFilter filter in assoc.Filters)
				{
					sbFilter.Clear();
					for (int i = 0; i < filter.FileNameFilters.Count; i++)
					{
						sbFilter.Append(filter.FileNameFilters[i]);
						if (i < filter.FileNameFilters.Count - 1)
							sbFilter.Append("; ");
					}
					dlg.FileNameFilters.Add(filter.Title, sbFilter.ToString());
				}
			}

			DataFormat df = assocs[0].DataFormats[0].Create();

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				_CurrentSolutionDocument = new Document(CurrentSolution, df, new FileAccessor(dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1], true, true));
				_CurrentSolutionDocument.Accessor.Open();
				_CurrentSolutionDocument.Save();
				_CurrentSolutionDocument.Accessor.Close();
			}
		}

		public void SaveProjectAs(string FileName, DataFormat df)
		{
			throw new NotImplementedException();
		}

		public void SaveAll()
		{
			foreach (DockingItem item in dckContainer.Items)
			{
				if (!(item is DockingWindow)) continue;
				if ((item as DockingWindow).ChildControl is EditorPage)
				{
					SaveFile(((item as DockingWindow).ChildControl as EditorPage).Document);
				}
			}
		}

		public void SwitchPerspective(int index)
		{
			throw new NotImplementedException();
		}

		private System.Collections.Generic.List<Window> Windows = new System.Collections.Generic.List<Window>();
		public void CloseFile(DockingWindow dw = null)
		{
			if (dw == null)
				dw = (dckContainer.CurrentItem as DockingWindow);

			EditorPage ep = (dw?.ChildControl as EditorPage);
			if (ep != null && !ConfirmExit(ep))
			{
				return;
			}

			dckContainer.Items.Remove(dw);
			documentWindowCount--;

			if (documentWindowCount == 0)
			{
				CloseWindow ();
			}
		}

		public void CloseProject()
		{
			CurrentSolution = null;
		}

		public void CloseWindow()
		{
			this.Destroy ();
		}

		public void PrintDocument()
		{
			Editor editor = GetCurrentEditor ();
			if (editor != null)
			{
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
				else
				{
					MessageDialog.ShowDialog(String.Format("No print handlers are associated with the ObjectModel.\r\n\r\n{0}", editor.ObjectModel?.GetType()?.FullName ?? "(null)"), "Print Document", MessageDialogButtons.OK, MessageDialogIcon.Error);
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

		/// <summary>
		/// Gets the <see cref="Page" />s (center-docked <see cref="DockingWindow" />s) contained within the specified <see cref="IDockingItemContainer" />.
		/// </summary>
		/// <returns>The <see cref="Page" />s contained within the specified <see cref="IDockingItemContainer" />.</returns>
		/// <param name="parent">The <see cref="IDockingItemContainer" /> in which to search for <see cref="Page" />s.</param>
		private Page[] GetPages(IDockingItemContainer parent)
		{
			List<Page> list = new List<Page>();
			for (int i = 0; i < parent.Items.Count; i++)
			{
				if (parent.Items[i] is DockingWindow)
				{
					DockingWindow dw = (parent.Items[i] as DockingWindow);
					if (dw.ChildControl is Page)
					{
						list.Add(dw.ChildControl as Page);
					}
				}
				else if (parent.Items[i] is DockingContainer)
				{
					Page[] pages = GetPages(parent.Items[i] as DockingContainer);
					list.AddRange(pages);
				}
			}
			return list.ToArray();
		}
		/// <summary>
		/// Gets the <see cref="Page" />s (center-docked <see cref="DockingWindow" />s) contained within all <see cref="DockingContainer" />s in this
		/// <see cref="MainWindow" />.
		/// </summary>
		/// <returns>The <see cref="Page" />s contained within all <see cref="DockingContainer" />s in this <see cref="MainWindow" />.</returns>
		public Page[] GetPages()
		{
			return GetPages(dckContainer);
		}

		public Pages.EditorPage GetCurrentEditorPage()
		{
			DockingWindow curitem = dckContainer.CurrentItem as DockingWindow;
			if (curitem == null) return null;

			Pages.EditorPage editorPage = (curitem.ChildControl as Pages.EditorPage);
			if (editorPage == null) return null;

			return editorPage;
		}
		public EditorPage[] GetEditorPages()
		{
			List<EditorPage> list = new List<EditorPage>();
			for (int i = 0; i < dckContainer.Items.Count; i++)
			{
				DockingWindow dw = dckContainer.Items[i] as DockingWindow;
				if (dw == null) continue;

				if (dw.ChildControl is EditorPage)
				{
					list.Add(dw.ChildControl as EditorPage);
				}
			}
			return list.ToArray();
		}

		public bool ShowOptionsDialog()
		{
			return Application.ShowSettingsDialog();
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

		public bool FullScreen { get; set; }

		private SolutionObjectModel _CurrentSolution = null;
		private Document _CurrentSolutionDocument = null;
		public SolutionObjectModel CurrentSolution
		{
			get { return _CurrentSolution; }
			set
			{
				bool changed = (_CurrentSolution != value);
				_CurrentSolution = value;

				if (value == null || changed)
					_CurrentSolutionDocument = null;

				pnlSolutionExplorer.Solution = value;
			}
		}
		public ProjectObjectModel CurrentProject
		{
			get
			{
				if (pnlSolutionExplorer.Project != null)
					return pnlSolutionExplorer.Project;
				return null;
			}
			set
			{
				pnlSolutionExplorer.Project = value;
			}
		}

		#endregion

		public void ShowDocumentPropertiesDialog()
		{
			Editor editor = GetCurrentEditor();
			if (editor != null)
				editor.ShowDocumentPropertiesDialog();
		}
	}
}
	