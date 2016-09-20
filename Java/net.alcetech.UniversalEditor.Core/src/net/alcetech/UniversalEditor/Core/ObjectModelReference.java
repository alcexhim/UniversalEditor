package net.alcetech.UniversalEditor.Core;

import net.alcetech.ApplicationFramework.Collections.Generic.Dictionary;

public class ObjectModelReference
{
	private static Dictionary<Class<?>, ObjectModelReference> _omrsByClass = new Dictionary<Class<?>, ObjectModelReference>();
	public static ObjectModelReference fromClass(Class<?> clazz)
	{
		if (_omrsByClass.containsKey(clazz)) return _omrsByClass.get(clazz);
		return null;
	}
	
	private String mvarTypeName = null;
	public ObjectModelReference(String typeName)
	{
		mvarTypeName = typeName;
	}
	
	public ObjectModel Create() throws InstantiationException, IllegalAccessException, ClassNotFoundException
	{
		Object newInst = Class.forName(this.mvarTypeName).newInstance();
		return (ObjectModel)newInst;
	}
}
