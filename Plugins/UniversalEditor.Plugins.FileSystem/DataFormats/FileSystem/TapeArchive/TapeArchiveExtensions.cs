//
//  TapeArchiveExtensions.cs - extension methods used with the TapeArchiveDataFormat
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

using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.FileSystem.TapeArchive
{
	/// <summary>
	/// Extension methods used with the <see cref="TapeArchiveDataFormat" />.
	/// </summary>
	public static class TapeArchiveExtensions
	{
		public static string ReadPaddedNullTerminatedString(this Reader reader, int length)
		{
			return reader.ReadFixedLengthString(length).TrimNull().Trim();
		}
		public static void WritePaddedNullTerminatedString(this Writer writer, string value, int length)
		{
			writer.WriteFixedLengthString(value, length - 1, ' ');
			writer.WriteFixedLengthString("\0");
		}
		public static void WritePaddedNullableInt64(this Writer writer, long? value, int length)
		{
			writer.WritePaddedNullTerminatedString((value == null ? String.Empty : value.Value.ToString()), length);
		}
	}
}
