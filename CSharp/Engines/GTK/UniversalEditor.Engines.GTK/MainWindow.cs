using System;

using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.GTK
{
	public class MainWindow : GtkWindow, IHostApplicationWindow
	{
		public MainWindow ()
		{
			this.Text = "Universal Editor";


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

