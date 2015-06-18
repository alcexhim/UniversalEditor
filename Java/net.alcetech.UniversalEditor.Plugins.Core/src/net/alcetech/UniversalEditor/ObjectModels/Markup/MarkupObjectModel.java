package net.alcetech.UniversalEditor.ObjectModels.Markup;

import net.alcetech.Core.Collections.ObjectModel.*;

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
	
	private MarkupElementCollection mvarElementCollection = new MarkupElementCollection();
	public MarkupElementCollection getElementCollection()
	{
		return mvarElementCollection;
	}
	
	public ReadOnlyCollection<MarkupTagElement> getElements()
	{
		Collection<MarkupTagElement> list = new Collection<MarkupTagElement>();
		int count = mvarElementCollection.count();
		for (int i = 0; i < count; i++)
		{
			MarkupElement el = mvarElementCollection.getByIndex(i);
			if (MarkupTagElement.class.isInstance(el)) list.add((MarkupTagElement)el);
		}
		return list.toReadOnlyCollection();
	}
	
	public ReadOnlyCollection<MarkupTagElement> getElementsByTagName(String tagName)
	{
		Collection<MarkupTagElement> list = new Collection<MarkupTagElement>();
		ReadOnlyCollection<MarkupTagElement> elems = getElements();
		for (int i = 0; i < elems.count(); i++)
		{
			if (elems.getByIndex(i).getFullName().equals(tagName)) list.add(elems.getByIndex(i));
		}
		return list.toReadOnlyCollection();
	}
}
