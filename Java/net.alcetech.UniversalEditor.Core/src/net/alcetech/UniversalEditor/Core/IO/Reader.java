package net.alcetech.UniversalEditor.Core.IO;

public class Reader
{
	private Endianness mvarEndianness = Endianness.BigEndian;
	public void setEndianness(Endianness value)
	{
		mvarEndianness = value;
	}
	public Endianness getEndianness()
	{
		return mvarEndianness;
	}
	
	
}
