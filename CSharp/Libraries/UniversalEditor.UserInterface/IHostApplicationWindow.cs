using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public interface IHostApplicationWindow
	{
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
		void SaveProjectAs(string FileName);

		void SaveAll();
		
		void ToggleMenuItemEnabled(string menuItemName, bool enabled);
		void RefreshCommand(object nativeCommandObject);

		void UpdateStatus(string statusText);
		
		void UpdateProgress(bool visible);
		void UpdateProgress(int minimum, int maximium, int value);

		void ActivateWindow();
	}
}
