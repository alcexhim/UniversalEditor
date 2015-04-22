using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles.Actions
{
	public class ProgramGroupAction : Action
	{
		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the Program Group to create.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private ProgramGroupShortcut.ProgramGroupShortcutCollection mvarShortcuts = new ProgramGroupShortcut.ProgramGroupShortcutCollection();
		/// <summary>
		/// The shortcuts added to this Program Group.
		/// </summary>
		public ProgramGroupShortcut.ProgramGroupShortcutCollection Shortcuts { get { return mvarShortcuts; } }

		public override object Clone()
		{
			ProgramGroupAction clone = new ProgramGroupAction();
			foreach (ProgramGroupShortcut item in mvarShortcuts)
			{
				clone.Shortcuts.Add(item.Clone() as ProgramGroupShortcut);
			}
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
