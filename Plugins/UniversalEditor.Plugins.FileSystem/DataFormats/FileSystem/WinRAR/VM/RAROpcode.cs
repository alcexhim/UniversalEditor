//
//  RAROpcode.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.FileSystem.WinRAR.VM
{
	public abstract class RAROpcode
	{
		public abstract RAROpcodeType Type { get; }

		public uint value1;
		public uint value2;

		public bool bytemode;
		public byte addressingmode1;
		public byte addressingmode2;

		public RARGetterFunction operand1getter;
		public RARSetterFunction operand1setter;

		public RARGetterFunction operand2getter;
		public RARSetterFunction operand2setter;

		public static readonly RAROpcodeFlags[] InstructionFlags = new RAROpcodeFlags[]
		{
			/*/*[RARMovInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag,
			/*[RARCmpInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARAddInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARSubInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARJzInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsUnconditionalJumpFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARJnzInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARIncInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARDecInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARJmpInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag,
			/*[RARXorInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARAndInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RAROrInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARTestInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARJsInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARJnsInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARJbInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARJbeInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARJaInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARJaeInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARPushInstruction]*/ RAROpcodeFlags.RAR1OperandFlag,
			/*[RARPopInstruction]*/ RAROpcodeFlags.RAR1OperandFlag,
			/*[RARCallInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARIsRelativeJumpFlag,
			/*[RARRetInstruction]*/ RAROpcodeFlags.RAR0OperandsFlag | RAROpcodeFlags.RARIsUnconditionalJumpFlag,
			/*[RARNotInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag,
			/*[RARShlInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARShrInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARSarInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARNegInstruction]*/ RAROpcodeFlags.RAR1OperandFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARPushaInstruction]*/ RAROpcodeFlags.RAR0OperandsFlag,
			/*[RARPopaInstruction]*/ RAROpcodeFlags.RAR0OperandsFlag,
			/*[RARPushfInstruction]*/ RAROpcodeFlags.RAR0OperandsFlag | RAROpcodeFlags.RARReadsStatusFlag,
			/*[RARPopfInstruction]*/ RAROpcodeFlags.RAR0OperandsFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARMovzxInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARWritesFirstOperandFlag,
			/*[RARMovsxInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARWritesFirstOperandFlag,
			/*[RARXchgInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARWritesSecondOperandFlag | RAROpcodeFlags.RARHasByteModeFlag,
			/*[RARMulInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag,
			/*[RARDivInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag,
			/*[RARAdcInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARReadsStatusFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARSbbInstruction]*/ RAROpcodeFlags.RAR2OperandsFlag | RAROpcodeFlags.RARHasByteModeFlag | RAROpcodeFlags.RARWritesFirstOperandFlag | RAROpcodeFlags.RARReadsStatusFlag | RAROpcodeFlags.RARWritesStatusFlag,
			/*[RARPrintInstruction]*/ RAROpcodeFlags.RAR0OperandsFlag
		};

		public bool RARInstructionWritesSecondOperand()
		{
			return ((RAROpcode.InstructionFlags[(int)Type] & RAROpcodeFlags.RARWritesSecondOperandFlag) == RAROpcodeFlags.RARWritesSecondOperandFlag);
		}
	}
}
