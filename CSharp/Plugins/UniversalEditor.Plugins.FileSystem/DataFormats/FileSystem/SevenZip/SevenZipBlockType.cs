using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.SevenZip
{
	public enum SevenZipBlockType : long
	{
		End = 0x00,
		Header = 0x01,
		ArchiveProperties = 0x02,

		AdditionalStreamsInfo = 0x03,
		MainStreamsInfo = 0x04,
		FilesInfo = 0x05,

		PackInfo = 0x06,
		UnpackInfo = 0x07,
		SubStreamsInfo = 0x08,

		Size = 0x09,
		CRC = 0x0A,

		Folder = 0x0B,

		CodersUnpackSize = 0x0C,
		NumUnpackStream = 0x0D,

		EmptyStream = 0x0E,
		EmptyFile = 0x0F,
		Anti = 0x10,

		Name = 0x11,
		CTime = 0x12,
		ATime = 0x13,
		MTime = 0x14,
		WinAttributes = 0x15,
		Comment = 0x16,

		EncodedHeader = 0x17,

		StartPos = 0x18,
		Dummy = 0x19
	}
}
