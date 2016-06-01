/*
 * DataFormat.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#ifndef DATAFORMAT_H_
#define DATAFORMAT_H_

#include "Accessor.h"
#include "ObjectModel.h"

namespace UniversalEditor {

class DataFormat
{
private:
	Accessor* _accessor;
protected:
	virtual void loadInternal(ObjectModel* objectModel);
	virtual void saveInternal(ObjectModel* objectModel);
public:
	DataFormat();
	virtual ~DataFormat();

	void load(ObjectModel* objectModel);
	void save(ObjectModel* objectModel);

	Accessor* getAccessor();
	void setAccessor(Accessor* value);
};

} /* namespace UniversalEditor */

#endif /* DATAFORMAT_H_ */
