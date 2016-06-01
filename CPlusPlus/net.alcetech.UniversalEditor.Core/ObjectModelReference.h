/*
 * ObjectModelReference.h
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODELREFERENCE_H_
#define OBJECTMODELREFERENCE_H_

#include <Guid.h>
#include <Property.h>
#include <String.h>

#include "ObjectModel.h"

using ApplicationFramework::Guid;
using ApplicationFramework::String;

namespace UniversalEditor {

class ObjectModel;

class ObjectModelReference {

	AFX_PROPERTY(Guid*, ID);
	AFX_PROPERTY(String*, TypeName);

	AFX_PROPERTY_BOOL(Visible);

private:
	static List<ObjectModelReference*>* _objectModelReferencesList;
	static List<ObjectModelReference*>* _visibleObjectModelReferencesList;

public:
	ObjectModelReference();
	virtual ~ObjectModelReference();

	static List<ObjectModelReference*>* getObjectModelReferencesList();
	static List<ObjectModelReference*>* getVisibleObjectModelReferencesList();

	static ObjectModelReference* getByTypeName(String* typeName, bool includeHidden = false);
	static ObjectModelReference* getByTypeName(const char* typeName, bool includeHidden = false);

	ObjectModel* createInstance();
};

} /* namespace UniversalEditor */

#endif /* OBJECTMODELREFERENCE_H_ */
