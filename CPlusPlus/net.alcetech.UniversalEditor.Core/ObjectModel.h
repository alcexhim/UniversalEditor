/*
 * ObjectModel.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODEL_H_
#define OBJECTMODEL_H_

#include "ObjectModelReference.h"

namespace UniversalEditor {

class ObjectModelReference;

class ObjectModel {
private:
	ObjectModelReference* _reference;
public:
	ObjectModel();
	virtual ~ObjectModel();

	ObjectModelReference* getReference();
};

} /* namespace UniversalEditor */

#endif /* OBJECTMODEL_H_ */
