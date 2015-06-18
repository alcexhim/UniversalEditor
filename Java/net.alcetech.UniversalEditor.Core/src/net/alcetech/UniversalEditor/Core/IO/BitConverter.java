package net.alcetech.UniversalEditor.Core.IO;

import net.alcetech.Core.NotImplementedException;

public class BitConverter
{
	public static byte[] getBytes(boolean value)
	{
		return new byte[] { value ? (byte)1 : (byte)0 };
	}
	public static byte[] getBytes(char value)
	{
		return getBytes((short)value);
	}
	public static byte[] getBytes(short value)
	{
		return new byte[]
		{
			(byte)((value & (0xFF << 0)) >> 0),
			(byte)((value & (0xFF << 8)) >> 8)
		};
	}
	public static byte[] getBytes(int value)
	{
		return new byte[]
		{
			(byte)((value & (0xFF << 0)) >> 0),
			(byte)((value & (0xFF << 8)) >> 8),
			(byte)((value & (0xFF << 16)) >> 16),
			(byte)((value & (0xFF << 24)) >> 24)
		};
	}
	public static byte[] getBytes(long value)
	{
		return new byte[]
		{
			(byte)((value & (0xFF << 0)) >> 0),
			(byte)((value & (0xFF << 8)) >> 8),
			(byte)((value & (0xFF << 16)) >> 16),
			(byte)((value & (0xFF << 24)) >> 24),
			(byte)((value & (0xFF << 32)) >> 32),
			(byte)((value & (0xFF << 40)) >> 40),
			(byte)((value & (0xFF << 48)) >> 48),
			(byte)((value & (0xFF << 56)) >> 56)
		};
	}
	public static byte[] getBytes(float value)
	{
		throw new NotImplementedException();
		/*
		return new byte[]
		{
			(byte)(((int)value & (0xFF << 0)) >> 0),
			(byte)(((int)value & (0xFF << 8)) >> 8),
			(byte)(((int)value & (0xFF << 16)) >> 16),
			(byte)(((int)value & (0xFF << 24)) >> 24)
		};
		*/
	}
}
