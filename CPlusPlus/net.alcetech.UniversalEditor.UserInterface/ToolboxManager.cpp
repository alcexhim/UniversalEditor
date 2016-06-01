/*
 * ToolboxManager.cpp
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#include "ToolboxManager.h"

namespace UniversalEditor {
namespace UserInterface {

List<ToolboxGroup*>* ToolboxManager::_groups = new List<ToolboxGroup*>();

void ToolboxManager::addGroup(ToolboxGroup* item) {
	ToolboxManager::getGroupsList()->add(item);
}
List<ToolboxGroup*>* ToolboxManager::getGroupsList() {
	return ToolboxManager::_groups;
}

} /* namespace UserInterface */
} /* namespace UniversalEditor */
