using AwesomeControls.DockingWindows;
using AwesomeControls.PropertyGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Solution;
using UniversalEditor.UserInterface.WindowsForms.Dialogs;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using UniversalEditor.ObjectModels.Project;

namespace UniversalEditor.UserInterface.WindowsForms
{
	public partial class MainWindow : AwesomeControls.Window, IHostApplicationWindow
	{
		public event EventHandler WindowClosed;

		#region Docking Windows
		private Controls.ErrorList pnlErrorList = new Controls.ErrorList();
		private DockingWindow dwErrorList = null;

		private DockingWindow dwProperties = null;
		
		private Controls.SolutionExplorer pnlSolutionExplorer = new Controls.SolutionExplorer();
		private DockingWindow dwSolutionExplorer = null;

		private DockingWindow dwToolbox = null;

		private Controls.OutputWindow wndOutputWindow = null;
		private DockingWindow dwOutput = null;
		#endregion

		public void ActivateWindow()
		{
			// Invoke required when being called from SingleInstance_Callback
			Invoke(new Action(delegate()
			{
				Activate();
				BringToFront();
			}));
		}

		public MainWindow()
		{
			InitializeComponent();
			InitializeDockingWindows();
			InitializeCommandBars();

			pnlSolutionExplorer.ParentWindow = this;

			this.Icon = LocalConfiguration.MainIcon;
			this.Text = LocalConfiguration.ApplicationName;
			
			mnuContextDocumentTypeDataFormat.Font = new Font(SystemFonts.MenuFont, FontStyle.Bold);

			UpdatePreferencesMenu();

			RefreshRecentFilesList();

			HostApplication.OutputWindow.TextWritten += OutputWindow_TextWritten;
			HostApplication.OutputWindow.TextCleared += OutputWindow_TextCleared;
			HostApplication.Messages.MessageAdded += Messages_MessageAdded;
			HostApplication.Messages.MessageRemoved += Messages_MessageRemoved;

			mnuBookmarksSep1.Visible = (Engine.CurrentEngine.BookmarksManager.FileNames.Count > 0);
			foreach (string FileName in Engine.CurrentEngine.BookmarksManager.FileNames)
			{
				ToolStripMenuItem tsmi = new ToolStripMenuItem();
				tsmi.Text = System.IO.Path.GetFileName(FileName);
				tsmi.ToolTipText = FileName;
				tsmi.Click += tsmiBookmark_Click;
				mnuBookmarks.DropDownItems.Insert(mnuBookmarks.DropDownItems.Count - 2, tsmi);
			}

			ShowStartPage();
		}

		private void InitializeCommandBars()
		{
			mbMenuBar.Items.Clear();
			foreach (CommandItem item in Engine.CurrentEngine.MainMenu.Items)
			{
				LoadCommandBarMenuItem(item, null);
			}

			foreach (CommandBar bar in Engine.CurrentEngine.CommandBars)
			{
				LoadCommandBar(bar);
			}
		}

		private void LoadCommandBar(CommandBar bar)
		{
			AwesomeControls.CommandBars.CBToolBar tb = new AwesomeControls.CommandBars.CBToolBar();
			tb.Text = bar.Title;
			foreach (CommandItem item in bar.Items)
			{
				LoadCommandBarItem(item, tb);
			}
			cbc.TopToolStripPanel.Controls.Add(tb);
		}

		private ToolStripItem InitializeCommandBarItem(CommandItem item, bool isOnDropDown = false)
		{
			if (item is SeparatorCommandItem)
			{
				return new ToolStripSeparator();
			}
			else if (item is CommandReferenceCommandItem)
			{
				CommandReferenceCommandItem crci = (item as CommandReferenceCommandItem);
				Command cmd = Engine.CurrentEngine.Commands[crci.CommandID];
				if (cmd == null)
				{
					Console.WriteLine("could not find command '" + crci.CommandID + "'");
					return null;
				}

				if (cmd.Items.Count > 0)
				{
					ToolStripDropDownItem tsi = null;
					if (!isOnDropDown)
					{
						if (!String.IsNullOrEmpty(cmd.DefaultCommandID))
						{
							ToolStripSplitButton tsb = new ToolStripSplitButton();
							tsb.ButtonClick += tsbCommandBarButton_Click;
							tsi = tsb;
						}
						else
						{
							ToolStripDropDownButton tsb = new ToolStripDropDownButton();
							tsi = tsb;
						}
					}
					else
					{
						ToolStripMenuItem tsmi = new ToolStripMenuItem();
						tsi = tsmi;
					}

					if (tsi == null)
					{
						Console.WriteLine("ToolStripDropDownItem was not loaded!");
						return null;
					}

					tsi.Tag = cmd;
					tsi.Text = cmd.Title.Replace("_", "&");
					foreach (CommandItem item1 in cmd.Items)
					{
						LoadCommandBarItem(item1, tsi);
					}
					return tsi;
				}
				else
				{
					ToolStripItem tsi = null;
					if (!isOnDropDown)
					{
						tsi = new ToolStripButton();
					}
					else
					{
						tsi = new ToolStripMenuItem();
					}
					tsi.Tag = cmd;
					tsi.Text = cmd.Title.Replace("_", "&");
					tsi.Click += tsbCommandBarButton_Click;
					return tsi;
				}
			}
			return null;
		}

		private void LoadCommandBarItem(CommandItem item, ToolStripDropDownItem parent)
		{
			ToolStripItem tsi = InitializeCommandBarItem(item, true);
			if (tsi == null) return;

			parent.DropDownItems.Add(tsi);
		}
		private void LoadCommandBarItem(CommandItem item, AwesomeControls.CommandBars.CBToolBar parent)
		{
			ToolStripItem tsi = InitializeCommandBarItem(item);
			if (tsi == null) return;

			parent.Items.Add(tsi);
		}

		private void tsbCommandBarButton_Click(object sender, EventArgs e)
		{
			if (sender is ToolStripDropDownItem)
			{
				ToolStripDropDownItem tsi = (sender as ToolStripDropDownItem);
				Command cmd = (tsi.Tag as Command);
				if (tsi.DropDownItems.Count > 0)
				{
					Command cmd1 = Engine.CurrentEngine.Commands[cmd.DefaultCommandID];
					if (cmd1 == null)
					{
						Console.WriteLine("could not find command '" + cmd.DefaultCommandID + "'");
						return;
					}
					cmd1.Execute();
				}
				else
				{
					cmd.Execute();
				}
			}
			else if (sender is ToolStripItem)
			{
				ToolStripItem tsi = (sender as ToolStripItem);
				Command cmd = (tsi.Tag as Command);
				cmd.Execute();
			}
		}

		private void LoadCommandBarMenuItem(CommandItem item, ToolStripMenuItem parent)
		{
			ToolStripItem tsi = null;

			if (item is CommandReferenceCommandItem)
			{
				CommandReferenceCommandItem crci = (item as CommandReferenceCommandItem);
				Command cmd = Engine.CurrentEngine.Commands[crci.CommandID];
				if (cmd == null)
				{
					Console.WriteLine("Skipping invalid command reference '" + crci.CommandID + "'");
					return;
				}
				
				ToolStripMenuItem tsmi = new ToolStripMenuItem();
				tsmi.Click += tsmiCommand_Click;
				tsmi.Tag = cmd;
				tsmi.Text = cmd.Title.Replace("_", "&");
				foreach (CommandItem item1 in cmd.Items)
				{
					LoadCommandBarMenuItem(item1, tsmi);
				}
				tsi = tsmi;
			}
			else if (item is SeparatorCommandItem)
			{
				tsi = new ToolStripSeparator();
			}

			if (parent == null)
			{
				mbMenuBar.Items.Add(tsi);
			}
			else
			{
				parent.DropDownItems.Add(tsi);
			}
		}

		void tsmiCommand_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			Command cmd = (tsmi.Tag as Command);
			if (cmd != null) cmd.Execute();
		}


		private void Messages_MessageAdded(object sender, HostApplicationMessageModifiedEventArgs e)
		{
			pnlErrorList.Invoke(new Action(delegate()
			{
				pnlErrorList.Messages.Add(e.Message);
				pnlErrorList.RefreshList();
			}));
		}
		private void Messages_MessageRemoved(object sender, HostApplicationMessageModifiedEventArgs e)
		{
			pnlErrorList.Invoke(new Action(delegate()
			{
				pnlErrorList.Messages.Remove(e.Message);
				pnlErrorList.RefreshList();
			}));
		}

		private void tsmiBookmark_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			OpenFile(tsmi.ToolTipText);
		}

		PropertyGridControl pgc = new PropertyGridControl();

		private void InitializeDockingWindows()
		{
			dwSolutionExplorer = dcc.Windows.Add("Solution Explorer", pnlSolutionExplorer);
			dwProperties = dcc.Windows.Add("Properties", pgc);
			dwToolbox = dcc.Windows.Add("Toolbox", new AwesomeControls.Toolbox.ToolboxControl());

			dcc.Areas[DockPosition.Right].Areas[DockPosition.Top].Windows.Add(dwSolutionExplorer);
			dcc.Areas[DockPosition.Right].Areas[DockPosition.Bottom].Windows.Add(dwProperties);
			
			dcc.Areas[DockPosition.Left].Windows.Add(dwToolbox);
			dwToolbox.Behavior = DockBehavior.AutoHide;

			#region Message Log
			wndOutputWindow = new Controls.OutputWindow();
			dwOutput = dcc.Windows.Add("Output", wndOutputWindow);
			dcc.Areas[DockPosition.Center].Areas[DockPosition.Bottom].Windows.Add(dwOutput);
			#endregion
			#region Error List
			pnlErrorList = new Controls.ErrorList();
			dwErrorList = dcc.Windows.Add("Error List", pnlErrorList);
			dcc.Areas[DockPosition.Center].Areas[DockPosition.Bottom].Windows.Add(dwErrorList);
			#endregion

			#region Property Grid
			pgc.PropertyChanging += new PropertyChangingEventHandler(pgc_PropertyChanging);
			pgc.PropertyChanged += new PropertyChangedEventHandler(pgc_PropertyChanged);
			#endregion
		}

		private void pgc_PropertyChanging(object sender, PropertyChangingEventArgs e)
		{

		}
		private void pgc_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

		}

		private void _AppendText(string text)
		{
			wndOutputWindow.AppendText(text);
		}
		private void _ClearText(string text)
		{
			wndOutputWindow.ClearText();
		}

		private void OutputWindow_TextWritten(object sender, TextWrittenEventArgs e)
		{
			if (wndOutputWindow != null)
			{
				wndOutputWindow.Invoke(new Action<string>(_AppendText), e.Text);
			}
		}
		private void OutputWindow_TextCleared(object sender, EventArgs e)
		{
			if (wndOutputWindow != null)
			{
				wndOutputWindow.Invoke(new Action<string>(_ClearText));
			}
		}

		private SolutionObjectModel mvarCurrentSolution = null;
		public SolutionObjectModel CurrentSolution { get { return mvarCurrentSolution; } set { mvarCurrentSolution = value; RefreshSolution(); } }

		private ProjectObjectModel mvarCurrentProject = null;
		public ProjectObjectModel CurrentProject { get { return mvarCurrentProject; } set { mvarCurrentProject = value; RefreshProject(); } }

		private void RefreshSolution()
		{
			pnlSolutionExplorer.Solution = mvarCurrentSolution;

			if (mvarCurrentSolution == null) return;

		}

		#region Editor-Specific Menus
		// TODO: Implement Editor-Specific Menus!!
		private List<ToolStrip> mvarEditorSpecificToolbars = new List<ToolStrip>();
		private void RecursiveLoadCustomToolbar(Toolbar toolbar)
		{
			AwesomeControls.CommandBars.CBToolBar cbt = new AwesomeControls.CommandBars.CBToolBar();
			cbt.Text = toolbar.Title;
			cbt.Tag = toolbar;
			foreach (MenuItem mi in toolbar.Items)
			{
				RecursiveLoadCustomToolbarMenuItem(mi, cbt);
			}
			cbt.Top = cbc.TopToolStripPanel.Height;
			cbc.TopToolStripPanel.Controls.Add(cbt);

			// TODO: Add the "View->Toolbars" option for this toolbar to let the user show/hide it...

			mvarEditorSpecificToolbars.Add(cbt);
		}
		private void RecursiveLoadCustomToolbarMenuItem(MenuItem mi, AwesomeControls.CommandBars.CBToolBar cbt)
		{
			if (mi is ActionMenuItem)
			{
				ActionMenuItem ami = (mi as ActionMenuItem);
				ToolStripItem tsi = GetToolStripButtonFromUniversalMenuItem(ami);
				// tsi.Image = ami.Image;
				switch (ami.DisplayStyle)
				{
					case CommandDisplayStyle.None:
					{
						tsi.DisplayStyle = ToolStripItemDisplayStyle.None;
						break;
					}
					case CommandDisplayStyle.ImageOnly:
					{
						tsi.DisplayStyle = ToolStripItemDisplayStyle.Image;
						break;
					}
					case CommandDisplayStyle.ImageAndText:
					{
						tsi.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
						break;
					}
					case CommandDisplayStyle.TextOnly:
					{
						tsi.DisplayStyle = ToolStripItemDisplayStyle.Text;
						break;
					}
				}
				cbt.Items.Add(tsi);
			}
		}

		public void RefreshCommand(object nativeCommandObject)
		{
			if (nativeCommandObject is ToolStripItem)
			{
				ToolStripItem tsi = (nativeCommandObject as ToolStripItem);
				MenuItem mi = (tsi.Tag as MenuItem);
				tsi.Enabled = mi.Enabled;
				tsi.Visible = mi.Visible;
			}
		}

		private List<ToolStripItem> mvarEditorSpecificMenuItems = new List<ToolStripItem>();
		private void RecursiveLoadCustomMenuItems(MenuItem item, ToolStripDropDownItem tsiParent = null)
		{
			if (item is ActionMenuItem)
			{
				ActionMenuItem ami = (item as ActionMenuItem);
				ToolStripMenuItem tsmi = null;

				if (tsiParent != null && tsiParent.DropDownItems.ContainsKey(item.Name) && tsiParent.DropDownItems[item.Name] is ToolStripMenuItem)
				{
					tsmi = (tsiParent.DropDownItems[item.Name] as ToolStripMenuItem);
					foreach (MenuItem mi in ami.Items)
					{
						RecursiveLoadCustomMenuItems(mi, tsmi);
					}
				}
				else if (mbMenuBar.Items.ContainsKey(item.Name) && mbMenuBar.Items[item.Name] is ToolStripMenuItem)
				{
					tsmi = (mbMenuBar.Items[item.Name] as ToolStripMenuItem);
					foreach (MenuItem mi in ami.Items)
					{
						RecursiveLoadCustomMenuItems(mi, tsmi);
					}
				}
				else
				{
					tsmi = GetToolStripMenuItemFromUniversalMenuItem(ami);
					if (tsiParent == null)
					{
						if (ami.Position < 0)
						{
							mbMenuBar.Items.Insert(mbMenuBar.Items.Count + (ami.Position + 1), tsmi);
						}
						else
						{
							mbMenuBar.Items.Insert(ami.Position, tsmi);
						}
					}
					else
					{
						if (ami.Position < 0)
						{
							tsiParent.DropDownItems.Insert(tsiParent.DropDownItems.Count + (ami.Position + 1), tsmi);
						}
						else
						{
							tsiParent.DropDownItems.Insert(ami.Position, tsmi);
						}
					}
					mvarEditorSpecificMenuItems.Add(tsmi);
				}


				ami.NativeControls.Add(tsmi);
			}
			else if (item is SeparatorMenuItem)
			{
				SeparatorMenuItem smi = (item as SeparatorMenuItem);
				ToolStripSeparator tsmi = new ToolStripSeparator();
				if (tsiParent == null)
				{
					mbMenuBar.Items.Insert(smi.Position, tsmi);
				}
				else
				{
					tsiParent.DropDownItems.Insert(smi.Position, tsmi);
				}
				mvarEditorSpecificMenuItems.Add(tsmi);

				smi.NativeControls.Add(tsmi);
			}
		}

		private ToolStripMenuItem GetToolStripMenuItemFromUniversalMenuItem(ActionMenuItem ami)
		{
			ToolStripMenuItem tsmi = new ToolStripMenuItem();
			tsmi.Name = ami.Name;

			Editor currentEditor = GetCurrentWindowsFormsEditor();
			string imagePath = currentEditor.DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Images" + System.IO.Path.DirectorySeparatorChar.ToString() + "Commands" + System.IO.Path.DirectorySeparatorChar.ToString() + ami.Name + ".png";
			if (System.IO.File.Exists(imagePath)) tsmi.Image = Image.FromFile(imagePath);

			tsmi.Enabled = ami.Enabled;
			tsmi.Visible = ami.Visible;
			tsmi.Text = ami.Title;
			tsmi.Tag = ami;
			tsmi.Click += tsmiEditorSpecificMenuItem_Click;
			foreach (MenuItem mi in ami.Items)
			{
				RecursiveLoadCustomMenuItems(mi, tsmi);
			}
			return tsmi;
		}
		private ToolStripItem GetToolStripButtonFromUniversalMenuItem(ActionMenuItem ami)
		{
			ToolStripItem tsi = null;
			if (ami.Items.Count == 0)
			{
				tsi = new ToolStripButton();
				tsi.Click += tsmiEditorSpecificToolBarButton_Click;
			}
			else
			{
				ToolStripSplitButton tsb = new ToolStripSplitButton();
				foreach (MenuItem mi in ami.Items)
				{
					RecursiveLoadCustomMenuItems(mi, tsb);
				}
				tsb.ButtonClick += tsmiEditorSpecificToolBarButton_Click;

				tsi = tsb;
			}

			Editor currentEditor = GetCurrentWindowsFormsEditor();
			string imagePath = currentEditor.DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Images" + System.IO.Path.DirectorySeparatorChar.ToString() + "Commands" + System.IO.Path.DirectorySeparatorChar.ToString() + ami.Name + ".png";
			if (System.IO.File.Exists(imagePath)) tsi.Image = Image.FromFile(imagePath);

			tsi.Enabled = ami.Enabled;
			tsi.Visible = ami.Visible;
			tsi.Text = ami.Title;
			tsi.Tag = ami;
			return tsi;
		}

		private void tsmiEditorSpecificMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			ActionMenuItem ami = (tsmi.Tag as ActionMenuItem);
			ami.OnClick(e);
		}
		private void tsmiEditorSpecificToolBarButton_Click(object sender, EventArgs e)
		{
			ToolStripButton tsb = (sender as ToolStripButton);
			ActionMenuItem ami = (tsb.Tag as ActionMenuItem);
			ami.OnClick(e);
		}
		#endregion

		private void RefreshProject()
		{
		}

		/// <summary>
		/// Causes the application to refresh the current document view, including any
		/// document-specific menus and toolbars.
		/// </summary>
		public void RefreshDocument()
		{
			mnuFileSaveFile.Enabled = (CurrentDocument != null);
			mnuFileSaveFileAs.Enabled = (CurrentDocument != null);
			
			mnuFileCloseFile.Enabled = (dcc.SelectedWindow != null);

			mnuEditCut.Enabled = (CurrentDocument != null);
			mnuEditCopy.Enabled = (CurrentDocument != null);
			mnuEditPaste.Enabled = (CurrentDocument != null);
			mnuEditDelete.Enabled = (CurrentDocument != null);

			mnuBookmarksAdd.Enabled = (CurrentDocument != null);

			#region Remove Editor-Specific Menus and Toolbars
			mnuViewToolbars.DropDownItems.Clear();

			foreach (ToolStrip tb in mvarEditorSpecificToolbars)
			{
				tb.Parent.Controls.Remove(tb);
			}
			mvarEditorSpecificToolbars.Clear();

			foreach (ToolStripItem tsi in mvarEditorSpecificMenuItems)
			{
				ToolStrip parent = tsi.Owner;
				if (parent != null)
				{
					parent.Items.Remove(tsi);
				}
			}
			mvarEditorSpecificMenuItems.Clear();

			#region Remove Editor-Specific Property Pages
			pgc.Groups.Clear();
			#endregion
			#endregion

			if (CurrentDocument != null)
			{
				Editor editor = GetCurrentWindowsFormsEditor();
				if (editor != null)
				{
					foreach (MenuItem mi in editor.MenuBar.Items)
					{
						RecursiveLoadCustomMenuItems(mi);
					}
					foreach (Toolbar tb in editor.Toolbars)
					{
						RecursiveLoadCustomToolbar(tb);

						ToolStripMenuItem mnuViewToolbarsToolbar = new ToolStripMenuItem();
						mnuViewToolbarsToolbar.Text = tb.Title;
						mnuViewToolbarsToolbar.Checked = tb.Visible;
						mnuViewToolbars.DropDownItems.Add(mnuViewToolbarsToolbar);
					}
				}

				lblObjectModel.Text = CurrentDocument.ObjectModel.MakeReference().Title;
				lblObjectModel.Visible = true;

				if (CurrentDocument.DataFormat != null)
				{
					mnuContextDocumentTypeDataFormat.Text = CurrentDocument.DataFormat.MakeReference().Title;
				}
				else
				{
					mnuContextDocumentTypeDataFormat.Text = "(unsaved)";
				}
				
				mnuContextDocumentType.Items.Clear();
				mnuContextDocumentType.Items.Add(mnuContextDocumentTypeDataFormat);

				if (CurrentDocument.InputDataFormat != null)
				{
					mnuContextDocumentType.Items.Add(mnuContextDocumentTypeSep1);

					foreach (ObjectModelReference omr in UniversalEditor.Common.Reflection.GetAvailableObjectModels(CurrentDocument.InputDataFormat.MakeReference()))
					{
						ToolStripMenuItem mnu = new ToolStripMenuItem();
						mnu.Text = omr.Title;
						mnu.Tag = omr;
						mnu.Click += new EventHandler(mnuSwitchObjectModel_Click);
						if (omr.ObjectModelType == CurrentDocument.ObjectModel.GetType())
						{
							mnu.Checked = true;
						}
						mnuContextDocumentType.Items.Add(mnu);
					}
				}
			}
			else
			{
				lblObjectModel.Visible = false;
			}

			if (mnuViewToolbars.DropDownItems.Count > 0) mnuViewToolbars.DropDownItems.Add(mnuViewToolbarsSep1);
			mnuViewToolbars.DropDownItems.Add(mnuViewToolbarsCustomize);
		}
		
		private void mnuSwitchObjectModel_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			ObjectModelReference omr = (tsmi.Tag as ObjectModelReference);
			
			MessageBox.Show("Implement switching the current ObjectModel to \"" + omr.Title + "\"!", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		private Document mvarCurrentDocument = null;
		public Document CurrentDocument
		{
			get { return mvarCurrentDocument; }
		}

		private void UpdatePreferencesMenu()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					mnuEditPreferences.Visible = true;
					mnuEditSep4.Visible = true;

					mnuToolsOptions.Visible = false;
					break;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				case PlatformID.Xbox:
				{
					mnuEditPreferences.Visible = false;
					mnuEditSep4.Visible = false;

					mnuToolsOptions.Visible = true;
					break;
				}
			}
		}

		#region Implementation
		public void ToggleMenuItemEnabled(string MenuItemName, bool Enabled)
		{
			MessageBox.Show("ToggleMenuItemEnabled(\"" + MenuItemName + "\"): not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void NewFile()
		{
			NewDialog dlg = new NewDialog();
			dlg.Mode = NewDialogMode.File;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				DocumentTemplate template = (dlg.SelectedItem as DocumentTemplate);
				if (template == null) return;

				Pages.EditorPage page = new Pages.EditorPage();
				page.DocumentEdited += page_DocumentEdited;
				page.FileName = "<untitled>";

				ObjectModel objm = template.ObjectModelReference.Create();
				if (objm == null)
				{
					MessageBox.Show("Failed to create an ObjectModel for the type \"" + template.ObjectModelReference.ObjectModelTypeName + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				page.Document = new Document(objm, null, null);
				DockingWindow dwNewDocument = dcc.Windows.Add("<untitled>", "<untitled>", page);
				dwNewDocument.Behavior = DockBehavior.Dock;

				dcc.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows.Add(dwNewDocument);

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
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

		public void OpenFile()
		{
			DocumentPropertiesDialog dlg = new DocumentPropertiesDialog();
			dlg.Mode = DocumentPropertiesDialogMode.Open;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				OpenFile((dlg.Accessor as FileAccessor).FileName);
			}

			// Display the Open File dialog
			/*
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.AutoUpgradeEnabled = true;
				ofd.Filter = "All Files (*.*)|*.*";
				ofd.Multiselect = true;

				Glue.Common.Methods.SendApplicationEvent(new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.BeforeOpenFileDialog,
					new KeyValuePair<string, object>("Filter", ofd.Filter),
					new KeyValuePair<string, object>("FilterIndex", ofd.FilterIndex),
					new KeyValuePair<string, object>("FileNames", ofd.FileNames)
				));

				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Glue.Common.Methods.SendApplicationEvent(new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.AfterOpenFileDialog,
						new KeyValuePair<string, object>("Filter", ofd.Filter),
						new KeyValuePair<string, object>("FilterIndex", ofd.FilterIndex),
						new KeyValuePair<string, object>("FileNames", ofd.FileNames)
					));

					OpenFile(ofd.FileNames);
				}
			}
			*/
		}
		public void OpenFile(params string[] FileNames)
		{
			foreach (string FileName in FileNames)
			{
				OpenFile(FileName, false);
			}
		}
		public void OpenFile(string FileName, bool reuseTab)
		{
			Pages.EditorPage page = new Pages.EditorPage();
			if (reuseTab && dcc.SelectedWindow != null)
			{
			}
			else
			{
				DockingWindow wnd = dcc.Windows.Add(System.IO.Path.GetFileName(FileName), page);
				dcc.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows.Add(wnd);
			}

			page.OpenFile(FileName);
			page.FileOpened += page_FileOpened;
		}

		private void page_FileOpened(object sender, EventArgs e)
		{
			RefreshDocument();
		}

		/*
		private void OpenFileInternal(string Path, bool reuseTab = false)
		{
#if !DEBUG
			try
			{
#endif
				Glue.ApplicationEventEventArgs e = new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.BeforeOpenFile,
					new KeyValuePair<string, object>("FileName", Path)
				);
				Glue.Common.Methods.SendApplicationEvent(e);
	
				if (e.CancelApplication) return;
	
				string[] FileNameParts = Path.Split(new string[] { "::/" }, 2, StringSplitOptions.None);
				string FileName = FileNameParts[0];
				string SecondaryFileName = String.Empty;
				if (FileNameParts.Length > 1) SecondaryFileName = FileNameParts[1];
	
				if (System.IO.Directory.Exists(FileName))
				{
					Pages.ExplorerPage page = new Pages.ExplorerPage();
					page.Navigate += page_Navigate;
					page.Path = FileName;
					mdcc.Documents.Add(System.IO.Path.GetFileName(FileName), Path, page);
				}
				else if (!System.IO.File.Exists(FileName))
				{
					return;
				}
				else
				{

#if HANDLER
					HandlerReference[] handlers = UniversalEditor.Common.Reflection.GetAvailableHandlers(FileName);
					if (handlers.Length == 1)
					{
					}
					else
					{
						foreach (HandlerReference hr in handlers)
						{
							FileHandler fh = (hr.Create() as FileHandler);
							if (fh == null) continue;
	
							// got an object model/data format pair for this handler
						}
					}
#endif


					Pages.EditorPage page = new Pages.EditorPage();
					page.OpenFile(FileName);
					mdcc.Documents.Add(System.IO.Path.GetFileName(FileName), page);

				}
	
				// Add the file name to the list of recently-opened files
				if (!RecentFileManager.FileNames.Contains(FileName))
				{
					if (RecentFileManager.FileNames.Count >= RecentFileManager.MaximumDocumentFileNames)
					{
						RecentFileManager.FileNames.RemoveAt(0);
					}
					RecentFileManager.FileNames.Insert(RecentFileManager.FileNames.Count, FileName);
					RefreshRecentFilesList();
				}
#if !DEBUG
			}
			catch (System.IO.IOException ex)
			{
				MessageBox.Show("Could not open the file.  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
#endif
		}
		*/

		/// <summary>
		/// Opens the specified file from a <see cref="UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel" />
		/// and marks the parent file as changed when the file is
		/// changed.
		/// </summary>
		/// <param name="file">The child file to open.</param>
		/// <param name="FileName">The file name of the parent file.</param>
		private void OpenFileInternal(ObjectModels.FileSystem.File file, string FileName)
		{
			Pages.EmbeddedEditorPage page = new Pages.EmbeddedEditorPage();
			// page.DocumentEdited += new EventHandler(page_DocumentEdited);
			page.Title = file.Name;
			page.Description = FileName + "::/" + file.Name;

			byte[] data = file.GetDataAsByteArray();
			System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
			DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(ms, file.Name);
			foreach (DataFormatReference dfr in dfrs)
			{
				ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(dfr);

				DataFormat df = dfr.Create();
				ObjectModel om = omrs[0].Create();

				page.Document = new Document(om, df, new StreamAccessor(ms));
				// page.DocumentEdited += page_DocumentEdited;
				page.Document.InputAccessor.Open();
				page.Document.Load();
				page.Document.IsSaved = true;

				dcc.Windows.Add(System.IO.Path.GetFileName(file.Name), FileName + "::/" + file.Name, page);
				break;
			}

			/*
			DataFormatReference dfr = fmt.MakeReference();
			if (dfr.ImportOptions.Count > 0)
			{
				DataFormatOptionsDialog dlg = new DataFormatOptionsDialog();
				dlg.DialogType = DataFormatOptionsDialogType.Import;
				dlg.DataFormat = fmt;
				if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
			}

			FileAccessor fa = new FileAccessor(objm, fmt);
			file.Open(file.GetDataAsByteArray());
			file.Load();
			// file.Close();

			page.Document = new Document(file, objm, fmt);
			page.DocumentEdited += page_DocumentEdited;
			page.Document.IsSaved = true;
			page.Document.FileName = FileName;

			mdcc.Documents.Add(System.IO.Path.GetFileName(FileName), FileName, page);

			Glue.ApplicationEventEventArgs e = new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.AfterOpenFile,
				new KeyValuePair<string, object>("FileName", FileName),
				new KeyValuePair<string, object>("DataFormat", fmt),
				new KeyValuePair<string, object>("ObjectModel", objm)
			);

			Glue.Common.Methods.SendApplicationEvent(e);
			*/
		}
		/*
		private void OpenFileInternal(string FileName, ObjectModel objm, DataFormat fmt)
		{
		retryOpen:
			Pages.EditorPage page = new Pages.EditorPage();
			// page.DocumentEdited += new EventHandler(page_DocumentEdited);
			page.FileName = FileName;

			if (!this.Visible) this.Show();

			DataFormatReference dfr = fmt.MakeReference();
			if (dfr.ImportOptions.Count > 0)
			{
				DataFormatOptionsDialog dlg = new DataFormatOptionsDialog();
				dlg.DialogType = DataFormatOptionsDialogType.Import;
				dlg.DataFormat = fmt;
				if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel) return;
			}

			FileAccessor file = new FileAccessor(objm, fmt);
			objm.Clear();

			try
			{
				file.Open(FileName);
				file.Load();
			}
			catch (DataFormatOptionArgumentException ex)
			{
				file.Close();

				if (MessageBox.Show("One or more of the parameters you specified is invalid.  Please ensure you have provided the correct parameters, and then try again.\r\n\r\n" + ex.Message, "Invalid Parameters Specified", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry) goto retryOpen;
				return;
			}
			catch (DataCorruptedException ex)
			{
				file.Close();

				MessageBox.Show("The file is corrupted.", "Corrupt File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			// file.Close();

			page.Document = new Document(file, objm, fmt);
			page.DocumentEdited += page_DocumentEdited;
			page.Document.IsSaved = true;
			page.Document.FileName = FileName;

			mdcc.Documents.Add(System.IO.Path.GetFileName(FileName), FileName, page);

			Glue.ApplicationEventEventArgs e = new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.AfterOpenFile,
				new KeyValuePair<string, object>("FileName", FileName),
				new KeyValuePair<string, object>("DataFormat", fmt),
				new KeyValuePair<string, object>("ObjectModel", objm)
			);

			Glue.Common.Methods.SendApplicationEvent(e);
		}
		*/

		public void OpenProject(bool combineObjects = false)
		{
			SolutionObjectModel solution = new SolutionObjectModel();
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(solution.MakeReference());
			ofd.Multiselect = false;
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				OpenProject(ofd.FileName, combineObjects);
			}
		}
		public void OpenProject(string FileName, bool combineObjects = false)
		{
			SolutionObjectModel solution = UniversalEditor.Common.Reflection.GetAvailableObjectModel<SolutionObjectModel>(FileName);
			if (combineObjects)
			{
				SolutionObjectModel oldsolution = CurrentSolution;
				solution.CopyTo(oldsolution);
				CurrentSolution = oldsolution;
			}
			else
			{
				CurrentSolution = solution;
			}
		}

		public void SaveFile()
		{
			if (CurrentDocument != null)
			{
				SaveDocument(CurrentDocument);
			}
		}

		public Editor[] GetEditorsForDocument(Document doc)
		{
			List<Editor> editors = new List<Editor>();
			foreach (AwesomeControls.DockingWindows.DockingWindow mdc in dcc.Windows)
			{
				if (mdc.Control is Pages.EditorPage)
				{
					if ((mdc.Control as Pages.EditorPage).Document == doc)
					{
						Pages.EditorPage page = (mdc.Control as Pages.EditorPage);
						if (page.Controls.Count > 0)
						{
							if (page.Controls[0] is Editor)
							{
								editors.Add(page.Controls[0] as Editor);
							}
							else if (page.Controls[0] is TabControl)
							{
								foreach (TabPage tab in (page.Controls[0] as TabControl).TabPages)
								{
									if (tab.Controls[0] is Editor)
									{
										editors.Add(tab.Controls[0] as Editor);
									}
								}
							}
						}
					}
				}
			}
			return editors.ToArray();
		}

		private void NotifySaving(Document doc)
		{
			Editor[] editors = GetEditorsForDocument(doc);
			foreach (Editor editor in editors)
			{
				editor.NotifySaving();
			}
		}

		public void SaveFileAs()
		{
			if (CurrentDocument != null)
			{
				SaveDocumentAs(CurrentDocument);
			}
		}
		public void SaveFileAs(string FileName, DataFormat format)
		{
			if (CurrentDocument != null)
			{
				CurrentDocument.DataFormat = format;
				SaveDocumentAs(CurrentDocument, FileName);
			}
		}

		public void SaveProject()
		{
			SaveProjectAs();
		}
		public void SaveProjectAs()
		{
			if (mvarCurrentSolution == null)
			{
				return;
			}

			List<DataFormatReference> refs = new List<DataFormatReference>();
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(mvarCurrentSolution.MakeReference(), out refs);
			sfd.FileName = mvarCurrentSolution.Title + ".sln";
			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(sfd.FileName);
				SaveProjectAs(sfd.FileName, dfrs[0].Create());
			}
		}
		public void SaveProjectAs(string FileName, DataFormat df)
		{
			if (CurrentSolution != null)
			{
				Document.Save(CurrentSolution, df, new FileAccessor(FileName, true, true), true);
			}
		}
		public void SaveAll()
		{
			MessageBox.Show("Not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private bool SaveDocument(Document doc)
		{
			if (doc.Accessor == null)
			{
				// The accessor is null, so display the save as dialog
				return SaveDocumentAs(doc);
			}
			doc.Save();
			return true;
		}
		private bool SaveDocumentAs(Document doc, string FileName = null)
		{
			DataFormatReference dfr = null;

		retrySaveFileAs:
			if (FileName == null)
			{
				DocumentPropertiesDialog dlg = new DocumentPropertiesDialog();
				dlg.ObjectModel = doc.ObjectModel;
				dlg.DataFormat = doc.DataFormat;
				dlg.Accessor = doc.Accessor;
				dlg.Mode = DocumentPropertiesDialogMode.Save;
				if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					dfr = dlg.DataFormat.MakeReference();

					// TODO: Rewrite Save code to make everything use Accessor instead
					// of FileName to support multiple types of accessors
					FileName = (dlg.Accessor as FileAccessor).FileName;
				}
				else
				{
					return false;
				}

				/*
				SaveFileDialog sfd = new SaveFileDialog();

				List<DataFormatReference> list = new List<DataFormatReference>();
				sfd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(doc.ObjectModel.MakeReference(), out list);

				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
				{
					return false;
				}

				if (sfd.FilterIndex > 1 && sfd.FilterIndex <= list.Count + 1)
				{
					dfr = list[sfd.FilterIndex - 2];
				}
				FileName = sfd.FileName;
				*/
			}
			/*
			if (dfr == null)
			{
				DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(FileName, doc.ObjectModel.MakeReference());
				if (dfrs.Length == 0)
				{
					if (MessageBox.Show("Could not determine the data format to use to save the file.  Please check to see that you typed the file extension correctly.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
					{
						FileName = null;
						goto retrySaveFileAs;
					}
					else
					{
						return false;
					}
				}
				dfr = dfrs[0];
			}
			*/

			DataFormat df = dfr.Create();
			DataFormatOptionsDialog.ShowDialog(ref df, DataFormatOptionsDialogType.Export);
			
			#region Save Code
			NotifySaving(doc);

			if (FileName == doc.Title && doc.Accessor != null)
			{
				doc.Accessor.Close();
			}

			doc.DataFormat = df;
			doc.OutputAccessor = new FileAccessor(FileName, true, true, true);
			doc.Save();

			dcc.SelectedWindow.Title = System.IO.Path.GetFileName(FileName);
			doc.IsSaved = true;
			#endregion
			return true;
		}

		/*
		private bool SaveDocument(Document doc)
		{
			if (!doc.IsSaved)
			{
				return SaveDocumentAs(doc);
			}
			else
			{
				#region Save Code
				NotifySaving(doc);

				if (doc.Accessor != null) doc.Accessor.Close();

				doc.OutputAccessor = new FileAccessor(doc)
				FileAccessor file = new FileAccessor(doc.ObjectModel, doc.DataFormat);
				file.AllowWrite = true;
				file.ForceOverwrite = true;

				file.Open(doc.FileName);
				file.Save();
				doc.Accessor = file;

				dcc.SelectedWindow.Title = System.IO.Path.GetFileName(doc.FileName);
				doc.IsSaved = true;
				#endregion
			}
			return true;
		}
		public bool SaveDocumentAs(Document doc, string FileName = null)
		{
			DataFormatReference dfr = null;

		retrySaveFileAs:
			if (FileName == null)
			{
				SaveFileDialog sfd = new SaveFileDialog();

				List<DataFormatReference> list = new List<DataFormatReference>();
				sfd.Filter = UniversalEditor.Common.Dialog.GetCommonDialogFilter(doc.ObjectModel.MakeReference(), out list);

				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
				{
					return false;
				}

				if (sfd.FilterIndex > 1 && sfd.FilterIndex <= list.Count + 1)
				{
					dfr = list[sfd.FilterIndex - 2];
				}
				FileName = sfd.FileName;
			}

			if (dfr == null)
			{
				DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(FileName, doc.ObjectModel.MakeReference());
				if (dfrs.Length == 0)
				{
					if (MessageBox.Show("Could not determine the data format to use to save the file.  Please check to see that you typed the file extension correctly.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
					{
						FileName = null;
						goto retrySaveFileAs;
					}
					else
					{
						return false;
					}
				}
				dfr = dfrs[0];
			}

			DataFormat df = dfr.Create();
			DataFormatOptionsDialog.ShowDialog(ref df, DataFormatOptionsDialogType.Export);

			#region Save Code
			NotifySaving(doc);

			if (FileName == doc.FileName && doc.Accessor != null)
			{
				doc.Accessor.Close();
			}


			doc.DataFormat = df;

			FileAccessor file = new FileAccessor(doc.ObjectModel, doc.DataFormat);
			file.AllowWrite = true;
			file.ForceOverwrite = true;
			file.Open(FileName);
			file.Save();
			doc.Accessor = file;

			dcc.SelectedWindow.Title = System.IO.Path.GetFileName(FileName);
			doc.FileName = FileName;
			doc.IsSaved = true;
			#endregion
			return true;
		}
		*/

		public void Undo()
		{
			Editor current = GetCurrentWindowsFormsEditor();
			if (current == null) return;
			current.Undo();
		}
		public void Redo()
		{
			Editor current = GetCurrentWindowsFormsEditor();
			if (current == null) return;
			current.Redo();
		}

		/// <summary>
		/// Copies the currently selected item in the current editor
		/// onto the clipboard, and then deletes it. This will
		/// overwrite the data currently on the Windows clipboard, and
		/// append the data currently on the Universal Editor
		/// clipboard.
		/// </summary>
		public void Cut()
		{
			MessageBox.Show("Not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		/// <summary>
		/// Copies the currently selected item in the current editor
		/// onto the clipboard. This will overwrite the data currently
		/// on the Windows clipboard, and append the data currently on
		/// the Universal Editor clipboard.
		/// </summary>
		public void Copy()
		{
			MessageBox.Show("Not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		/// <summary>
		/// Pastes the data currently on the Windows clipboard into the
		/// current editor.
		/// </summary>
		public void Paste()
		{
			MessageBox.Show("Not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		/// <summary>
		/// Deletes the currently selected item in the current editor.
		/// </summary>
		public void Delete()
		{
			MessageBox.Show("Not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public IEditorImplementation GetCurrentEditor()
		{
			return GetCurrentWindowsFormsEditor();
		}
		public Editor GetCurrentWindowsFormsEditor()
		{
			if (dcc.SelectedWindow == null) return null;
			Pages.EditorPage editorPage = (dcc.SelectedWindow.Control as Pages.EditorPage);
			if (editorPage == null) return null;

			if (editorPage.Controls.Count < 1) return null;

			Editor current = null;
			if (editorPage.Controls[editorPage.Controls.Count - 1] is TabControl)
			{
				TabControl tbs = (editorPage.Controls[editorPage.Controls.Count - 1] as TabControl);
				current = (tbs.SelectedTab.Controls[0] as Editor);
			}
			else
			{
				current = (editorPage.Controls[editorPage.Controls.Count - 1] as Editor);
			}
			return current;
		}


		#region Project
		public void AddSolutionProjectNew()
		{
			NewProject(true);
		}
		public void AddSolutionProjectExisting()
		{
			OpenProject(true);
		}
		public void AddProjectItemNew()
		{

		}
		public void AddProjectItemExisting()
		{

		}
		#endregion
		#endregion

		#region Event Handlers
		#region Window
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			List<Document> unsavedDocuments = new List<Document>();
			foreach (AwesomeControls.DockingWindows.DockingWindow doc in dcc.Windows)
			{
				if (doc.Control is Pages.EditorPage)
				{
					Pages.EditorPage page = (doc.Control as Pages.EditorPage);
					if (page.Document != null && !page.Document.IsSaved)
					{
						unsavedDocuments.Add(page.Document);
					}
				}
			}

			if (unsavedDocuments.Count > 0)
			{
				UnsavedDocumentsDialog dlg = new UnsavedDocumentsDialog();
				foreach (Document d in unsavedDocuments)
				{
					AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
					lvi.Text = System.IO.Path.GetFileName(d.Title);
					lvi.Data = d;

					AwesomeControls.ListView.ListViewDetailChoice choice = new AwesomeControls.ListView.ListViewDetailChoice();
					choice.Options.Add("Save", UnsavedDocumentOption.Save);
					choice.Options.Add("Discard", UnsavedDocumentOption.Discard);
					choice.SelectedOption = choice.Options[0];
					lvi.Details.Add(choice);

					dlg.lv.Items.Add(lvi);
				}
				switch (dlg.ShowDialog())
				{
					case System.Windows.Forms.DialogResult.Yes:
					{
						foreach (AwesomeControls.ListView.ListViewItem lvi in dlg.lv.Items)
						{
							Document d = (lvi.Data as Document);
							AwesomeControls.ListView.ListViewDetailChoice choice = (lvi.Details[0] as AwesomeControls.ListView.ListViewDetailChoice);
							if (choice.SelectedOption != null)
							{
								switch ((UnsavedDocumentOption)choice.SelectedOption.Value)
								{
									case UnsavedDocumentOption.Save:
									{
										SaveDocument(d);
										break;
									}
									case UnsavedDocumentOption.Discard:
									{
										break;
									}
								}
							}
						}
						break;
					}
					case System.Windows.Forms.DialogResult.No:
					{
						break;
					}
					case System.Windows.Forms.DialogResult.Cancel:
					{
						e.Cancel = true;
						break;
					}
				}
			}
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			if (WindowClosed != null) WindowClosed(this, e);

			if (!WindowsFormsEngine.SessionLoading && Engine.CurrentEngine.Windows.Count == 0)
			{
				Application.Exit();
			}
		}
		#endregion
		#region Menu Bar
		#region File
		private void FileNewFile_Click(object sender, EventArgs e)
		{
			NewFile();
		}
		private void FileNewProject_Click(object sender, EventArgs e)
		{
			NewProject();
		}

		#region Save
		private void FileSaveFile_Click(object sender, EventArgs e)
		{
			SaveFile();
		}
		private void FileSaveFileAs_Click(object sender, EventArgs e)
		{
			SaveFileAs();
		}
		private void FileSaveProject_Click(object sender, EventArgs e)
		{
			SaveProject();
		}
		private void FileSaveProjectAs_Click(object sender, EventArgs e)
		{
			SaveProjectAs();
		}
		private void FileSaveAll_Click(object sender, EventArgs e)
		{
			SaveAll();
		}
		#endregion
		#region Close   
		public void CloseFile()
		{
			if (dcc.SelectedWindow != null)
			{
				dcc.SelectedWindow.Close();
			}
			else
			{
				CloseWindow();
			}
		}
		private void FileCloseProject_Click(object sender, EventArgs e)
		{
			CurrentSolution = null;
		}
		#endregion
		private void FilePrint_Click(object sender, EventArgs e)
		{

		}
		private void mnuFileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion
		#region Edit
		private void EditUndo_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void EditRedo_Click(object sender, EventArgs e)
		{
			Redo();
		}

		private void EditCut_Click(object sender, EventArgs e)
		{
			Cut();
		}
		private void EditCopy_Click(object sender, EventArgs e)
		{
			Copy();
		}
		private void EditPaste_Click(object sender, EventArgs e)
		{
			Paste();
		}
		private void EditDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}
		#endregion
		#region View
		private void mnuViewFullScreen_Click(object sender, EventArgs e)
		{
			FullScreen = !FullScreen;
		}

		private void mnuViewStatusBar_Click(object sender, EventArgs e)
		{
			sbStatusBar.Visible = mnuViewStatusBar.Checked;
		}

		private void mnuViewToolbarsToolbar_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem mnuViewToolbarsToolbar = (sender as ToolStripMenuItem);
			Toolbar toolbar = (mnuViewToolbarsToolbar.Tag as Toolbar);
			toolbar.Visible = !toolbar.Visible;
		}
		private void mnuViewToolbarsCustomize_Click(object sender, EventArgs e)
		{
			cbc.ShowCustomizeDialog();
		}

		private void mnuViewStartPage_Click(object sender, EventArgs e)
		{
			ShowStartPage();
		}
		#endregion
		#region Project
		private void mnuProjectAddNewItem_Click(object sender, EventArgs e)
		{
			AddProjectItemNew();
		}

		private void mnuProjectAddExistingItem_Click(object sender, EventArgs e)
		{
			AddProjectItemExisting();
		}

		private void mnuProjectExclude_Click(object sender, EventArgs e)
		{

		}

		private void mnuProjectShowAllFiles_Click(object sender, EventArgs e)
		{

		}

		private void mnuProjectProperties_Click(object sender, EventArgs e)
		{

		}
		#endregion
		#endregion
		#endregion

		#region Editor Page Events
		private void page_DocumentEdited(object sender, EventArgs e)
		{
			Pages.EditorPage page = (sender as Pages.EditorPage);
			AwesomeControls.DockingWindows.DockingWindow doc = dcc.Windows[page];
			if (doc == null) return;

			if (String.IsNullOrEmpty(page.Document.Title))
			{
				doc.Title = "<untitled> (*)";
			}
			else
			{
				doc.Title = System.IO.Path.GetFileName(page.Document.Title) + " (*)";
			}
			page.Document.IsChanged = true;
		}
		private void page_Navigate(object sender, UniversalEditor.UserInterface.WindowsForms.Controls.NavigateEventArgs e)
		{
			OpenFile(e.FileName, ((Control.ModifierKeys & Keys.Alt) != Keys.Alt));
		}
		#endregion

		private bool mvarFullScreen = false;
		public bool FullScreen
		{
			get { return mvarFullScreen; }
			set
			{
				mvarFullScreen = value;
				mnuViewFullScreen.Checked = mvarFullScreen;

				if (mvarFullScreen)
				{
					this.cbc.TopToolStripPanel.Visible = false;
					this.cbc.BottomToolStripPanel.Visible = false;
					this.cbc.LeftToolStripPanel.Visible = false;
					this.cbc.RightToolStripPanel.Visible = false;

					tmrToolStripContainerPopup.Enabled = true;

					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
					this.WindowState = FormWindowState.Maximized;
				}
				else
				{
					this.cbc.TopToolStripPanel.Visible = true;
					this.cbc.BottomToolStripPanel.Visible = true;
					this.cbc.LeftToolStripPanel.Visible = true;
					this.cbc.RightToolStripPanel.Visible = true;

					tmrToolStripContainerPopup.Enabled = false;

					this.WindowState = FormWindowState.Normal;
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
				}
			}
		}
		
		#region Recent Files
		private void AddRecentMenuItem(string FileName)
		{
			ToolStripMenuItem tsmi = new ToolStripMenuItem();
			tsmi.Click += new EventHandler(tsmiRecentFile_Click);

			tsmi.ToolTipText = FileName;
			tsmi.Text = System.IO.Path.GetFileName(FileName);

			mnuFileRecentFiles.DropDownItems.Add(tsmi);
		}
		private void RefreshRecentFilesList()
		{
			mnuFileRecentFiles.DropDownItems.Clear();
			foreach (string fileName in Engine.CurrentEngine.RecentFileManager.FileNames)
			{
				AddRecentMenuItem(fileName);
			}

			mnuFileRecentFiles.Visible = (mnuFileRecentFiles.DropDownItems.Count > 0);
			mnuFileRecentProjects.Visible = (mnuFileRecentProjects.DropDownItems.Count > 0);
			mnuFileSep3.Visible = ((mnuFileRecentFiles.DropDownItems.Count > 0) || (mnuFileRecentProjects.DropDownItems.Count > 0));
		}
		private void tsmiRecentFile_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			if (!System.IO.File.Exists(tsmi.ToolTipText))
			{
				if (MessageBox.Show("The file or folder '" + tsmi.ToolTipText + "' cannot be opened.  Do you want to remove the reference(s) to it from the Recent list(s)?", "File Does Not Exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					Engine.CurrentEngine.RecentFileManager.FileNames.Remove(tsmi.ToolTipText);
				}
				return;
			}
			OpenFile(tsmi.ToolTipText);
		}
		#endregion

		public void ShowStartPage()
		{
			DockingWindow dwStartPage = dcc.Windows["pnlStartPage"];
			if (dwStartPage == null)
			{
				Pages.StartPage sp = new Pages.StartPage();
				sp.NewProjectClicked += delegate(object sender, EventArgs e)
				{
					Engine.CurrentEngine.Commands["FileNewProject"].Execute();
				};
				sp.OpenProjectClicked += delegate(object sender, EventArgs e)
				{
					Engine.CurrentEngine.Commands["FileOpenProject"].Execute();
				};

				dwStartPage = dcc.Windows.Add("pnlStartPage", "Start Page", sp);
				dcc.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows.Add(dwStartPage);
			}
			dcc.SwitchTab(dwStartPage);
		}
		
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.D && e.Control && e.Alt)
			{
				cboAddress.Focus();
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		private void mnuWindowWindows_Click(object sender, EventArgs e)
		{
			dcc.DisplayWindowListDialog();
		}

		private void ToolsCustomize_Click(object sender, EventArgs e)
		{
			cbc.ShowCustomizeDialog();
		}

		private void ToolsOptions_Click(object sender, EventArgs e)
		{
		}

		private OptionsDialog dlgOptions = null;
		public bool ShowOptionsDialog()
		{
			if (dlgOptions == null) dlgOptions = new OptionsDialog();
			if (dlgOptions.IsDisposed) dlgOptions = new OptionsDialog();
			if (dlgOptions.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				return true;
			}
			return false;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			Glue.Common.Methods.InitializeCustomizableMenuItems(mbMenuBar);

			ShowStartPage();
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			Engine.CurrentEngine.LastWindow = this;
			HostApplication.CurrentWindow = this;
		}

		private void tmrToolStripContainerPopup_Tick(object sender, EventArgs e)
		{
			Screen scr =  Screen.FromControl(this);
			
			cbc.LeftToolStripPanel.Visible = (Cursor.Position.X >= scr.Bounds.X && Cursor.Position.X <= scr.Bounds.X + 4);
			cbc.RightToolStripPanel.Visible = (Cursor.Position.X <= scr.Bounds.Right && Cursor.Position.X >= scr.Bounds.Right - 4);
			cbc.TopToolStripPanel.Visible = ((cbc.TopToolStripPanel.Visible && Cursor.Position.Y <= cbc.TopToolStripPanel.Height) || (Cursor.Position.Y >= scr.Bounds.Y && Cursor.Position.Y <= scr.Bounds.Y + 4));
			cbc.BottomToolStripPanel.Visible = (Cursor.Position.Y <= scr.Bounds.Bottom && Cursor.Position.Y >= scr.Bounds.Bottom - 4);
		}

		private void MainWindow_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
			if (e.Data.GetDataPresent("FileNameW"))
			{
				e.Effect = DragDropEffects.Link;
			}
		}

		private void MainWindow_DragDrop(object sender, DragEventArgs e)
		{
			string[] formats = e.Data.GetFormats();

			if (e.Data.GetDataPresent("Rich Text Format"))
			{
				string rtf = (e.Data.GetData("Rich Text Format") as string);
				
			}
			else if (e.Data.GetDataPresent("System.String"))
			{
				string rtf = (e.Data.GetData("System.String") as string);
			}
			else if (e.Data.GetDataPresent("UnicodeText"))
			{
				string rtf = (e.Data.GetData("UnicodeText") as string);
			}
			else if (e.Data.GetDataPresent("Text"))
			{
				string rtf = (e.Data.GetData("Text") as string);
			}
			else if (e.Data.GetDataPresent("FileNameW"))
			{
				string[] filenames = (e.Data.GetData("FileNameW") as string[]);
				OpenFile(filenames);
			}
			else if (e.Data.GetDataPresent("Shell IDList Array"))
			{
				System.IO.MemoryStream ms = (e.Data.GetData("Shell IDList Array") as System.IO.MemoryStream);
				Reader br = new Reader(new StreamAccessor(ms));
				br.Accessor.Position = 0;
				int unknown1 = br.ReadInt32();
				int unknown2 = br.ReadInt32();
				int unknown3 = br.ReadInt32();
				short unknown4 = br.ReadInt16();

				int unknown5 = br.ReadInt32();

				Guid guid = br.ReadGuid();
				short terminator = br.ReadInt16();

				OpenFile("shell://" + guid.ToString("B"));
			}
		}

		private void mnuWindowNewWindow_Click(object sender, EventArgs e)
		{
			Engine.CurrentEngine.OpenWindow();
		}

		private void cboAddress_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				OpenFile(cboAddress.Text, !e.Alt);
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}

		#region Document Container
		private void dcc_SelectedWindowChanged(object sender, EventArgs e)
		{
			if (dcc.SelectedWindow == null) return;
			if (dcc.SelectedWindow.ParentArea == null) return;
			if (dcc.SelectedWindow.ParentArea.Position != DockPosition.Center) return;

			if (!(dcc.SelectedWindow.Control is Pages.EditorPage))
			{
				mvarCurrentDocument = null;
				RefreshDocument();
				return;
			}

			Pages.EditorPage editorPage = (dcc.SelectedWindow.Control as Pages.EditorPage);
			cboAddress.Text = editorPage.FileName;

			if (mvarCurrentDocument != editorPage.Document)
			{
				mvarCurrentDocument = editorPage.Document;
			}
			RefreshDocument();
		}
		private void dcc_WindowClosing(object sender, AwesomeControls.DockingWindows.WindowClosingEventArgs e)
		{
			if (e.Window == null) return;
			if (e.Window.Control is Pages.EditorPage)
			{
				Editor editor = GetCurrentWindowsFormsEditor();
				if (editor != null)
				{
					CancelEventArgs ce = new CancelEventArgs();
					editor.NotifyClosing(ce);
					if (ce.Cancel)
					{
						e.Cancel = true;
						return;
					}
					editor.NotifyClosed(EventArgs.Empty);
				}

				Pages.EditorPage editorP = (e.Window.Control as Pages.EditorPage);
				if (editorP.Document != null)
				{
					if (editorP.Document.IsChanged)
					{
						switch (MessageBox.Show("The file has been changed and not saved.  Do you wish to save the changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
						{
							case System.Windows.Forms.DialogResult.Yes:
							{
								if (!SaveDocument(editorP.Document))
								{
									e.Cancel = true;
								}
								break;
							}
							case System.Windows.Forms.DialogResult.No:
							{
								break;
							}
							case System.Windows.Forms.DialogResult.Cancel:
							{
								e.Cancel = true;
								break;
							}
						}
					}

					editorP.Document.Close();
				}
			}
		}
		private void dcc_WindowClosed(object sender, WindowClosedEventArgs e)
		{
			if (dcc.SelectedWindow == null)
			{
				mvarCurrentDocument = null;
				RefreshDocument();
				return;
			}
			if (dcc.SelectedWindow.ParentArea == null) return;
			if (dcc.SelectedWindow.ParentArea.Position != DockPosition.Center) return;

			RefreshDocument();
		}
		#endregion

		private void mnuViewPanelsProjectExplorer_Click(object sender, EventArgs e)
		{
		}

		private void mnuHelpAbout_Click(object sender, EventArgs e)
		{
		}
		
		private void lblDataFormat_Click(object sender, EventArgs e)
		{
			mnuContextDocumentType.Show(sbStatusBar.PointToScreen(new Point(lblObjectModel.Bounds.Right, lblObjectModel.Bounds.Top)), ToolStripDropDownDirection.AboveLeft);
		}

		/// <summary>
		/// Creates a Bookmark for the specified document.
		/// </summary>
		/// <param name="doc"></param>
		public void AddBookmark(Document doc)
		{
			if (doc.Accessor is FileAccessor)
			{
				if (!Engine.CurrentEngine.BookmarksManager.FileNames.Contains((doc.Accessor as FileAccessor).FileName))
				{
					ToolStripMenuItem mnu = new ToolStripMenuItem();
					mnu.Text = System.IO.Path.GetFileName((doc.Accessor as FileAccessor).FileName);
					mnu.ToolTipText = (doc.Accessor as FileAccessor).FileName;
					mnu.Click += tsmiBookmark_Click;
					mnuBookmarks.DropDownItems.Insert(mnuBookmarks.DropDownItems.Count - 2, mnu);
					mnuBookmarksSep1.Visible = true;

					Engine.CurrentEngine.BookmarksManager.FileNames.Add((doc.Accessor as FileAccessor).FileName);
				}
			}
		}
		
		private void mnuBookmarksAdd_Click(object sender, EventArgs e)
		{
			if (CurrentDocument == null) return;

			AddBookmark(CurrentDocument);
		}
		
		private void mnuBookmarksAddAll_Click(object sender, EventArgs e)
		{
			foreach (DockingWindow dw in dcc.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows)
			{
				if (dw.Control is Pages.EditorPage)
				{
					Document doc = (dw.Control as Pages.EditorPage).Document;
					AddBookmark(doc);
				}
			}
		}

		private void mnuToolsSessionManager_Click(object sender, EventArgs e)
		{
			SessionDialog dlg = new SessionDialog();
			dlg.ShowDialog();
		}

		public void CloseWindow()
		{
			this.Close();
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Document> Documents
		{
			get
			{
				List<Document> documents = new List<Document>();
				foreach (AwesomeControls.DockingWindows.DockingWindow doc in dcc.Windows)
				{
					if (doc.Control is Pages.EditorPage)
					{
						documents.Add((doc.Control as Pages.EditorPage).Document);
					}
				}
				return new System.Collections.ObjectModel.ReadOnlyCollection<Document>(documents);
			}
		}


		public void UpdateStatus(string statusText)
		{
			lblStatus.Text = statusText;
		}

		public void UpdateProgress(bool visible)
		{
			pbProgress.Visible = visible;
		}

		public void UpdateProgress(int minimum, int maximium, int value)
		{
			pbProgress.Minimum = minimum;
			pbProgress.Maximum = maximium;
			pbProgress.Value = value;
		}

		private void mnuHelpLicensingAndActivation_Click(object sender, EventArgs e)
		{
			MessageBox.Show("This application has already been activated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void HelpContents_Click(object sender, EventArgs e)
		{

		}
	}
}
