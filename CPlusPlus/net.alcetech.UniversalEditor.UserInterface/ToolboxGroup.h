/*
 * ToolboxGroup.h
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#ifndef TOOLBOXGROUP_H_
#define TOOLBOXGROUP_H_

#include <String.h>

using ApplicationFramework::String;

namespace UniversalEditor {
namespace UserInterface {

class ToolboxGroup {
private:
	String* _title;
public:
	ToolboxGroup();
	virtual ~ToolboxGroup();

	String* getTitle();
	void setTitle(String* value);
};

} /* namespace UserInterface */
} /* namespace UniversalEditor */

#endif /* TOOLBOXGROUP_H_ */
