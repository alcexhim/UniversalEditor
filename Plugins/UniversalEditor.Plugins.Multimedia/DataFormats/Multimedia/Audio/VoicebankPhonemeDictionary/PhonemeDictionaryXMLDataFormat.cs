//
//  PhonemeDictionaryXMLDataFormat.cs - provides a DataFormat for manipulating synthesized audio voicebank phoneme dictionaries in Synthaloid XML format
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

using UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary;

namespace UniversalEditor.DataFormats.Multimedia.Audio.VoicebankPhonemeDictionary
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating synthesized audio voicebank phoneme dictionaries in Synthaloid XML format.
	/// </summary>
	public class PhonemeDictionaryXMLDataFormat : XMLDataFormat
	{
		private DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
				_dfr.Capabilities.Add(typeof(PhonemeDictionaryObjectModel), DataFormatCapabilities.All);
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
			PhonemeDictionaryObjectModel dict = (objectModels.Pop() as PhonemeDictionaryObjectModel);

			MarkupTagElement tagPhonemeLists = (mom.FindElement("Synthaloid", "PhonemeDictionary", "PhonemeLists") as MarkupTagElement);
			if (tagPhonemeLists == null) throw new InvalidDataFormatException();

			foreach (MarkupElement elPhonemeList in tagPhonemeLists.Elements)
			{
				MarkupTagElement tagPhonemeList = (elPhonemeList as MarkupTagElement);
				if (tagPhonemeList == null) continue;
				if (tagPhonemeList.FullName != "PhonemeList") continue;

				PhonemeList list = new PhonemeList();
				MarkupAttribute attLanguageID = tagPhonemeList.Attributes["LanguageID"];
				if (attLanguageID != null)
				{
					int langID = -1;
					if (Int32.TryParse(attLanguageID.Value, out langID))
					{
						list.LanguageID = langID;
					}
				}

				MarkupTagElement tagPhonemes = (tagPhonemeList.Elements["Phonemes"] as MarkupTagElement);
				foreach (MarkupElement elPhoneme in tagPhonemes.Elements)
				{
					MarkupTagElement tagPhoneme = (elPhoneme as MarkupTagElement);
					if (tagPhoneme == null) continue;
					if (tagPhoneme.FullName != "Phoneme") continue;

					MarkupAttribute attValue = tagPhoneme.Attributes["Value"];
					if (attValue == null) continue;

					Phoneme phoneme = new Phoneme();
					phoneme.Value = attValue.Value;

					MarkupTagElement tagMappings = (tagPhoneme.Elements["Mappings"] as MarkupTagElement);
					foreach (MarkupElement elMapping in tagMappings.Elements)
					{
						MarkupTagElement tagMapping = (elMapping as MarkupTagElement);
						if (tagMapping == null) continue;
						if (tagMapping.FullName != "Mapping") continue;

						MarkupAttribute attMappingValue = tagMapping.Attributes["Value"];
						if (attMappingValue != null) phoneme.Mappings.Add(attMappingValue.Value);
					}

					list.Phonemes.Add(phoneme);
				}

				dict.PhonemeLists.Add(list);
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PhonemeDictionaryObjectModel dict = (objectModels.Pop() as PhonemeDictionaryObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();


			objectModels.Push(mom);
		}
	}
}
