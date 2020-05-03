//
//  RAROpcodeType.cs
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
	public enum RAROpcodeType : byte
	{
		Mov = 0,
		Cmp = 1,
		Add = 2,
		Sub = 3,
		Jz = 4,
		Jnz = 5,
		Inc = 6,
		Dec = 7,
		Jmp = 8,
		Xor = 9,
		And = 10,
		Or = 11,
		Test = 12,
		Js = 13,
		Jns = 14,
		Jb = 15,
		Jbe = 16,
		Ja = 17,
		Jae = 18,
		Push = 19,
		Pop = 20,
		Call = 21,
		Ret = 22,
		Not = 23,
		Shl = 24,
		Shr = 25,
		Sar = 26,
		Neg = 27,
		Pusha = 28,
		Popa = 29,
		Pushf = 30,
		Popf = 31,
		Movzx = 32,
		Movsx = 33,
		Xchg = 34,
		Mul = 35,
		Div = 36,
		Adc = 37,
		Sbb = 38,
		Print = 39
	}
}
