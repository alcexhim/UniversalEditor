using System;
namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable
{
	public enum ILOpcode : short
	{
		/// <summary>
		/// add - Add two values, returning a new value.
		/// </summary>
		Add = 0x58,
		/// <summary>
		/// add.ovf - Add signed integer values with overflow check.
		/// </summary>
		AddOvf = 0xD6,
		/// <summary>
		/// add.ovf.un - Add unsigned integer values with overflow check.
		/// </summary>
		AddOvfUn = 0xD7,
		/// <summary>
		/// and - Bitwise AND of two integral values, returns an integral value.
		/// </summary>
		And = 0x5F,
		/// <summary>
		/// arglist - Return argument list handle for the current method.
		/// </summary>
		ArgList = 0x00FE,
		/// <summary>
		/// beq &lt;int32 (target)&gt; - Branch to target if equal.
		/// </summary>
		Beq = 0x3B,
		/// <summary>
		/// beq.s &lt;int8 (target)&gt; - Branch to target if equal, short form.
		/// </summary>
		BeqS = 0x2E,

		/// <summary>
		/// bge &lt;int32 (target)&gt; - Branch to target if greater than or equal to.
		/// </summary>
		Bge = 0x3C,
		/// <summary>
		/// bge.s &lt;int8 (target)&gt; - Branch to target if greater than or equal to, short form.
		/// </summary>
		BgeS = 0x2F,
		/// <summary>
		/// bge.un &lt;int32 (target)&gt; - Branch to target if greater than or equal to (unsigned or unordered).
		/// </summary>
		BgeUn = 0x41,
		/// <summary>
		/// bge.un.s &lt;int8 (target)&gt; - Branch to target if greater than or equal to (unsigned or unordered), short form
		/// </summary>
		BgeUnS = 0x34,

		/// <summary>
		/// bgt &lt;int32 (target)&gt; Branch to target if greater than.
		/// </summary>
		Bgt = 0x3D,
		/// <summary>
		/// bgt.s &lt;int8 (target)&gt; Branch to target if greater than, short form.
		/// </summary>
		BgtS = 0x30,
		/// <summary>
		/// bgt.un &lt;int32 (target)&gt; Branch to target if greater than (unsigned or unordered).
		/// </summary>
		BgtUn = 0x42,
		/// <summary>
		/// bgt.un.s &lt;int8 (target)&gt; Branch to target if greater than (unsigned or unordered), short form.
		/// </summary>
		BgtUnS = 0x35,
		
		/// <summary>
		/// ble &lt;int32 (target)&gt; Branch to target if less than or equal to.
		/// </summary>
		Ble = 0x3E,
		/// <summary>
		/// ble.s &lt;int8 (target)&gt; Branch to target if less than or equal to, short form.
		/// </summary>
		BleS = 0x31,
		/// <summary>
		/// ble.un &lt;int32 (target)&gt; Branch to target if less than or equal to (unsigned or unordered).
		/// </summary>
		BleUn = 0x43,
		/// <summary>
		/// ble.un.s &lt;int8 (target)&gt; Branch to target if less than or equal to (unsigned or unordered), short form.
		/// </summary>
		BleUnS = 0x36,

		/// <summary>
		/// blt &lt;int32 (target)&gt; Branch to target if less than.
		/// </summary>
		Blt = 0x3F,
		/// <summary>
		/// blt.s &lt;int8 (target)&gt; Branch to target if less than, short form.
		/// </summary>
		BltS = 0x32,
		/// <summary>
		/// blt.un &lt;int32 (target)&gt; Branch to target if less than (unsigned or unordered).
		/// </summary>
		BltUn = 0x44,
		/// <summary>
		/// blt.un.s &lt;int8 (target)&gt; Branch to target if less than (unsigned or unordered), short form.
		/// </summary>
		BltUnS = 0x37,

		/// <summary>
		/// bne.un &lt;int32 (target)&gt; Branch to target if unequal or unordered.
		/// </summary>
		BneUn = 0x40,
		/// <summary>
		/// bne.un.s &lt;int8 (target)&gt; Branch to target if unequal or unordered, short form.
		/// </summary>
		BneUnS = 0x33,

		/// <summary>
		/// Convert a boxable value to its boxed form
		/// </summary>
		Box = 0x8C,

		/// <summary>
		/// br &lt;int32 (target)&gt; Branch to target.
		/// </summary>
		Br = 0x38,
		/// <summary>
		/// br.s &lt;int8 (target)&gt; Branch to target, short form.
		/// </summary>
		BrS = 0x2B,

		/// <summary>
		/// break - Inform a debugger that a breakpoint has been reached.
		/// </summary>
		Break = 0x01,

		/// <summary>
		/// brfalse &lt;int32 (target)&gt; - Branch to target if value is zero (false).
		/// </summary>
		BrFalse = 0x39,
		/// <summary>
		/// brfalse.s &lt;int8 (target)&gt; - Branch to target if value is zero (false), short form.
		/// </summary>
		BrFalseS = 0x2C,
		/// <summary>
		/// brinst &lt;int32 (target)&gt; - Branch to target if value is a non-null object reference (alias for brtrue).
		/// </summary>
		BrInst = 0x3A,
		/// <summary>
		/// brinst.s &lt;int8 (target)&gt; - Branch to target if value is a non-null object reference, short form (alias for brtrue.s).
		/// </summary>
		BrInstS = 0x2D,
		/// <summary>
		/// brnull &lt;int32 (target)&gt; - Branch to target if value is null (alias for brfalse).
		/// </summary>
		BrNull = 0x39,
		/// <summary>
		/// brnull.s &lt;int8 (target)&gt; - Branch to target if value is null (alias for brfalse.s), short form.
		/// </summary>
		BrNullS = 0x2C,
		/// <summary>
		/// brtrue &lt;int32 (target)&gt; - Branch to target if value is non-zero (true).
		/// </summary>
		BrTrue = 0x3A,
		/// <summary>
		/// brtrue.s &lt;int8 (target)&gt; - Branch to target if value is non-zero (true), short form.
		/// </summary>
		BrTrueS = 0x2D,
		/// <summary>
		/// brzero &lt;int32 (target)&gt; - Branch to target if value is zero (alias for brfalse).
		/// </summary>
		BrZero = 0x39,
		/// <summary>
		/// brzero.s &lt;int8 (target)&gt; - Branch to target if value is zero (alias for brfalse.s), short form.
		/// </summary>
		BrZeroS = 0x2C,
		/// <summary>
		/// call &lt;method&gt; - Call method described by method.
		/// </summary>
		Call = 0x28,
		/// <summary>
		/// calli &lt;callsitedescr&gt; - Call method indicated on the stack with arguments described by callsitedescr.
		/// </summary>
		CallI = 0x29,
		/// <summary>
		/// callvirt &lt;method&gt; - Call a method associated with an object.
		/// </summary>
		CallVirt = 0x6F,
		/// <summary>
		/// castclass &lt;class&gt; - Cast obj to class.
		/// </summary>
		CastClass = 0x74,
		/// <summary>
		/// ceq - Push 1 (of type int32) if value1 equals value2, else push 0.
		/// </summary>
		Ceq = 0x01FE,
		/// <summary>
		/// cgt - Push 1 (of type int32) if value1 greater that value2, else push 0.
		/// </summary>
		Cgt = 0x02FE,
		/// <summary>
		/// cgt.un - Push 1 (of type int32) if value1 greater that value2, unsigned or unordered, else push 0.
		/// </summary>
		CgtUn = 0x03FE,
		/// <summary>
		/// Throw ArithmeticException if value is not a finite number.
		/// </summary>
		CkFinite = 0xC3,
		/// <summary>
		/// clt - Push 1 (of type int32) if value1 lower than value2, else push 0.
		/// </summary>
		Clt = 0x04FE,
		/// <summary>
		/// clt.un - Push 1 (of type int32) if value1 lower than value2, unsigned or unordered, else push 0.
		/// </summary>
		CltUn = 0x05FE,
		/// <summary>
		/// constrained &lt;thisType&gt; - Call a virtual method on a type constrained to be type T
		/// </summary>
		Constrained = 0x16FE,
		/// <summary>
		/// Convert to native int, pushing native int on stack.
		/// </summary>
		ConvI = 0xD3,
		/// <summary>
		/// conv.i1 - Convert to int8, pushing int32 on stack.
		/// </summary>
		ConvI1 = 0x67,
		/// <summary>
		/// conv.i2 - Convert to int16, pushing int32 on stack.
		/// </summary>
		ConvI2 = 0x68,
		/// <summary>
		/// conv.i4 - Convert to int32, pushing int32 on stack.
		/// </summary>
		ConvI4 = 0x69,
		/// <summary>
		/// conv.i8 - Convert to int64, pushing int64 on stack.
		/// </summary>
		ConvI8 = 0x6A,
		/// <summary>
		/// conv.ovf.i - Convert to a native int (on the stack as native int) and throw an exception on overflow.
		/// </summary>
		ConvOvfI = 0xD4,
		/// <summary>
		/// conv.ovf.i.un - Convert unsigned to a native int (on the stack as native int) and throw an exception on overflow.
		/// </summary>
		ConvOvfIUn = 0x8A,
		/// <summary>
		/// conv.ovf.i1 - Convert to an int8 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfI1 = 0xB3,
		/// <summary>
		/// conv.ovf.i1.un - Convert unsigned to an int8 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfI1Un = 0x82,
		/// <summary>
		/// conv.ovf.i2 - Convert to an int16 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfI2 = 0xB5,
		/// <summary>
		/// conv.ovf.i2.un - Convert unsigned to an int16 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfI2Un = 0x83,
		/// <summary>
		/// conv.ovf.i4 - Convert to an int32 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfI4 = 0xB7,
		/// <summary>
		/// conv.ovf.i4.un - Convert unsigned to an int32 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfI4Un = 0x84,
		/// <summary>
		/// conv.ovf.i8 - Convert to an int64 (on the stack as int64) and throw an exception on overflow.
		/// </summary>
		ConvOvfI8 = 0xB9,
		/// <summary>
		/// conv.ovf.i8.un - Convert unsigned to an int64 (on the stack as int64) and throw an exception on overflow.
		/// </summary>
		ConvOvfI8Un = 0x85,
		/// <summary>
		/// conv.ovf.u - Convert to a native unsigned int (on the stack as native int) and throw an exception on overflow.
		/// </summary>
		ConvOvfU = 0xD5,
		/// <summary>
		/// conv.ovf.u.un - Convert unsigned to a native unsigned int (on the stack as native int) and throw an exception on overflow.
		/// </summary>
		ConvOvfUUn = 0x8B,
		/// <summary>
		/// conv.ovf.u1 - Convert to an unsigned int8 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfU1 = 0xB4,
		/// <summary>
		/// conv.ovf.u1.un - Convert unsigned to an unsigned int8 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfU1Un = 0x86,
		/// <summary>
		/// conv.ovf.u2 - Convert to an unsigned int16 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfU2 = 0xB6,
		/// <summary>
		/// conv.ovf.u2.un - Convert unsigned to an unsigned int16 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfU2Un = 0x87,
		/// <summary>
		/// conv.ovf.u4 - Convert to an unsigned int32 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfU4 = 0xB8,
		/// <summary>
		/// conv.ovf.u4.un - Convert unsigned to an unsigned int32 (on the stack as int32) and throw an exception on overflow.
		/// </summary>
		ConvOvfU4Un = 0x88,
		/// <summary>
		/// conv.ovf.u8 - Convert to an unsigned int64 (on the stack as int64) and throw an exception on overflow.
		/// </summary>
		ConvOvfU8 = 0xBA,
		/// <summary>
		/// conv.ovf.u8.un - Convert unsigned to an unsigned int64 (on the stack as int64) and throw an exception on overflow.
		/// </summary>
		ConvOvfU8Un = 0x89,

		/// <summary>
		/// conv.r.un - Convert unsigned integer to floating-point, pushing F on stack.
		/// </summary>
		ConvRUn = 0x76,
		/// <summary>
		/// conv.r4 - Convert to float32, pushing F on stack.
		/// </summary>
		ConvR4 = 0x6B,
		/// <summary>
		/// conv.r8 - Convert to float64, pushing F on stack.
		/// </summary>
		ConvR8 = 0x6C,
		/// <summary>
		/// conv.u - Convert to native unsigned int, pushing native int on stack.
		/// </summary>
		ConvU = 0xE0,
		/// <summary>
		/// conv.u1 - Convert to unsigned int8, pushing int32 on stack.
		/// </summary>
		ConvU1 = 0xD2,
		/// <summary>
		/// conv.u2 - Convert to unsigned int16, pushing int32 on stack.
		/// </summary>
		ConvU2 = 0xD1,
		/// <summary>
		/// conv.u4 - Convert to unsigned int32, pushing int32 on stack.
		/// </summary>
		ConvU4 = 0x6D,
		/// <summary>
		/// conv.u8 - Convert to unsigned int64, pushing int64 on stack.
		/// </summary>
		ConvU8 = 0x6E,
		/// <summary>
		/// cpblk - Copy data from memory to memory.
		/// </summary>
		CpBlk = 0x17FE,
		/// <summary>
		/// cpobj &lt;typeTok&gt; - Copy a value type from src to dest.
		/// </summary>
		CpObj = 0x70,
		/// <summary>
		/// div - Divide two values to return a quotient or floating-point result.
		/// </summary>
		Div = 0x5B,
		/// <summary>
		/// div.un - Divide two values, unsigned, returning a quotient.
		/// </summary>
		DivUn = 0x5C,
		/// <summary>
		/// dup - Duplicate the value on the top of the stack.
		/// </summary>
		Dup = 0x25,
		/// <summary>
		/// endfault - End fault clause of an exception block.
		/// </summary>
		EndFault = 0xDC,
		/// <summary>
		/// endfilter - End an exception handling filter clause.
		/// </summary>
		EndFilter = 0x11FE,
		/// <summary>
		/// endfinally - End finally clause of an exception block.
		/// </summary>
		EndFinally = 0xDC,
		/// <summary>
		/// initblk - Set all bytes in a block of memory to a given byte value.
		/// </summary>
		InitBlk = 0x18FE,
		/// <summary>
		/// initobj &lt;typeTok&gt; - initialize the value at address dest.
		/// </summary>
		InitObj = 0x15FE,
		/// <summary>
		/// isinst &lt;class&gt; - Test if obj is an instance of class, returning null or an instance of that class or interface. 
		/// </summary>
		IsInst = 0x75,
		/// <summary>
		/// jmp &lt;method&gt; - Exit current method and jump to the specified method.
		/// </summary>
		Jmp = 0x27,
		/// <summary>
		/// ldarg &lt;uint16 (num)&gt; - Load argument numbered num onto the stack.
		/// </summary>
		LdArg = 0x09FE,
		/// <summary>
		/// ldarg.0 - Load argument 0 onto the stack.
		/// </summary>
		LdArg0 = 0x02,
		/// <summary>
		/// ldarg.1 - Load argument 1 onto the stack.
		/// </summary>
		LdArg1 = 0x03,
		/// <summary>
		/// ldarg.2 - Load argument 2 onto the stack.
		/// </summary>
		LdArg2 = 0x04,
		/// <summary>
		/// ldarg.3 - Load argument 3 onto the stack.
		/// </summary>
		LdArg3 = 0x05,
		/// <summary>
		/// ldarg.s &lt;uint8 (num)&gt; Load argument numbered num onto the stack, short form.
		/// </summary>
		LdArgS = 0x0E,
		/// <summary>
		/// ldarga &lt;uint16 (argNum)&gt; - Fetch the address of argument argNum.
		/// </summary>
		LdArgA = 0x0AFE,
		/// <summary>
		/// ldarga.s &lt;uint8 (argNum)&gt; - Fetch the address of argument argNum, short form.
		/// </summary>
		LdArgAS = 0x0F,
		/// <summary>
		/// Push num of type int32 onto the stack as int32.
		/// </summary>
		LdCI4 = 0x20,
		/// <summary>
		/// ldc.i4.m1 - Push the constant value -1 onto the stack as int32.
		/// </summary>
		LdCI4_M1 = 0x15,
		/// <summary>
		/// ldc.i4.0 - Push the constant value 0 onto the stack as int32.
		/// </summary>
		LdCI4_0 = 0x16,
		/// <summary>
		/// ldc.i4.1 - Push the constant value 1 onto the stack as int32.
		/// </summary>
		LdCI4_1 = 0x17,
		/// <summary>
		/// ldc.i4.2 - Push the constant value 2 onto the stack as int32.
		/// </summary>
		LdCI4_2 = 0x18,
		/// <summary>
		/// ldc.i4.3 - Push the constant value 3 onto the stack as int32.
		/// </summary>
		LdCI4_3 = 0x19,
		/// <summary>
		/// ldc.i4.4 - Push the constant value 4 onto the stack as int32.
		/// </summary>
		LdCI4_4 = 0x1A,
		/// <summary>
		/// ldc.i4.5 - Push the constant value 5 onto the stack as int32.
		/// </summary>
		LdCI4_5 = 0x1B,
		/// <summary>
		/// ldc.i4.6 - Push the constant value 6 onto the stack as int32.
		/// </summary>
		LdCI4_6 = 0x1C,
		/// <summary>
		/// ldc.i4.7 - Push the constant value 7 onto the stack as int32.
		/// </summary>
		LdCI4_7 = 0x1D,
		/// <summary>
		/// ldc.i4.8 - Push the constant value 8 onto the stack as int32.
		/// </summary>
		LdCI4_8 = 0x1E,
		/// <summary>
		/// ldc.i4.s &lt;int8 (num)&gt; - Push num onto the stack as int32, short form.
		/// </summary>
		LdCI4S = 0x1F,

		/// <summary>
		/// ldc.i8 &lt;int64 (num)&gt; - Push num of type int64 onto the stack as int64.
		/// </summary>
		LdCI8 = 0x21,
		/// <summary>
		/// ldc.r4 &lt;float32 (num)&gt; - Push num of type float32 onto the stack as F.
		/// </summary>
		LdCR4 = 0x22,
		/// <summary>
		/// ldc.r8 &lt;float64 (num)&gt; - Push num of type float64 onto the stack as F.
		/// </summary>
		LdCR8 = 0x23,
		/// <summary>
		/// ldelem &lt;typeTok&gt; - Load the element at index onto the top of the stack.
		/// </summary>
		LdElem = 0xA3,
		/// <summary>
		/// ldelem.i - Load the element with type native int at index onto the top of the stack as a native int.
		/// </summary>
		LdElemI = 0x97,
		/// <summary>
		/// ldelem.i1 - Load the element with type int8 at index onto the top of the stack as an int32.
		/// </summary>
		LdElemI1 = 0x90,
		/// <summary>
		/// ldelem.u1 - Load the element with type unsigned int8 at index onto the top of the stack as an int32.
		/// </summary>
		LdElemU1 = 0x91,
		/// <summary>
		/// ldelem.i2 - Load the element with type int16 at index onto the top of the stack as an int32.
		/// </summary>
		LdElemI2 = 0x92,
		/// <summary>
		/// ldelem.u1 - Load the element with type unsigned int8 at index onto the top of the stack as an int32.
		/// </summary>
		LdElemU2 = 0x93,
		/// <summary>
		/// ldelem.i4 - Load the element with type int32 at index onto the top of the stack as an int32.
		/// </summary>
		LdElemI4 = 0x94,
		/// <summary>
		/// ldelem.u1 - Load the element with type unsigned int8 at index onto the top of the stack as an int32.
		/// </summary>
		LdElemU4 = 0x95,
		/// <summary>
		/// ldelem.i8 / ldelem.u8 - Load the element with type (unsigned) int64 at index onto the top of the stack as an int64.
		/// </summary>
		LdElemI8 = 0x96,
		/// <summary>
		/// ldelem.i4 - Load the element with type float32 at index onto the top of the stack as an F.
		/// </summary>
		LdElemR4 = 0x98,
		/// <summary>
		/// ldelem.i8 - Load the element with type float64 at index onto the top of the stack as an F.
		/// </summary>
		LdElemR8 = 0x99,
		/// <summary>
		/// ldelem.ref - Load the element at index onto the top of the stack as an O. The type of the O is the same as the element type of the array pushed on the CIL stack.
		/// </summary>
		LdElemRef = 0x9A,
		/// <summary>
		/// ldelema &lt;class&gt; - Load the address of element at index onto the top of the stack.
		/// </summary>
		LdElemA = 0x8F,
		/// <summary>
		/// ldfld &lt;field&gt; - Push the value of field of object (or value type) obj, onto the stack.
		/// </summary>
		LdFld = 0x7B,
		/// <summary>
		/// ldflda &lt;field&gt; - Push the address of field of object obj on the stack.
		/// </summary>
		LdFldA = 0x7C,
		/// <summary>
		/// ldftn &lt;method&gt; - Push a pointer to a method referenced by method, on the stack.
		/// </summary>
		LdFtn = 0x06FE,
		/// <summary>
		/// ldind.i - Indirect load value of type native int as native int on the stack
		/// </summary>
		LdIndI = 0x4D,
		/// <summary>
		/// ldind.i1 - Indirect load value of type int8 as int32 on the stack
		/// </summary>
		LdIndI1 = 0x46,
		/// <summary>
		/// ldind.u1 - Indirect load value of type unsigned int8 as int32 on the stack
		/// </summary>
		LdIndU1 = 0x47,
		/// <summary>
		/// ldind.i2 - Indirect load value of type int16 as int32 on the stack
		/// </summary>
		LdIndI2 = 0x48,
		/// <summary>
		/// ldind.u2 - Indirect load value of type unsigned int16 as int32 on the stack
		/// </summary>
		LdIndU2 = 0x49,
		/// <summary>
		/// ldind.i4 - Indirect load value of type int32 as int32 on the stack
		/// </summary>
		LdIndI4 = 0x4A,
		/// <summary>
		/// ldind.u4 - Indirect load value of type unsigned int32 as int32 on the stack
		/// </summary>
		LdIndU4 = 0x4B,
		/// <summary>
		/// ldind.i8 / ldind.u8 - Indirect load value of type (unsigned) int64 as int64 on the stack
		/// </summary>
		LdIndI8 = 0x4C,
		/// <summary>
		/// ldind.r8 - Indirect load value of type float32 as F on the stack
		/// </summary>
		LdIndR4 = 0x4E,
		/// <summary>
		/// ldind.r8 - Indirect load value of type float64 as F on the stack
		/// </summary>
		LdIndR8 = 0x4F,
		/// <summary>
		/// ldind.ref - Indirect load value of type object ref as O on the stack.
		/// </summary>
		LdIndRef = 0x50,

		/// <summary>
		/// ldlen - Push the length (of type native unsigned int) of array on the stack.
		/// </summary>
		LdLen = 0x8E,

		/// <summary>
		/// ldloc &lt;uint16 (indx)&gt; - Load local variable of index indx onto stack.
		/// </summary>
		LdLoc = 0x0CFE,
		/// <summary>
		/// ldloc.0 - Load local variable 0 onto stack.
		/// </summary>
		LdLoc0 = 0x06,
		/// <summary>
		/// ldloc.1 - Load local variable 1 onto stack.
		/// </summary>
		LdLoc1 = 0x07,
		/// <summary>
		/// ldloc.2 - Load local variable 2 onto stack.
		/// </summary>
		LdLoc2 = 0x08,
		/// <summary>
		/// ldloc.3 - Load local variable 3 onto stack.
		/// </summary>
		LdLoc3 = 0x09,
		/// <summary>
		/// ldloc.s &lt;uint8 (indx)&gt; - Load local variable of index indx onto stack, short form.
		/// </summary>
		LdLocS = 0x11,
		/// <summary>
		/// ldloca &lt;uint16 (indx)&gt; - Load address of local variable with index indx.
		/// </summary>
		LdLocA = 0x0DFE,
		/// <summary>
		/// ldloca.s &lt;uint16 (indx)&gt; - Load address of local variable with index indx, short form.
		/// </summary>
		LdLocAS = 0x12,
		/// <summary>
		/// ldnull - Push a null reference on the stack.
		/// </summary>
		LdNull = 0x14,
		/// <summary>
		/// ldobj &lt;typeTok&gt; - Copy the value stored at address src to the stack.
		/// </summary>
		LdObj = 0x71,
		/// <summary>
		/// ldsfld &lt;field&gt; - Push the value of the static field on the stack.
		/// </summary>
		LdSFld = 0x7E,
		/// <summary>
		/// ldsflda &lt;field&gt; - Push the address of the static field, field, on the stack.
		/// </summary>
		LdSFldA = 0x7F,
		/// <summary>
		/// ldstr &lt;string&gt; - Push a string object for the literal string.
		/// </summary>
		LdStr = 0x72,
		/// <summary>
		/// ldtoken &lt;token&gt; - Convert metadata token to its runtime representation.
		/// </summary>
		LdToken = 0xD0,
		/// <summary>
		/// ldvirtftn &lt;method&gt; - Push address of virtual method on the stack.
		/// </summary>
		LdVirtFtn = 0x07FE,
		/// <summary>
		/// leave &lt;int32 (target)&gt; - Exit a protected region of code.
		/// </summary>
		Leave = 0xDD,
		/// <summary>
		/// leave &lt;int8 (target)&gt; - Exit a protected region of code, short form.
		/// </summary>
		LeaveS = 0xDE,
		/// <summary>
		/// localloc - Allocate space from the local memory pool.
		/// </summary>
		LocAlloc = 0x0FFE,
		/// <summary>
		/// mkrefany &lt;class&gt; - Push a typed reference to ptr of type class onto the stack.
		/// </summary>
		MkRefAny = 0xC6,
		/// <summary>
		/// mul - Multiply values.
		/// </summary>
		Mul = 0x5A,
		/// <summary>
		/// mul.ovf - Multiply signed integer values. Signed result shall fit in same size.
		/// </summary>
		MulOvf = 0xD8,
		/// <summary>
		/// mul.ovf.un - Multiply unsigned integer values. Unsigned result shall fit in same size.
		/// </summary>
		MulOvfUn = 0xD9,
		/// <summary>
		/// neg - Negate value.
		/// </summary>
		Neg = 0x65,
		/// <summary>
		/// newarr &lt;etype&gt; - Create a new array with elements of type etype.
		/// </summary>
		NewArr = 0x8D,
		/// <summary>
		/// newobj &lt;ctor&gt; - Allocate an uninitialized object or value type and call ctor.
		/// </summary>
		NewObj = 0x73,
		/// <summary>
		/// no.{typecheck, rangecheck, nullcheck} - The specified fault check(s) normally performed as part of the execution of the subsequent instruction can/shall be skipped.
		/// </summary>
		No = 0x19FE,
		/// <summary>
		/// nop - Do nothing (No operation).
		/// </summary>
		Nop = 0x00,
		/// <summary>
		/// not - Bitwise complement (logical not).
		/// </summary>
		Not = 0x66,
		/// <summary>
		/// or - Bitwise OR of two integer values, returns an integer.
		/// </summary>
		Or = 0x60,
		/// <summary>
		/// pop - Pop value from the stack.
		/// </summary>
		Pop = 0x26,
		/// <summary>
		/// readonly. - Specify that the subsequent array address operation performs no type check at runtime, and that it returns a controlled-mutability managed pointer.
		/// </summary>
		Readonly = 0x1EFE,
		/// <summary>
		/// refanytype - Push the type token stored in a typed reference.
		/// </summary>
		RefAnyType = 0x1DFE,
		/// <summary>
		/// refanyval &lt;type&gt; - Push the address stored in a typed reference.
		/// </summary>
		RefAnyVal = 0xC2,
		/// <summary>
		/// rem - Remainder when dividing one value by another.
		/// </summary>
		Rem = 0x5D,
		/// <summary>
		/// rem.un - Remainder when dividing one unsigned value by another.
		/// </summary>
		RemUn = 0x5E,
		/// <summary>
		/// ret - Return from method, possibly with a value.
		/// </summary>
		Ret = 0x2A,
		/// <summary>
		/// rethrow - Rethrow the current exception.
		/// </summary>
		Rethrow = 0x1AFE,
		/// <summary>
		/// shl - Shift an integer left (shifting in zeros), return an integer.
		/// </summary>
		Shl = 0x62,
		/// <summary>
		/// shr - Shift an integer right (shift in sign), return an integer.
		/// </summary>
		Shr = 0x63,
		/// <summary>
		/// shr.un - Shift an integer right (shift in zero), return an integer.
		/// </summary>
		ShrUn = 0x64,
		/// <summary>
		/// sizeof &lt;typeTok&gt; - Push the size, in bytes, of a type as an unsigned int32.
		/// </summary>
		SizeOf = 0x1CFE,
		/// <summary>
		/// starg &lt;uint16 (num)&gt; - Store value to the argument numbered num.
		/// </summary>
		StArg = 0x0BFE,
		/// <summary>
		/// starg.s &lt;uint8 (num)&gt; - Store value to the argument numbered num, short form.
		/// </summary>
		StArgS = 0x10,
		/// <summary>
		/// stelem &lt;typeTok&gt; - Replace array element at index with the value on the stack
		/// </summary>
		StElem = 0xA4,
		/// <summary>
		/// stelem.i - Replace array element at index with the i value on the stack.
		/// </summary>
		StElemI = 0x9B,
		/// <summary>
		/// stelem.i1 - Replace array element at index with the int8 value on the stack.
		/// </summary>
		StElemI1 = 0x9C,
		/// <summary>
		/// stelem.i2 - Replace array element at index with the int16 value on the stack.
		/// </summary>
		StElemI2 = 0x9D,
		/// <summary>
		/// stelem.i1 - Replace array element at index with the int32 value on the stack.
		/// </summary>
		StElemI4 = 0x9E,
		/// <summary>
		/// stelem.i2 - Replace array element at index with the int64 value on the stack.
		/// </summary>
		StElemI8 = 0x9F,
		/// <summary>
		/// stelem.r4 - Replace array element at index with the float32 value on the stack.
		/// </summary>
		StElemR4 = 0xA0,
		/// <summary>
		/// stelem.r8 - Replace array element at index with the float64 value on the stack.
		/// </summary>
		StElemR8 = 0xA1,
		/// <summary>
		/// stelem.ref - Replace array element at index with the ref value on the stack.
		/// </summary>
		StElemRef = 0xA2,
		/// <summary>
		/// stfld &lt;field&gt; - Replace the value of field of the object obj with value.
		/// </summary>
		StFld = 0x7D,
		/// <summary>
		/// stind.i - Store value of type native int into memory at address
		/// </summary>
		StIndI = 0xDF,
		/// <summary>
		/// stind.i1 - Store value of type int8 into memory at address
		/// </summary>
		StIndI1 = 0x52,
		/// <summary>
		/// stind.i2 - Store value of type int16 into memory at address
		/// </summary>
		StIndI2 = 0x53,
		/// <summary>
		/// stind.i4 - Store value of type int32 into memory at address
		/// </summary>
		StIndI4 = 0x54,
		/// <summary>
		/// stind.i8 - Store value of type int64 into memory at address
		/// </summary>
		StIndI8 = 0x55,
		/// <summary>
		/// stind.r4 - Store value of type float32 into memory at address
		/// </summary>
		StIndR4 = 0x56,
		/// <summary>
		/// stind.r8 - Store value of type float64 into memory at address
		/// </summary>
		StIndR8 = 0x57,
		/// <summary>
		/// stind.ref - Store value of type object ref (type O) into memory at address
		/// </summary>
		StIndRef = 0x51,
		/// <summary>
		/// stloc &lt;uint16 (indx)&gt; - Pop a value from stack into local variable indx.
		/// </summary>
		StLoc = 0x0EFE,
		/// <summary>
		/// stloc.0 - Pop a value from stack into local variable 0.
		/// </summary>
		StLoc0 = 0x0A,
		/// <summary>
		/// stloc.1 - Pop a value from stack into local variable 1.
		/// </summary>
		StLoc1 = 0x0B,
		/// <summary>
		/// stloc.2 - Pop a value from stack into local variable 2.
		/// </summary>
		StLoc2 = 0x0C,
		/// <summary>
		/// stloc.3 - Pop a value from stack into local variable 3.
		/// </summary>
		StLoc3 = 0x0D,
		/// <summary>
		/// stloc.s &lt;uint8 (indx)&gt; - Pop a value from stack into local variable indx, short form.
		/// </summary>
		StLocS = 0x13,
		/// <summary>
		/// stobj &lt;typeTok&gt; - Store a value of type typeTok at an address.
		/// </summary>
		StObj = 0x81,
		/// <summary>
		/// stsfld &lt;field&gt; - Replace the value of the static field with val.
		/// </summary>
		StSFld = 0x80,
		/// <summary>
		/// sub - Subtract value2 from value1, returning a new value.
		/// </summary>
		Sub = 0x59,
		/// <summary>
		/// sub.ovf - Subtract native int from a native int. Signed result shall fit in same size.
		/// </summary>
		SubOvf = 0xDA,
		/// <summary>
		/// sub.ovf.un - Subtract native unsigned int from a native unsigned int. Unsigned result shall fit in same size.
		/// </summary>
		SubOvfUn = 0xDB,
		/// <summary>
		/// switch &lt;uint32, int32, int32 (t1..tN)&gt; - Jump to one of n values.
		/// </summary>
		Switch = 0x45,
		/// <summary>
		/// tail. - Subsequent call terminates current method.
		/// </summary>
		Tail = 0x14FE,
		/// <summary>
		/// throw - Throw an exception.
		/// </summary>
		Throw = 0x7A,

		/// <summary>
		/// unaligned.(alignment) - Subsequent pointer instruction might be unaligned.
		/// </summary>
		Unaligned = 0x12FE,
		/// <summary>
		/// unbox &lt;valuetype&gt; - Extract a value-type from obj, its boxed representation, and push a controlled-mutability managed pointer to it to the top of the stack.
		/// </summary>
		Unbox = 0x79,
		/// <summary>
		/// unbox.any &lt;typeTok&gt; - Extract a value-type from obj, its boxed representation, and copy to the top of the stack
		/// </summary>
		UnboxAny = 0xA5,
		/// <summary>
		/// Subsequent pointer reference is volatile.
		/// </summary>
		Volatile = 0x13FE,
		/// <summary>
		/// xor - Bitwise XOR of integer values, returns an integer.
		/// </summary>
		Xor = 0x61
	}
}
