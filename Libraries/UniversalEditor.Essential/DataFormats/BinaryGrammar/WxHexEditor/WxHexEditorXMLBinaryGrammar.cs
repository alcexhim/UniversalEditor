//
//  WxHexEditorXMLBinaryGrammar.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.BinaryGrammar;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.BinaryGrammar.WxHexEditor
{
	public class WxHexEditorXMLBinaryGrammar : XMLDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			BinaryGrammarObjectModel grammar = (objectModels.Pop() as BinaryGrammarObjectModel);


		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			BinaryGrammarObjectModel grammar = (objectModels.Pop() as BinaryGrammarObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagwxHexEditor_XML_TAG = new MarkupTagElement();
			tagwxHexEditor_XML_TAG.FullName = "wxHexEditor_XML_TAG";

			MarkupTagElement tagFileName = new MarkupTagElement();
			tagFileName.FullName = "filename";
			tagFileName.Attributes.Add("path", Accessor.GetFileName());

			if (grammar.InitialStructure == null) throw new ObjectModelNotSupportedException();

			int tagID = 0;
			foreach (GrammarItem item in grammar.InitialStructure.Items)
			{
				MarkupTagElement tag = new MarkupTagElement();
				tag.FullName = "TAG";
				tag.Attributes.Add("id", tagID.ToString());

				MarkupTagElement tagStartOffset = new MarkupTagElement();
				tagStartOffset.FullName = "start_offset";


				tagFileName.Elements.Add(tag);
				tagID++;
			}

			tagwxHexEditor_XML_TAG.Elements.Add(tagFileName);
			mom.Elements.Add(tagwxHexEditor_XML_TAG);

			objectModels.Push(mom);
		}
	}
}
