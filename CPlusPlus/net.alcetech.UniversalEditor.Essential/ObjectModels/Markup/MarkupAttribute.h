/*
 * MarkupAttribute.h
 *
 *  Created on: Apr 2, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODELS_MARKUP_MARKUPATTRIBUTE_H_
#define OBJECTMODELS_MARKUP_MARKUPATTRIBUTE_H_

#include <String.h>
#include <Text/StringBuilder.h>

using ApplicationFramework::String;
using ApplicationFramework::Text::StringBuilder;

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

class MarkupAttribute {
private:
	String* _name;
	String* _namespace;
	String* _value;
public:
	MarkupAttribute();
	MarkupAttribute(const char* name);
	MarkupAttribute(const char* name, const char* value);
	MarkupAttribute(String* name);
	MarkupAttribute(String* name, String* value);

	virtual ~MarkupAttribute();

	String* getName();
	void setName(String* value);

	String* getNamespace();
	void setNamespace(String* value);

	String* getFullName();
	void setFullName(String* value);

	String* getValue();
	void setValue(String* value);
};

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */

#endif /* OBJECTMODELS_MARKUP_MARKUPATTRIBUTE_H_ */
