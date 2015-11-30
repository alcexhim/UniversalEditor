using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Help.Compiled
{
	public class CompiledHelpObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "WinHelp compiled documentation file";
				_omr.Path = new string[] { "Documentation Writer", "Compiled Documentation" };
			}
			return _omr;
		}

		public override void Clear()
		{
		}

		public override void CopyTo(ObjectModel where)
		{
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarCopyright = String.Empty;
		public string Copyright { get { return mvarCopyright; } set { mvarCopyright = value; } }
	}
}
