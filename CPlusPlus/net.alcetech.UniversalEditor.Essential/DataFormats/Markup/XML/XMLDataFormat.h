/*
 * XMLDataFormat.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include <DataFormat.h>

#include "../../../ObjectModels/Markup/MarkupObjectModel.h"
#include "../../../ObjectModels/Markup/MarkupAttribute.h"
#include "../../../ObjectModels/Markup/MarkupElement.h"
#include "../../../ObjectModels/Markup/MarkupTagElement.h"
#include "../../../ObjectModels/Markup/MarkupCommentElement.h"
#include "../../../ObjectModels/Markup/MarkupPreprocessorElement.h"

#include <Console.h>
#include <IO/Reader.h>
#include <Text/StringBuilder.h>

#include <stdio.h>

#include "XMLDataFormatContext.h"
#include "XMLDataFormatSettings.h"

using UniversalEditor::ObjectModels::Markup::MarkupObjectModel;
using UniversalEditor::ObjectModels::Markup::MarkupAttribute;
using UniversalEditor::ObjectModels::Markup::MarkupElement;
using UniversalEditor::ObjectModels::Markup::MarkupTagElement;
using UniversalEditor::ObjectModels::Markup::MarkupCommentElement;
using UniversalEditor::ObjectModels::Markup::MarkupPreprocessorElement;

#ifndef DATAFORMATS_MARKUP_XML_XMLDATAFORMAT_H_
#define DATAFORMATS_MARKUP_XML_XMLDATAFORMAT_H_

namespace UniversalEditor {
namespace DataFormats {
namespace Markup {
namespace XML {

class XMLDataFormat : public DataFormat {
private:
	XMLDataFormatSettings* _settings;
	const char* replaceEntitiesInput(const char* value);
protected:
	virtual void loadInternal(ObjectModel* objectModel);
	virtual void saveInternal(ObjectModel* objectModel);
public:
	XMLDataFormat();
	virtual ~XMLDataFormat();

	XMLDataFormatSettings* getSettings();
};

} /* namespace XML */
} /* namespace Markup */
} /* namespace DataFormats */
} /* namespace UniversalEditor */

#endif /* DATAFORMATS_MARKUP_XML_XMLDATAFORMAT_H_ */
