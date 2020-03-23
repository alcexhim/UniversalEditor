using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;

using UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.PrintTicket.PrintTicketItems;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS.PrintTicket
{
	public class PrintTicketXMLDataFormat : XMLDataFormat
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
			PrintTicketObjectModel pt = (objectModels.Pop() as PrintTicketObjectModel);

			MarkupTagElement tagPrintTicket = (mom.Elements["psf:PrintTicket"] as MarkupTagElement);
			if (tagPrintTicket == null) throw new InvalidDataFormatException("File does not contain a 'psf:PrintTicket' top-level element");

			foreach (MarkupElement el in tagPrintTicket.Elements)
			{
				MarkupTagElement tag = (el as MarkupTagElement);
				if (tag == null) continue;

				MarkupAttribute attName = tag.Attributes["name"];
				if (attName == null) continue;

				switch (tag.FullName)
				{
					case "psf:ParameterInit":
					{
						ParameterInit item = new ParameterInit();
						item.Name = attName.Value;

						MarkupTagElement tagValue = (tag.Elements["psf:Value"] as MarkupTagElement);
						if (tagValue != null)
						{
							item.Value = GetXMLValue(tagValue);
						}

						pt.Items.Add(item);
						break;
					}
					case "psf:Feature":
					{
						Feature item = new Feature();
						item.Name = attName.Value;

						MarkupTagElement tagOption = (tag.Elements["psf:Option"] as MarkupTagElement);
						if (tagOption != null)
						{
							MarkupAttribute attOptionName = tagOption.Attributes["name"];
							if (attOptionName == null) continue;

							FeatureOption option = new FeatureOption();
							option.Name = attOptionName.Value;

							foreach (MarkupElement elProp in tagOption.Elements)
							{
								MarkupTagElement tagProp = (elProp as MarkupTagElement);
								if (tagProp == null) continue;
								if (tagProp.FullName != "psf:ScoredProperty") continue;

								MarkupAttribute attPropertyName = tagProp.Attributes["name"];
								if (attPropertyName == null) continue;

								ScoredProperty prop = new ScoredProperty();
								prop.Name = attPropertyName.Value;
								prop.Value = GetXMLValue(tagProp.Elements["psf:Value"] as MarkupTagElement);
								option.ScoredProperties.Add(prop);
							}

							item.Option = option;
						}

						pt.Items.Add(item);
						break;
					}
				}
			}
		}
	}
}
