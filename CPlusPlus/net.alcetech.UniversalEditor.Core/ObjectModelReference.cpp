/*
 * ObjectModelReference.cpp
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#include "ObjectModelReference.h"

namespace UniversalEditor {

List<ObjectModelReference*>* ObjectModelReference::_objectModelReferencesList = new List<ObjectModelReference*>();
List<ObjectModelReference*>* ObjectModelReference::_visibleObjectModelReferencesList = NULL;

ObjectModelReference::ObjectModelReference() {
	this->_ID = Guid::EMPTY;
	this->_TypeName = String::EMPTY;
	this->_Visible = true;
}

ObjectModelReference::~ObjectModelReference() {
	// TODO Auto-generated destructor stub
}

List<ObjectModelReference*>* ObjectModelReference::getObjectModelReferencesList() {
	return ObjectModelReference::_objectModelReferencesList;
}
List<ObjectModelReference*>* ObjectModelReference::getVisibleObjectModelReferencesList() {
	if (ObjectModelReference::_visibleObjectModelReferencesList == NULL)
	{
		List<ObjectModelReference*>* listAll = ObjectModelReference::getObjectModelReferencesList();
		List<ObjectModelReference*>* listVisible = new List<ObjectModelReference*>();
		for (int i = 0; i < listAll->count(); i++)
		{
			ObjectModelReference* item = listAll->get(i);
			if (item->isVisible())
			{
				listVisible->add(item);
				if (item->getTypeName() != NULL)
				{
					printf("'%s' is visible\n", item->getTypeName()->toCharArray());
				}
			}
			else
			{
				if (item->getTypeName() != NULL)
				{
					printf("'%s' is not visible\n", item->getTypeName()->toCharArray());
				}
			}
		}
		ObjectModelReference::_visibleObjectModelReferencesList = listVisible;
	}
	return ObjectModelReference::_visibleObjectModelReferencesList;
}

ObjectModelReference* ObjectModelReference::getByTypeName(String* typeName, bool includeHidden) {
	return ObjectModelReference::getByTypeName(typeName->toCharArray(), includeHidden);
}
ObjectModelReference* ObjectModelReference::getByTypeName(const char* typeName, bool includeHidden) {
	List<ObjectModelReference*>* list = NULL;
	if (includeHidden)
	{
		list = ObjectModelReference::getObjectModelReferencesList();
	}
	else
	{
		list = ObjectModelReference::getVisibleObjectModelReferencesList();
	}
	for (int i = 0; i < list->count(); i++)
	{
		ObjectModelReference* item = list->get(i);
		if (item->getTypeName() == NULL) continue;

		if (item->getTypeName()->equals(typeName)) return item;
	}
	return NULL;
}

ObjectModel* ObjectModelReference::createInstance() {
	return NULL;
}

} /* namespace UniversalEditor */
