//
//  BSPMapDataFormat.cs - provides a DataFormat for manipulating Binary Space Partitioning (BSP) game maps
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Scene;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Quake.BSP
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Binary Space Partitioning (BSP) game maps.
	/// </summary>
	public class BSPMapDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SceneObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();

			IO.Reader br = base.Accessor.Reader;
			string magic = br.ReadFixedLengthString(4);
			if (magic != "IBSP") throw new InvalidDataFormatException("File does not begin with \"IBSP\"");

			int version = br.ReadInt32();
			for (int i = 0; i < 17; i++)
			{
				int directoryEntryOffset = br.ReadInt32();
				int directoryEntryLength = br.ReadInt32();

				long currentPos = br.Accessor.Position;

				br.Accessor.Seek(directoryEntryOffset, SeekOrigin.Begin);



				br.Accessor.Seek(currentPos, SeekOrigin.Begin);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
