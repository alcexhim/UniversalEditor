package net.alcetech.UniversalEditor.Core;

public abstract class ObjectModel
{
	public ObjectModelReference getObjectModelReference()
	{
		return new ObjectModelReference(this.getClass().getName());
	}
}
