/*
 * MarkupCommentElement.h
 *
 *  Created on: Apr 3, 2016
 *      Author: beckermj
 */

#ifndef OBJECTMODELS_MARKUP_MARKUPCOMMENTELEMENT_H_
#define OBJECTMODELS_MARKUP_MARKUPCOMMENTELEMENT_H_

#include "MarkupElement.h"
#include <String.h>

using ApplicationFramework::String;

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

class MarkupCommentElement : public MarkupElement {
private:
	String* _content;
public:
	MarkupCommentElement();
	virtual ~MarkupCommentElement();

	String* getContent();
	void setContent(String* value);
};

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */

#endif /* OBJECTMODELS_MARKUP_MARKUPCOMMENTELEMENT_H_ */
