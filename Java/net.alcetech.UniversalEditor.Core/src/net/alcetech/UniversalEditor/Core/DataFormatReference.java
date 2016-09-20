package net.alcetech.UniversalEditor.Core;

import net.alcetech.ApplicationFramework.Exceptions.*;
import net.alcetech.ApplicationFramework.Collections.ObjectModel.*;

public class DataFormatReference
{
	private String mvarTypeName = null;
	public DataFormatReference(String typeName)
	{
		mvarTypeName = typeName;
	}
	
	public DataFormat Create() throws NotImplementedException
	{
		throw new NotImplementedException();
	}
	
	private Collection<ObjectModelReference> _supportedObjectModelCollection = new Collection<ObjectModelReference>();
	public Collection<ObjectModelReference> getSupportedObjectModelCollection() { return _supportedObjectModelCollection; }
}
