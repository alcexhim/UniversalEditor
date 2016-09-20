package net.alcetech.UniversalEditor.Core;

public class Document
{
	private Accessor mvarInputAccessor = null;
	public Accessor getInputAccessor() { return mvarInputAccessor; }
	public void setInputAccessor(Accessor value) { mvarInputAccessor = value; }

	private Accessor mvarOutputAccessor = null;
	public Accessor getOutputAccessor() { return mvarOutputAccessor; }
	public void setOutputAccessor(Accessor value) { mvarOutputAccessor = value; }
	
	public void setAccessor(Accessor value) {
		setInputAccessor(value);
		setOutputAccessor(value);
	}
	
	private DataFormat mvarInputDataFormat = null;
	public DataFormat getInputDataFormat() { return mvarInputDataFormat; }
	public void setInputDataFormat(DataFormat value) { mvarInputDataFormat = value; }

	private DataFormat mvarOutputDataFormat = null;
	public DataFormat getOutputDataFormat() { return mvarOutputDataFormat; }
	public void setOutputDataFormat(DataFormat value) { mvarOutputDataFormat = value; }
	
	public void setDataFormat(DataFormat value) {
		setInputDataFormat(value);
		setOutputDataFormat(value);
	}
	
	private ObjectModel mvarObjectModel = null;
	public ObjectModel getObjectModel() { return mvarObjectModel; }
	public void setObjectModel(ObjectModel value) { mvarObjectModel = value; }
	
	public static Document load(ObjectModel objectModel, DataFormat dataFormat, Accessor accessor) {
		Document document = new Document();
		document.setObjectModel(objectModel);
		document.setDataFormat(dataFormat);
		document.setAccessor(accessor);
		return document;
	}
}
