/*
 * DataFormat.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "Accessor.h"
#include "DataFormat.h"

#include <stdio.h>

namespace UniversalEditor {

DataFormat::DataFormat() {
	// TODO Auto-generated constructor stub

}

DataFormat::~DataFormat() {
	// TODO Auto-generated destructor stub
}

void DataFormat::loadInternal(ObjectModel* objectModel)
{
	printf("ue: DataFormat: call to DataFormat::loadInternal requires subclass implementation");
}
void DataFormat::saveInternal(ObjectModel* objectModel)
{
	printf("ue: DataFormat: call to DataFormat::saveInternal requires subclass implementation");
}

void DataFormat::load(ObjectModel* objectModel)
{
	if (objectModel == nullptr)
	{
		printf("ue: DataFormat::load() - objectModel must not be null");
		return;
	}

	// TODO: BEGIN: implement Stack<ObjectModel> beforeLoadInternal() stuff here...
	// TODO: END: implement Stack<ObjectModel> beforeLoadInternal() stuff here...

	ObjectModel* omb = objectModel;
	this->loadInternal(omb);

	// TODO: BEGIN: implement Stack<ObjectModel> afterLoadInternal() stuff here...
	// TODO: END: implement Stack<ObjectModel> afterLoadInternal() stuff here...
}

void DataFormat::save(ObjectModel* objectModel)
{
	if (objectModel == nullptr)
	{
		printf("ue: DataFormat::save() - objectModel must not be null");
		return;
	}

	// TODO: BEGIN: implement Stack<ObjectModel> beforeLoadInternal() stuff here...
	// TODO: END: implement Stack<ObjectModel> beforeLoadInternal() stuff here...

	ObjectModel* omb = objectModel;
	this->saveInternal(omb);

	// TODO: BEGIN: implement Stack<ObjectModel> afterLoadInternal() stuff here...
	// TODO: END: implement Stack<ObjectModel> afterLoadInternal() stuff here...
}

Accessor* DataFormat::getAccessor()
{
	return this->_accessor;
}
void DataFormat::setAccessor(Accessor* value)
{
	this->_accessor = value;
}

} /* namespace UniversalEditor */
