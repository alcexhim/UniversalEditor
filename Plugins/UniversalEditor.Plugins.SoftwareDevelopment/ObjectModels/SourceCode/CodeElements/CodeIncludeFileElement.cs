//
//  CodeIncludeFileElement.cs - represents a CodeElement indicating a file to be included into the current code file
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
	/// Represents a <see cref="CodeElement" /> indicating a file to be included into the current code file.
	/// </summary>
	public class CodeIncludeFileElement : CodeElement
	{
		public CodeIncludeFileElement()
		{
		}
		public CodeIncludeFileElement(string fileName) : this(fileName, true)
		{
		}
		public CodeIncludeFileElement(string fileName, bool isRelativePath)
		{
			FileName = fileName;
			IsRelativePath = isRelativePath;
		}

		/// <summary>
		/// Gets or sets the full path to the file that should be included.
		/// </summary>
		/// <value>The full path of the file to be included.</value>
		public string FileName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets a value indicating whether the path specified by the <see cref="FileName" /> property is relative to a known include directory.
		/// </summary>
		/// <value><c>true</c> if the path is relative; otherwise, <c>false</c>.</value>
		public bool IsRelativePath { get; set; } = true;
	}
}
