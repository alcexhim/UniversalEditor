using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration;

using UniversalEditor.ObjectModels.Web.StyleSheet;

namespace UniversalEditor.DataFormats.Web.StyleSheet
{
	public class CSSDataFormat : ExtensibleConfigurationDataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(StyleSheetObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.Bootstrap);
				_dfr.Filters.Add("Cascading Style Sheet", new string[] { "*.css" });
			}
			return _dfr;
		}

		public CSSDataFormat()
		{
			Settings.PropertyNamePrefix = "";
			Settings.PropertyNameSuffix = "";
			Settings.PropertyNameValueSeparator = ":";
			Settings.PropertyValuePrefix = "\"";
			Settings.PropertyValueSuffix = "\"";
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			StyleSheetObjectModel style = (objectModels.Pop() as StyleSheetObjectModel);

			foreach (Group group in plom.Groups)
			{
				// TODO: figure out how to parse preprocessor non-groups (@import...) and
				// groups (@media { ... }) at the same time, in base ExtensibleConfiguration

				// For now we'll just create a new StyleSheetRule
				string fullSelector = group.Name;

				StyleSheetRule rule = new StyleSheetRule();
				rule.ObjectName = fullSelector;

				bool insideAttribute = false;
				string next = String.Empty;
				string nextAttributeName = null;

				for (int i = 0; i < fullSelector.Length; i++)
				{
					if (fullSelector[i] == '[')
					{
						rule.ObjectName = fullSelector.Substring(0, i);
						insideAttribute = true;
					}
					else if (fullSelector[i] == ']')
					{
						insideAttribute = false;
						rule.Attributes.Add(nextAttributeName, next);
						nextAttributeName = null;
						next = String.Empty;
					}
					else if (fullSelector[i] == '=' && insideAttribute)
					{
						nextAttributeName = next;
						next = String.Empty;
					}
					else if (insideAttribute)
					{
						next += fullSelector[i];
					}
				}

				foreach (Property prop in group.Properties)
				{
					rule.Properties.Add(new StyleSheetProperty(prop.Name, prop.Value.ToString()));
				}
				style.Rules.Add(rule);
			}
		}
	}
}
