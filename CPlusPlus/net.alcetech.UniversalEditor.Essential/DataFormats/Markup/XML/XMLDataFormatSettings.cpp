/*
 * XMLDataFormatSettings.cpp
 *
 *  Created on: Apr 2, 2016
 *      Author: beckermj
 */

#include "XMLDataFormatSettings.h"

namespace UniversalEditor {
namespace DataFormats {
namespace Markup {
namespace XML {

XMLDataFormatSettings::XMLDataFormatSettings() {
	this->_entitiesDictionary = new BidirectionalDictionary<String*, String*>();
	this->_autoCloseTagNamesList = new List<String*>();

	this->_tagBeginChar = '<';
	this->_tagEndChar = '>';
	this->_tagCloseChar = '/';
	this->_tagSpecialDeclarationStartChar = '!';
	this->_tagSpecialDeclarationCommentStart = "--";
	this->_preprocessorChar = '?';

	this->_attributeNameValueSeparatorChar = '=';
}

XMLDataFormatSettings::~XMLDataFormatSettings() {
	// TODO Auto-generated destructor stub
}

BidirectionalDictionary<String*, String*>* XMLDataFormatSettings::getEntitiesDictionary() {
	return this->_entitiesDictionary;
}
List<String*>* XMLDataFormatSettings::getAutoCloseTagNamesList() {
	return this->_autoCloseTagNamesList;
}

/**
 * Gets the tag begin char (default '<' )
 */
char XMLDataFormatSettings::getTagBeginChar() {
	return this->_tagBeginChar;
}
/**
 * Sets the tag begin char (default '<' )
 */
void XMLDataFormatSettings::setTagBeginChar(char value) {
	this->_tagBeginChar = value;
}

/**
 * Gets the tag end char (default '>' )
 */
char XMLDataFormatSettings::getTagEndChar() {
	return this->_tagEndChar;
}
/**
 * Sets the tag end char (default '>' )
 */
void XMLDataFormatSettings::setTagEndChar(char value) {
	this->_tagEndChar = value;
}

/**
 * Gets the tag close char (default '/' )
 */
char XMLDataFormatSettings::getTagCloseChar() {
	return this->_tagCloseChar;
}
/**
 * Sets the tag end char (default '/' )
 */
void XMLDataFormatSettings::setTagCloseChar(char value) {
	this->_tagCloseChar = value;
}

/**
 * Gets the tag special declaration start char (default '!' )
 */
char XMLDataFormatSettings::getTagSpecialDeclarationStartChar() {
	return this->_tagSpecialDeclarationStartChar;
}
/**
 * Sets the tag special declaration start char (default '!' )
 */
void XMLDataFormatSettings::setTagSpecialDeclarationStartChar(char value) {
	this->_tagSpecialDeclarationStartChar = value;
}

const char* XMLDataFormatSettings::getTagSpecialDeclarationCommentStart() {
	return this->_tagSpecialDeclarationCommentStart;
}
void XMLDataFormatSettings::setTagSpecialDeclarationCommentStart(const char* value) {
	this->_tagSpecialDeclarationCommentStart = value;
}

/**
 * Gets the preprocessor char (default '?' )
 */
char XMLDataFormatSettings::getPreprocessorChar() {
	return this->_preprocessorChar;
}
/**
 * Sets the preprocessor char (default '?' )
 */
void XMLDataFormatSettings::setPreprocessorChar(char value) {
	this->_preprocessorChar = value;
}

/**
 * Gets the attribute name value separator char (default '=' )
 */
char XMLDataFormatSettings::getAttributeNameValueSeparatorChar() {
	return this->_attributeNameValueSeparatorChar;
}
/**
 * Sets the attribute name value separator char (default '=' )
 */
void XMLDataFormatSettings::setAttributeNameValueSeparatorChar(char value) {
	this->_attributeNameValueSeparatorChar = value;
}

} /* namespace XML */
} /* namespace Markup */
} /* namespace DataFormats */
} /* namespace UniversalEditor */
