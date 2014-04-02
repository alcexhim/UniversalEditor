using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ARJ
{
	[Flags()]
	public enum ARJInternalFlags : byte
	{
		None = 0x0000,
		PasswordRequired = 0x0001,
		Reserved = 0x0002,
		ContinueOnNextDisk = 0x0004,
		StartPositionFieldAvailable = 0x0008,
		PathTranslation = 0x0010,
		Backup = 0x0020
	}
}
