//
//  ExecutableSubsystem.cs - indicates the subsystem required to run an executable
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Indicates the subsystem required to run an executable.
	/// </summary>
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
