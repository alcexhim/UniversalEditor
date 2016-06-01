/*
 * Association.cpp
 *
 *  Created on: Apr 17, 2016
 *      Author: beckermj
 */

#include "Association.h"

namespace UniversalEditor {

List<Association*>* Association::_associationsList = new List<Association*>();

Association::Association() {
	this->_DataFormatsList = new List<DataFormatReference*>();
	this->_ObjectModelsList = new List<ObjectModelReference*>();
}

Association::~Association() {
	// TODO Auto-generated destructor stub
}

List<Association*>* Association::getAssociationsList() {
	return Association::_associationsList;
}

} /* namespace UniversalEditor */
