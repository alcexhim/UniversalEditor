package net.alcetech.UniversalEditor.ObjectModels.Markup;

import java.util.ArrayList;

import net.alcetech.UniversalEditor.ObjectModels.Markup.*;

public class MarkupTagElement extends MarkupElement
{
	public MarkupTagElement(String fullName)
	{
		super(fullName);
	}
	public MarkupTagElement(String fullName, String value)
	{
		super(fullName, value);
	}
	
	private ArrayList<MarkupAttribute> _attributes = new ArrayList<MarkupAttribute>();
	public void addAttribute(MarkupAttribute item) {
		_attributes.add(item);
	}
	public MarkupAttribute addAttribute(String name) {
		return addAttribute(name, "");
	}
	public MarkupAttribute addAttribute(String name, String value) {
		MarkupAttribute att = new MarkupAttribute(name, value);
		addAttribute(att);
		return att;
	}

	private ArrayList<MarkupElement> _elements = new ArrayList<MarkupElement>();
	public void addElement(MarkupElement item) {
		item.setParentElement(this);
		_elements.add(item);
	}
	public MarkupElement[] getElements() {
		MarkupElement[] elements = new MarkupElement[_elements.size()];
		_elements.toArray(elements);
		return elements;
	}

}
