/*
 * DataFormatReference.h
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#ifndef DATAFORMATREFERENCE_H_
#define DATAFORMATREFERENCE_H_

#include <Guid.h>
#include <Property.h>
#include <String.h>
#include <Collections/Generic/List.h>

#include "DataFormatLayoutItem.h"

using ApplicationFramework::Guid;
using ApplicationFramework::String;
using ApplicationFramework::Collections::Generic::List;

namespace UniversalEditor {

class DataFormatReference {
private:
	static List<DataFormatReference*>* _dataFormatReferencesList;
public:
	DataFormatReference();
	virtual ~DataFormatReference();

	AFX_PROPERTY(Guid*, ID);
	AFX_PROPERTY(String*, TypeName);
	AFX_PROPERTY(List<DataFormatLayoutItem*>*, LayoutItemsList);

	static List<DataFormatReference*>* getDataFormatReferencesList();
};

} /* namespace UniversalEditor */

#endif /* DATAFORMATREFERENCE_H_ */
