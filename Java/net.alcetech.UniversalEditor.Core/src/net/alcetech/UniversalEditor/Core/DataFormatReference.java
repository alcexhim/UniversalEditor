package net.alcetech.UniversalEditor.Core;

import net.alcetech.Core.NotImplementedException;
import net.alcetech.Core.Collections.ObjectModel.Collection;

public class DataFormatReference
{
	private String mvarTypeName = null;
	public DataFormatReference(String typeName)
	{
		mvarTypeName = typeName;
	}
	
	public DataFormat Create()
	{
		throw new NotImplementedException();
	}
	
	private Collection<ObjectModelReference> _supportedObjectModelCollection = new Collection<ObjectModelReference>();
	public Collection<ObjectModelReference> getSupportedObjectModelCollection() { return _supportedObjectModelCollection; }
}
