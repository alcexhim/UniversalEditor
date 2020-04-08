//
//  TextEditorSelection.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Text
{
	public class TextEditorSelection : EditorSelection
	{
		public int Start { get; set; } = -1;
		public int Length { get; set; } = -1;

		private string mvarContent = null;
		public override object Content
		{
			get => mvarContent;
			set
			{
				mvarContent = (value is string ? (string)value : null);
				if (mvarContent == null)
				{
					Length = 0;
					(Editor as TextEditor).ClearSelectedText();
				}
			}
		}

		protected override void DeleteInternal()
		{
			Length = 0;
			(Editor as TextEditor).ClearSelectedText();
		}

		internal TextEditorSelection(TextEditor parent, string text)
			: base(parent)
		{
			mvarContent = text;
		}
		internal TextEditorSelection(TextEditor parent, string text, int start, int length)
			: base(parent)
		{
			mvarContent = text;
			Start = start;
			Length = length;
		}
	}
}
