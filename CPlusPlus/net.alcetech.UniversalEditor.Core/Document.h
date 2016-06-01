/*
 * Document.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef DOCUMENT_H_
#define DOCUMENT_H_

#include "Accessor.h"
#include "DataFormat.h"
#include "ObjectModel.h"

namespace UniversalEditor
{

class Document
{
private:
	ObjectModel* _objectModel;
	DataFormat* _dataFormat;
	Accessor* _accessor;
public:
	Document();
	virtual ~Document();

	static Document* load(ObjectModel* om, DataFormat* df, Accessor* ac);
	static Document* save(ObjectModel* om, DataFormat* df, Accessor* ac);

	ObjectModel* getObjectModel();
	void setObjectModel(ObjectModel* objectModel);

	DataFormat* getDataFormat();
	void setDataFormat(DataFormat* dataFormat);

	Accessor* getAccessor();
	void setAccessor(Accessor* accessor);

	void open();

	void load();
	void save();

	void close();
};

} /* namespace UniversalEditor */

#endif /* DOCUMENT_H_ */
