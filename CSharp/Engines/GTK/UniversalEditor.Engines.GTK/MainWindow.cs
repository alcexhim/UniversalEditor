using System;

using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.GTK
{
	public class MainWindow : GtkWindow, IHostApplicationWindow
	{
		private GtkWidget LoadCommandItem(CommandItem ci)
		{
			if (ci is CommandReferenceCommandItem)
			{
				CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);

				Command cmd = Engine.CurrentEngine.Commands [crci.CommandID];
				if (cmd == null)
					return null;

				GtkMenuItem miFile = new GtkMenuItem ();
				miFile.Text = cmd.Title;

				if (cmd.Items.Count > 0)
				{
					GtkMenu miFile_Menu = new GtkMenu ();
					miFile.Menu = miFile_Menu;

					foreach (CommandItem ci1 in cmd.Items)
					{
						GtkWidget mi1 = LoadCommandItem (ci1);
						if (mi1 == null)
							continue;

						miFile_Menu.Items.Add (mi1);
					}
				}
				return miFile;
			}
			else if (ci is SeparatorCommandItem)
			{
				GtkSeparator sep = new GtkSeparator (GtkBoxOrientation.Horizontal);
				return sep;
			}
			return null;
		}


		public MainWindow ()
		{
			this.Text = "Universal Editor";

			GtkMenuBar mbMenuBar = new GtkMenuBar ();

			foreach (CommandItem ci in Engine.CurrentEngine.MainMenu.Items)
			{
				GtkWidget mi = LoadCommandItem (ci);
				if (mi == null)
					continue;

				mbMenuBar.Items.Add (mi);
			}

			GtkBox box = new GtkBox (GtkBoxOrientation.Vertical, 5);
			box.Pack (PackDirection.Start, mbMenuBar, false, false, 3);
			Controls.Add (box);
		}

		protected override void OnClosed (EventArgs e)
		{
			base.OnClosed (e);

			GtkApplication.Quit ();
		}
		
		public event EventHandler WindowClosed;
		protected virtual void OnWindowClosed(EventArgs e)
		{
			if (WindowClosed != null)
				WindowClosed (this, e);
		}

		public void NewFile()
		{
		}
		public void NewProject(bool combineObjects = false)
		{
		}

		public void OpenFile() { }
		public void OpenFile(params string[] fileNames) { }
		public void OpenFile(params Document[] documents) { }
		public void OpenProject(bool combineObjects = false) { }
		public void OpenProject(string FileName, bool combineObjects = false) { }

		public void SaveFile() { }
		public void SaveFileAs() { }
		public void SaveFileAs(string FileName, DataFormat df) { }

		public void SaveProject() { }
		public void SaveProjectAs() { }
		public void SaveProjectAs(string FileName, DataFormat df) { }

		public void SaveAll() { }

		/// <summary>
		/// Switches the current window's perspective.
		/// </summary>
		/// <param name="index">The index of the perspective to switch to.</param>
		public void SwitchPerspective(int index) { }

		public void CloseFile() { }
		public void CloseProject() { }
		public void CloseWindow() { }

		public IEditorImplementation GetCurrentEditor()
		{
			return null;
		}

		public bool FullScreen { get; set; }

		/// <summary>
		/// Displays the "Options" dialog (on Windows, under the "Tools" menu; on Linux, under the "Edit"
		/// menu, labeled as "Preferences").
		/// </summary>
		/// <returns>True if the user accepted the dialog; false otherwise.</returns>
		public bool ShowOptionsDialog() { return false; }

		public void ToggleMenuItemEnabled(string menuItemName, bool enabled) { }
		public void RefreshCommand(object nativeCommandObject) { }

		public void UpdateStatus(string statusText) { }

		public void UpdateProgress(bool visible) { }
		public void UpdateProgress(int minimum, int maximium, int value) { }

		public void ActivateWindow()
		{

		}

		public void ShowStartPage() { }

		/// <summary>
		/// Shows or hides the window list based on the given options.
		/// </summary>
		/// <param name="visible">True if the window list should be shown; false if the window list should be hidden.</param>
		/// <param name="modal">True if the window list should be presented as a modal dialog; false if it should be presented as a popup (for example, during a window switch action).</param>
		public void SetWindowListVisible(bool visible, bool modal) { }
	}
}

