package net.alcetech.UniversalEditor.ObjectModels.Markup;

import java.util.ArrayList;

import net.alcetech.UniversalEditor.Core.*;
import net.alcetech.UniversalEditor.ObjectModels.Markup.Elements.*;

public class MarkupObjectModel extends ObjectModel
{
	private static ObjectModelReference _omr = null;
	public ObjectModelReference getObjectModelReference()
	{
		if (_omr == null)
		{
			_omr = super.getObjectModelReference();
		}
		return _omr;
	}
	
	private ArrayList<MarkupElement> _elements = new ArrayList<MarkupElement>();
	
	public void addElement(MarkupElement element)
	{
		_elements.add(element);
	}
	
	public MarkupElement[] getElements()
	{
		MarkupElement[] elements = new MarkupElement[_elements.size()];
		_elements.toArray(elements);
		return elements;
	}

	public MarkupTagElement[] getTagsByTagName(String tagName)
	{
		ArrayList<MarkupTagElement> list = new ArrayList<MarkupTagElement>();
		for (int i = 0; i < _elements.size(); i++)
		{
			MarkupTagElement tag = ((MarkupTagElement)_elements.get(i));
			if (tag == null) continue;
			
			if (tag.getFullName().equals(tagName)) list.add(tag);
		}
		
		MarkupTagElement[] array = new MarkupTagElement[list.size()];
		list.toArray(array);
		return array;
	}
}
