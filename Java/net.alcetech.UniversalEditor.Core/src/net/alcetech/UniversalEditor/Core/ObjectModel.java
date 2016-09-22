package net.alcetech.UniversalEditor.Core;

public abstract class ObjectModel
{
	public ObjectModelReference getObjectModelReference()
	{
		return new ObjectModelReference(this.getClass().getName());
	}
	
	public abstract void copyTo(ObjectModel destination);
	public void copyFrom(ObjectModel source) {
		source.copyTo(this);
	}
	
	public abstract void clear();
}
