/*
 * PlainTextDataFormat.cpp
 *
 *  Created on: Apr 28, 2016
 *      Author: beckermj
 */

#include "PlainTextDataFormat.h"

namespace UniversalEditor {
namespace DataFormats {
namespace Text {
namespace Plain {

PlainTextDataFormat::PlainTextDataFormat() {
	// TODO Auto-generated constructor stub

}

PlainTextDataFormat::~PlainTextDataFormat() {
	// TODO Auto-generated destructor stub
}


void PlainTextDataFormat::loadInternal(ObjectModel* objectModel) {
	Reader* reader = this->getAccessor()->getReader();
	PlainTextObjectModel* ptom = dynamic_cast<PlainTextObjectModel*>(objectModel);
	// if (ptom == NULL) throw new ObjectModelNotSupportedException();

	String* data = reader->readStringToEnd();
	ptom->setText(data);
}
void PlainTextDataFormat::saveInternal(ObjectModel* objectModel) {

}

} /* namespace Plain */
} /* namespace Text */
} /* namespace DataFormats */
} /* namespace UniversalEditor */
