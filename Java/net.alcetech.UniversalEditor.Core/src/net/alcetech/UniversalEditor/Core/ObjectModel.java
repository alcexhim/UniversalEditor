package net.alcetech.UniversalEditor.Core;

import net.alcetech.UniversalEditor.Exceptions.ObjectModelNotSupportedException;

public abstract class ObjectModel
{
	public ObjectModelReference getObjectModelReference()
	{
		return new ObjectModelReference(this.getClass().getName());
	}
	
	public abstract void copyTo(ObjectModel destination) throws ObjectModelNotSupportedException;
	public void copyFrom(ObjectModel source) throws ObjectModelNotSupportedException {
		source.copyTo(this);
	}
	
	public abstract void clear();
}
