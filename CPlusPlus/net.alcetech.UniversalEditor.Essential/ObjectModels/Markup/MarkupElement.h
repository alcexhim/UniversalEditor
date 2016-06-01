/*
 * MarkupElement.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODELS_MARKUP_MARKUPELEMENT_H_
#define OBJECTMODELS_MARKUP_MARKUPELEMENT_H_

#include <Text/StringBuilder.h>
#include <String.h>

using ApplicationFramework::Text::StringBuilder;
using ApplicationFramework::String;

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

class MarkupElement {
private:
	String* _name;
	String* _namespace;

	MarkupElement* _parentElement;
public:
	MarkupElement();
	MarkupElement(MarkupElement* parentElement);

	virtual ~MarkupElement();

	String* getName();
	void setName(String* value);

	String* getNamespace();
	void setNamespace(String* value);

	String* getFullName();
	void setFullName(String* value);

	String* getXmlNamespace();

	MarkupElement* getParentElement();
	void setParentElement(MarkupElement* value);
};

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */

#endif /* OBJECTMODELS_MARKUP_MARKUPELEMENT_H_ */
