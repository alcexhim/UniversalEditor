using System;

using UniversalEditor.UserInterface;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;

namespace UniversalEditor.Engines.UWT
{
	public class MainWindow : Window, IHostApplicationWindow
	{
		public MainWindow ()
		{

			foreach (CommandItem ci in UniversalEditor.UserInterface.Engine.CurrentEngine.MainMenu.Items) {
				UniversalWidgetToolkit.MenuItem mi = LoadMenuItem (ci);
				if (mi == null)
					continue;
				this.MenuBar.Items.Add (mi);
			}

		}

		private UniversalWidgetToolkit.MenuItem LoadMenuItem(CommandItem ci)
		{
			if (ci is CommandReferenceCommandItem) {
				CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);

				Command cmd = UniversalEditor.UserInterface.Engine.CurrentEngine.Commands [crci.CommandID];
				if (cmd != null) {
					CommandMenuItem mi = new CommandMenuItem (cmd.Title);
					foreach (CommandItem ci1 in cmd.Items) {
						UniversalWidgetToolkit.MenuItem mi1 = LoadMenuItem (ci1);
						mi.Items.Add (mi1);
					}
					return mi;
				} else {
					Console.WriteLine ("attempted to load unknown cmd '" + crci.CommandID + "'");
				}
				return null;
			} else if (ci is SeparatorCommandItem) {
				return new UniversalWidgetToolkit.SeparatorMenuItem ();
			}
			return null;
		}

		#region IHostApplicationWindow implementation

		public void NewFile ()
		{
			throw new NotImplementedException ();
		}

		public void NewProject (bool combineObjects = false)
		{
			throw new NotImplementedException ();
		}

		public void OpenFile ()
		{
			throw new NotImplementedException ();
		}

		public void OpenFile (params string[] fileNames)
		{
			throw new NotImplementedException ();
		}

		public void OpenFile (params Document[] documents)
		{
			throw new NotImplementedException ();
		}

		public void OpenProject (bool combineObjects = false)
		{
			throw new NotImplementedException ();
		}

		public void OpenProject (string FileName, bool combineObjects = false)
		{
			throw new NotImplementedException ();
		}

		public void SaveFile ()
		{
			throw new NotImplementedException ();
		}

		public void SaveFileAs ()
		{
			throw new NotImplementedException ();
		}

		public void SaveFileAs (string FileName, DataFormat df)
		{
			throw new NotImplementedException ();
		}

		public void SaveProject ()
		{
			throw new NotImplementedException ();
		}

		public void SaveProjectAs ()
		{
			throw new NotImplementedException ();
		}

		public void SaveProjectAs (string FileName, DataFormat df)
		{
			throw new NotImplementedException ();
		}

		public void SaveAll ()
		{
			throw new NotImplementedException ();
		}

		public void SwitchPerspective (int index)
		{
			throw new NotImplementedException ();
		}

		public void CloseFile ()
		{
			throw new NotImplementedException ();
		}

		public void CloseProject ()
		{
			throw new NotImplementedException ();
		}

		public void CloseWindow ()
		{
			throw new NotImplementedException ();
		}

		public IEditorImplementation GetCurrentEditor ()
		{
			throw new NotImplementedException ();
		}

		public bool ShowOptionsDialog ()
		{
			throw new NotImplementedException ();
		}

		public void ToggleMenuItemEnabled (string menuItemName, bool enabled)
		{
			throw new NotImplementedException ();
		}

		public void RefreshCommand (object nativeCommandObject)
		{
			throw new NotImplementedException ();
		}

		public void UpdateStatus (string statusText)
		{
			throw new NotImplementedException ();
		}

		public void UpdateProgress (bool visible)
		{
			throw new NotImplementedException ();
		}

		public void UpdateProgress (int minimum, int maximium, int value)
		{
			throw new NotImplementedException ();
		}

		public void ActivateWindow ()
		{
			throw new NotImplementedException ();
		}

		public void ShowStartPage ()
		{
			throw new NotImplementedException ();
		}

		public void SetWindowListVisible (bool visible, bool modal)
		{
			throw new NotImplementedException ();
		}

		public event EventHandler WindowClosed;

		public bool FullScreen { get; set; }

		#endregion
	}
}

