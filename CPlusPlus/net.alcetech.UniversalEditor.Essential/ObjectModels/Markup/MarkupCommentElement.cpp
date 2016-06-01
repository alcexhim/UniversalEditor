/*
 * MarkupCommentElement.cpp
 *
 *  Created on: Apr 3, 2016
 *      Author: beckermj
 */

#include "MarkupCommentElement.h"

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

MarkupCommentElement::MarkupCommentElement() {
	this->_content = String::EMPTY;
}

MarkupCommentElement::~MarkupCommentElement() {
	// TODO Auto-generated destructor stub
}

String* MarkupCommentElement::getContent() {
	return this->_content;
}
void MarkupCommentElement::setContent(String* value) {
	this->_content = value;
}

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */
