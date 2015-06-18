package net.alcetech.UniversalEditor.Core;

public abstract class DataFormat
{
	public DataFormatReference getDataFormatReference()
	{
		return new DataFormatReference(this.getClass().getName());
	}
}
