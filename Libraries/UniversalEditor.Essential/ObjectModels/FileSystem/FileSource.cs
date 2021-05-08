//
//  FileSource.cs - the abstract base class for defining how a deferred File retrieves its data
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
	/// <summary>
	/// The abstract base class for defining how a deferred <see cref="File" /> retrieves its data.
	/// </summary>
	public abstract class FileSource
	{

		private FileSourceTransformation.FileSourceTransformationCollection mvarTransformations = new FileSourceTransformation.FileSourceTransformationCollection();
		public FileSourceTransformation.FileSourceTransformationCollection Transformations { get { return mvarTransformations; } }

		public byte[] GetData() { return GetData(0, GetLength()); }
		public byte[] GetData(long offset, long length)
		{
			byte[] data = GetDataInternal(offset, length);
			System.IO.MemoryStream msInput = new System.IO.MemoryStream(data);
			for (int i = 0; i < Transformations.Count; i++)
			{
				System.IO.MemoryStream msOutput = new System.IO.MemoryStream();
				Transformations[i].Function(this, msInput, msOutput);
				msInput = msOutput;
			}
			return msInput.ToArray();
		}

		public abstract byte[] GetDataInternal(long offset, long length);
		public abstract long GetLength();
	}
}
