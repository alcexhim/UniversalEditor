using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Plain
{
	public class PlainTextObjectModel : ObjectModel
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Text Document";
				_omr.Path = new string[] { "General", "Text", "Plain" };
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarText = String.Empty;
			mvarLines.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			if (where is PlainTextObjectModel)
			{
				(where as PlainTextObjectModel).Text = mvarText;
				return;
			}
			throw new InvalidCastException();
		}

		private System.Collections.Specialized.StringCollection mvarLines = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Lines { get { return mvarLines; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; RebuildLines(); } }

		private string mvarLineTerminator = System.Environment.NewLine;
		public string LineTerminator { get { return mvarLineTerminator; } set { mvarLineTerminator = value; RebuildLines(); } }

		private void RebuildLines()
		{
			mvarLines.Clear();
			string[] splittt = mvarText.Split(new string[] { mvarLineTerminator }, StringSplitOptions.None);
			foreach (string splitt in splittt)
			{
				mvarLines.Add(splitt);
			}
		}
	}
}
