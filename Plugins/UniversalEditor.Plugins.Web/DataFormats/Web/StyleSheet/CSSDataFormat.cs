//
//  CSSDataFormat.cs - provides a DataFormat for reading and writing Cascading Style Sheets (CSS) files
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

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration;

using UniversalEditor.ObjectModels.Web.StyleSheet;
using System.Linq;

namespace UniversalEditor.DataFormats.Web.StyleSheet
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for reading and writing Cascading Style Sheets (CSS) files.
	/// </summary>
	public class CSSDataFormat : ExtensibleConfigurationDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(StyleSheetObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.Bootstrap);
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

			foreach (PropertyListItem item in plom.Items)
			{
				Group group = (item as Group);
				if (group == null) continue;

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

				IEnumerable<Property> properties = group.Items.OfType<Property>();
				foreach (Property prop in properties)
				{
					rule.Properties.Add(new StyleSheetProperty(prop.Name, prop.Value.ToString()));
				}
				style.Rules.Add(rule);
			}
		}
	}
}
