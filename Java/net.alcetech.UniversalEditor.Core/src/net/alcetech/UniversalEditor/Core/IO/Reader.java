package net.alcetech.UniversalEditor.Core.IO;

import net.alcetech.UniversalEditor.Core.Accessor;

public class Reader
{
	private Accessor _accessor = null;
	public Accessor getAccessor() { return _accessor; }
	public void setAccessor(Accessor value) { _accessor = value; }
	
	public Reader(Accessor accessor)
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
	
	
}
