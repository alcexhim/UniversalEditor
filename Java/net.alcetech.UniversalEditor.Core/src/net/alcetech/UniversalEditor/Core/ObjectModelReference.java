package net.alcetech.UniversalEditor.Core;

import net.alcetech.Core.NotImplementedException;
import net.alcetech.Core.Collections.Generic.Dictionary;

public class ObjectModelReference
{
	private static Dictionary<Class<?>, ObjectModelReference> _omrsByClass = new Dictionary<Class<?>, ObjectModelReference>();
	public static ObjectModelReference fromClass(Class<?> clazz)
	{
		if (_omrsByClass.containsKey(clazz)) return _omrsByClass.getValueByKey(clazz);
		return null;
	}
	
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
