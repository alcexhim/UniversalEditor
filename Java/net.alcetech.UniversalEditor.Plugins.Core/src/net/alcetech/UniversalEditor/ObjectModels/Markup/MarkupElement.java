package net.alcetech.UniversalEditor.ObjectModels.Markup;

public abstract class MarkupElement
{

	private String mvarName = "";
	public void setName(String value) { mvarName = value; }
	public String getName() { return mvarName; }
	
	private String mvarNamespace = "";
	public void setNamespace(String value) { mvarNamespace = value; }
	public String getNamespace() { return mvarNamespace; }
	
	public MarkupElement(String fullName)
	{
		this.setFullName(fullName);
	}
	public MarkupElement(String fullName, String value)
	{
		this.setFullName(fullName);
		this.setValue(value);
	}
	
	public String getFullName()
	{
		StringBuilder sb = new StringBuilder();
		if (mvarNamespace != "")
		{
			sb.append(mvarNamespace);
			sb.append(':');
		}
		sb.append(mvarName);
		return sb.toString();
	}
	public void setFullName(String value)
	{
		String[] parts = value.split(":");
		if (parts.length > 1)
		{
			mvarNamespace = parts[0];
			mvarName = parts[1];
		}
		else if (parts.length > 0)
		{
			mvarNamespace = "";
			mvarName = parts[0];
		}
	}
	
	private String mvarValue = "";
	public void setValue(String value) { mvarValue = value; mvarHasValue = true; }
	public String getValue() { return mvarValue; }
	
	private boolean mvarHasValue = false;
	public boolean hasValue() { return mvarHasValue; }
	
	public void clearValue()
	{
		mvarHasValue = false;
		mvarValue = "";
	}
	
	private MarkupTagElement _parentElement = null;
	public MarkupTagElement getParentElement() {
		return _parentElement;
	}
	/* internal */ void setParentElement(MarkupTagElement value) {
		_parentElement = value;
	}
}
