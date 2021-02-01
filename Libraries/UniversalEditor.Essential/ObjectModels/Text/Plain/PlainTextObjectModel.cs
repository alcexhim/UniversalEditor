//
//  PlainTextObjectModel.cs - provides an ObjectModel for manipulating text files in unformatted plain text format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace UniversalEditor.ObjectModels.Text.Plain
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating text files in unformatted plain text format.
	/// </summary>
	public class PlainTextObjectModel : ObjectModel
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Text", "Plain", "Plain text document" };
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
		public string Text { get { return String.Join(LineTerminator, Lines); } set { mvarText = value; RebuildLines(); } }

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

		private int indentSize = 4;
		public string GetIndent(int length)
		{
			return new string(' ', indentSize * length);
		}

		public void Write(string value)
		{
			if (Lines.Count < 1)
			{
				Lines.Add(value);
			}
			else
			{
				Lines[Lines.Count - 1] = Lines[Lines.Count - 1] + value;
			}
		}
		public void WriteLine(string value = "")
		{
			Write(value + LineTerminator);
		}
	}
}
