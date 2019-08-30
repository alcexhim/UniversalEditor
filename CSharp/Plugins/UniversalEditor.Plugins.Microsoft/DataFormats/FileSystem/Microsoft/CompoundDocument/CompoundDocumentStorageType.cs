using System;
namespace UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument
{
	public enum CompoundDocumentStorageType : byte
	{
		Empty = 0x00,
		UserStorage = 0x01,
		UserStream = 0x02,
		LockBytes = 0x03,
		Property = 0x04,
		RootStorage = 0x05
	}
}
