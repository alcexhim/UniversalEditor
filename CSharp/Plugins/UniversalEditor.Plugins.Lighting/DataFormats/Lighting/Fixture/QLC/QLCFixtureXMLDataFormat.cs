using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Lighting.Fixture;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Lighting.Fixture.QLC
{
	public class QLCFixtureXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(this.GetType());
				_dfr.Capabilities.Add(typeof(FixtureObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Q Light Controller fixture", new string[] { "*.xml" });
				_dfr.ExportOptions.Add(new CustomOptionText("CreatorApplication", "&Software:", "Q Light Controller Plus"));
				_dfr.ExportOptions.Add(new CustomOptionText("Author", "&Author:"));
			}
			return _dfr;
		}

		private string mvarCreatorApplication = "Q Light Controller Plus";
		public string CreatorApplication { get { return mvarCreatorApplication; } set { mvarCreatorApplication = value; } }

		private Version mvarCreatorVersion = new Version(4, 1, 3);
		public Version CreatorVersion { get { return mvarCreatorVersion; } set { mvarCreatorVersion = value; } }

		private string mvarAuthor = String.Empty;
		public string Author { get { return mvarAuthor; } set { mvarAuthor = value; } }

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			FixtureObjectModel fixture = (objectModels.Pop() as FixtureObjectModel);

			MarkupTagElement tagFixtureDefinition = (mom.Elements["FixtureDefinition"] as MarkupTagElement);
			if (tagFixtureDefinition == null) throw new InvalidDataFormatException("XML file does not contain starting tag \"FixtureDefinition\"");

			MarkupTagElement tagCreator = (tagFixtureDefinition.Elements["Creator"] as MarkupTagElement);
			if (tagCreator != null)
			{
				MarkupTagElement tagName = (tagCreator.Elements["Name"] as MarkupTagElement);
				if (tagName != null) mvarCreatorApplication = tagName.Value;

				MarkupTagElement tagVersion = (tagCreator.Elements["Version"] as MarkupTagElement);
				if (tagVersion != null) mvarCreatorVersion = new Version(tagVersion.Value);

				MarkupTagElement tagAuthor = (tagCreator.Elements["Author"] as MarkupTagElement);
				if (tagAuthor != null) mvarAuthor = tagAuthor.Value;
			}

			MarkupTagElement tagManufacturer = (tagFixtureDefinition.Elements["Manufacturer"] as MarkupTagElement);
			if (tagManufacturer != null)
			{
				fixture.Manufacturer = tagManufacturer.Value;
			}

			MarkupTagElement tagModel = (tagFixtureDefinition.Elements["Model"] as MarkupTagElement);
			if (tagModel != null)
			{
				fixture.Model = tagModel.Value;
			}

			MarkupTagElement tagType = (tagFixtureDefinition.Elements["Type"] as MarkupTagElement);
			if (tagType != null)
			{
				fixture.Type = tagType.Value;
			}

			foreach (MarkupElement el in tagFixtureDefinition.Elements)
			{
				MarkupTagElement tag = (el as MarkupTagElement);
				if (tag == null) continue;
				switch (tag.FullName)
				{
					case "Channel":
					{
						foreach (MarkupElement el1 in tag.Elements)
						{
							MarkupTagElement tag1 = (el1 as MarkupTagElement);
							if (tag1 == null) continue;
							switch (tag1.FullName)
							{
								case "Capability":
								{
									break;
								}
							}
						}
						break;
					}
				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
		}
	}
}
