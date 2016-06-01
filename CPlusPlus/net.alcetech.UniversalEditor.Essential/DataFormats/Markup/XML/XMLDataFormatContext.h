/*
 * XMLDataFormatContext.h
 *
 *  Created on: Apr 12, 2016
 *      Author: beckermj
 */

#ifndef DATAFORMATS_MARKUP_XML_XMLDATAFORMATCONTEXT_H_
#define DATAFORMATS_MARKUP_XML_XMLDATAFORMATCONTEXT_H_

#include "../../../ObjectModels/Markup/MarkupAttribute.h"
#include "../../../ObjectModels/Markup/MarkupElement.h"
#include "../../../ObjectModels/Markup/MarkupTagElement.h"

#include <Property.h>

using UniversalEditor::ObjectModels::Markup::MarkupAttribute;
using UniversalEditor::ObjectModels::Markup::MarkupElement;
using UniversalEditor::ObjectModels::Markup::MarkupTagElement;

namespace UniversalEditor {
namespace DataFormats {
namespace Markup {
namespace XML {

class XMLDataFormatContext {

	AFX_PROPERTY_BOOL(InsideTag);
	AFX_PROPERTY_BOOL(InsideTagName);
	AFX_PROPERTY_BOOL(InsideString);
	AFX_PROPERTY_BOOL(InsidePreprocessor);
	AFX_PROPERTY_BOOL(InsideSpecialDeclaration);
	AFX_PROPERTY_BOOL(InsideAttributeArea);
	AFX_PROPERTY_BOOL(InsideTagValue);
	AFX_PROPERTY_BOOL(ClosingCurrentElement);

	AFX_PROPERTY(MarkupAttribute*, NextAttribute);
	AFX_PROPERTY(MarkupElement*, CurrentElement);
	AFX_PROPERTY(char, NextChar);
	AFX_PROPERTY(char, PrevChar);
	AFX_PROPERTY(StringBuilder*, NextAttributeName);
	AFX_PROPERTY(StringBuilder*, NextString);

public:
	XMLDataFormatContext();
	virtual ~XMLDataFormatContext();

	void continueLoop();
	MarkupTagElement* getCurrentTag();

	bool isWhitespace(char value);
	bool isWhitespaceNotSpace(char value);
};

} /* namespace XML */
} /* namespace Markup */
} /* namespace DataFormats */
} /* namespace UniversalEditor */

#endif /* DATAFORMATS_MARKUP_XML_XMLDATAFORMATCONTEXT_H_ */
