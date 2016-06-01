/*
 * ToolboxManager.h
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#ifndef TOOLBOXMANAGER_H_
#define TOOLBOXMANAGER_H_

#include <Collections/Generic/List.h>

#include "ToolboxGroup.h"

using ApplicationFramework::Collections::Generic::List;

namespace UniversalEditor {
namespace UserInterface {

class ToolboxManager {
private:
	static List<ToolboxGroup*>* _groups;
public:
	static void addGroup(ToolboxGroup* item);
	static List<ToolboxGroup*>* getGroupsList();
};

} /* namespace UserInterface */
} /* namespace UniversalEditor */

#endif /* TOOLBOXMANAGER_H_ */
