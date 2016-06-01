/*
 * MarkupAttribute.cpp
 *
 *  Created on: Apr 2, 2016
 *      Author: beckermj
 */

#include "MarkupAttribute.h"

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

MarkupAttribute::MarkupAttribute() {
	this->_name = NULL;
	this->_namespace = NULL;
	this->_value = NULL;
}
MarkupAttribute::MarkupAttribute(const char* name) {
	this->_name = NULL;
	this->_namespace = NULL;
	this->_value = NULL;

	this->setFullName(new String(name));
}
MarkupAttribute::MarkupAttribute(const char* name, const char* value) {
	this->_name = NULL;
	this->_namespace = NULL;
	this->_value = new String(value);

	this->setFullName(new String(name));
}
MarkupAttribute::MarkupAttribute(String* name) {
	this->_name = NULL;
	this->_namespace = NULL;
	this->_value = NULL;

	this->setFullName(name);
}
MarkupAttribute::MarkupAttribute(String* name, String* value) {
	this->_name = NULL;
	this->_namespace = NULL;
	this->_value = value;

	this->setFullName(name);
}

MarkupAttribute::~MarkupAttribute() {
	// TODO Auto-generated destructor stub
}

String* MarkupAttribute::getName() {
	return this->_name;
}
void MarkupAttribute::setName(String* value) {
	this->_name = value;
}

String* MarkupAttribute::getNamespace() {
	return this->_namespace;
}
void MarkupAttribute::setNamespace(String* value) {
	this->_namespace = value;
}

String* MarkupAttribute::getFullName() {
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
void MarkupAttribute::setFullName(String* name) {
}

String* MarkupAttribute::getValue() {
	return this->_value;
}
void MarkupAttribute::setValue(String* value) {
	this->_value = value;
}

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */
