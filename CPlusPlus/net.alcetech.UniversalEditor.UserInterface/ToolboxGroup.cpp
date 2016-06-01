/*
 * ToolboxGroup.cpp
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#include "ToolboxGroup.h"

namespace UniversalEditor {
namespace UserInterface {

ToolboxGroup::ToolboxGroup() {
	this->_title = String::EMPTY;
}

ToolboxGroup::~ToolboxGroup() {
	// TODO Auto-generated destructor stub
}

String* ToolboxGroup::getTitle() {
	return this->_title;
}
void ToolboxGroup::setTitle(String* value) {
	this->_title = value;
}

} /* namespace UserInterface */
} /* namespace UniversalEditor */
