/*
 * Association.h
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#ifndef ASSOCIATION_H_
#define ASSOCIATION_H_

#include <Collections/Generic/List.h>

using ApplicationFramework::Collections::Generic::List;

#include "ObjectModelReference.h"
#include "DataFormatReference.h"
#include <Property.h>

namespace UniversalEditor {

class Association {

	AFX_PROPERTY_READONLY(List<ObjectModelReference*>*, ObjectModelsList);
	AFX_PROPERTY_READONLY(List<DataFormatReference*>*, DataFormatsList);

private:
	static List<Association*>* _associationsList;

public:
	Association();
	virtual ~Association();

	static List<Association*>* getAssociationsList();
};

} /* namespace UniversalEditor */

#endif /* ASSOCIATION_H_ */
