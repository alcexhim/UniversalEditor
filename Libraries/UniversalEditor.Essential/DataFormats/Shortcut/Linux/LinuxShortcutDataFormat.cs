//
//  INTXLINKDataFormat.cs - provides a DataFormat for manipulating shortcuts in Interix symlink format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Shortcut;

namespace UniversalEditor.DataFormats.Shortcut.Linux
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating shortcuts in Interix symlink format.
	/// </summary>
	public class LinuxShortcutDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ShortcutObjectModel shortcut = (objectModel as ShortcutObjectModel);
			if (shortcut == null)
				throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			string INTXLNK = br.ReadFixedLengthString(7);
			if (INTXLNK != "INTXLNK")
				throw new InvalidDataFormatException();

			byte b = br.ReadByte();
			shortcut.ExecutableFileName = br.ReadStringToEnd(Encoding.UTF16LittleEndian);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			ShortcutObjectModel shortcut = (objectModel as ShortcutObjectModel);
			if (shortcut == null)
				throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("INTXLNK");
			bw.WriteByte(1);
			bw.WriteFixedLengthString(shortcut.ExecutableFileName, Encoding.UTF16LittleEndian);
		}
	}
}
