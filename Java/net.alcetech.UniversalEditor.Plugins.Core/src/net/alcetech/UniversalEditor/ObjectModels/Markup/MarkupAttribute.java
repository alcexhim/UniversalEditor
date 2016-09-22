package net.alcetech.UniversalEditor.ObjectModels.Markup;

public class MarkupAttribute {
	
	private String _Schema = "";
	public String getSchema() { return _Schema; }
	public void setSchema(String value) { _Schema = value; }
	
	private String _Name = "";
	public String getName() { return _Name; }
	public void setName(String value) { _Name = value; }
	
	private String _Value = "";
	public String getValue() { return _Value; }
	public void setValue(String value) { _Value = value; }
	
	public String getFullName() {
		if (!"".equals(this.getSchema())) {
			return this.getSchema() + ":" + this.getName();
		}
		return this.getName();
	}
	public void setFullName(String value) {
		String[] parts = value.split(":", 2);
		if (parts.length == 2) {
			this.setSchema(parts[0]);
			this.setName(parts[1]);
		}
		else {
			this.setSchema("");
			this.setName(parts[0]);
		}
	}
	
	public MarkupAttribute(String fullName) {
		this(fullName, "");
	}
	public MarkupAttribute(String fullName, String value) {
		this.setFullName(fullName);
		this.setValue(value);
	}
}
