//
//  ExecutableInstructionPush.cs - represents the PUSH opcode
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable.Instructions
{
	/// <summary>
	/// Represents the PUSH opcode.
	/// </summary>
	public class ExecutableInstructionPush<T> : ExecutableInstruction where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
	{
		public override ExecutableInstructionOpcode OpCode
		{
			get { return ExecutableInstructionOpcode.PushByte; }
		}

		private T mvarValue = default(T);
		/// <summary>
		/// The value to push onto the stack.
		/// </summary>
		public T Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString()
		{
			return "PUSH imm" + (System.Runtime.InteropServices.Marshal.SizeOf(mvarValue) * 8).ToString() + " 0x" + mvarValue.ToString("X", null).PadLeft(2, '0');
		}
	}
}
