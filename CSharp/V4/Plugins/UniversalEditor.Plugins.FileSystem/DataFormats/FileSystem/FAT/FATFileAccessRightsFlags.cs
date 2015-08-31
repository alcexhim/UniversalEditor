using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	[Flags()]
	public enum FATFileAccessRightsFlags : short
	{
		None = 0x0000,
		/// <summary>
		/// Owner delete/rename/attribute change requires permission
		/// </summary>
		OwnerMetaPermissionRequired = 0x0001,
		/// <summary>
		/// Owner execute requires permission (FlexOS, 4680 OS, 4690 OS only)
		/// </summary>
		OwnerExecutePermissionRequired = 0x0002,
		/// <summary>
		/// Owner write/modify requires permission
		/// </summary>
		OwnerWritePermissionRequired = 0x0004,
		/// <summary>
		/// Owner read/copy requires permission
		/// </summary>
		OwnerReadPermissionRequired = 0x0008,
		/// <summary>
		/// Group delete/rename/attribute change requires permission
		/// </summary>
		GroupMetaPermissionRequired = 0x0010,
		/// <summary>
		/// Group execute requires permission (FlexOS, 4680 OS, 4690 OS only)
		/// </summary>
		GroupExecutePermissionRequired = 0x0020,
		/// <summary>
		/// Group write/modify requires permission
		/// </summary>
		GroupWritePermissionRequired = 0x0040,
		/// <summary>
		/// Group read/copy requires permission
		/// </summary>
		GroupReadPermissionRequired = 0x0080,
		/// <summary>
		/// World delete/rename/attribute change requires permission
		/// </summary>
		WorldMetaPermissionRequired = 0x0100,
		/// <summary>
		/// World execute requires permission (FlexOS, 4680 OS, 4690 OS only)
		/// </summary>
		WorldExecutePermissionRequired = 0x0200,
		/// <summary>
		/// World write/modify requires permission
		/// </summary>
		WorldWritePermissionRequired = 0x0400,
		/// <summary>
		/// World read/copy requires permission
		/// </summary>
		WorldReadPermissionRequired = 0x0800,
	}
}
