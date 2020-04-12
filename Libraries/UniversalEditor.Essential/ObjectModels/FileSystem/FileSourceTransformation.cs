//
//  FileSourceTransformation.cs - represents a transformation function applied to a File's data during the read or write process
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

namespace UniversalEditor.ObjectModels.FileSystem
{
	public delegate void FileSourceTransformationFunction(object sender, System.IO.Stream inputStream, System.IO.Stream outputStream);
	/// <summary>
	/// Indicates whether the <see cref="FileSourceTransformation" /> is applied during the input (read) or output (write) process, or both.
	/// </summary>
	public enum FileSourceTransformationType
	{
		None = 0,
		Input = 1,
		Output = 2,
		InputAndOutput = 3
	}
	/// <summary>
	/// Represents a transformation function applied to a <see cref="File" />'s data during the read or write process.
	/// </summary>
	public class FileSourceTransformation
	{
		public class FileSourceTransformationCollection
			: System.Collections.ObjectModel.Collection<FileSourceTransformation>
		{

		}

		/// <summary>
		/// Indicates whether the <see cref="FileSourceTransformation" /> is applied during the input (read) or output (write) process, or both.
		/// </summary>
		/// <value>The type.</value>
		public FileSourceTransformationType Type { get; set; } = FileSourceTransformationType.None;
		/// <summary>
		/// Gets or sets the transformation function to apply.
		/// </summary>
		/// <value>The transformation function to apply.</value>
		public FileSourceTransformationFunction Function { get; set; } = null;

		public FileSourceTransformation(FileSourceTransformationType type, FileSourceTransformationFunction func)
		{
			Type = type;
			Function = func;
		}
	}
}
