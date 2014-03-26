using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.Text
{
	public class TextPropertyListDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Text-based property list", new byte?[][] { new byte?[] { (byte)'#', (byte)'T', (byte)'P', (byte)'L', (byte)'-', (byte)'1', (byte)'.', (byte)'0' } }, new string[] { "*.tpl" });
				_dfr.ExportOptions.Add(new CustomOptionText("Title", "&Title: ", "Text Property List"));
			}
			return _dfr;
		}

		private TextPropertyListSettings mvarSettings = new TextPropertyListSettings();
		public TextPropertyListSettings Settings { get { return mvarSettings; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			base.Accessor.DefaultEncoding = IO.Encoding.UTF8;
			IO.Reader tr = new IO.Reader(base.Accessor);
			string signature = tr.ReadLine();
			if (!signature.StartsWith("#TPL-1.0")) throw new InvalidDataFormatException();

			Group parentGroup = null;

			plom.Title = signature.Substring(8);

			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				line = line.Trim();
				line = TrimComments(line);
				if (String.IsNullOrEmpty(line)) continue;

				if (line.StartsWith("Begin "))
				{
					string blockName = line.Substring(6);
					Group group = new Group();
					group.Name = blockName;
					if (parentGroup != null)
					{
						parentGroup.Groups.Add(group);
					}
					else
					{
						plom.Groups.Add(group);
					}
					parentGroup = group;
				}
				else if (line.StartsWith("End "))
				{
					string blockName = line.Substring(4);
					if (parentGroup != null && parentGroup.Name == blockName)
					{
						parentGroup = parentGroup.Parent;
					}
					else if (parentGroup != null)
					{
						throw new InvalidDataFormatException("End block name \"" + blockName + "\" does not match start block name \"" + parentGroup.Name + "\"");
					}
					else
					{
						throw new InvalidDataFormatException("End block name \"" + blockName + "\" encountered when block was not started");
					}
				}
				else
				{
					string[] splits = line.Split(Settings.PropertyNameValueSeparators, mvarSettings.IgnoreBegin, mvarSettings.IgnoreEnd, StringSplitOptions.RemoveEmptyEntries, 2, true);
					string name = splits[0].Trim();
					string value = null;
					if (splits.Length > 1) value = splits[1];

					Property property = new Property(name, value);
					if (parentGroup != null)
					{
						parentGroup.Properties.Add(property);
					}
					else
					{
						plom.Properties.Add(property);
					}
				}
			}
		}

		private string TrimComments(string line)
		{
			string line2 = (line.Clone() as string);
			foreach (string comment in mvarSettings.CommentSignals)
			{
				int indexof = line2.IndexOf(comment);
				if (indexof > -1)
				{
					line2 = line2.Substring(0, indexof);
				}
			}
			return line2;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			base.Accessor.DefaultEncoding = UniversalEditor.IO.Encoding.UTF8;
			IO.Writer tw = new IO.Writer(base.Accessor);
			tw.WriteLine("#TPL-1.0 " + plom.Title);

			foreach (Group group in plom.Groups)
			{
				WriteGroup(tw, group);
			}
			foreach (Property property in plom.Properties)
			{
				WriteProperty(tw, property);
			}

			tw.Flush();
		}

		private void WriteGroup(IO.Writer tw, Group group, int indent = 0)
		{
			string indentStr = new string('\t', indent);
			tw.WriteLine(indentStr + "Begin " + group.Name);
			foreach (Group group1 in group.Groups)
			{
				WriteGroup(tw, group1, indent + 1);
			}
			foreach (Property property in group.Properties)
			{
				WriteProperty(tw, property, indent);
			}
			tw.WriteLine(indentStr + "End " + group.Name);
		}

		private void WriteProperty(IO.Writer tw, Property property, int indent = 0)
		{
			string indentStr = new string('\t', indent);
			tw.WriteLine(indentStr + property.Name + "\t" + property.Value);
		}
	}
}
