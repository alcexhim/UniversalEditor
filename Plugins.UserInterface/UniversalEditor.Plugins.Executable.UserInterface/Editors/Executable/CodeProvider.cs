//
//  CodeProvider.cs - the abstract base class from which all MSIL code translators inherit
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Reflection;

namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable
{
	/// <summary>
	/// The abstract base class from which all MSIL code translators inherit.
	/// </summary>
	public abstract class CodeProvider
	{
		/// <summary>
		/// Gets the <see cref="CodeProviders.CSharpCodeProvider" />
		/// </summary>
		/// <value>The CS harp.</value>
		public static CodeProviders.CSharpCodeProvider CSharp { get; } = new CodeProviders.CSharpCodeProvider();

		public abstract string Title { get; }
		public abstract string CodeFileExtension { get; }

		protected abstract string GetAccessModifiersInternal(bool isPublic, bool isFamily, bool isAssembly, bool isPrivate, bool isAbstract, bool isSealed);

		public string GetAccessModifiers(EventInfo ei)
		{
			MethodInfo miAdd = ei.GetAddMethod();
			MethodInfo miRemove = ei.GetRemoveMethod();
			MethodInfo miRaise = ei.GetRaiseMethod();

			bool IsPublic = false;
			bool IsFamily = false;
			bool IsAssembly = false;
			bool IsPrivate = false;

			if (miAdd != null)
			{
				IsPublic = miAdd.IsPublic;
				IsFamily = miAdd.IsFamily;
				IsAssembly = miAdd.IsAssembly;
				IsPrivate = miAdd.IsPrivate;
			}
			else if (miRemove != null)
			{
				IsPublic = miRemove.IsPublic;
				IsFamily = miRemove.IsFamily;
				IsAssembly = miRemove.IsAssembly;
				IsPrivate = miRemove.IsPrivate;
			}
			else if (miRaise != null)
			{
				IsPublic = miRaise.IsPublic;
				IsFamily = miRaise.IsFamily;
				IsAssembly = miRaise.IsAssembly;
				IsPrivate = miRaise.IsPrivate;
			}
			else
			{
			}

			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, false, false);
		}
		public string GetAccessModifiers(MethodInfo mi)
		{
			bool IsPublic = mi.IsPublic;
			bool IsFamily = mi.IsFamily;
			bool IsAssembly = mi.IsAssembly;
			bool IsPrivate = mi.IsPrivate;
			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, mi.IsAbstract, false);
		}
		public string GetAccessModifiers(FieldInfo mi)
		{
			bool IsPublic = mi.IsPublic;
			bool IsFamily = mi.IsFamily;
			bool IsAssembly = mi.IsAssembly;
			bool IsPrivate = mi.IsPrivate;
			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, false, false);
		}
		public string GetAccessModifiers(PropertyInfo mi)
		{
			bool IsPublic = (mi.GetGetMethod()?.IsPublic).GetValueOrDefault() || (mi.GetSetMethod()?.IsPublic).GetValueOrDefault();
			bool IsFamily = (mi.GetGetMethod()?.IsFamily).GetValueOrDefault() && (mi.GetSetMethod()?.IsFamily).GetValueOrDefault();
			bool IsAssembly = (mi.GetGetMethod()?.IsAssembly).GetValueOrDefault() && (mi.GetSetMethod()?.IsAssembly).GetValueOrDefault();
			bool IsPrivate = (mi.GetGetMethod()?.IsPrivate).GetValueOrDefault() && (mi.GetSetMethod()?.IsPrivate).GetValueOrDefault();
			return GetAccessModifiersInternal(IsPublic, IsFamily, IsAssembly, IsPrivate, false, false);
		}
		public string GetAccessModifiers(Type mi)
		{
			bool IsPublic = mi.IsPublic;
			bool IsAbstract = mi.IsAbstract;
			bool IsSealed = mi.IsSealed;
			return GetAccessModifiersInternal(IsPublic, false, false, false, IsAbstract, IsSealed);
		}

		protected abstract string GetBeginBlockInternal(int indentLevel);
		public string GetBeginBlock(int indentLevel)
		{
			return GetBeginBlockInternal(indentLevel);
		}
		protected abstract string GetBeginBlockInternal(Type type, int indentLevel);
		public string GetBeginBlock(Type type, int indentLevel)
		{
			return GetBeginBlockInternal(type, indentLevel);
		}
		protected abstract string GetEndBlockInternal(string elementName, int indentLevel);
		public string GetEndBlock(Type type, int indentLevel)
		{
			return GetEndBlockInternal(GetElementName(type), indentLevel);
		}

		protected abstract string GetBeginBlockInternal(System.Reflection.MethodInfo mi, int indentLevel);
		public string GetBeginBlock(System.Reflection.MethodInfo mi, int indentLevel)
		{
			return GetBeginBlockInternal(mi, indentLevel);
		}
		protected abstract string GetEndBlockInternal(System.Reflection.MethodInfo mi, int indentLevel);
		public string GetEndBlock(System.Reflection.MethodInfo mi, int indentLevel)
		{
			return GetEndBlockInternal(GetElementName(mi), indentLevel);
		}

		protected abstract string GetElementNameInternal(System.Reflection.MethodInfo mi);
		public string GetElementName(System.Reflection.MethodInfo mi)
		{
			return GetElementNameInternal(mi);
		}
		protected abstract string GetElementNameInternal(Type type);
		public string GetElementName(Type type)
		{
			return GetElementNameInternal(type);
		}

		protected abstract string GetSourceCodeInternal(Type mi, int indentLevel);
		public string GetSourceCode(Type mi, int indentLevel)
		{
			return GetSourceCodeInternal(mi, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.EventInfo item, int indentLevel);
		public string GetSourceCode(System.Reflection.EventInfo item, int indentLevel)
		{
			return GetSourceCodeInternal(item, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.FieldInfo mi, int indentLevel);
		public string GetSourceCode(System.Reflection.FieldInfo mi, int indentLevel)
		{
			return GetSourceCodeInternal(mi, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.MethodInfo mi, int indentLevel);
		public string GetSourceCode(System.Reflection.MethodInfo mi, int indentLevel)
		{
			return GetSourceCodeInternal(mi, indentLevel);
		}
		protected abstract string GetSourceCodeInternal(System.Reflection.PropertyInfo item, bool autoProperty, int indentLevel);
		public string GetSourceCode(System.Reflection.PropertyInfo item, bool autoProperty, int indentLevel)
		{
			return GetSourceCodeInternal(item, autoProperty, indentLevel);
		}

		protected abstract string GetTypeNameInternal(Type type);
		public string GetTypeName(Type type)
		{
			return GetTypeNameInternal(type);
		}

		protected abstract string GetMethodSignatureInternal(System.Reflection.MethodInfo mi);
		public string GetMethodSignature(System.Reflection.MethodInfo mi)
		{
			return GetMethodSignatureInternal(mi);
		}

		private static BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

		public MethodInfo[] GetMethods(Type type)
		{
			MethodInfo[] meths = type.GetMethods(bindingFlags);
			List<MethodInfo> list = new List<MethodInfo>();
			for (int j = 0; j < meths.Length; j++)
			{
				if (meths[j].IsSpecialName && (meths[j].Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase) || meths[j].Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase)) && (type.GetProperty(meths[j].Name.Substring(4), bindingFlags) != null))
				{
					// we can be REASONABLY sure that this is a property setter / getter,
					// and as we've already gotten all the properties, ignore it
					continue;
				}
				list.Add(meths[j]);
			}
			return list.ToArray();
		}
		public FieldInfo[] GetFields(Type type, out List<string> AutoPropertyNames)
		{
			List<FieldInfo> list = new List<FieldInfo>();
			AutoPropertyNames = new List<string>();

			FieldInfo[] fields = type.GetFields(bindingFlags);
			for (int j = 0; j < fields.Length; j++)
			{
				if (fields[j].Name.StartsWith("<", StringComparison.OrdinalIgnoreCase) && fields[j].Name.Contains(">"))
				{
					// might be a backing field
					int k__backingfield_name_start = fields[j].Name.IndexOf('<');
					int k__backingfield_name_end = fields[j].Name.IndexOf('>', k__backingfield_name_start);

					string k__backingfield_name = fields[j].Name.Substring(k__backingfield_name_start + 1, k__backingfield_name_end - k__backingfield_name_start - 1);
					PropertyInfo pi = type.GetProperty(k__backingfield_name, bindingFlags);
					if (pi != null)
					{
						AutoPropertyNames.Add(pi.Name);
						continue;
					}
				}
				else
				{
					list.Add(fields[j]);
				}
			}
			return list.ToArray();
		}

		public string GetILOpcodeStr(short opcode)
		{
			return GetILOpcodeStr((ILOpcode)opcode);
		}
		public string GetILOpcodeStr(ILOpcode opcode)
		{
			switch (opcode)
			{
				case ILOpcode.Add: return "add";
				case ILOpcode.AddOvf: return "add.ovf";
				case ILOpcode.AddOvfUn: return "add.ovf.un";
				case ILOpcode.And: return "and";
				case ILOpcode.ArgList: return "arglist";
				case ILOpcode.Beq: return "beq <int32 (target)>";
				case ILOpcode.BeqS: return "beq <int32 (target)>";
				case ILOpcode.Bge: return "bge <int32 (target)>";
				case ILOpcode.BgeS: return "bge.s <int8 (target)>";
				case ILOpcode.BgeUn: return "bge.un <int32 (target)>";
				case ILOpcode.BgeUnS: return "bge.un.s <int8 (target)>";
				case ILOpcode.Bgt: return "bgt <int32 (target)>";
				case ILOpcode.BgtS: return "bgt.s <int8 (target)>";
				case ILOpcode.BgtUn: return "bgt.un <int32 (target)>";
				case ILOpcode.BgtUnS: return "bgt.un.s <int8 (target)>";
				case ILOpcode.Ble: return "ble <int32 (target)>";
				case ILOpcode.BleS: return "ble.s <int8 (target)>";
				case ILOpcode.BleUn: return "ble.un <int32 (target)>";
				case ILOpcode.BleUnS: return "ble.un.s <int8 (target)>";
				case ILOpcode.Blt: return "blt <int32 (target)>";
				case ILOpcode.BltS: return "blt.s <int8 (target)>";
				case ILOpcode.BltUn: return "blt.un <int32 (target)>";
				case ILOpcode.BltUnS: return "blt.un.s <int8 (target)>";
				case ILOpcode.BneUn: return "bne.un <int32 (target)>";
				case ILOpcode.BneUnS: return "bne.un.s <int8 (target)>";
				case ILOpcode.Box: return "box <typeTok>";
				case ILOpcode.Br: return "br <int32 (target)>";
				case ILOpcode.BrS: return "br.s <int8 (target)>";
				case ILOpcode.Break: return "break";
				case ILOpcode.BrFalse: return "brfalse <int32 (target)>";
				case ILOpcode.BrFalseS: return "brfalse.s <int8 (target)>";
				case ILOpcode.BrTrue: return "brtrue <int32 (target)>";
				case ILOpcode.BrTrueS: return "brtrue.s <int8 (target)>";
				case ILOpcode.Call: return "call <method>";
				case ILOpcode.CallI: return "calli <callsitedescr>";
				case ILOpcode.CallVirt: return "callvirt <method>";
				case ILOpcode.CastClass: return "castclass <class>";
				case ILOpcode.Ceq: return "ceq";
				case ILOpcode.Cgt: return "cgt";
				case ILOpcode.CgtUn: return "cgt.un";
				case ILOpcode.CkFinite: return "ckfinite";
				case ILOpcode.Clt: return "clt";
				case ILOpcode.CltUn: return "clt.un";
				case ILOpcode.Constrained: return "constrained <thisType>";
				case ILOpcode.ConvI: return "conv.i";
				case ILOpcode.ConvI1: return "conv.i1";
				case ILOpcode.ConvI2: return "conv.i2";
				case ILOpcode.ConvI4: return "conv.i4";
				case ILOpcode.ConvI8: return "conv.i8";
				case ILOpcode.ConvOvfI: return "conv.ovf.i";
				case ILOpcode.ConvOvfIUn: return "conv.ovf.i.un";
				case ILOpcode.ConvOvfI1: return "conv.ovf.i1";
				case ILOpcode.ConvOvfI1Un: return "conv.ovf.i1.un";
				case ILOpcode.ConvOvfI2: return "conv.ovf.i2";
				case ILOpcode.ConvOvfI2Un: return "conv.ovf.i2.un";
				case ILOpcode.ConvOvfI4: return "conv.ovf.i4";
				case ILOpcode.ConvOvfI4Un: return "conv.ovf.i4.un";
				case ILOpcode.ConvOvfI8: return "conv.ovf.i8";
				case ILOpcode.ConvOvfI8Un: return "conv.ovf.i8.un";
				case ILOpcode.ConvOvfU: return "conv.ovf.u";
				case ILOpcode.ConvOvfUUn: return "conv.ovf.u.un";
				case ILOpcode.ConvOvfU1: return "conv.ovf.u1";
				case ILOpcode.ConvOvfU1Un: return "conv.ovf.u1.un";
				case ILOpcode.ConvOvfU2: return "conv.ovf.u2";
				case ILOpcode.ConvOvfU2Un: return "conv.ovf.u2.un";
				case ILOpcode.ConvOvfU4: return "conv.ovf.u4";
				case ILOpcode.ConvOvfU4Un: return "conv.ovf.u4.un";
				case ILOpcode.ConvOvfU8: return "conv.ovf.u8";
				case ILOpcode.ConvOvfU8Un: return "conv.ovf.u8.un";
				case ILOpcode.ConvRUn: return "conv.r.un";
				case ILOpcode.ConvR4: return "conv.r4";
				case ILOpcode.ConvR8: return "conv.r8";
				case ILOpcode.ConvU: return "conv.u";
				case ILOpcode.ConvU1: return "conv.u1";
				case ILOpcode.ConvU2: return "conv.u2";
				case ILOpcode.ConvU4: return "conv.u4";
				case ILOpcode.ConvU8: return "conv.u8";
				case ILOpcode.CpBlk: return "cpblk";
				case ILOpcode.CpObj: return "cpobj <typeTok>";
				case ILOpcode.Div: return "div";
				case ILOpcode.DivUn: return "div.un";
				case ILOpcode.Dup: return "dup";
				case ILOpcode.EndFault: return "endfault";
				case ILOpcode.EndFilter: return "endfilter";
				// case ILOpcode.EndFinally: return "endfinally"; // same as endfault?
				case ILOpcode.InitBlk: return "initblk";
				case ILOpcode.InitObj: return "initobj <typeTok>";
				case ILOpcode.IsInst: return "isinst <class>";
				case ILOpcode.Jmp: return "jmp";
				case ILOpcode.LdArg: return "ldarg <uint16 (num)>";
				case ILOpcode.LdArg0: return "ldarg.0";
				case ILOpcode.LdArg1: return "ldarg.1";
				case ILOpcode.LdArg2: return "ldarg.2";
				case ILOpcode.LdArg3: return "ldarg.3";
				case ILOpcode.LdArgS: return "ldarg.s <uint8 (num)>";
				case ILOpcode.LdArgA: return "ldarg.a <uint16 (num)>";
				case ILOpcode.LdArgAS: return "ldarg.a.s <uint8 (num)>";
				case ILOpcode.LdCI4: return "ldc.i4 <int32 (num)>";
				case ILOpcode.LdCI4_M1: return "ldc.i4.m1";
				case ILOpcode.LdCI4_0: return "ldc.i4.0";
				case ILOpcode.LdCI4_1: return "ldc.i4.1";
				case ILOpcode.LdCI4_2: return "ldc.i4.2";
				case ILOpcode.LdCI4_3: return "ldc.i4.3";
				case ILOpcode.LdCI4_4: return "ldc.i4.4";
				case ILOpcode.LdCI4_5: return "ldc.i4.5";
				case ILOpcode.LdCI4_6: return "ldc.i4.6";
				case ILOpcode.LdCI4_7: return "ldc.i4.7";
				case ILOpcode.LdCI4_8: return "ldc.i4.8";
				case ILOpcode.LdCI4S: return "ldc.i4.s <int8 (num)>";
				case ILOpcode.LdCI8: return "ldc.i8 <int64 (num)>";
				case ILOpcode.LdCR4: return "ldc.r4 <float32 (num)>";
				case ILOpcode.LdCR8: return "ldc.r8 <float64 (num)>";
				case ILOpcode.LdElem: return "ldelem <typeTok>";
				case ILOpcode.LdElemI: return "ldelem.i";
				case ILOpcode.LdElemI1: return "ldelem.i1";
				case ILOpcode.LdElemU1: return "ldelem.u1";
				case ILOpcode.LdElemI2: return "ldelem.i2";
				case ILOpcode.LdElemU2: return "ldelem.u2";
				case ILOpcode.LdElemI4: return "ldelem.i4";
				case ILOpcode.LdElemU4: return "ldelem.u4";
				case ILOpcode.LdElemI8: return "ldelem.i8";
				case ILOpcode.LdElemR4: return "ldelem.r4";
				case ILOpcode.LdElemR8: return "ldelem.r8";
				case ILOpcode.LdElemRef: return "ldelem.ref";
				case ILOpcode.LdElemA: return "ldelema <class>";
				case ILOpcode.LdFld: return "ldfld <field>";
				case ILOpcode.LdFldA: return "ldflda <field>";
				case ILOpcode.LdFtn: return "ldftn <method>";
				case ILOpcode.LdIndI: return "ldind.i";
				case ILOpcode.LdIndI1: return "ldind.i1";
				case ILOpcode.LdIndU1: return "ldind.u1";
				case ILOpcode.LdIndI2: return "ldind.i2";
				case ILOpcode.LdIndU2: return "ldind.u2";
				case ILOpcode.LdIndI4: return "ldind.i4";
				case ILOpcode.LdIndU4: return "ldind.u4";
				case ILOpcode.LdIndI8: return "ldind.i8";
				case ILOpcode.LdIndR4: return "ldind.r4";
				case ILOpcode.LdIndR8: return "ldind.r8";
				case ILOpcode.LdLen: return "ldlen";
				case ILOpcode.LdLoc: return "ldloc <uint16 (indx)>";
				case ILOpcode.LdLoc0: return "ldloc.0";
				case ILOpcode.LdLoc1: return "ldloc.1";
				case ILOpcode.LdLoc2: return "ldloc.2";
				case ILOpcode.LdLoc3: return "ldloc.3";
				case ILOpcode.LdLocS: return "ldloc.s <uint8 (indx)>";
				case ILOpcode.LdLocA: return "ldloca <uint16 (indx)>";
				case ILOpcode.LdLocAS: return "ldloca.s <uint8 (indx)>";
				case ILOpcode.LdNull: return "ldnull";
				case ILOpcode.LdObj: return "ldobj <typeTok>";
				case ILOpcode.LdSFld: return "ldsfld <field>";
				case ILOpcode.LdSFldA: return "ldsflda <field>";
				case ILOpcode.LdStr: return "ldstr <string>";
				case ILOpcode.LdToken: return "ldtoken <token>";
				case ILOpcode.LdVirtFtn: return "ldvirtftn <method>";
				case ILOpcode.Leave: return "leave <int32 (target)>";
				case ILOpcode.LeaveS: return "leave <int8 (target)>";
				case ILOpcode.LocAlloc: return "localloc";
				case ILOpcode.MkRefAny: return "mkrefany <class>";
				case ILOpcode.Mul: return "mul";
				case ILOpcode.MulOvf: return "mul.ovf";
				case ILOpcode.MulOvfUn: return "mul.ovf.un";
				case ILOpcode.Neg: return "neg";
				case ILOpcode.NewArr: return "newarr <etype>";
				case ILOpcode.NewObj: return "newobj <ctor>";
				case ILOpcode.No: return "no.{typecheck, rangecheck, nullcheck}";
				case ILOpcode.Nop: return "nop";
				case ILOpcode.Not: return "not";
				case ILOpcode.Or: return "or";
				case ILOpcode.Pop: return "pop";
				case ILOpcode.Readonly: return "readonly.";
				case ILOpcode.RefAnyType: return "refanytype";
				case ILOpcode.RefAnyVal: return "refanyval <type>";
				case ILOpcode.Rem: return "rem";
				case ILOpcode.RemUn: return "rem.un";
				case ILOpcode.Ret: return "ret";
				case ILOpcode.Rethrow: return "rethrow";
				case ILOpcode.Shl: return "shl";
				case ILOpcode.Shr: return "shl";
				case ILOpcode.ShrUn: return "shl";
				case ILOpcode.SizeOf: return "sizeof <typeTok>";
				case ILOpcode.StArg: return "starg <uint16 (num)>";
				case ILOpcode.StArgS: return "starg.s <uint8 (num)>";
				case ILOpcode.StElem: return "stelem <typeTok>";
				case ILOpcode.StElemI: return "stelem.i";
				case ILOpcode.StElemI1: return "stelem.i1";
				case ILOpcode.StElemI2: return "stelem.i2";
				case ILOpcode.StElemI4: return "stelem.i4";
				case ILOpcode.StElemI8: return "stelem.i8";
				case ILOpcode.StElemR4: return "stelem.r4";
				case ILOpcode.StElemR8: return "stelem.r8";
				case ILOpcode.StElemRef: return "stelem.ref";
				case ILOpcode.StFld: return "stfld <field>";
				case ILOpcode.StIndI: return "stind.i";
				case ILOpcode.StIndI1: return "stind.i1";
				case ILOpcode.StIndI2: return "stind.i2";
				case ILOpcode.StIndI4: return "stind.i4";
				case ILOpcode.StIndI8: return "stind.i8";
				case ILOpcode.StIndR4: return "stind.r4";
				case ILOpcode.StIndR8: return "stind.r8";
				case ILOpcode.StIndRef: return "stind.ref";
				case ILOpcode.StLoc: return "stloc <uint16 (indx)>";
				case ILOpcode.StLoc0: return "stloc.0";
				case ILOpcode.StLoc1: return "stloc.1";
				case ILOpcode.StLoc2: return "stloc.2";
				case ILOpcode.StLoc3: return "stloc.3";
				case ILOpcode.StLocS: return "stloc.s <uint8 (indx)>";
				case ILOpcode.StObj: return "stobj <typeTok>";
				case ILOpcode.StSFld: return "stsfld <field>";
				case ILOpcode.Sub: return "sub";
				case ILOpcode.SubOvf: return "sub.ovf";
				case ILOpcode.SubOvfUn: return "sub.ovf.un";
				case ILOpcode.Switch: return "switch <uint32, int32, int32 (t1..tN)>";
				case ILOpcode.Tail: return "tail.";
				case ILOpcode.Throw: return "throw";
				case ILOpcode.Unaligned: return "unaligned.(alignment)";
				case ILOpcode.Unbox: return "unbox <typeTok>";
				case ILOpcode.UnboxAny: return "unbox.any <typeTok>";
				case ILOpcode.Volatile: return "volatile.";
				case ILOpcode.Xor: return "xor";
			}
			return "<OPCODE:" + ((short)opcode).ToString("X").PadLeft(2, '0') + ">";
		}

		public string GetVariableName(MethodInfo mi, LocalVariableInfo localVariableInfo)
		{
			// TODO: read the PDB file (if exists) to get the local variable info
			return "local_" + localVariableInfo.LocalIndex.ToString();
		}
	}
}
