using System;
namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	public enum NewExecutableTargetOperatingSystem : byte
	{
		/// <summary>
		/// Unknown
		/// </summary>
		Unknown = 0x00,
		/// <summary>
		/// OS/2
		/// </summary>
		OS2 = 0x01,
		/// <summary>
		/// Windows
		/// </summary>
		Windows = 0x02,
		/// <summary>
		/// European MS-DOS 4.x
		/// </summary>
		EuropeanMSDOS4x = 0x03,
		/// <summary>
		/// Windows/386
		/// </summary>
		Windows386 = 0x04,
		/// <summary>
		/// BOSS (Borland Operating System Services)
		/// </summary>
		Borland = 0x05,
		/// <summary>
		/// PharLap 286|DOS-Extender, OS/2
		/// </summary>
		PharLapOS2 = 0x81,
		/// <summary>
		/// PharLap 286|DOS-Extender, Windows
		/// </summary>
		PharLapWindows = 0x82
	}
}
