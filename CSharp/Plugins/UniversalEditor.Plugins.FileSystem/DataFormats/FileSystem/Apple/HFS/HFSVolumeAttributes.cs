using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	[Flags()]
	public enum HFSVolumeAttributes : short
	{
		/// <summary>
		/// Set if the volume is locked by hardware
		/// </summary>
		HardwareLocked = 0x0040,
		/// <summary>
		/// Set if the volume was successfully unmounted
		/// </summary>
		SuccessfullyUnmounted = 0x0080,
		/// <summary>
		/// Set if the volume has had its bad blocks spared
		/// </summary>
		BadBlocksSpared = 0x0100,
		/// <summary>
		/// Set if the volume is locked by software
		/// </summary>
		SoftwareLocked = 0x0400
	}
}
