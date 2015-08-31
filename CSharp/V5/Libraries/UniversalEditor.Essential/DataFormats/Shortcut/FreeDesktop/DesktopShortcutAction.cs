using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Shortcut.FreeDesktop
{
	public class DesktopShortcutAction
	{
		public class DesktopShortcutActionCollection
			: System.Collections.ObjectModel.Collection<DesktopShortcutAction>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTitle = String.Empty;
		/// <summary>
		/// Label that will be shown to the user. Since actions are always shown in the context of
		/// a specific application (that is, as a submenu of a launcher), this only needs to be
		/// unambiguous within one application and should not include the application name.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarIconFileName = String.Empty;
		/// <summary>
		/// Icon to be shown togheter with the action. If the name is an absolute path, the given
		/// file will be used. If the name is not an absolute path, the algorithm described in the
		/// Icon Theme Specification will be used to locate the icon. Implementations may choose
		/// to ignore it.
		/// </summary>
		public string IconFileName { get { return mvarIconFileName; } set { mvarIconFileName = value; } }

		private string mvarExecutableFileName = String.Empty;
		/// <summary>
		/// Program to execute for this action. See the Exec key for details on how this key
		/// works. The Exec key is required if DBusActivatable is not set to true in the main
		/// desktop entry group. Even if DBusActivatable is true, Exec should be specified for
		/// compatibility with implementations that do not understand DBusActivatable.
		/// </summary>
		public string ExecutableFileName { get { return mvarExecutableFileName; } set { mvarExecutableFileName = value; } }

		private System.Collections.Specialized.StringCollection mvarExecutableArguments = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// Arguments passed into the associated program when it is executed.
		/// </summary>
		public System.Collections.Specialized.StringCollection ExecutableArguments { get { return mvarExecutableArguments; } }
	}
}
