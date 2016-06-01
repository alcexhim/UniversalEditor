/*
 * DataFormatLayoutItem.h
 *
 *  Created on: Apr 18, 2016
 *      Author: beckermj
 */

#ifndef DATAFORMATLAYOUTITEM_H_
#define DATAFORMATLAYOUTITEM_H_

#include <Property.h>
#include <String.h>

using ApplicationFramework::String;

namespace UniversalEditor {

class DataFormatLayoutItem {

	AFX_PROPERTY(String*, ID);

public:
	DataFormatLayoutItem();
	virtual ~DataFormatLayoutItem();
};

} /* namespace UniversalEditor */

#endif /* DATAFORMATLAYOUTITEM_H_ */
