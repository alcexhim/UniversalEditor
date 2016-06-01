/*
 * MarkupObjectModel.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "MarkupObjectModel.h"

#include <string.h>

using ApplicationFramework::Collections::Generic::List;

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

MarkupObjectModel::MarkupObjectModel() {
	this->_elements = new List<MarkupElement*>();
}

MarkupObjectModel::~MarkupObjectModel() {
	// TODO Auto-generated destructor stub
}

List<MarkupElement*>* MarkupObjectModel::getChildElementsList() {
	return this->_elements;
}

MarkupElement* MarkupObjectModel::getChildElement(int index) {
	MarkupElement* element = this->_elements->get(index);
	return element;
}
MarkupElement* MarkupObjectModel::getChildElement(String* name) {
	for (int i = 0; i < this->_elements->count(); i++)
	{
		MarkupElement* element = this->_elements->get(i);
		if (element->getFullName()->equals(name)) return element;
	}
	return NULL;
}

List<MarkupTagElement*>* MarkupObjectModel::getChildTagsList() {
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

MarkupTagElement* MarkupObjectModel::getChildTag(int index) {
	MarkupElement* childElement = this->getChildElement(index);
	MarkupTagElement* tag = dynamic_cast<MarkupTagElement*>(childElement);
	return tag;
}
MarkupTagElement* MarkupObjectModel::getChildTag(String* name) {
	MarkupElement* childElement = this->getChildElement(name);
	MarkupTagElement* tag = dynamic_cast<MarkupTagElement*>(childElement);
	return tag;
}

void MarkupObjectModel::addChildElement(MarkupElement* element) {
	List<MarkupElement*>* list = this->getChildElementsList();
	list->add(element);
}
void MarkupObjectModel::removeChildElement(MarkupElement* element) {
	List<MarkupElement*>* list = this->getChildElementsList();
	list->remove(element);
}

void MarkupObjectModel::addChildTag(MarkupTagElement* element) {
	return this->addChildTag(element, true);
}
void MarkupObjectModel::addChildTag(MarkupTagElement* element, bool autoMerge) {
	if (autoMerge)
	{
		MarkupAttribute* attID = element->getAttribute("ID");
		if (attID != NULL)
		{
			printf("MarkupObjectModel: merging elements\n");
			MarkupTagElement* existing = this->getChildTag(attID->getValue());
			existing->merge(element);
		}
		return;
	}
	this->addChildElement(element);
}

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */
