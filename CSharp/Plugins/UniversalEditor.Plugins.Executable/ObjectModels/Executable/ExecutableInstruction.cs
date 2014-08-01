using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
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
	public abstract class ExecutableInstruction
	{
		public class ExecutableInstructionCollection
			: System.Collections.ObjectModel.Collection<ExecutableInstruction>
		{
		}

		public abstract ExecutableInstructionOpcode OpCode { get; }

	}
}
