/*
 * XMLDataFormatSettings.h
 *
 *  Created on: Apr 2, 2016
 *      Author: beckermj
 */

#ifndef DATAFORMATS_MARKUP_XML_XMLDATAFORMATSETTINGS_H_
#define DATAFORMATS_MARKUP_XML_XMLDATAFORMATSETTINGS_H_

#include <String.h>
#include <Collections/Generic/List.h>
#include <Collections/Generic/BidirectionalDictionary.h>

using ApplicationFramework::String;
using ApplicationFramework::Collections::Generic::BidirectionalDictionary;
using ApplicationFramework::Collections::Generic::List;

namespace UniversalEditor {
namespace DataFormats {
namespace Markup {
namespace XML {

class XMLDataFormatSettings {
private:
	BidirectionalDictionary<String*, String*>* _entitiesDictionary;
	List<String*>* _autoCloseTagNamesList;

	char _tagBeginChar;
	char _tagEndChar;
	char _tagCloseChar;
	char _tagSpecialDeclarationStartChar;
	const char* _tagSpecialDeclarationCommentStart;
	char _preprocessorChar;
	char _attributeNameValueSeparatorChar;
public:
	XMLDataFormatSettings();
	virtual ~XMLDataFormatSettings();

	BidirectionalDictionary<String*, String*>* getEntitiesDictionary();
	List<String*>* getAutoCloseTagNamesList();

	char getTagBeginChar();
	void setTagBeginChar(char value);

	char getTagEndChar();
	void setTagEndChar(char value);

	char getTagCloseChar();
	void setTagCloseChar(char value);

	char getTagSpecialDeclarationStartChar();
	void setTagSpecialDeclarationStartChar(char value);

	const char* getTagSpecialDeclarationCommentStart();
	void setTagSpecialDeclarationCommentStart(const char* value);

	char getPreprocessorChar();
	void setPreprocessorChar(char value);

	char getAttributeNameValueSeparatorChar();
	void setAttributeNameValueSeparatorChar(char value);
};

} /* namespace XML */
} /* namespace Markup */
} /* namespace DataFormats */
} /* namespace UniversalEditor */

#endif /* DATAFORMATS_MARKUP_XML_XMLDATAFORMATSETTINGS_H_ */
