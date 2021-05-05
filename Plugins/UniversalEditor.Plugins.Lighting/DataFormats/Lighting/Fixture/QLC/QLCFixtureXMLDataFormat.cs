using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Lighting.Fixture;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Lighting.Fixture.QLC
{
	public class QLCFixtureXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(this.GetType());
				_dfr.Capabilities.Add(typeof(FixtureObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(CreatorApplication), "_Software", "Q Light Controller Plus"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(CreatorVersion), "_Version", "4.1.3"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Author), "_Author"));
			}
			return _dfr;
		}

		public string CreatorApplication { get; set; } = "Q Light Controller Plus";
		public string CreatorVersion { get; set; } = "4.1.3";
		public string Author { get; set; } = String.Empty;

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
				if (tagName != null) CreatorApplication = tagName.Value;

				MarkupTagElement tagVersion = (tagCreator.Elements["Version"] as MarkupTagElement);
				if (tagVersion != null) CreatorVersion = tagVersion.Value;

				MarkupTagElement tagAuthor = (tagCreator.Elements["Author"] as MarkupTagElement);
				if (tagAuthor != null) Author = tagAuthor.Value;
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
						Channel c = new Channel();

						MarkupAttribute attName = tag.Attributes["Name"];
						c.Name = attName?.Value;

						foreach (MarkupElement el1 in tag.Elements)
						{
							MarkupTagElement tag1 = (el1 as MarkupTagElement);
							if (tag1 == null) continue;
							switch (tag1.FullName)
							{
								case "Capability":
								{
									MarkupAttribute attMin = tag1.Attributes["Min"];
									MarkupAttribute attMax = tag1.Attributes["Max"];
									MarkupAttribute attPreset = tag1.Attributes["Preset"];
									MarkupAttribute attRes1 = tag1.Attributes["Res1"];
									MarkupAttribute attRes2 = tag1.Attributes["Res2"];

									Capability cap = new Capability();
									cap.MinimumValue = Byte.Parse(attMin.Value);
									cap.MaximumValue = Byte.Parse(attMax.Value);
									cap.Title = tag1.Value;

									if (attPreset != null)
									{
										switch (attPreset.Value)
										{
											case "ColorMacro":
											{
												Color firstColor = Color.Parse(tag1.Attributes["Res1"].Value);
												break;
											}
											case "ColorDoubleMacro":
											{
												Color firstColor = Color.Parse(tag1.Attributes["Res1"].Value);
												Color secondColor = Color.Parse(tag1.Attributes["Res2"].Value);
												break;
											}
											case "GoboMacro":
											{
												string imageFileName = tag1.Attributes["Res1"].Value;
												break;
											}
										}
									}
									c.Capabilities.Add(cap);
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
