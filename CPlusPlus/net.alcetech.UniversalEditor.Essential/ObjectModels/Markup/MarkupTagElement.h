/*
 * MarkupTagElement.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODELS_MARKUP_MARKUPTAGELEMENT_H_
#define OBJECTMODELS_MARKUP_MARKUPTAGELEMENT_H_

#include "MarkupAttribute.h"
#include "MarkupElement.h"

#include <Collections/Generic/List.h>
#include <Text/StringBuilder.h>

using ApplicationFramework::Collections::Generic::List;
using ApplicationFramework::Text::StringBuilder;

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

class MarkupTagElement : public MarkupElement {
private:
	List<MarkupAttribute*>* _attributes;
	List<MarkupElement*>* _elements;

	StringBuilder* _value;
public:
	MarkupTagElement();
	virtual ~MarkupTagElement();

	String* getValue();
	void setValue(String* value);

	void appendValue(String* value);

	void addAttribute(MarkupAttribute* attribute);
	void addAttribute(const char* name, const char* value);
	void addAttribute(String* name, String* value);

	List<MarkupAttribute*>* getAttributesList();

	MarkupAttribute* getAttribute(int index);
	MarkupAttribute* getAttribute(const char* name);
	MarkupAttribute* getAttribute(String* name);

	List<MarkupElement*>* getChildElementsList();

	List<MarkupTagElement*>* getChildTagsList();

	MarkupElement* getChildElement(int index);
	MarkupElement* getChildElement(const char* name);
	MarkupElement* getChildElement(String* name);

	MarkupTagElement* getChildTag(int index);
	MarkupTagElement* getChildTag(const char* name);
	MarkupTagElement* getChildTag(String* name);

	void addChildElement(MarkupElement* element);

	void merge(MarkupTagElement* element);
};

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */

#endif /* OBJECTMODELS_MARKUP_MARKUPTAGELEMENT_H_ */
