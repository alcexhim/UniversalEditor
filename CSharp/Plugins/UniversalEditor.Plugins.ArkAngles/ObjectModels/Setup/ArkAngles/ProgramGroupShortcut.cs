using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles
{
	public class ProgramGroupShortcut : ICloneable
	{
		public class ProgramGroupShortcutCollection
			: System.Collections.ObjectModel.Collection<ProgramGroupShortcut>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		private string mvarIconFileName = null;
		/// <summary>
		/// The file name of the icon to display in the shortcut.
		/// </summary>
		public string IconFileName { get { return mvarIconFileName; } set { mvarIconFileName = value; } }

		private int? mvarIconIndex = null;
		/// <summary>
		/// The index of the icon to display in the shortcut.
		/// </summary>
		public int? IconIndex { get { return mvarIconIndex; } set { mvarIconIndex = value; } }

		public object Clone()
		{
			ProgramGroupShortcut clone = new ProgramGroupShortcut();
			clone.FileName = (mvarFileName.Clone() as string);
			clone.IconFileName = (mvarIconFileName.Clone() as string);
			clone.IconIndex = mvarIconIndex;
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
