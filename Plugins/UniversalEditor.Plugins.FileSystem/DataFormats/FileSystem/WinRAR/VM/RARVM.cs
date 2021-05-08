//
//  RARVM.cs - work in progress to implement WinRAR VM from the unarchiver
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
using System.Diagnostics.Contracts;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR.VM
{
	/// <summary>
	/// Work in progress to implement WinRAR VM from the unarchiver.
	/// </summary>
	public class RARVM
	{
		private uint[] registers = new uint[8];
		public void SetRegisters(uint[] registers)
		{
			Contract.Requires(registers.Length == 8);

			this.registers = registers;
		}

		static int RARRegisterAddressingMode(int n)
		{
			return (0 + (n));
		}
		static int RARRegisterIndirectAddressingMode(int n)
		{
			return (8 + (n));
		}
		static int RARIndexedAbsoluteAddressingMode(int n)
		{
			return (16 + (n));
		}
		const int RARAbsoluteAddressingMode = 24;
		const int RARImmediateAddressingMode = 25;
		const int RARNumberOfAddressingModes = 26;

		static RARGetterFunction[] OperandGetters_32 = new RARGetterFunction[RARNumberOfAddressingModes];
		static RARGetterFunction[] OperandGetters_8 = new RARGetterFunction[RARNumberOfAddressingModes];
		static RARSetterFunction[] OperandSetters_32 = new RARSetterFunction[RARNumberOfAddressingModes];
		static RARSetterFunction[] OperandSetters_8 = new RARSetterFunction[RARNumberOfAddressingModes];

		static readonly byte RARNumberOfInstructions = (byte)Enum.GetValues(typeof(RAROpcodeType)).Length;

		int NumberOfRARInstructionOperands(RAROpcodeType instruction)
		{
			if ((int)instruction >= RARNumberOfInstructions) return 0;
			return (int)(RAROpcode.InstructionFlags[(int)instruction] & RAROpcodeFlags.RAROperandsFlag);
		}

		bool RARInstructionHasByteMode(RAROpcodeType instruction)
		{
			if ((int)instruction >= RARNumberOfInstructions) return false;
			return (RAROpcode.InstructionFlags[(int)instruction] & RAROpcodeFlags.RARHasByteModeFlag) != 0;
		}
		bool RARInstructionWritesFirstOperand(RAROpcodeType instruction)
		{
			if ((int)instruction >= RARNumberOfInstructions) return false;
			return (RAROpcode.InstructionFlags[(int)instruction] & RAROpcodeFlags.RARWritesFirstOperandFlag) != 0;
		}


		public uint GetOperand1()
		{
			return _opcodes[_ip].operand1getter(this, _opcodes[_ip].value1);
		}
		public uint GetOperand2()
		{
			return _opcodes[_ip].operand2getter(this, _opcodes[_ip].value2);
		}
		public void SetOperand1(uint data)
		{
			_opcodes[_ip].operand1setter(this, _opcodes[_ip].value1, data);
		}
		public void SetOperand2(uint data)
		{
			_opcodes[_ip].operand2setter(this, _opcodes[_ip].value2, data);
		}

		private bool Init(RAROpcode[] opcodes)
		{
			for (int i = 0; i < opcodes.Length; i++)
			{
				if ((byte)opcodes[i].Type >= RARNumberOfInstructions) return false;

				RARInstructionLabel[] instructionlabels;
				RARInstructionLabel[] instructionlabels_32;
				RARInstructionLabel[] instructionlabels_8;

				RARSetterFunction[] setterfunctions;
				RARGetterFunction[] getterfunctions;

				if (opcodes[i].Type == RAROpcodeType.Movsx || opcodes[i].Type == RAROpcodeType.Movzx)
				{
					// instructionlabels = instructionlabels_32;
					getterfunctions = OperandGetters_8;
					setterfunctions = OperandSetters_32;
				}
				else if (opcodes[i].bytemode)
				{
					if (!RARInstructionHasByteMode(opcodes[i].Type)) return false;

					// instructionlabels = instructionlabels_8;
					getterfunctions = OperandGetters_8;
					setterfunctions = OperandSetters_8;
				}
				else
				{
					// instructionlabels = instructionlabels_32;
					getterfunctions = OperandGetters_32;
					setterfunctions = OperandSetters_32;
				}

				// opcodes[i].instructionlabel = instructionlabels[opcodes[i].Type];

				int numoperands = NumberOfRARInstructionOperands(opcodes[i].Type);

				if (numoperands >= 1)
				{
					if (opcodes[i].addressingmode1 >= RARNumberOfAddressingModes) return false;
					opcodes[i].operand1getter = getterfunctions[opcodes[i].addressingmode1];
					opcodes[i].operand1setter = setterfunctions[opcodes[i].addressingmode1];

					if (opcodes[i].addressingmode1 == RARImmediateAddressingMode)
					{
						if (RARInstructionWritesFirstOperand(opcodes[i].Type)) return false;
					}
					else if (opcodes[i].addressingmode1 == RARAbsoluteAddressingMode)
					{
						opcodes[i].value1 &= RARProgramMemoryMask;
					}
				}

				if (numoperands == 2)
				{
					if (opcodes[i].addressingmode2 >= RARNumberOfAddressingModes) return false;
					opcodes[i].operand2getter = getterfunctions[opcodes[i].addressingmode2];
					opcodes[i].operand2setter = setterfunctions[opcodes[i].addressingmode2];

					if (opcodes[i].addressingmode2 == RARImmediateAddressingMode)
					{
						if (opcodes[i].RARInstructionWritesSecondOperand()) return false;
					}
					else if (opcodes[i].addressingmode2 == RARAbsoluteAddressingMode)
					{
						opcodes[i].value2 &= RARProgramMemoryMask;
					}
				}
			}
			return true;
		}

		private RARVMFlags flags = RARVMFlags.None;

		public void SetFlagsWithCarry(uint res, uint carry)
		{
			uint result = res;
			flags = (RARVMFlags)((result == 0 ? (uint)RARVMFlags.ZeroFlag : (result & (uint)RARVMFlags.SignFlag)) | carry);
		}

		private RAROpcode[] _opcodes = new RAROpcode[0];
		private int _ip = 0;

		public void Run(RAROpcode[] opcodes)
		{
			_opcodes = opcodes;
			_ip = -1;
			NextInstruction();
		}

		public void NextInstruction()
		{
			_ip++;
			// Debug();
			Execute(_opcodes[_ip]); // goto *opcode->instructionlabel;
		}

		private uint _RARRead32(byte[] b, uint ofs)
		{
			return ((uint)b[ofs + 3] << 24) | ((uint)b[ofs + 2] << 16) | ((uint)b[ofs + 1] << 8) | (uint)b[ofs + 0];
		}
		private void _RARWrite32(byte[] b, uint ofs, uint n)
		{
			b[ofs + 3] = (byte)((n >> 24) & 0xff);
			b[ofs + 2] = (byte)((n >> 16) & 0xff);
			b[ofs + 1] = (byte)((n >> 8) & 0xff);
			b[ofs + 0] = (byte)(n & 0xff);
		}

		private const int RARProgramMemorySize = 0x40000;
		private const int RARProgramMemoryMask = (RARProgramMemorySize - 1);

		private byte[] memory = new byte[0];
		private uint RARVirtualMachineRead32(uint address)
		{
			return _RARRead32(memory, address & RARProgramMemoryMask);
		}
		private void RARVirtualMachineWrite32(uint address, uint val)
		{
			_RARWrite32(memory, address & RARProgramMemoryMask, val);
		}

		private uint SignExtend(uint a)
		{
			return ((uint)((byte)(a)));
		}

		private void SetByteFlagsWithCarry(uint res, uint carry)
		{
			uint result = (res);
			flags = (RARVMFlags)((result == 0 ? (uint)RARVMFlags.ZeroFlag : (SignExtend(result) & (uint)RARVMFlags.SignFlag)) | (carry));
		}
		private void SetFlags(uint res)
		{
			SetFlagsWithCarry(res, 0);
		}

		private void SetOperand1AndFlagsWithCarry(uint res, uint carry)
		{
			uint r = (res);
			SetFlagsWithCarry(r, carry);
			SetOperand1(r);
		}
		private void SetOperand1AndByteFlagsWithCarry(uint res, uint carry)
		{
			uint r = (res);
			SetByteFlagsWithCarry(r, carry);
			SetOperand1(r);
		}
		private void SetOperand1AndFlags(uint res)
		{
			uint r = (res);
			SetFlags(r);
			SetOperand1(r);
		}

		public bool Jump(uint ofs)
		{
			uint o = ofs;
			if (o >= _opcodes.Length) return false;
			_ip = (int)o;

			// Debug();

			Execute(_opcodes[_ip]);
			return true;
		}

		public void Execute(RAROpcode opcode)
		{
			/*
			RAROpcode* opcode;
			uint flags = self->flags;

			Jump(0);
			*/
			switch (opcode.Type)
			{
				case RAROpcodeType.Mov:
				{
					SetOperand1(GetOperand2());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Cmp:
				{
					uint term1 = GetOperand1();
					uint result = term1 - GetOperand2();
					SetFlagsWithCarry(result, (uint)(result > term1 ? 1 : 0));
					NextInstruction();
					break;
				}
				case RAROpcodeType.Add:
				{
					uint term1 = GetOperand1();
					uint result = term1 + GetOperand2();
					SetOperand1AndFlagsWithCarry(result, (uint)(result < term1 ? 1 : 0));
					NextInstruction();
					break;
				}
				/*
				case RAROpcodeType.AddByte:
				{
					uint term1 = GetOperand1();
					SetOperand1AndByteFlagsWithCarry(term1 + GetOperand2() & 0xff, result < term1);
					NextInstruction();
					break;
				}
				*/
				case RAROpcodeType.Sub:
				{
					uint term1 = GetOperand1();
					uint result = term1 - GetOperand2();
					SetOperand1AndFlagsWithCarry(result, (uint)(result > term1 ? 1 : 0));
					NextInstruction();
					break;
				}
				/*
				case RAROpcodeType.SubByte: // Not correctly implemented in the RAR VM
				{
					uint term1 = GetOperand1();
					SetOperandAndByteFlagsWithCarry(term1-GetOperand2() & 0xFF, result > term1);
					NextInstruction();
					break;
				}
				*/
				case RAROpcodeType.Jz:
				{
					if ((flags & RARVMFlags.ZeroFlag) == RARVMFlags.ZeroFlag) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Jnz:
				{
					if ((flags & RARVMFlags.ZeroFlag) != RARVMFlags.ZeroFlag) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Inc:
				{
					SetOperand1AndFlags(GetOperand1() + 1);
					NextInstruction();
					break;
				}
				/*
				case RAROpcodeType.IncByte:
				{
					SetOperand1AndFlags(GetOperand1() + 1 & 0xff);
					NextInstruction();
					break;
				}
				*/
				case RAROpcodeType.Dec:
				{
					SetOperand1AndFlags(GetOperand1() - 1);
					NextInstruction();
					break;
				}
				/*
				case RAROpcodeType.DecByte:
				{
					SetOperand1AndFlags(GetOperand1() - 1 & 0xff);
					NextInstruction();
					break;
				}
				*/
				case RAROpcodeType.Jmp:
				{
					Jump(GetOperand1());
					break;
				}
				case RAROpcodeType.Xor:
				{
					SetOperand1AndFlags(GetOperand1() ^ GetOperand2());
					NextInstruction();
					break;
				}
				case RAROpcodeType.And:
				{
					SetOperand1AndFlags(GetOperand1() & GetOperand2());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Or:
				{
					SetOperand1AndFlags(GetOperand1() | GetOperand2());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Test:
				{
					SetFlags(GetOperand1() & GetOperand2());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Js:
				{
					if ((flags & RARVMFlags.SignFlag) == RARVMFlags.SignFlag) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Jns:
				{
					if ((flags & RARVMFlags.SignFlag) != RARVMFlags.SignFlag) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Jb:
				{
					if ((flags & RARVMFlags.CarryFlag) == RARVMFlags.CarryFlag) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Jbe:
				{
					if ((flags & (RARVMFlags.CarryFlag | RARVMFlags.ZeroFlag)) == (RARVMFlags.CarryFlag | RARVMFlags.ZeroFlag)) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Ja:
				{
					if (!((flags & (RARVMFlags.CarryFlag | RARVMFlags.ZeroFlag)) == (RARVMFlags.CarryFlag | RARVMFlags.ZeroFlag))) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Jae:
				{
					if ((flags & RARVMFlags.CarryFlag) != RARVMFlags.CarryFlag) Jump(GetOperand1());
					else NextInstruction();
					break;
				}
				case RAROpcodeType.Push:
				{
					registers[7] -= 4;
					RARVirtualMachineWrite32(registers[7], GetOperand1());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Pop:
				{
					SetOperand1(RARVirtualMachineRead32(registers[7]));
					registers[7] += 4;
					NextInstruction();
					break;
				}
				case RAROpcodeType.Call:
				{
					registers[7] -= 4;
					throw new NotImplementedException();
					// RARVirtualMachineWrite32(registers[7], opcode - opcodes + 1);
					Jump(GetOperand1());
					break;
				}
				case RAROpcodeType.Ret:
				{
					if (registers[7] >= RARProgramMemorySize)
					{
						// this.flags = flags;
						// return true;
						return;
					}
					uint retaddr = RARVirtualMachineRead32(registers[7]);
					registers[7] += 4;
					Jump(retaddr);
					break;
				}
				case RAROpcodeType.Not:
				{
					SetOperand1(~GetOperand1());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Shl:
				{
					uint op1 = GetOperand1();
					uint op2 = GetOperand2();
					SetOperand1AndFlagsWithCarry(op1 << (ushort)op2, (uint)(((op1 << (ushort)(op2 - 1)) & 0x80000000) != 0 ? 1 : 0));
					NextInstruction();
					break;
				}
				case RAROpcodeType.Shr:
				{
					uint op1 = GetOperand1();
					uint op2 = GetOperand2();
					SetOperand1AndFlagsWithCarry(op1 >> (ushort)op2, (uint)(((op1 >> (ushort)(op2 - 1)) & 1) != 0 ? 1 : 0));
					NextInstruction();
					break;
				}
				case RAROpcodeType.Sar:
				{
					uint op1 = GetOperand1();
					uint op2 = GetOperand2();
					SetOperand1AndFlagsWithCarry(op1 >> (ushort)op2, (uint)((((op1 >> (ushort)(op2 - 1)) & 1) != 0) ? 1 : 0));
					NextInstruction();
					break;
				}
				case RAROpcodeType.Neg:
				{
					long result = -GetOperand1();
					SetOperand1AndFlagsWithCarry((uint)result, (uint)(result != 0 ? 1 : 0));
					NextInstruction();
					break;
				}
				case RAROpcodeType.Pusha:
				{
					for (int i = 0; i < 8; i++) RARVirtualMachineWrite32((uint)(registers[7] - 4 - i * 4), registers[i]);
					registers[7] -= 32;

					NextInstruction();
					break;
				}
				case RAROpcodeType.Popa:
				{
					for (int i = 0; i < 8; i++)
					{
						registers[i] = RARVirtualMachineRead32((uint)(registers[7] + 28 - i * 4));
					}
					NextInstruction();
					break;
				}
				case RAROpcodeType.Pushf:
				{
					registers[7] -= 4;
					RARVirtualMachineWrite32(registers[7], (uint)flags);
					NextInstruction();
					break;
				}
				case RAROpcodeType.Popf:
				{
					flags = (RARVMFlags)RARVirtualMachineRead32((uint)(registers[7]));
					registers[7] += 4;
					NextInstruction();
					break;
				}
				case RAROpcodeType.Movzx:
				{
					SetOperand1(GetOperand2());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Movsx:
				{
					SetOperand1(SignExtend(GetOperand2()));
					NextInstruction();
					break;
				}
				case RAROpcodeType.Xchg:
				{
					uint op1 = GetOperand1();
					uint op2 = GetOperand2();
					SetOperand1(op2);
					SetOperand2(op1);
					NextInstruction();
					break;
				}
				case RAROpcodeType.Mul:
				{
					SetOperand1(GetOperand1() * GetOperand2());
					NextInstruction();
					break;
				}
				case RAROpcodeType.Div:
				{
					uint denominator = GetOperand2();
					if (denominator != 0) SetOperand1(GetOperand1() / denominator);
					NextInstruction();
					break;
				}
				case RAROpcodeType.Adc:
				{
					uint term1 = GetOperand1();
					uint carry = (uint)(flags & RARVMFlags.CarryFlag);
					uint result = term1 + GetOperand2() + carry;
					SetOperand1AndFlagsWithCarry(result, (uint)((result < term1 || (result == term1 && (carry != 0))) ? 1 : 0));
					NextInstruction();
					break;
				}
				/*
				case RAROpcodeType.AdcByte:
				{
					uint term1 = GetOperand1();
					uint carry = (uint)(flags & RARVMFlags.CarryFlag);
					SetOperand1AndFlagsWithCarry(term1 + GetOperand2() + carry & 0xff, result < term1 || result == term1 && carry); // Does not correctly set sign bit.
					NextInstruction();
					break;
				}
				*/
				case RAROpcodeType.Sbb:
				{
					uint term1 = GetOperand1();
					uint carry = (uint)(flags & RARVMFlags.CarryFlag);
					uint result = (term1 - GetOperand2() - carry);
					SetOperand1AndFlagsWithCarry(result, (uint)((result > term1 || (result == term1 && (carry != 0))) ? 1 : 0));
					NextInstruction();
					break;
				}
				/*
				case RAROpcodeType.SbbByte:
				{
					uint term1 = GetOperand1();
					uint carry = (uint)(flags & RARVMFlags.CarryFlag);
					uint result = (term1 - GetOperand2() - carry & 0xff);
					SetOperand1AndFlagsWithCarry(result, (uint)(result > term1 || (result == term1 && (carry != 0)) ? 1 : 0)); // Does not correctly set sign bit.
					NextInstruction();
					break;
				}
				*/
				case RAROpcodeType.Print:
				{
					NextInstruction();
					break;
				}
			}
		}
	}
}
