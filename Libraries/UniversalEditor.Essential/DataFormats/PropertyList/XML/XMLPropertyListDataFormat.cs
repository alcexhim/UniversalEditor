//
//  XMLPropertyListDataFormat.cs - provides a DataFormat for manipulating property lists in Universal Editor's XML Configuration format
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

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.XML
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating property lists in Universal Editor's XML Configuration format.
	/// </summary>
	public class XMLPropertyListDataFormat : XMLDataFormat
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
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);

			MarkupTagElement tagConfiguration = (mom.Elements["Configuration"] as MarkupTagElement);
			if (tagConfiguration == null) throw new InvalidDataFormatException();

			LoadMarkup(tagConfiguration, ref plom);
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagConfiguration = new MarkupTagElement();
			tagConfiguration.FullName = "Configuration";

			SaveMarkup(ref tagConfiguration, plom);

			mom.Elements.Add(new MarkupPreprocessorElement("xml", "version=\"1.0\" encoding=\"UTF-8\""));
			mom.Elements.Add(tagConfiguration);
			objectModels.Push(mom);
		}

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Gets the format version of this <see cref="XMLPropertyListDataFormat" />.
		/// </summary>
		/// <value>The format version of this <see cref="XMLPropertyListDataFormat" />.</value>
		public static Version FormatVersion { get; } = new Version(2, 0);

		public static void LoadMarkup(MarkupTagElement tagConfiguration, ref PropertyListObjectModel plom)
		{
			MarkupTagElement tagProperties = (tagConfiguration.Elements["Properties"] as MarkupTagElement);
			if (tagProperties != null)
			{
				foreach (MarkupElement el in tagProperties.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "Property") continue;

					Property p = LoadPropertyListProperty(tag);
					if (p != null) plom.Properties.Add(p);
				}
			}
			MarkupTagElement tagGroups = (tagConfiguration.Elements["Groups"] as MarkupTagElement);
			if (tagGroups != null)
			{
				foreach (MarkupElement el in tagGroups.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.FullName != "Group") continue;

					Group g = LoadPropertyListGroup(tag);
					if (g != null) plom.Groups.Add(g);
				}
			}
		}

		private static Property LoadPropertyListProperty(MarkupTagElement tag)
		{
			MarkupAttribute attID = tag.Attributes["ID"];
			MarkupAttribute attValue = tag.Attributes["Value"];
			if (attID == null) return null;

			Property property = new Property();
			property.Name = attID.Value;

			if (attValue != null)
			{
				property.Value = ParseObject(attValue.Value);
			}
			else if (tag.Elements.Count > 0)
			{
				List<object> items = new List<object>();
				foreach (MarkupElement el in tag.Elements)
				{
					MarkupTagElement tag1 = (el as MarkupTagElement);
					if (tag1.FullName != "Value") continue;

					MarkupAttribute attItemDataType = tag1.Attributes["DataType"];
					if (attItemDataType != null)
					{

					}

					items.Add(ParseObject(tag1.Value));
				}
				property.Value = items.ToArray();
			}
			return property;
		}

		private static object ParseObject(string p)
		{
			#region Byte
			{
				byte result = 0;
				if (Byte.TryParse(p, out result)) return result;
			}
			#endregion
			#region SByte
			{
				sbyte result = 0;
				if (SByte.TryParse(p, out result)) return result;
			}
			#endregion
			#region Boolean
			{
				bool result = false;
				if (Boolean.TryParse(p, out result)) return result;
			}
			#endregion
			#region Char
			{
				char result = '\0';
				if (Char.TryParse(p, out result)) return result;
			}
			#endregion
			#region UInt16
			{
				ushort result = 0;
				if (UInt16.TryParse(p, out result)) return result;
			}
			#endregion
			#region UInt32
			{
				uint result = 0;
				if (UInt32.TryParse(p, out result)) return result;
			}
			#endregion
			#region UInt64
			{
				ulong result = 0;
				if (UInt64.TryParse(p, out result)) return result;
			}
			#endregion
			#region Int16
			{
				short result = 0;
				if (Int16.TryParse(p, out result)) return result;
			}
			#endregion
			#region Int32
			{
				int result = 0;
				if (Int32.TryParse(p, out result)) return result;
			}
			#endregion
			#region Int64
			{
				long result = 0;
				if (Int64.TryParse(p, out result)) return result;
			}
			#endregion
			#region Single
			{
				float result = 0.0f;
				if (Single.TryParse(p, out result)) return result;
			}
			#endregion
			#region Double
			{
				double result = 0.0D;
				if (Double.TryParse(p, out result)) return result;
			}
			#endregion
			#region Decimal
			{
				decimal result = 0.0M;
				if (Decimal.TryParse(p, out result)) return result;
			}
			#endregion
			#region TimeSpan
			{
				TimeSpan result = TimeSpan.Zero;
				if (TimeSpan.TryParse(p, out result)) return result;
			}
			#endregion
			#region DateTime
			{
				DateTime result = DateTime.Now;
				if (DateTime.TryParse(p, out result)) return result;
			}
			#endregion
			#region GUID
			{
				Guid result = Guid.Empty;
				try
				{
					return new Guid(p);
				}
				catch
				{
				}
			}
			#endregion

			return p;
		}

		private static Group LoadPropertyListGroup(MarkupTagElement tag)
		{
			MarkupAttribute attID = tag.Attributes["ID"];
			if (attID == null) return null;

			Group group = new Group();
			group.Name = attID.Value;

			MarkupTagElement tagProperties = (tag.Elements["Properties"] as MarkupTagElement);
			if (tagProperties != null)
			{
				foreach (MarkupElement el1 in tagProperties.Elements)
				{
					MarkupTagElement tag1 = (el1 as MarkupTagElement);
					if (tag1 == null) continue;
					if (tag1.Name != "Property") continue;

					Property p = LoadPropertyListProperty(tag1);
					if (p != null) group.Properties.Add(p);
				}
			}

			MarkupTagElement tagGroups = (tag.Elements["Groups"] as MarkupTagElement);
			if (tagGroups != null)
			{
				foreach (MarkupElement el1 in tagGroups.Elements)
				{
					MarkupTagElement tag1 = (el1 as MarkupTagElement);
					if (tag1 == null) continue;
					if (tag1.Name != "Group") continue;

					Group g = LoadPropertyListGroup(tag1);
					if (g != null) group.Groups.Add(g);
				}
			}

			return group;
		}


		public static void SaveMarkup(ref MarkupTagElement tagConfiguration, PropertyListObjectModel plom)
		{
			tagConfiguration.FullName = "Configuration";
			tagConfiguration.Attributes.Add("Version", FormatVersion.ToString(2));
			if (plom.Properties.Count > 0)
			{
				MarkupTagElement tagProperties = new MarkupTagElement();
				tagProperties.FullName = "Properties";
				foreach (Property property in plom.Properties)
				{
					RecursiveSaveObject(property, tagProperties);
				}
				tagConfiguration.Elements.Add(tagProperties);
			}
			if (plom.Groups.Count > 0)
			{
				MarkupTagElement tagGroups = new MarkupTagElement();
				tagGroups.FullName = "Groups";
				foreach (Group group in plom.Groups)
				{
					RecursiveSaveObject(group, tagGroups);
				}
				tagConfiguration.Elements.Add(tagGroups);
			}
		}

		private static void RecursiveSaveObject(Property item, MarkupTagElement tagParent)
		{
			MarkupTagElement tagProperty = new MarkupTagElement();
			tagProperty.FullName = "Property";
			tagProperty.Attributes.Add("ID", item.Name);
			if (item.Value is Array)
			{
				Array ary = (item.Value as Array);
				for (long i = 0; i < ary.LongLength; i++)
				{
					object obj = ary.GetValue(i);

					MarkupTagElement tagValue = new MarkupTagElement();
					tagValue.Attributes.Add("DataType", obj.GetType().FullName);
					tagValue.FullName = "Value";
					tagValue.Value = obj.ToString();
					tagProperty.Elements.Add(tagValue);
				}
			}
			else
			{
				tagProperty.Attributes.Add("Value", item.Value.ToString());
			}
			tagParent.Elements.Add(tagProperty);
		}
		private static void RecursiveSaveObject(Group item, MarkupTagElement tagParent)
		{
			MarkupTagElement tagGroup = new MarkupTagElement();
			tagGroup.FullName = "Group";
			tagGroup.Attributes.Add("ID", item.Name);
			if (item.Properties.Count > 0)
			{
				MarkupTagElement tagProperties = new MarkupTagElement();
				tagProperties.FullName = "Properties";
				foreach (Property property in item.Properties)
				{
					RecursiveSaveObject(property, tagProperties);
				}
				tagGroup.Elements.Add(tagProperties);
			}
			if (item.Groups.Count > 0)
			{
				MarkupTagElement tagGroups = new MarkupTagElement();
				tagGroups.FullName = "Groups";
				foreach (Group group in item.Groups)
				{
					RecursiveSaveObject(group, tagGroups);
				}
				tagGroup.Elements.Add(tagGroups);
			}
			tagParent.Elements.Add(tagGroup);
		}
	}
}
