using System;
namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	[Flags()]
	public enum NewExecutableOS2Flags
	{
		None = 0,
		/// <summary>
		/// Supports long filenames.
		/// </summary>
		LongFileNameSupport = 0x01,
		/// <summary>
		/// 2.X protected mode
		/// </summary>
		ProtectedMode = 0x02,
		/// <summary>
		/// 2.X proportional font
		/// </summary>
		ProportionalFont = 0x04,
		/// <summary>
		/// Gangload area
		/// </summary>
		GangloadArea = 0x08
	}
}
