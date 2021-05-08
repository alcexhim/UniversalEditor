//
//  Super6DataFormat.cs - provides a DataFormat for manipulating 3D model files in Super6 format
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

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Super6
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D model files in Super6 format.
	/// </summary>
	public class Super6DataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;
			byte[] signature = br.ReadBytes(4);
			byte[] realsig = new byte[] { 0xFF, 0xFF, 0xFF, 0x9B };
			if (!signature.Match(realsig)) throw new InvalidDataFormatException("File does not begin with 0xFF, 0xFF, 0xFF, 0x9B");

			uint unknown1 = br.ReadUInt32();
			uint sectionCount = br.ReadUInt32();
			uint firstSectionOffset = br.ReadUInt32();
			for (uint i = 0; i < sectionCount; i++)
			{
				uint unknown3 = br.ReadUInt32();
				string sectionName = br.ReadFixedLengthString(32).TrimNull();
				uint sectionLength = br.ReadUInt32();
				uint sectionOffset = br.ReadUInt32();
				sectionOffset += firstSectionOffset;

				uint unknown2 = br.ReadUInt32();
			}

			uint unknown6 = br.ReadUInt32();
			uint unknown7 = br.ReadUInt32();
			uint unknown8 = br.ReadUInt32();
			uint unknown9 = br.ReadUInt32();
			uint unknown10 = br.ReadUInt32();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
