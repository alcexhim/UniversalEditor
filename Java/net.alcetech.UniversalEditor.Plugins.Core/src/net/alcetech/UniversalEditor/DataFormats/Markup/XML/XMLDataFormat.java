package net.alcetech.UniversalEditor.DataFormats.Markup.XML;

import net.alcetech.UniversalEditor.Core.*;
import net.alcetech.UniversalEditor.ObjectModels.Markup.*;

public class XMLDataFormat extends DataFormat
{
	private static DataFormatReference _dfr = null;
	public DataFormatReference getDataFormatReference()
	{
		if (_dfr == null)
		{
			_dfr = super.getDataFormatReference();
			_dfr.getSupportedObjectModelCollection().add(ObjectModelReference.fromClass(MarkupObjectModel.class));
		}
		return _dfr;
	}
}
