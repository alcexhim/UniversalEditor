//
//  StoryXMLDataFormat.cs - provides a DataFormat for manipulating stories in StoryWriter XML format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.StoryWriter.Story;

namespace UniversalEditor.DataFormats.StoryWriter.Story.StoryXML
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating stories in StoryWriter XML format.
	/// </summary>
	public class StoryXMLDataFormat : XMLDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}

		private PersonalName LoadPersonalNameTag(MarkupTagElement tag)
		{
			PersonalName item = new PersonalName();

			if (tag == null)
				return null;
			if (!(tag.XMLSchema == XMLSchemas.StoryWriter && tag.Name == "name"))
				return null;

			MarkupTagElement tagGivenName = (tag.FindElementUsingSchema(XMLSchemas.StoryWriter, "givenName") as MarkupTagElement);
			if (tagGivenName != null)
				item.GivenName = tagGivenName.Value;

			MarkupTagElement tagFamilyName = (tag.FindElementUsingSchema(XMLSchemas.StoryWriter, "familyName") as MarkupTagElement);
			if (tagFamilyName != null)
				item.FamilyName = tagFamilyName.Value;

			MarkupTagElement tagNickname = (tag.FindElementUsingSchema(XMLSchemas.StoryWriter, "nickName") as MarkupTagElement);
			if (tagNickname != null)
				item.Nickname = tagNickname.Value;

			return item;
		}

		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel markup = (objectModels.Pop() as MarkupObjectModel);
			StoryObjectModel story = (objectModels.Pop() as StoryObjectModel);

			MarkupTagElement tagStory = (markup.FindElementUsingSchema(XMLSchemas.StoryWriter, "story") as MarkupTagElement);
			if (tagStory == null)
				throw new InvalidDataFormatException();

			MarkupTagElement tagCharacters = (markup.FindElementUsingSchema(XMLSchemas.StoryWriter, "characters") as MarkupTagElement);
			if (tagCharacters != null)
			{
				foreach (MarkupElement elCharacter in tagCharacters.Elements)
				{
					MarkupTagElement tagCharacter = (elCharacter as MarkupTagElement);
					if (tagCharacter == null)
						continue;
					if (!(tagCharacter.XMLSchema == XMLSchemas.StoryWriter && tagCharacter.Name == "character"))
						continue;

					MarkupAttribute attCharacterID = tagCharacter.Attributes["id"];
					if (attCharacterID == null)
						continue;

					Character chara = new Character();
					chara.ID = new Guid(attCharacterID.Value);

					MarkupTagElement tagInformation = (tagCharacter.FindElementUsingSchema(XMLSchemas.StoryWriter, "information") as MarkupTagElement);
					if (tagInformation != null)
					{

						MarkupTagElement tagName = (tagInformation.FindElementUsingSchema(XMLSchemas.StoryWriter, "name") as MarkupTagElement);
						if (tagName != null)
						{
							chara.Name = LoadPersonalNameTag(tagName);
						}

						MarkupTagElement tagGender = (tagInformation.FindElementUsingSchema(XMLSchemas.StoryWriter, "gender") as MarkupTagElement);
						if (tagGender != null)
						{

						}

					}
				}
			}

		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			StoryObjectModel story = (objectModels.Pop() as StoryObjectModel);

			MarkupObjectModel markup = new MarkupObjectModel();
			objectModels.Push(markup);
		}
	}
}
