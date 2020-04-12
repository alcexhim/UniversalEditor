//
//  ExecutableInstruction.cs - contains opcodes and class for representing an instruction in an executable text segment
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
	/// Indicates the opcode of the instruction.
	/// </summary>
	public enum ExecutableInstructionOpcode : byte
	{
		Add = 0,
        Move = 0x89,
        Push0 = 0x50,
        Push1 = 0x51,
        Push2 = 0x52,
        Push3 = 0x53,           // pushl EBP
        Push4 = 0x54,
        Push5 = 0x55,
        Push6 = 0x56,
        Push7 = 0x57,
		PushWord = 0x68,
		PushByte = 0x6A,
		Nop = 0x90,
		Call = 0xFF
	}
	/// <summary>
	/// Represents an instruction in an executable text segment.
	/// </summary>
	public abstract class ExecutableInstruction
	{
		public class ExecutableInstructionCollection
			: System.Collections.ObjectModel.Collection<ExecutableInstruction>
		{
		}

		/// <summary>
		/// Indicates the opcode of the instruction.
		/// </summary>
		/// <value>The opcode of the instruction.</value>
		public abstract ExecutableInstructionOpcode OpCode { get; }

	}
}
