//
//  XMLBinaryGrammarDataFormat.cs - provides a DataFormat for manipulating Synalysis binary grammar definition files in XML format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.BinaryGrammar;
using UniversalEditor.ObjectModels.BinaryGrammar.GrammarItems;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.Plugins.Synalysis.DataFormats.XMLBinaryGrammar
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Synalysis binary grammar definition files in XML format.
	/// </summary>
	public class XMLBinaryGrammarDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(BinaryGrammarObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

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

			MarkupTagElement tagUFWB = (mom.Elements["ufwb"] as MarkupTagElement);
			if (tagUFWB == null)
				throw new InvalidDataFormatException("xml file does not contain top-level 'ufwb' tag");

			MarkupTagElement tagGrammar = (tagUFWB.Elements["grammar"] as MarkupTagElement);
			if (tagGrammar == null)
				throw new InvalidDataFormatException("'ufwb' tag does not contain child-level 'grammar' tag");

			MarkupAttribute attStart = tagGrammar.Attributes["start"];
			if (attStart == null)
				throw new InvalidDataFormatException("'grammar' tag does not contain 'start' attribute (entry point)");

			grammar.Name = tagGrammar.Attributes["name"]?.Value;
			grammar.Author = tagGrammar.Attributes["author"]?.Value;
			grammar.FileExtension = tagGrammar.Attributes["fileextension"]?.Value;
			grammar.UniversalTypeIdentifier = tagGrammar.Attributes["uti"]?.Value;
			grammar.IsComplete = (tagGrammar.Attributes["complete"]?.Value.Equals("yes")).GetValueOrDefault();

			grammar.Description = (tagGrammar.Elements["description"] as MarkupTagElement)?.Value;

			for (int i = 0; i < tagGrammar.Elements.Count; i++)
			{
				MarkupTagElement tag = (tagGrammar.Elements[i] as MarkupTagElement);
				if (tag == null) continue;

				if (tag.FullName == "structure")
				{
					GrammarItemStructure s = LoadStructure(tag);
					if (attStart.Value.Equals(String.Format("id:{0}", s.ID)) || attStart.Value.Equals(s.Name))
					{
						grammar.InitialStructure = s;
					}
					grammar.Structures.Add(s);
				}
			}
		}

		private GrammarItemStructure LoadStructure(MarkupTagElement tag)
		{
			GrammarItemStructure s = new GrammarItemStructure();
			s.ID = tag.Attributes["id"]?.Value;
			s.Name = tag.Attributes["name"]?.Value;
			s.Length = tag.Attributes["length"]?.Value;
			s.Encoding = tag.Attributes["encoding"]?.Value;
			s.Endianness = "big".Equals(tag.Attributes["endian"]?.Value) ? Endianness.BigEndian : Endianness.LittleEndian;
			s.Signed = !"no".Equals(tag.Attributes["signed"]?.Value);
			s.Extends = tag.Attributes["extends"]?.Value;
			foreach (MarkupElement el in tag.Elements)
			{
				MarkupTagElement tag1 = (el as MarkupTagElement);
				if (tag1 == null) continue;

				GrammarItem item = null;

				string fieldName = tag1.Attributes["name"]?.Value;
				string fieldID = tag1.Attributes["id"]?.Value;
				string fieldLength = tag1.Attributes["length"]?.Value;
				string repeatMax = tag1.Attributes["repeatmax"]?.Value;

				switch (tag1.FullName.ToLower())
				{
					case "structref":
					{
						item = new GrammarItemStructureReference();
						(item as GrammarItemStructureReference).Structure = tag1.Attributes["structure"]?.Value;
						break;
					}
					case "number":
					{
						item = new GrammarItemNumber();
						break;
					}
					case "string":
					{
						item = new GrammarItemString();
						break;
					}
				}

				if (item != null)
				{
					item.Name = fieldName;
					item.ID = fieldID;
					item.Length = fieldLength;

					MarkupTagElement tagFixedValues = (tag1.Elements["fixedvalues"] as MarkupTagElement);
					if (tagFixedValues != null)
					{
						foreach (MarkupElement elFixedValue in tagFixedValues.Elements)
						{
							MarkupTagElement tagFixedValue = (elFixedValue as MarkupTagElement);
							if (tagFixedValue == null) continue;

							FixedValue val = new FixedValue();
							val.Name = tagFixedValue.Attributes["name"]?.Value;
							val.Value = tagFixedValue.Attributes["value"]?.Value;
							s.FixedValues.Add(val);
						}
					}

					s.Items.Add(item);
				}
			}
			return s;
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			BinaryGrammarObjectModel grammar = (objectModels.Pop() as BinaryGrammarObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();
		}
	}
}
