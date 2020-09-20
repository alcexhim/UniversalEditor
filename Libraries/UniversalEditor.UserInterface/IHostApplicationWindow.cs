using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.Docking;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.Solution;

namespace UniversalEditor.UserInterface
{
	public interface IHostApplicationWindow
	{
		Document NewFile();
		void NewProject(bool combineObjects = false);

		void OpenFile();
		void OpenFile(params string[] fileNames);
		void OpenFile(params Document[] documents);
		void OpenProject(bool combineObjects = false);
		void OpenProject(string FileName, bool combineObjects = false);

		bool SaveFile();
		bool SaveFileAs();
		bool SaveFileAs(Accessor accessor, DataFormat df);
		
		void SaveProject();
		void SaveProjectAs();
		void SaveProjectAs(string FileName, DataFormat df);

		void SaveAll();

		/// <summary>
		/// Switches the current window's perspective.
		/// </summary>
		/// <param name="index">The index of the perspective to switch to.</param>
		void SwitchPerspective(int index);

		void CloseFile(DockingWindow dw = null);
		void CloseProject();
		void CloseWindow();

		void PrintDocument();

		void ShowDocumentPropertiesDialog();

		/// <summary>
		/// Gets the <see cref="Page" />s (center-docked <see cref="DockingWindow" />s) contained within all <see cref="DockingContainer" />s in this
		/// <see cref="IHostApplicationWindow" />.
		/// </summary>
		/// <returns>The <see cref="Page" />s contained within all <see cref="DockingContainer" />s in this <see cref="IHostApplicationWindow" />.</returns>
		Page[] GetPages();

		Editor GetCurrentEditor();

		Control ActiveControl { get; }

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

		void Present(DateTime timestamp = default(DateTime));

		void ShowStartPage();

		/// <summary>
		/// Shows or hides the window list based on the given options.
		/// </summary>
		/// <param name="visible">True if the window list should be shown; false if the window list should be hidden.</param>
		/// <param name="modal">True if the window list should be presented as a modal dialog; false if it should be presented as a popup (for example, during a window switch action).</param>
		void SetWindowListVisible(bool visible, bool modal);

		StatusBar StatusBar { get; }

		ProjectObjectModel CurrentProject { get; set; }
		SolutionObjectModel CurrentSolution { get; set; }
	}
	public class IHostApplicationWindowCollection
		: System.Collections.ObjectModel.Collection<IHostApplicationWindow>
	{

	}
}
