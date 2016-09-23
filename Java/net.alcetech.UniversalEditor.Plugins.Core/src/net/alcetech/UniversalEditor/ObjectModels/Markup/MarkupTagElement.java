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
	public MarkupAttribute[] getAttributes() {
		MarkupAttribute[] array = new MarkupAttribute[_attributes.size()];
		_attributes.toArray(array);
		return array;
	}
	public MarkupAttribute getAttribute(String name) {
		MarkupAttribute[] atts = this.getAttributes();
		for (int i = 0; i < atts.length; i++) {
			if (atts[i].getName().equals(name)) {
				return atts[i];
			}
		}
		return null;
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
	
	public MarkupTagElement getTagByTagName(String name) {
		MarkupElement[] elements = getElements();
		for (int i = 0; i < elements.length; i++) {
			if (elements[i].getName().equals(name)) {
				return (MarkupTagElement)elements[i];
			}
		}
		return null;
	}
	public MarkupTagElement[] getTags() {
		ArrayList<MarkupTagElement> list = new ArrayList<MarkupTagElement>();
		MarkupElement[] elements = getElements();
		for (int i = 0; i < elements.length; i++) {
			if (elements[i].getClass().isAssignableFrom(MarkupTagElement.class)) {
				list.add((MarkupTagElement)elements[i]);
			}
		}
		
		MarkupTagElement[] array = new MarkupTagElement[list.size()];
		list.toArray(array);
		return array;
	}

}
