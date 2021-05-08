using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable.ResourceBlocks
{
	/// <summary>
	/// Specifies the general type of file.
	/// </summary>
	[Flags()]
	public enum VersionFileType : uint
	{
		/// <summary>
		/// The file type is unknown to Windows.
		/// </summary>
		Unknown = 0x00000000,
		/// <summary>
		/// The file contains an application.
		/// </summary>
		Application = 0x00000001,
		/// <summary>
		/// The file contains a dynamic-link library (DLL).
		/// </summary>
		DynamicLinkLibrary = 0x00000002,
		/// <summary>
		/// The file contains a device driver. If dwFileType is VFT_DRV, dwFileSubtype contains a more specific description of the driver.
		/// </summary>
		DeviceDriver = 0x00000003,
		/// <summary>
		/// The file contains a font. If dwFileType is VFT_FONT, dwFileSubtype contains a more specific description of the font file.
		/// </summary>
		Font = 0x00000004,
		/// <summary>
		/// The file contains a virtual device driver.
		/// </summary>
		VirtualDeviceDriver = 0x00000005,
		/// <summary>
		/// Unknown purpose.
		/// </summary>
		Reserved = 0x00000006,
		/// <summary>
		/// The file contains a static-link library.
		/// </summary>
		StaticLibrary = 0x00000007
	}
	/// <summary>
	/// Specifies the function of the file. The possible values depend on the value of
	/// <see cref="FileType" />. If <see cref="FileType" /> is
	/// <see cref="VersionFileType.VirtualDeviceDriver" />, contains the virtual device identifier
	/// included in the virtual device control block.
	/// </summary>
	[Flags()]
	public enum VersionFileSubType : uint
	{
		/// <summary>
		/// The driver or font type is unknown by Windows.
		/// </summary>
		Unknown = 0x00000000,
		/// <summary>
		/// The file contains a printer driver or a raster font.
		/// </summary>
		DriverPrinter = 0x00000001,
		/// <summary>
		/// The file contains a keyboard driver or a vector font.
		/// </summary>
		DriverKeyboard = 0x00000002,
		/// <summary>
		/// The file contains a language driver or a TrueType font.
		/// </summary>
		DriverLanguage = 0x00000003,
		/// <summary>
		/// The file contains a display driver.
		/// </summary>
		DriverDisplay = 0x00000004,
		/// <summary>
		/// The file contains a mouse driver.
		/// </summary>
		DriverMouse = 0x00000005,
		/// <summary>
		/// The file contains a network driver.
		/// </summary>
		DriverNetwork = 0x00000006,
		/// <summary>
		/// The file contains a system driver.
		/// </summary>
		DriverSystem = 0x00000007,
		/// <summary>
		/// The file contains an installable driver.
		/// </summary>
		DriverInstallable = 0x00000008,
		/// <summary>
		/// The file contains a sound driver.
		/// </summary>
		DriverSound = 0x00000009,
		/// <summary>
		/// The file contains a communications driver.
		/// </summary>
		DriverCommunications = 0x0000000A,
		/// <summary>
		/// The file contains an input method driver.
		/// </summary>
		DriverInputMethod = 0x0000000B,
		/// <summary>
		/// The file contains a versioned printer driver.
		/// </summary>
		DriverPrinterVersioned = 0x0000000C,
		/// <summary>
		/// The file contains a printer driver or a raster font.
		/// </summary>
		FontRaster = 0x00000001,
		/// <summary>
		/// The file contains a keyboard driver or a vector font.
		/// </summary>
		FontVector = 0x00000002,
		/// <summary>
		/// The file contains a language driver or a TrueType font.
		/// </summary>
		FontTrueType = 0x00000003
	}
	/// <summary>
	/// Specifies the operating system for which this file was designed.
	/// </summary>
	[Flags()]
	public enum VersionOperatingSystem : uint
	{
		/// <summary>
		/// The operating system for which the file was designed is unknown to Windows.
		/// </summary>
		Unknown = 0x00000000,
		/// <summary>
		/// The file was designed for MS-DOS.
		/// </summary>
		DOS = 0x00010000,
		/// <summary>
		/// The file was designed for Windows NT.
		/// </summary>
		WindowsNT = 0x00040000,
		/// <summary>
		/// The file was designed for 16-bit Windows.
		/// </summary>
		Windows16 = 0x00000001,
		/// <summary>
		/// The file was designed for the Win32 API.
		/// </summary>
		Windows32 = 0x00000004,
		/// <summary>
		/// The file was designed for 16-bit OS/2.
		/// </summary>
		OS216 = 0x00020000,
		/// <summary>
		/// The file was designed for 32-bit OS/2.
		/// </summary>
		OS232 = 0x00030000,
		/// <summary>
		/// The file was designed for 16-bit Presentation Manager.
		/// </summary>
		PresentationManager16 = 0x00000002,
		/// <summary>
		/// The file was designed for 32-bit Presentation Manager.
		/// </summary>
		PresentationManager32 = 0x00000003
	}
	/// <summary>
	/// Contains a bitmask that specifies the Boolean attributes of the file.
	/// </summary>
	[Flags()]
	public enum VersionFileAttributes : uint
	{
		/// <summary>
		/// The file has no version flags set.
		/// </summary>
		None = 0x00,
		/// <summary>
		/// The file contains debugging information or is compiled with debugging features enabled.
		/// </summary>
		DebugMode = 0x01,
		/// <summary>
		/// The file has been modified and is not identical to the original shipping file of the same
		/// version number.
		/// </summary>
		Patched = 0x04,
		/// <summary>
		/// The file is a development version, not a commercially released product.
		/// </summary>
		PreRelease = 0x02,
		/// <summary>
		/// The file was not built using standard release procedures. If this flag is set, the
		/// StringFileInfo structure should contain a PrivateBuild entry.
		/// </summary>
		PrivateBuild = 0x08,
		/// <summary>
		/// The file's version structure was created dynamically; therefore, some of the members in
		/// this structure may be empty or incorrect. This flag should never be set in a file's
		/// VS_VERSION_INFO data.
		/// </summary>
		InformationInferred = 0x10,
		/// <summary>
		/// The file was built by the original company using standard release procedures but is a variation
		/// of the normal file of the same version number. If this flag is set, the StringFileInfo structure
		/// should contain a SpecialBuild entry.
		/// </summary>
		SpecialBuild = 0x20
	}

	public abstract class VersionResourceChildBlock
	{
		public class VersionResourceChildBlockCollection
			: System.Collections.ObjectModel.Collection<VersionResourceChildBlock>
		{
		}
	}
	public class VersionResourceFixedFileInfoBlock
	{
		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private Version mvarStructureVersion = new Version();
		public Version StructureVersion { get { return mvarStructureVersion; } set { mvarStructureVersion = value; } }

		private Version mvarFileVersion = new Version();
		public Version FileVersion { get { return mvarFileVersion; } set { mvarFileVersion = value; } }

		private Version mvarProductVersion = new Version();
		public Version ProductVersion { get { return mvarProductVersion; } set { mvarProductVersion = value; } }

		private VersionFileAttributes mvarFileAttributes = VersionFileAttributes.None;
		/// <summary>
		/// Contains a bitmask that specifies the Boolean attributes of the file.
		/// </summary>
		public VersionFileAttributes FileAttributes { get { return mvarFileAttributes; } set { mvarFileAttributes = value; } }

		private VersionFileAttributes mvarValidFileAttributes = VersionFileAttributes.None;
		/// <summary>
		/// Contains a bitmask that specifies the valid Boolean attributes of the file. An attribute is valid only if it was defined when the file was created.
		/// </summary>
		public VersionFileAttributes ValidFileAttributes { get { return mvarValidFileAttributes; } set { mvarValidFileAttributes = value; } }

		private VersionOperatingSystem mvarOperatingSystem = VersionOperatingSystem.Unknown;
		/// <summary>
		/// Specifies the operating system for which this file was designed.
		/// </summary>
		public VersionOperatingSystem OperatingSystem { get { return mvarOperatingSystem; } set { mvarOperatingSystem = value; } }

		private VersionFileType mvarFileType = VersionFileType.Unknown;
		/// <summary>
		/// Specifies the general type of file.
		/// </summary>
		public VersionFileType FileType { get { return mvarFileType; } set { mvarFileType = value; } }

		private VersionFileSubType mvarFileSubType = VersionFileSubType.Unknown;
		/// <summary>
		/// Specifies the function of the file. The possible values depend on the value of
		/// <see cref="FileType" />. If <see cref="FileType" /> is
		/// <see cref="VersionFileType.VirtualDeviceDriver" />, contains the virtual device identifier
		/// included in the virtual device control block.
		/// </summary>
		public VersionFileSubType FileSubType { get { return mvarFileSubType; } set { mvarFileSubType = value; } }

		private DateTime mvarFileDate = DateTime.Now;
		public DateTime FileDate { get { return mvarFileDate; } set { mvarFileDate = value; } }
	}

	public class VersionResourceStringFileInfoBlockLanguage
	{
		public class VersionResourceStringFileInfoBlockEntryCollection
			: System.Collections.ObjectModel.Collection<VersionResourceStringFileInfoBlockLanguage>
		{
		}

		private int mvarLanguageID = 0;
		public int LanguageID { get { return mvarLanguageID; } set { mvarLanguageID = value; } }

		private VersionResourceStringTableEntry.VersionResourceStringTableEntryCollection mvarStringTableEntries = new VersionResourceStringTableEntry.VersionResourceStringTableEntryCollection();
		public VersionResourceStringTableEntry.VersionResourceStringTableEntryCollection StringTableEntries { get { return mvarStringTableEntries; } }

		public override string ToString()
		{
			return "0x" + mvarLanguageID.ToString("x");
		}
	}
	public class VersionResourceStringTableEntry
	{
		public class VersionResourceStringTableEntryCollection
			: System.Collections.ObjectModel.Collection<VersionResourceStringTableEntry>
		{
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString()
		{
			return mvarName + ": \"" + mvarValue + "\"";
		}
	}

	public class VersionResourceStringFileInfoBlock : VersionResourceChildBlock
	{
		private VersionResourceStringFileInfoBlockLanguage.VersionResourceStringFileInfoBlockEntryCollection mvarEntries = new VersionResourceStringFileInfoBlockLanguage.VersionResourceStringFileInfoBlockEntryCollection();
		public VersionResourceStringFileInfoBlockLanguage.VersionResourceStringFileInfoBlockEntryCollection Entries { get { return mvarEntries; } }

		public override string ToString()
		{
			return "StringFileInfo";
		}
	}

	public class VersionResourceVarFileInfoBlockEntry
	{
		public class VersionResourceVarFileInfoBlockEntryCollection
			: System.Collections.ObjectModel.Collection<VersionResourceVarFileInfoBlockEntry>
		{
		}

		private List<int> mvarValues = new List<int>();
		public List<int> Values { get { return mvarValues; } }

		public override string ToString()
		{
			return "Translation";
		}
	}
	public class VersionResourceVarFileInfoBlock : VersionResourceChildBlock
	{
		private VersionResourceVarFileInfoBlockEntry.VersionResourceVarFileInfoBlockEntryCollection mvarEntries = new VersionResourceVarFileInfoBlockEntry.VersionResourceVarFileInfoBlockEntryCollection();
		public VersionResourceVarFileInfoBlockEntry.VersionResourceVarFileInfoBlockEntryCollection Entries { get { return mvarEntries; } }

		public override string ToString()
		{
			return "VarFileInfo";
		}
	}

	/// <summary>
	/// Represents a Version resource in an executable file (VS_VERSION_INFO on Windows).
	/// </summary>
	public class VersionResourceBlock : ExecutableResourceBlock
	{
		private ExecutableResourceType mvarType = ExecutableResourceType.Version;
		public override ExecutableResourceType Type
		{
			get { return mvarType; }
		}

		private VersionResourceFixedFileInfoBlock mvarFixedFileInfo = new VersionResourceFixedFileInfoBlock();
		public VersionResourceFixedFileInfoBlock FixedFileInfo { get { return mvarFixedFileInfo; } }

		private VersionResourceChildBlock.VersionResourceChildBlockCollection mvarChildBlocks = new VersionResourceChildBlock.VersionResourceChildBlockCollection();
		public VersionResourceChildBlock.VersionResourceChildBlockCollection ChildBlocks { get { return mvarChildBlocks; } }

		public override object Clone()
		{
			VersionResourceBlock clone = new VersionResourceBlock();
			return clone;
		}
	}
}
