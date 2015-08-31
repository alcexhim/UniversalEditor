using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.FileSystem.TapeArchive
{
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
