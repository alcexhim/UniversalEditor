/*
 * MarkupPreprocessorElement.h
 *
 *  Created on: Apr 2, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODELS_MARKUP_MARKUPPREPROCESSORELEMENT_H_
#define OBJECTMODELS_MARKUP_MARKUPPREPROCESSORELEMENT_H_

#include "MarkupElement.h"

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

class MarkupPreprocessorElement : public MarkupElement {
private:
	String* _content;
public:
	MarkupPreprocessorElement();
	virtual ~MarkupPreprocessorElement();

	String* getContent();
	void setContent(String* value);
};

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */

#endif /* OBJECTMODELS_MARKUP_MARKUPPREPROCESSORELEMENT_H_ */
