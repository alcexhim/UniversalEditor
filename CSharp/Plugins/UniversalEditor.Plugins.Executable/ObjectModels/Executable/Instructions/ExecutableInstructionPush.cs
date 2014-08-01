using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable.Instructions
{
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
