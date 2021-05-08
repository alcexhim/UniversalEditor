//
//  CodeCommentElement.cs - represents a CodeElement that is ignored by the compiler
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

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	/// <summary>
	/// Represents a <see cref="CodeElement" /> that is ignored by the compiler.
	/// </summary>
	public class CodeCommentElement : CodeElement
	{
		public CodeCommentElement(string content = null, bool multiline = false, bool isDocumentationComment = false)
		{
			if (content == null) content = String.Empty;
			mvarContent = content;
			mvarMultiline = multiline;
			mvarIsDocumentationComment = isDocumentationComment;
		}

		private string mvarContent = String.Empty;
		public string Content { get { return mvarContent; } set { mvarContent = value; } }

		private bool mvarMultiline = false;
		public bool Multiline { get { return mvarMultiline; } set { mvarMultiline = value; } }

		private bool mvarIsDocumentationComment = false;
		public bool IsDocumentationComment { get { return mvarIsDocumentationComment; } set { mvarIsDocumentationComment = value; } }
	}
}
