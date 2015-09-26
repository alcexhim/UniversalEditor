using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable.Instructions
{
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
