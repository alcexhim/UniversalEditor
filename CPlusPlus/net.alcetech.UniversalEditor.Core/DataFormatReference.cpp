/*
 * DataFormatReference.cpp
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#include "DataFormatReference.h"

namespace UniversalEditor {

List<DataFormatReference*>* DataFormatReference::_dataFormatReferencesList = new List<DataFormatReference*>();

DataFormatReference::DataFormatReference() {
	this->_ID = Guid::EMPTY;
	this->_TypeName = String::EMPTY;
	this->_LayoutItemsList = new List<DataFormatLayoutItem*>();
}

DataFormatReference::~DataFormatReference() {
	// TODO Auto-generated destructor stub
}

List<DataFormatReference*>* DataFormatReference::getDataFormatReferencesList() {
	return DataFormatReference::_dataFormatReferencesList;
}

} /* namespace UniversalEditor */
