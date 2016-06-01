/*
 * XMLDataFormatContext.cpp
 *
 *  Created on: Apr 12, 2016
 *      Author: beckermj
 */

#include "XMLDataFormatContext.h"

namespace UniversalEditor {
namespace DataFormats {
namespace Markup {
namespace XML {

XMLDataFormatContext::XMLDataFormatContext() {
	this->_ClosingCurrentElement = false;
	this->_CurrentElement = NULL;
	this->_InsideAttributeArea = false;
	this->_InsidePreprocessor = false;
	this->_InsideSpecialDeclaration = false;
	this->_InsideString = false;
	this->_InsideTag = false;
	this->_InsideTagName = false;
	this->_InsideTagValue = false;
	this->_NextAttribute = NULL;
	this->_NextChar = '\0';
	this->_NextAttributeName = new StringBuilder();
	this->_NextString = new StringBuilder();
	this->_PrevChar = '\0';
}

XMLDataFormatContext::~XMLDataFormatContext() {
	// TODO Auto-generated destructor stub
}

void XMLDataFormatContext::continueLoop() {
	this->setPrevChar(this->getNextChar());
}

MarkupTagElement* XMLDataFormatContext::getCurrentTag() {
	MarkupTagElement* tag = dynamic_cast<MarkupTagElement*>(this->getCurrentElement());
	return tag;
}

bool XMLDataFormatContext::isWhitespace(char value) {
	return (value == '\t' || value == '\r' || value == '\n' || value == ' ');
}
bool XMLDataFormatContext::isWhitespaceNotSpace(char value) {
	return (value == '\t' || value == '\r' || value == '\n');
}

} /* namespace XML */
} /* namespace Markup */
} /* namespace DataFormats */
} /* namespace UniversalEditor */
