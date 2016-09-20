package net.alcetech.UniversalEditor.Core.IO;

import java.io.IOException;

import net.alcetech.ApplicationFramework.Exceptions.NotImplementedException;
import net.alcetech.UniversalEditor.Core.*;

public class Writer
{
	private Accessor _accessor = null;
	public Accessor getAccessor() { return _accessor; }
	public void setAccessor(Accessor value) { _accessor = value; }
	
	public Writer(Accessor accessor)
	{
		_accessor = accessor;
	}
	
	private Endianness mvarEndianness = Endianness.LittleEndian;
	public void setEndianness(Endianness value)
	{
		mvarEndianness = value;
	}
	public Endianness getEndianness()
	{
		return mvarEndianness;
	}

	public void writeByte(byte value) throws IOException
	{
		byte[] buffer = new byte[] { value };
		writeByteArray(buffer);
	}
	public void writeInt16(short value) throws IOException
	{
		byte[] _buffer = BitConverter.getBytes(value);
		byte[] buffer = new byte[2];
		if (getEndianness() == Endianness.BigEndian)
		{
			buffer[1] = _buffer[0];
			buffer[0] = _buffer[1];
		}
		else
		{
			buffer[0] = _buffer[0];
			buffer[1] = _buffer[1];
		}
		writeByteArray(buffer);
	}
	public void writeInt32(int value) throws IOException
	{
		byte[] _buffer = BitConverter.getBytes(value);
		byte[] buffer = new byte[4];
		if (getEndianness() == Endianness.BigEndian)
		{
			buffer[3] = _buffer[0];
			buffer[2] = _buffer[1];
			buffer[1] = _buffer[2];
			buffer[0] = _buffer[3];
		}
		else
		{
			buffer[0] = _buffer[0];
			buffer[1] = _buffer[1];
			buffer[2] = _buffer[2];
			buffer[3] = _buffer[3];
		}
		writeByteArray(buffer);
	}
	public void writeInt64(long value) throws IOException
	{
		byte[] _buffer = BitConverter.getBytes(value);
		byte[] buffer = new byte[8];
		if (getEndianness() == Endianness.BigEndian)
		{
			buffer[7] = _buffer[0];
			buffer[6] = _buffer[1];
			buffer[5] = _buffer[2];
			buffer[4] = _buffer[3];
			buffer[3] = _buffer[4];
			buffer[2] = _buffer[5];
			buffer[1] = _buffer[6];
			buffer[0] = _buffer[7];
		}
		else
		{
			buffer[0] = _buffer[0];
			buffer[1] = _buffer[1];
			buffer[2] = _buffer[2];
			buffer[3] = _buffer[3];
			buffer[4] = _buffer[4];
			buffer[5] = _buffer[5];
			buffer[6] = _buffer[6];
			buffer[7] = _buffer[7];
		}
		writeByteArray(buffer);
	}
	public void writeSingle(float value) throws IOException, NotImplementedException
	{
		byte[] _buffer = BitConverter.getBytes(value);
		byte[] buffer = new byte[4];
		if (getEndianness() == Endianness.BigEndian)
		{
			buffer[3] = _buffer[0];
			buffer[2] = _buffer[1];
			buffer[1] = _buffer[2];
			buffer[0] = _buffer[3];
		}
		else
		{
			buffer[0] = _buffer[0];
			buffer[1] = _buffer[1];
			buffer[2] = _buffer[2];
			buffer[3] = _buffer[3];
		}
		writeByteArray(buffer);
	}
	
	public void writeByteArray(byte[] value) throws IOException
	{
		_accessor.write(value);
	}
	
}
