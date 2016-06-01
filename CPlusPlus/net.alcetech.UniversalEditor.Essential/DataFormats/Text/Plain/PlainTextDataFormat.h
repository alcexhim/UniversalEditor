/*
 * PlainTextDataFormat.h
 *
 *  Created on: Apr 28, 2016
 *      Author: beckermj
 */

#ifndef DATAFORMATS_TEXT_PLAIN_PLAINTEXTDATAFORMAT_H_
#define DATAFORMATS_TEXT_PLAIN_PLAINTEXTDATAFORMAT_H_

#include <DataFormat.h>
#include <IO/Reader.h>

#include "../../../ObjectModels/Text/Plain/PlainTextObjectModel.h"

using UniversalEditor::DataFormat;
using UniversalEditor::IO::Reader;

using UniversalEditor::ObjectModels::Text::Plain::PlainTextObjectModel;

namespace UniversalEditor {
namespace DataFormats {
namespace Text {
namespace Plain {

class PlainTextDataFormat : public DataFormat {
protected:
	virtual void loadInternal(ObjectModel* objectModel);
	virtual void saveInternal(ObjectModel* objectModel);
public:
	PlainTextDataFormat();
	virtual ~PlainTextDataFormat();
};

} /* namespace Plain */
} /* namespace Text */
} /* namespace DataFormats */
} /* namespace UniversalEditor */

#endif /* DATAFORMATS_TEXT_PLAIN_PLAINTEXTDATAFORMAT_H_ */
