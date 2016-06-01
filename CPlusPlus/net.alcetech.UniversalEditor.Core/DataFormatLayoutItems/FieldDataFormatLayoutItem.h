/*
 * FieldDataFormatLayoutItem.h
 *
 *  Created on: Apr 18, 2016
 *      Author: beckermj
 */

#ifndef DATAFORMATLAYOUTITEMS_FIELDDATAFORMATLAYOUTITEM_H_
#define DATAFORMATLAYOUTITEMS_FIELDDATAFORMATLAYOUTITEM_H_

#include "../DataFormatLayoutItem.h"
#include <Property.h>
#include <String.h>

using UniversalEditor::DataFormatLayoutItem;
using ApplicationFramework::String;

namespace UniversalEditor {
namespace DataFormatLayoutItems {

class FieldDataFormatLayoutItem : public DataFormatLayoutItem {

	AFX_PROPERTY(String*, DataType);

public:
	FieldDataFormatLayoutItem();
	virtual ~FieldDataFormatLayoutItem();
};

} /* namespace DataFormatLayoutItems */
} /* namespace UniversalEditor */

#endif /* DATAFORMATLAYOUTITEMS_FIELDDATAFORMATLAYOUTITEM_H_ */
