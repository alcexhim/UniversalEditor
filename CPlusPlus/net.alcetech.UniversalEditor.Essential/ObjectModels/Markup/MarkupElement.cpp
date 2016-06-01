/*
 * MarkupElement.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "MarkupElement.h"

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

MarkupElement::MarkupElement() {
	this->_name = String::EMPTY;
	this->_namespace = NULL;
	this->_parentElement = NULL;
}
MarkupElement::MarkupElement(MarkupElement* parentElement) {
	this->_name = String::EMPTY;
	this->_namespace = NULL;
	this->_parentElement = parentElement;
}

MarkupElement::~MarkupElement() {
	// TODO Auto-generated destructor stub
}

String* MarkupElement::getFullName()
{
	StringBuilder* sb = new StringBuilder();
	if (this->_namespace != NULL)
	{
		sb->append(this->_namespace);
		sb->append(":");
	}
	if (this->_name != NULL)
	{
		sb->append(this->_name);;
	}
	return sb->toString();
}
void MarkupElement::setFullName(String* value)
{
	StringBuilder* sb = new StringBuilder(value);
	List<String*>* vals = sb->split(":");
	if (vals->count() > 1)
	{
		this->setNamespace(vals->get(0));
		this->setName(vals->get(1));
	}
	else
	{
		this->setName(vals->get(0));
	}
}

String* MarkupElement::getName() {
	return this->_name;
}
void MarkupElement::setName(String* value) {
	this->_name = value;
}
String* MarkupElement::getNamespace() {
	return this->_namespace;
}
void MarkupElement::setNamespace(String* value) {
	this->_namespace = value;
}

String* MarkupElement::getXmlNamespace() {
	// first check to see if there is an attribute xmlns:* on this element

}

void MarkupElement::setParentElement(MarkupElement* element) {
	this->_parentElement = element;
}
MarkupElement* MarkupElement::getParentElement() {
	return this->_parentElement;
}

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */
