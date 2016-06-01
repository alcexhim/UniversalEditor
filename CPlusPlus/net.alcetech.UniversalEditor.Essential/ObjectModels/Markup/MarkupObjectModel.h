/*
 * MarkupObjectModel.h
 *
 *  Created on: Apr 1, 2016
 *      Author: beckermj
 */

#include <ObjectModel.h>

#include <Collections/Generic/List.h>

#include "MarkupElement.h"
#include "MarkupTagElement.h"

using ApplicationFramework::Collections::Generic::List;

#ifndef OBJECTMODELS_MARKUP_MARKUPOBJECTMODEL_H_
#define OBJECTMODELS_MARKUP_MARKUPOBJECTMODEL_H_

namespace UniversalEditor {
namespace ObjectModels {
namespace Markup {

class MarkupObjectModel : public ObjectModel
{
private:
	List<MarkupElement*>* _elements;
public:
	MarkupObjectModel();
	virtual ~MarkupObjectModel();

	List<MarkupElement*>* getChildElementsList();

	MarkupElement* getChildElement(int index);
	MarkupElement* getChildElement(String* name);

	List<MarkupTagElement*>* getChildTagsList();

	MarkupTagElement* getChildTag(int index);
	MarkupTagElement* getChildTag(String* name);

	void addChildElement(MarkupElement* element);
	void removeChildElement(MarkupElement* element);

	void addChildTag(MarkupTagElement* element);
	void addChildTag(MarkupTagElement* element, bool autoMerge);
};

} /* namespace Markup */
} /* namespace ObjectModels */
} /* namespace UniversalEditor */

#endif /* OBJECTMODELS_MARKUP_MARKUPOBJECTMODEL_H_ */
