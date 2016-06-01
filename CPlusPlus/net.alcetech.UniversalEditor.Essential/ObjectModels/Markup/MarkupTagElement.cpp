/*
 * MarkupTagElement.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "MarkupTagElement.h"

#include <malloc.h>
#include <string.h>

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

MarkupTagElement::MarkupTagElement() {
	this->_attributes = new List<MarkupAttribute*>();
	this->_elements = new List<MarkupElement*>();
	this->_value = new StringBuilder();
}

MarkupTagElement::~MarkupTagElement() {
	// TODO Auto-generated destructor stub
}

String* MarkupTagElement::getValue() {
	return this->_value->toString();
}
void MarkupTagElement::setValue(String* value) {
	this->_value->set(value);
}
void MarkupTagElement::appendValue(String* value) {
	this->_value->append(value);
}

void MarkupTagElement::addAttribute(MarkupAttribute* attribute) {
	this->getAttributesList()->add(attribute);
}
void MarkupTagElement::addAttribute(const char* name, const char* value) {
	this->addAttribute(new MarkupAttribute(name, value));
}
void MarkupTagElement::addAttribute(String* name, String* value) {
	this->addAttribute(new MarkupAttribute(name, value));
}

List<MarkupAttribute*>* MarkupTagElement::getAttributesList() {
	return this->_attributes;
}
MarkupAttribute* MarkupTagElement::getAttribute(int index) {
	List<MarkupAttribute*>* list = this->getAttributesList();
	return list->get(index);
}
MarkupAttribute* MarkupTagElement::getAttribute(const char* name) {
	return this->getAttribute(new String(name));
}
MarkupAttribute* MarkupTagElement::getAttribute(String* name) {
	List<MarkupAttribute*>* list = this->getAttributesList();
	for (int i = 0; i < list->count(); i++)
	{
		MarkupAttribute* att = list->get(i);
		if (att->getFullName()->equals(name)) return att;
	}
	return NULL;
}

List<MarkupElement*>* MarkupTagElement::getChildElementsList() {
	return this->_elements;
}

List<MarkupTagElement*>* MarkupTagElement::getChildTagsList() {
	List<MarkupElement*>* elements = this->getChildElementsList();
	List<MarkupTagElement*>* list = new List<MarkupTagElement*>();
	for (int i = 0; i < elements->count(); i++)
	{
		MarkupTagElement* tag = dynamic_cast<MarkupTagElement*>(elements->get(i));
		if (tag == NULL) continue;

		list->add(tag);
	}
	return list;
}

MarkupElement* MarkupTagElement::getChildElement(int index) {
	List<MarkupElement*>* elements = this->getChildElementsList();
	return elements->get(index);
}
MarkupElement* MarkupTagElement::getChildElement(const char* name) {
	return this->getChildElement(new String(name));
}
MarkupElement* MarkupTagElement::getChildElement(String* name) {
	List<MarkupElement*>* elements = this->getChildElementsList();
	for (int i = 0; i < elements->count(); i++)
	{
		MarkupElement* elem = elements->get(i);
		if (elem->getFullName()->equals(name)) return elem;
	}
	return NULL;
}

MarkupTagElement* MarkupTagElement::getChildTag(int index) {
	MarkupElement* element = this->getChildElement(index);
	MarkupTagElement* tag = dynamic_cast<MarkupTagElement*>(element);
	return tag;
}
MarkupTagElement* MarkupTagElement::getChildTag(const char* name) {
	MarkupElement* element = this->getChildElement(name);
	MarkupTagElement* tag = dynamic_cast<MarkupTagElement*>(element);
	return tag;
}

void MarkupTagElement::addChildElement(MarkupElement* element){
	List<MarkupElement*>* list = this->getChildElementsList();
	element->setParentElement(this);
	list->add(element);
}

void MarkupTagElement::merge(MarkupTagElement* element) {
	List<MarkupAttribute*>* atts = element->getAttributesList();
	for (int i = 0; i < atts->count(); i++)
	{
		this->addAttribute(atts->get(i));
	}
	List<MarkupElement*>* elems = element->getChildElementsList();
	for (int i = 0; i < elems->count(); i++)
	{
		this->addChildElement(elems->get(i));
	}
}

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */
