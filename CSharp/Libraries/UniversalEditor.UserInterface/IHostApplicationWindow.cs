using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public interface IHostApplicationWindow
	{
		event EventHandler WindowClosed;

		void NewFile();
		void NewProject(bool combineObjects = false);

		void OpenFile();
		void OpenFile(params string[] FileNames);
		void OpenProject(bool combineObjects = false);
		void OpenProject(string FileName, bool combineObjects = false);

		void SaveFile();
		void SaveFileAs();
		void SaveFileAs(string FileName, DataFormat df);
		
		void SaveProject();
		void SaveProjectAs();
		void SaveProjectAs(string FileName, DataFormat df);

		void SaveAll();

		/// <summary>
		/// Switches the current window's perspective.
		/// </summary>
		/// <param name="index">The index of the perspective to switch to.</param>
		void SwitchPerspective(int index);

		void CloseFile();
		void CloseWindow();
		
		IEditorImplementation GetCurrentEditor();

		bool FullScreen { get; set; }

		/// <summary>
		/// Displays the "Options" dialog (on Windows, under the "Tools" menu; on Linux, under the "Edit"
		/// menu, labeled as "Preferences").
		/// </summary>
		/// <returns>True if the user accepted the dialog; false otherwise.</returns>
		bool ShowOptionsDialog();
		
		void ToggleMenuItemEnabled(string menuItemName, bool enabled);
		void RefreshCommand(object nativeCommandObject);

		void UpdateStatus(string statusText);
		
		void UpdateProgress(bool visible);
		void UpdateProgress(int minimum, int maximium, int value);

		void ActivateWindow();

		void ShowStartPage();
	}
	public class IHostApplicationWindowCollection
		: System.Collections.ObjectModel.Collection<IHostApplicationWindow>
	{

	}
}
