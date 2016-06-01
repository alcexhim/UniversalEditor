/*
 * PlainTextObjectModel.h
 *
 *  Created on: Apr 28, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODELS_TEXT_PLAIN_PLAINTEXTOBJECTMODEL_H_
#define OBJECTMODELS_TEXT_PLAIN_PLAINTEXTOBJECTMODEL_H_

#include <ObjectModel.h>

#include <Property.h>
#include <String.h>

using ApplicationFramework::String;

using UniversalEditor::ObjectModel;

namespace UniversalEditor {
namespace ObjectModels {
namespace Text {
namespace Plain {

class PlainTextObjectModel: public ObjectModel {

AFX_PROPERTY(String*, Text)

public:
	PlainTextObjectModel();
	virtual ~PlainTextObjectModel();
};

} /* namespace Plain */
} /* namespace Text */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */

#endif /* OBJECTMODELS_TEXT_PLAIN_PLAINTEXTOBJECTMODEL_H_ */

