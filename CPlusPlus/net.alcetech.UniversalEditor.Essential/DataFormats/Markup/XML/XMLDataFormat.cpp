/*
 * XMLDataFormat.cpp
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include "XMLDataFormat.h"

using UniversalEditor::IO::Reader;

using ApplicationFramework::Console;
using ApplicationFramework::Text::StringBuilder;

namespace UniversalEditor {
namespace DataFormats {
namespace Markup {
namespace XML {

XMLDataFormat::XMLDataFormat() {
	this->_settings = new XMLDataFormatSettings();

	// this->getSettings()->getAutoCloseTagNamesList()->add("hr");
	// this->getSettings()->getAutoCloseTagNamesList()->add("br");
	// this->getSettings()->getAutoCloseTagNamesList()->add("img");
	this->getSettings()->getEntitiesDictionary()->add(new String("amp"), new String("&"));
	this->getSettings()->getEntitiesDictionary()->add(new String("quot"), new String("\""));
	this->getSettings()->getEntitiesDictionary()->add(new String("copy"), new String("©"));
	this->getSettings()->getEntitiesDictionary()->add(new String("apos"), new String("'"));
}

XMLDataFormat::~XMLDataFormat() {
	// TODO Auto-generated destructor stub
}

XMLDataFormatSettings* XMLDataFormat::getSettings() {
	return this->_settings;
}

const char* replaceEntitiesInput(const char* value) {
	return value;
}

void XMLDataFormat::loadInternal(ObjectModel* objectModel)
{
	// TODO: figure out why comment <!-- --> doesn't work...

	MarkupObjectModel* mom = (MarkupObjectModel*)objectModel;
	if (mom == NULL)
	{
		// TODO: throw a new ObjectModelNotSupported exception here
		printf("ue: XMLDataFormat: loadInternal() - object model not supported\n");
		return;
	}

	Accessor* acc = this->getAccessor();
	Reader* reader = new Reader(acc);
	XMLDataFormatContext* ctx = new XMLDataFormatContext();

	ctx->setNextChar(reader->readChar());
	int times = 0, maxtimes = 10;
	while (ctx->getNextChar() != '<')
	{
		// clear out junk
		ctx->setNextChar(reader->readChar());
		times++;
		if (times == maxtimes) break;
	}
	acc->seek(-1, SeekOrigin::Current);

	while (!acc->endOfStream())
	{
		ctx->setNextChar(reader->readChar());

		if (ctx->isInsideString() || ctx->isInsidePreprocessor())
		{
			if (ctx->getNextChar() == '"' && ctx->isInsideString())
			{
				ctx->setInsideString(false);
				ctx->continueLoop();
				continue;
			}

			if (ctx->getNextChar() == this->getSettings()->getTagEndChar() && ctx->getPrevChar() == this->getSettings()->getPreprocessorChar() && ctx->isInsidePreprocessor())
			{
				ctx->setInsidePreprocessor(false);

				ctx->getNextString()->set(ctx->getNextString()->substring(0, ctx->getNextString()->getLength() - 1));

				MarkupPreprocessorElement* preproc = new MarkupPreprocessorElement();
				preproc->setName(new String("preproc"));
				preproc->setContent(ctx->getNextString()->toString());

				MarkupTagElement* tag = ctx->getCurrentTag();
				if (tag != NULL)
				{
					printf("adding child MarkupPreprocessorElement to tag\n");
					tag->addChildElement(preproc);
				}
				else
				{
					printf("adding child MarkupPreprocessorElement to mom\n");
					mom->addChildElement(preproc);
				}

				ctx->getNextString()->clear();
				ctx->continueLoop();
				continue;
			}

			ctx->getNextString()->append(ctx->getNextChar());
			ctx->continueLoop();
			continue;
		}
		else
		{
			if (ctx->isWhitespaceNotSpace(ctx->getNextChar())) continue;
		}

		if (ctx->getNextChar() == this->getSettings()->getTagBeginChar())
		{
			ctx->setInsideTag(true);
			ctx->setInsideTagValue(false);
			ctx->continueLoop();
			continue;
		}
		else if (ctx->getNextChar() == this->getSettings()->getTagCloseChar())
		{
			if (ctx->getPrevChar() == this->getSettings()->getTagBeginChar())
			{
				ctx->setInsideTagValue(false);

				StringBuilder* closingTagName = new StringBuilder();
				while (ctx->getNextChar() != this->getSettings()->getTagEndChar())
				{
					ctx->setNextChar(reader->readChar());
					if (ctx->getNextChar() != this->getSettings()->getTagEndChar())
					{
						closingTagName->append(ctx->getNextChar());
					}
				}

				if (!ctx->getNextString()->isEmpty())
				{
					if (ctx->getCurrentTag() != NULL)
					{
						printf("setting tag '%s' value '%s'\n", ctx->getCurrentTag()->getFullName()->toCharArray(), ctx->getNextString()->toString()->toCharArray());
						ctx->getCurrentTag()->setValue(ctx->getNextString()->toString());
						ctx->getNextString()->clear();
					}
				}

				if (ctx->getCurrentElement() != NULL)
				{
					if (closingTagName->equals(ctx->getCurrentElement()->getFullName()))
					{
						ctx->setCurrentElement(ctx->getCurrentElement()->getParentElement());
					}
					else
					{
						printf("cannot close block tag '%s' with '%s'\n", ctx->getCurrentElement()->getFullName()->toCharArray(), closingTagName->toString()->toCharArray());
					}
				}
				else
				{
					printf("cannot close NULL block tag with '%s'\n", closingTagName->toString()->toCharArray());
				}
			}
			else
			{
				if (ctx->getCurrentElement() != NULL)
				{
					ctx->setCurrentElement(ctx->getCurrentElement()->getParentElement());
				}
			}
			continue;
		}
		else if (ctx->getNextChar() == this->getSettings()->getTagSpecialDeclarationStartChar())
		{
			ctx->setInsideSpecialDeclaration(true);
			continue;
		}
		else if (ctx->getNextChar() == this->getSettings()->getPreprocessorChar())
		{
			ctx->setInsidePreprocessor(true);
		}
		else if (ctx->getNextChar() == this->getSettings()->getAttributeNameValueSeparatorChar())
		{
			if (ctx->isInsideAttributeArea())
			{
				ctx->setNextAttribute(new MarkupAttribute());

				// i feel like we set this multiple times... once here and once in comparing with getTagEndChar
				// why the hell...??? it doesn't work without this one here though
				ctx->getNextAttributeName()->set(ctx->getNextString()->toString());

				ctx->getNextAttribute()->setName(ctx->getNextString()->toString());

				MarkupTagElement* tag = ctx->getCurrentTag();
				if (tag != NULL)
				{
					tag->addAttribute(ctx->getNextAttribute());
				}
				else
				{
					printf("tag is NULL\n");
				}
				ctx->getNextString()->clear();
				continue;
			}
		}
		else if (ctx->getNextChar() == this->getSettings()->getTagEndChar())
		{
			if (ctx->getNextString()->endsWith(this->getSettings()->getTagSpecialDeclarationCommentStart()))
			{

				/*
				printf("next string '%s' is comment\n", ctx->getNextString()->toString()->toCharArray());
				ctx->getNextString()->set(ctx->getNextString()->substring(0, ctx->getNextString()->getLength() - strlen(this->getSettings()->getTagSpecialDeclarationCommentStart())));

				MarkupCommentElement* comment = dynamic_cast<MarkupCommentElement*>(ctx->getCurrentElement());
				if (comment != NULL)
				{
					comment->setContent(ctx->getNextString()->toString());
					printf("adding comment '%s'\n", comment->getContent()->toCharArray());
				}
				else {
					printf("not found comment for '%s'\n", ctx->getNextString()->toString()->toCharArray());
				}
				ctx->getNextString()->clear();

				ctx->setCurrentElement(ctx->getCurrentElement()->getParentElement());
				*/
			}
			else
			{
				if (ctx->isClosingCurrentElement())
				{
					if (ctx->getCurrentElement() != NULL)
					{
						if (ctx->getPrevChar() == this->getSettings()->getTagCloseChar())
						{
							ctx->setCurrentElement(ctx->getCurrentElement()->getParentElement());
						}
						else
						{
							if (this->getSettings()->getAutoCloseTagNamesList()->contains(ctx->getCurrentElement()->getName()))
							{
								ctx->setCurrentElement(ctx->getCurrentElement()->getParentElement());
								if (ctx->getCurrentElement() != NULL)
								{
									ctx->setCurrentElement(ctx->getCurrentElement()->getParentElement());
								}
							}
							else
							{
								Console::writeLine("uds: XMLDataFormat: attempted to close element '%s'; invalid XML?", ctx->getCurrentElement()->getName()->toCharArray());
							}
						}
					}
					else
					{
						Console::writeLine("FIXME: UniversalDataStorage/FileFormats/Markup/XMLFileFormat.cs:144     currentElement is null");
					}
					ctx->setClosingCurrentElement(false);
				}
				else
				{
					if (ctx->isInsideSpecialDeclaration())
					{
						ctx->setInsideSpecialDeclaration(false);
					}
					else
					{
						ctx->setInsideTagValue(true);
						if (ctx->isInsideAttributeArea())
						{
							// TODO: fix this, see ".externalToolBuilders/Copy all files to share.launch"
							MarkupTagElement* tag = ctx->getCurrentTag();
							if (tag != NULL)
							{
								printf("adding to tag '%s' attribute %s = '%s'\n", tag->getFullName()->toCharArray(), ctx->getNextAttributeName()->toString()->toCharArray(), ctx->getNextString()->toString()->toCharArray());
								tag->addAttribute(ctx->getNextAttributeName()->toString(), ctx->getNextString()->toString());
							}
							ctx->getNextAttributeName()->clear();

							// OOPS: I thought this would fix #2694 but it doesn't
							// ctx->getNextString()->clear();
							ctx->setInsideAttributeArea(false);
						}
						else
						{
							MarkupElement* prevElement = ctx->getCurrentElement();

							MarkupTagElement* tag = new MarkupTagElement();
							ctx->setCurrentElement(tag);
							ctx->getCurrentElement()->setFullName(ctx->getNextString()->toString());
							ctx->getNextString()->clear();

							MarkupTagElement* prevTag = dynamic_cast<MarkupTagElement*>(prevElement);
							if (prevTag != NULL)
							{
								prevTag->addChildElement(ctx->getCurrentElement());
							}
							else
							{
								mom->addChildElement(ctx->getCurrentElement());
							}
							if (this->getSettings()->getAutoCloseTagNamesList()->contains(ctx->getCurrentElement()->getName()))
							{
								ctx->setCurrentElement(ctx->getCurrentElement()->getParentElement());
							}
							continue;
						}
					}
				}
			}

			if (!ctx->getNextString()->isEmpty())
			{
				if (ctx->getNextAttribute() != NULL)
				{
					ctx->getNextAttribute()->setValue(ctx->getNextString()->toString());

					// hack: (2694) we really need to figure out why NextAttribute is getting its value set twice
					// but in order to prevent it we just set it to NULL after the value has been set
					ctx->setNextAttribute(NULL);
				}
			}

			ctx->getNextString()->clear();
			ctx->setInsideAttributeArea(false);
			continue;
		}

		switch (ctx->getNextChar())
		{
			case '"':
			{
				if (!ctx->isInsideString()) ctx->setInsideString(true);
				break;
			}
			case ' ':
			{
				if (ctx->isInsideAttributeArea())
				{
					if (ctx->getNextAttribute() != NULL)
					{
						ctx->getNextAttribute()->setValue(ctx->getNextString()->toString());

						ctx->getNextString()->clear();
						ctx->setNextAttribute(NULL);
					}
				}
				else if (ctx->isInsideTagValue())
				{
					ctx->getNextString()->append(' ');
					continue;
				}
				else
				{
					if (ctx->getNextString()->startsWith(this->getSettings()->getTagSpecialDeclarationCommentStart()))
					{
						StringBuilder* sbCommentEnd = new StringBuilder();
						sbCommentEnd->append(this->getSettings()->getTagSpecialDeclarationCommentStart());
						sbCommentEnd->append(this->getSettings()->getTagEndChar());
						String* commentEndStr = sbCommentEnd->toString();
						ctx->getNextString()->append(' ');
						while (!ctx->getNextString()->endsWith(commentEndStr))
						{
							ctx->setNextChar(reader->readChar());
							ctx->getNextString()->append(ctx->getNextChar());
						}

						printf("%s\n", ctx->getNextString()->toString()->toCharArray());

						ctx->getNextString()->trim();

						/*
						int start = strlen(this->getSettings()->getTagSpecialDeclarationCommentStart());
						int length = ctx->getNextString()->getLength() - start;
						ctx->getNextString()->set(ctx->getNextString()->substring(start, length));

						MarkupCommentElement* comment = new MarkupCommentElement();
						comment->setContent(ctx->getNextString()->toString());
						if (ctx->getCurrentElement() != NULL)
						{
							MarkupTagElement* tag = ctx->getCurrentTag();
							if (tag != NULL)
							{
								printf("added comment '%s' to element '%s'\n", comment->getContent()->toCharArray(), tag->getFullName()->toCharArray());
								tag->addChildElement(comment);
							}
							else
							{
								printf("ue: XMLDataFormat - attempted to add a MarkupCommentElement to a non-Tag element\n");
							}
						}
						else
						{
							printf("added comment '%s' to mom\n", comment->getContent()->toCharArray());
							mom->addChildElement(comment);
						}

						*/
						ctx->getNextString()->clear();
						continue;
					}

					MarkupTagElement* tag = new MarkupTagElement();
					tag->setFullName(ctx->getNextString()->toString());

					MarkupTagElement* tagCurrent = ctx->getCurrentTag();
					if (tagCurrent != NULL)
					{
						// printf("created new MarkupTagElement named '%s'; added as child of currentElement '%s'\n", nextString->toString(), currentElement->getFullName());
						tagCurrent->addChildElement(tag);
					}
					else
					{
						printf("created new MarkupTagElement named '%s'; added as child of MarkupObjectModel\n", ctx->getNextString()->toString()->toCharArray());
						mom->addChildElement(tag);
					}

					ctx->setCurrentElement(tag);
					ctx->setInsideAttributeArea(true);
				}
				ctx->getNextString()->clear();
				break;
			}
			default:
			{
				ctx->getNextString()->append(ctx->getNextChar());
				break;
			}
		}

		ctx->continueLoop();
	}
}
void XMLDataFormat::saveInternal(ObjectModel* objectModel)
{
	MarkupObjectModel* mom = (MarkupObjectModel*)objectModel;
	if (mom == NULL)
	{
		// TODO: throw a new ObjectModelNotSupported exception here
		printf("ue: XMLDataFormat: saveInternal() - object model not supported\n");
		return;
	}
}

} /* namespace XML */
} /* namespace Markup */
} /* namespace DataFormats */
} /* namespace UniversalEditor */
