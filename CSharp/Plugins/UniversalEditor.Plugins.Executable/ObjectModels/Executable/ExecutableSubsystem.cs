using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	public enum ExecutableSubsystem : ushort
	{
		/// <summary>
		/// Unknown subsystem.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// No subsystem required (device drivers and native system processes).
		/// </summary>
		Native = 1,
		/// <summary>
		/// Windows graphical user interface (GUI) subsystem.
		/// </summary>
		WindowsGUI = 2,
		/// <summary>
		/// Windows character-mode user interface (CUI) subsystem.
		/// </summary>
		WindowsCUI = 3,
		/// <summary>
		/// OS/2 character-mode user interface (CUI) subsystem.
		/// </summary>
		OS2CUI = 5,
		/// <summary>
		/// POSIX character-mode user interface (CUI) subsystem.
		/// </summary>
		PosixCUI = 7,
		/// <summary>
		/// Windows CE system.
		/// </summary>
		WindowsCEGUI = 9,
		/// <summary>
		/// Extensible Firmware Interface (EFI) application.
		/// </summary>
		EFIApplication = 10,
		/// <summary>
		/// EFI driver with boot services.
		/// </summary>
		EFIBootServiceDriver = 11,
		/// <summary>
		/// EFI driver with run-time services.
		/// </summary>
		EFIRuntimeServiceDriver = 12,
		/// <summary>
		/// EFI ROM image.
		/// </summary>
		EFIROMImage = 13,
		/// <summary>
		/// Xbox system.
		/// </summary>
		Xbox = 14,
		/// <summary>
		/// Boot application
		/// </summary>
		WindowsBootApplication = 16
	}
}
