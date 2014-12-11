using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Multimedia.Audio.VoicebankPhonemeDictionary;

namespace UniversalEditor.DataFormats.Multimedia.Audio.VoicebankPhonemeDictionary
{
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
				_dfr.Filters.Add("Phoneme dictionary", new string[] { "*.xml" });
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
