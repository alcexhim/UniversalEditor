//
//  ExecutableInstructionCall.cs - represents the CALL opcode
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

using System;

namespace UniversalEditor.ObjectModels.Executable.Instructions
{
	/// <summary>
	/// Represents the CALL opcode.
	/// </summary>
	public class ExecutableInstructionCall<T> : ExecutableInstruction where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
	{
		public override ExecutableInstructionOpcode OpCode
		{
			get { return ExecutableInstructionOpcode.Call; }
		}

		private byte mvarExtra = 0;
		public byte Extra { get { return mvarExtra; } set { mvarExtra = value; } }

		private T mvarAddress = default(T);
		/// <summary>
		/// The address of the next instruction to execute.
		/// </summary>
		public T Address { get { return mvarAddress; } set { mvarAddress = value; } }

		public override string ToString()
		{
			return "CALL 0x" + mvarAddress.ToString("X", null).PadLeft(2, '0');
		}
	}
}
