package net.alcetech.UniversalEditor.Core;

public class Document
{
	private Accessor mvarInputAccessor = null;
	public Accessor getInputAccessor() { return mvarInputAccessor; }
	public void setInputAccessor(Accessor value) { mvarInputAccessor = value; }

	private ObjectModel mvarObjectModel = null;
	public ObjectModel getObjectModel() { return mvarObjectModel; }
	public void setObjectModel(ObjectModel value) { mvarObjectModel = value; }
}
