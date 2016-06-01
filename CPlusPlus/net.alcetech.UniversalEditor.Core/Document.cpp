/*
 * Document.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "Document.h"

namespace UniversalEditor {

Document::Document() {
	// TODO Auto-generated constructor stub

}

Document::~Document() {
	// TODO Auto-generated destructor stub
}

Document* Document::load(ObjectModel* om, DataFormat* df, Accessor* ac)
{
	Document* document = new Document();
	document->setObjectModel(om);
	document->setDataFormat(df);
	document->setAccessor(ac);

	document->open();
	document->load();
	return document;
}
Document* Document::save(ObjectModel* om, DataFormat* df, Accessor* ac)
{
	Document* document = new Document();
	document->setObjectModel(om);
	document->setDataFormat(df);
	document->setAccessor(ac);

	document->open();
	document->save();
	return document;
}

ObjectModel* Document::getObjectModel()
{
	return this->_objectModel;
}
void Document::setObjectModel(ObjectModel* value)
{
	this->_objectModel = value;
}

DataFormat* Document::getDataFormat()
{
	return this->_dataFormat;
}
void Document::setDataFormat(DataFormat* value)
{
	this->_dataFormat = value;
}

Accessor* Document::getAccessor()
{
	return this->_accessor;
}
void Document::setAccessor(Accessor* value)
{
	this->_accessor = value;
}

void Document::open()
{
	this->getAccessor()->open();
}

void Document::load()
{
	this->getDataFormat()->setAccessor(this->getAccessor());
	this->getDataFormat()->load(this->getObjectModel());
}
void Document::save()
{
	this->getDataFormat()->setAccessor(this->getAccessor());
	this->getDataFormat()->save(this->getObjectModel());
}

void Document::close()
{
	this->getAccessor()->close();
}

} /* namespace UniversalEditor */
