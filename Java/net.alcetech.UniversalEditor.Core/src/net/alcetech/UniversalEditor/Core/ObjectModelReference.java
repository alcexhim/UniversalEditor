package net.alcetech.UniversalEditor.Core;

import net.alcetech.Core.NotImplementedException;

public class ObjectModelReference
{
	private String mvarTypeName = null;
	public ObjectModelReference(String typeName)
	{
		mvarTypeName = typeName;
	}
	
	public ObjectModel Create()
	{
		throw new NotImplementedException();
	}
}
