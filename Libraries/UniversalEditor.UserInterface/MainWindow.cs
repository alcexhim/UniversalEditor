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
using System.Text;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework;
using UniversalEditor.UserInterface.Controls;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.UserInterface
{
	public class MainWindow : MBS.Framework.UserInterface.MainWindow, IHostApplicationWindow
	{
		private DockingContainerControl dckContainer = null;

		private ErrorListPanel pnlErrorList = new ErrorListPanel();
		private SolutionExplorerPanel pnlSolutionExplorer = new SolutionExplorerPanel();
		internal PropertyListPanel pnlPropertyList = new PropertyListPanel();
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
			base.OnKeyDown(e);

			/*
			// we have to process key shortcuts manually if we do not use a traditional menu bar
			foreach (Command cmd in ((UIApplication)Application.Instance).Commands)
			{
				if (!cmd.Enabled)
					continue;

				if (cmd is UICommand)
				{
					if (((UICommand)cmd).Shortcut == null) continue;

					if (((UICommand)cmd).Shortcut.Key == e.Key && ((UICommand)cmd).Shortcut.ModifierKeys == e.ModifierKeys)
					{
						((UIApplication)Application.Instance).ExecuteCommand(cmd.ID);
						e.Cancel = true;
						break;
					}
				}
			}
			*/
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
			for (int i = 0; i < Application.Instance.Contexts.Count; i++)
			{
				if (Application.Instance.Contexts[i] is UIContext)
				{
					for (int j = 0; j < ((UIContext)Application.Instance.Contexts[i]).KeyBindings.Count; j++)
					{
						KeyBinding keyb = ((UIContext)Application.Instance.Contexts[i]).KeyBindings[j];

						if (((int)(keyb.Key) >= (int)KeyboardKey.F1 && (int)(keyb).Key <= (int)KeyboardKey.F12)
						&& keyb.ModifierKeys == modifierKeys)
						{
							int q = (int)keyb.Key - (int)KeyboardKey.F1;
							SuperButtons[q].Text = keyb.Key.ToString() + "    " + keyb.Command?.Title;
							SuperButtons[q].SetExtraData<Command>("command", keyb.Command);
						}
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
				((UIApplication)Application.Instance).ExecuteCommand(cmd.ID);
		}

		private DefaultTreeModel tmToolbox = new DefaultTreeModel(new Type[] { typeof(string) });

		public MainWindow()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			this.IconName = "universal-editor";
			LogoutInhibitor = new Inhibitor(InhibitorType.SystemLogout, "There are unsaved documents", this);

			this.CommandDisplayMode = CommandDisplayMode.CommandBar;

			if (this.CommandDisplayMode == CommandDisplayMode.Ribbon || this.CommandDisplayMode == CommandDisplayMode.Both) {
				foreach (CommandBar cb in ((UIApplication)Application.Instance).CommandBars) {
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
			lvToolbox.Columns.Add(new ListViewColumn("Item", new CellRenderer[] { new CellRendererText(tmToolbox.Columns[0]) }));
			lvToolbox.HeaderStyle = ColumnHeaderStyle.None;
			AddPanel("Toolbox", DockingItemPlacement.Left, lvToolbox);

			AddPanel("Document Explorer", DockingItemPlacement.Bottom, pnlDocumentExplorer);

			DockingContainer dcExplorerProperties = null; // AddPanelContainer(DockingItemPlacement.Right, null);
			AddPanel("Solution Explorer", DockingItemPlacement.Left, pnlSolutionExplorer, dcExplorerProperties);
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
			this.Text = Application.Instance.Title;

			Application.Instance.ContextAdded += Application_ContextChanged;
			Application.Instance.ContextRemoved += Application_ContextChanged;

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
				di.Title = String.Format("{0} (*)", page.Document.Title);
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

				if (template.ObjectModel != null)
				{
					template.ObjectModel.CopyTo(objm);
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

		public event EventHandler<EditorChangingEventArgs> EditorChanging;
		protected virtual void OnEditorChanging(EditorChangingEventArgs e)
		{
			EditorChanging?.Invoke(this, e);
		}
		public event EventHandler<EditorChangedEventArgs> EditorChanged;
		protected virtual void OnEditorChanged(EditorChangedEventArgs e)
		{
			EditorChanged?.Invoke(this, e);
		}

		private bool _OnEditorChanging(EditorChangingEventArgs e)
		{
			EditorChangingEventArgs ee = new EditorChangingEventArgs(this, e.PreviousEditor, e.CurrentEditor);
			OnEditorChanging(ee);
			if (ee.Cancel)
				return false;

			((EditorApplication)Application.Instance).OnEditorChanging(ee);
			if (ee.Cancel)
				return false;

			return true;
		}
		private void _OnEditorChanged(EditorChangedEventArgs e)
		{
			if (e.CurrentEditor != null)
			{
				// initialize toolbox items
				EditorReference er = e.CurrentEditor.MakeReference();
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
			pnlDocumentExplorer.CurrentEditor = e.CurrentEditor;

			UpdateMenuItems();
			UpdatePropertyPanel();

			// forward to window event handler
			OnEditorChanged(e);

			// forward to application event handler
			((EditorApplication)Application.Instance).OnEditorChanged(e);
		}

		private void UpdatePropertyPanel()
		{
			pnlPropertyList.Objects.Clear();

			Editor editor = GetCurrentEditor();
			if (editor == null) return;

			foreach (PropertyPanelObject obj in editor.PropertiesPanel.Objects)
			{
				pnlPropertyList.Objects.Add(obj);
			}

			pnlPropertyList.cboObject.Visible = editor.PropertiesPanel.ShowObjectSelector;
		}

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
				if (!_OnEditorChanging(new EditorChangingEventArgs(this, _prevEditor, editor)))
				{
					// FIXME: reset to previous Editor if possible
					return;
				}

				if (_prevEditor != null)
				{
					Application.Instance.Contexts.Remove(_prevEditor.Context);
				}
				if (editor != null)
				{
					Application.Instance.Contexts.Add(editor.Context);
				}
				_OnEditorChanged(new EditorChangedEventArgs(this, _prevEditor, editor));
			}
			_prevEditor = editor;
		}

		private void UpdateMenuItems()
		{
			Editor editor = GetCurrentEditor();
			bool hasEditor = (editor != null);
			bool hasProject = CurrentProject != null;

			Page pg = GetCurrentPage();
			KeyValuePair<string, object>[] kvps = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Document.Title", pg?.Title),
				new KeyValuePair<string, object>("Application.Title", Application.Instance.Title)
			};

			Application.Instance.Commands["FileSaveDocument"].Enabled = hasEditor;
			Application.Instance.Commands["FileSaveDocumentAs"].Enabled = hasEditor;
			Application.Instance.Commands["FileSaveProject"].Enabled = hasProject;
			Application.Instance.Commands["FileSaveProjectAs"].Enabled = hasProject;
			Application.Instance.Commands["FilePrint"].Enabled = hasEditor;
			Application.Instance.Commands["FileCloseDocument"].Enabled = hasEditor;
			Application.Instance.Commands["FileCloseProject"].Enabled = hasProject;

			Application.Instance.Commands["ProjectAddNew"].Enabled = hasProject;
			Application.Instance.Commands["ProjectAddExisting"].Enabled = hasProject;
			Application.Instance.Commands["ProjectExclude"].Enabled = hasProject;
			Application.Instance.Commands["ProjectShowAllFiles"].Enabled = hasProject;
			Application.Instance.Commands["ProjectProperties"].Enabled = hasProject;

			Application.Instance.Commands["BookmarksAddAll"].Enabled = GetEditorPages().Length > 0;

			if (pg != null)
			{
				string fmt = "$(Document.Title) - $(Application.Title)"; // FIXME: replace with call to get main window title with document
				Text = fmt.ReplaceVariables(kvps);
			}
			else
			{
				string fmt = "$(Application.Title)"; // FIXME: replace with call to get main window title without document
				Text = fmt.ReplaceVariables(kvps);
			}

			if (editor != null)
			{
				Application.Instance.Commands["FileProperties"].Enabled = editor.Selections.Count > 0 || editor.HasDocumentProperties;

				Application.Instance.Commands["EditUndo"].Enabled = editor.UndoItemCount > 0;
				Application.Instance.Commands["EditRedo"].Enabled = editor.RedoItemCount > 0;

				Application.Instance.Commands["EditCut"].Enabled = editor.Selections.Count > 0;
				Application.Instance.Commands["EditCopy"].Enabled = editor.Selections.Count > 0;
				Application.Instance.Commands["EditPaste"].Enabled = true; // TODO: figure out whether the clipboard has a supported format
				Application.Instance.Commands["EditDelete"].Enabled = editor.Selections.Count > 0;

				Application.Instance.Commands["EditSelectAll"].Enabled = true;
				Application.Instance.Commands["EditInvertSelection"].Enabled = editor.Selections.Count > 0;

				Application.Instance.Commands["EditFindReplace"].Enabled = true;
				Application.Instance.Commands["EditBatchFindReplace"].Enabled = true;

				Application.Instance.Commands["EditGoTo"].Enabled = true;

				Application.Instance.Commands["BookmarksAdd"].Enabled = true;
			}
			else
			{
				Application.Instance.Commands["FileProperties"].Enabled = false;

				Application.Instance.Commands["EditUndo"].Enabled = false;
				Application.Instance.Commands["EditRedo"].Enabled = false;

				Application.Instance.Commands["EditCut"].Enabled = false;
				Application.Instance.Commands["EditCopy"].Enabled = false;
				Application.Instance.Commands["EditPaste"].Enabled = false;
				Application.Instance.Commands["EditDelete"].Enabled = false;

				Application.Instance.Commands["EditSelectAll"].Enabled = false;
				Application.Instance.Commands["EditInvertSelection"].Enabled = false;

				Application.Instance.Commands["EditFindReplace"].Enabled = false;
				Application.Instance.Commands["EditBatchFindReplace"].Enabled = false;

				Application.Instance.Commands["EditGoTo"].Enabled = false;

				Application.Instance.Commands["BookmarksAdd"].Enabled = false;
			}

			foreach (UserInterfacePlugin pl in UserInterfacePlugin.Get())
			{
				pl.UpdateMenuItems();
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

				// go through and update all referenced variables
				foreach (ProjectFile pf in project.FileSystem.Files)
				{
					pf.DestinationFileName = pf.DestinationFileName.Replace("$(Project.Title)", project.Title);
				}
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
			if (doc.DataFormat == null && doc.Accessor != null)
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

						DataFormat df = dfr.Create ();
						doc.DataFormat = df;
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

							while (true)
							{
								doc.Accessor.Position = initpos;
								try
								{
									doc.Load();
									doc.IsSaved = true;
									loaded = true;
									break;
								}
								catch (ArgumentException ex)
								{
									if (dfr.ImportOptions != null)
									{
										bool _break = false;
										foreach (SettingsGroup sg in dfr.ImportOptions.SettingsGroups)
										{
											foreach (Setting s in sg.Settings)
											{
												if (s.Required)
												{
													if (s.GetValue() == null)
													{
														_break = true;
														break;
													}
													else
													{
														if (((UIApplication)Application.Instance).GetSetting<bool>(SettingsGuids.Import.RememberLastImportSettingsValues))
														{
															// cool new feature! Application > Import > Remember last-used import settings
															df.GetType().GetProperty(s.Name).SetValue(df, s.GetValue(), null);
														}
														else
														{
															s.DefaultValue = s.GetValue();
															_break = true;
														}
														break;
													}
												}
											}
											if (_break)
												break;
										}

										if (_break)
										{
											if (!((EditorApplication)Application.Instance).ShowCustomOptionDialog(ref df, CustomOptionDialogType.Import))
												break;
										}
									}
									break;
								}
							}
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
						doc.Accessor.Position = initpos;
						break;
					}
					if (!found) {
						OpenDefaultEditor(doc);
						return;
					}
				}
			}

			// OKAY WHY THE **** ARE WE OPENING THE SAME FILE TWICE???

			if (doc.ObjectModel != null && doc.Accessor != null)
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
			else if (doc.ObjectModel == null)
			{
				Console.Error.WriteLine("ObjectModel not specified for accessor " + doc.Accessor?.ToString() + " ; using default editor");
				OpenDefaultEditor(doc);
			}
			else
			{
				EditorPage page = new EditorPage();
				page.Document = doc;
				page.DocumentEdited += page_DocumentEdited;

				string filename = doc.Accessor?.GetFileName();
				if (filename == null) filename = doc.Title;
				InitDocTab(filename, doc.Title, page);
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
			StartPage lblStartPage = new StartPage();
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
			((EditorApplication)Application.Instance).Windows.Remove(this);
			if (((EditorApplication)Application.Instance).Windows.Count <= 0)
			{
				Application.Instance.Stop();
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			this.RegisterDropTarget(new DragDropTarget[]
			{
				new DragDropTarget(DragDropTargetTypes.FileList, DragDropTargetFlags.SameApplication | DragDropTargetFlags.OtherApplication, 0x1)
			}, DragDropEffect.Copy, MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey.None);

			((EditorApplication)Application.Instance).Windows.Add(this);
			((EditorApplication)Application.Instance).LastWindow = this;

			UpdateMenuItems();
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			((EditorApplication)Application.Instance).LastWindow = this;
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

		private Inhibitor LogoutInhibitor = null;
		private int logoutInhibitorI = 0;

		public void OpenFile(params Document[] documents)
		{
			foreach (Document doc in documents)
			{
				try
				{
					InitEditorPage(doc);

					if (logoutInhibitorI == 0)
					{
						(Application.Instance as UIApplication).Inhibitors.Add(LogoutInhibitor);
					}
					logoutInhibitorI++;

					if (doc == null)
						continue;

					if (doc.Accessor is FileAccessor)
					{
						// FIXME: support Accessors other than FileAccessor
						string fileName = (doc.Accessor as FileAccessor).FileName;
						if (!((EditorApplication)Application.Instance).RecentFileManager.FileNames.Contains(fileName))
						{
							((EditorApplication)Application.Instance).RecentFileManager.FileNames.Add(fileName);
						}
					}
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
				bool inputClosed = false;
				if (document.InputAccessor != null && document.InputAccessor.IsOpen)
				{
					inputClosed = true;
					document.InputAccessor.Close();
				}

				if (document.OutputAccessor is FileAccessor)
				{
					// FIXME: ewww
					(document.OutputAccessor as FileAccessor).AllowWrite = true;
					(document.OutputAccessor as FileAccessor).ForceOverwrite = true;
				}

				try
				{
					document.OutputAccessor.Open();
					document.Save();
					document.OutputAccessor.Close();
				}
				catch (UnauthorizedAccessException ex)
				{
					if (inputClosed)
					{
						if (document.InputAccessor is FileAccessor)
						{
							// FIXME: ewww
							(document.InputAccessor as FileAccessor).AllowWrite = false;
							(document.InputAccessor as FileAccessor).ForceOverwrite = false;
						}
						document.InputAccessor.Open();
					}

					switch (HandleUnauthorizedAccessException(document, ex))
					{
						case MultipleDocumentErrorHandling.CancelAll: return false;
						case MultipleDocumentErrorHandling.CancelOne: return true;
						case MultipleDocumentErrorHandling.Ignore: break;
					}
				}

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

			try
			{
				page.Document.Save();
			}
			catch (UnauthorizedAccessException ex)
			{
				switch (HandleUnauthorizedAccessException(page.Document, ex))
				{
					case MultipleDocumentErrorHandling.CancelAll: return false;
					case MultipleDocumentErrorHandling.CancelOne: return true;
					case MultipleDocumentErrorHandling.Ignore: break;
				}
			}
			GetCurrentEditor().Document = page.Document;

			DockingWindow di = dckContainer.Items[page] as DockingWindow;
			if (di != null)
			{
				di.Name = accessor.GetFileName();
				di.Title = System.IO.Path.GetFileName(accessor.GetFileName());
			}
			return true;
		}

		private MultipleDocumentErrorHandling HandleUnauthorizedAccessException(Document document, UnauthorizedAccessException ex)
		{
			DialogResult dr = MessageDialog.ShowDialog(String.Format("Cannot save the file in its current location.  Would you like to choose another location?\r\n\r\n{0}", ex.Message), "Unauthorized", MessageDialogButtons.YesNoCancel, MessageDialogIcon.Warning);
			if (dr == DialogResult.Yes)
			{
				SaveFileAs(document);
			}
			else if (dr == DialogResult.No)
			{
				return MultipleDocumentErrorHandling.CancelOne;
			}
			else if (dr == DialogResult.Cancel)
			{
				return MultipleDocumentErrorHandling.CancelAll;
			}
			return MultipleDocumentErrorHandling.Ignore;
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
				SaveProjectAs(dlg.SelectedFileName, df);
			}
		}

		public void SaveProjectAs(string FileName, DataFormat df)
		{
			_CurrentSolutionDocument = new Document(CurrentSolution, df, new FileAccessor(FileName, true, true));
			_CurrentSolutionDocument.Accessor.Open();
			_CurrentSolutionDocument.Save();
			_CurrentSolutionDocument.Accessor.Close();

			CurrentSolution.BasePath = System.IO.Path.GetDirectoryName(FileName);
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

			logoutInhibitorI--;
			if (logoutInhibitorI == 0)
			{
				(Application.Instance as UIApplication).Inhibitors.Remove(LogoutInhibitor);
			}

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
					dlg.EnablePreview = true;

					PrintDialogOptionsTabPage tab1 = new PrintDialogOptionsTabPage();
					tab1.SelectedPrintHandler = phrs[0];
					tab1.PrintHandlers = phrs;

					dlg.TabPages.Add(tab1);

					DialogResult result = dlg.ShowDialog(this);

					switch (result)
					{
						case DialogResult.Apply:
						{
							// do print preview

							FileAccessor faINI = new FileAccessor(TemporaryFileManager.GetTemporaryFileName(), true, true, true);
							PropertyListObjectModel omINI = new PropertyListObjectModel();
							DataFormats.PropertyList.WindowsConfigurationDataFormat dfINI = new DataFormats.PropertyList.WindowsConfigurationDataFormat();

							omINI.Items.Add(new Group("Print Settings", new PropertyListItem[]
							{
							}));
							omINI.Items.Add(new Group("Page Setup", new PropertyListItem[]
							{
								new Property("PPDName", "Letter")
							}));
							omINI.Items.Add(new Group("Print Job", new PropertyListItem[]
							{
								new Property("title", "Universal Editor Print Preview Test")
							}));

							// we do not need to quote property values here
							dfINI.PropertyValuePrefix = String.Empty;
							dfINI.PropertyValueSuffix = String.Empty;

							Document.Save(omINI, dfINI, faINI);

							string pdfFileName = TemporaryFileManager.GetTemporaryFileName();
							System.IO.File.Copy("/tmp/1940.pdf", pdfFileName);

							(Application.Instance as UIApplication).Launch("evince-previewer", String.Format("--print-settings {0} --unlink-tempfile {1}", faINI.FileName, pdfFileName));
							break;
						}
					}
					if (result == DialogResult.OK)
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

		void Job_Preview(object sender, PrintPreviewEventArgs e)
		{
			e.Handled = true;
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

		public Page GetCurrentPage()
		{
			DockingWindow curitem = dckContainer.CurrentItem as DockingWindow;
			if (curitem == null) return null;

			Page pg = curitem.ChildControl as Page;
			if (pg == null)
			{
				Control[] ctls = dckContainer.GetAllControls();
				for (int i = 0; i < ctls.Length; i++)
				{
					if (ctls[i] is Page)
						return ctls[i] as Page;
				}
			}
			return pg;
		}
		public EditorPage GetCurrentEditorPage()
		{
			return GetCurrentPage() as EditorPage;
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
			return ((UIApplication)Application.Instance).ShowSettingsDialog();
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
				return pnlSolutionExplorer.Project;
			}
			set
			{
				pnlSolutionExplorer.Project = value;
				UpdateMenuItems();
			}
		}

		public Document.ReadOnlyDocumentCollection Documents
		{
			get
			{
				// retrieve all documents currently loaded in this window
				EditorPage[] pages = GetEditorPages();
				List<Document> list = new List<Document>();
				for (int i = 0; i < pages.Length; i++)
				{
					list.Add(pages[i].Document);
				}
				return new Document.ReadOnlyDocumentCollection(list);
			}
		}

		public Editor.ReadOnlyEditorCollection Editors
		{
			get
			{
				// retrieve all editors currently loaded in this window
				EditorPage[] pages = GetEditorPages();
				List<Editor> list = new List<Editor>();
				for (int i = 0; i < pages.Length; i++)
				{
					for (int j = 0; j < pages[i].Controls.Count; j++)
					{
						if (pages[i].Controls[j] is Editor)
						{
							list.Add(pages[i].Controls[j] as Editor);
						}
					}
				}
				return new Editor.ReadOnlyEditorCollection(list);
			}
		}

		#endregion

		public IDocumentPropertiesProvider FindDocumentPropertiesProvider(IControl control)
		{
			IControl parent = control;
			while (parent != null)
			{
				if (parent is IDocumentPropertiesProviderControl)
				{
					return parent as IDocumentPropertiesProviderControl;
				}
				parent = parent.Parent;
			}
			return null;
		}

		public void ShowDocumentPropertiesDialog()
		{
			IDocumentPropertiesProvider dpp = FindDocumentPropertiesProvider(ActiveControl);
			if (dpp != null)
			{
				dpp.ShowDocumentPropertiesDialog();
			}
		}
	}
}
