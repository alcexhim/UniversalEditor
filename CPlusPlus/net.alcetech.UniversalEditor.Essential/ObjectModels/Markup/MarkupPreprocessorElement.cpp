/*
 * MarkupPreprocessorElement.cpp
 *
 *  Created on: Apr 2, 2016
 *      Author: beckermj
 */

#include "MarkupPreprocessorElement.h"

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

MarkupPreprocessorElement::MarkupPreprocessorElement() {
	// TODO Auto-generated constructor stub

}

MarkupPreprocessorElement::~MarkupPreprocessorElement() {
	// TODO Auto-generated destructor stub
}

String* MarkupPreprocessorElement::getContent() {
	return this->_content;
}
void MarkupPreprocessorElement::setContent(String* value) {
	this->_content = value;
}

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */
