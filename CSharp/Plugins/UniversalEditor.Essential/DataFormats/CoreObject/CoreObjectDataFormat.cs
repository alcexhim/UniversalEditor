using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.CoreObject;

namespace UniversalEditor.DataFormats.CoreObject
{
	public class CoreObjectDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(CoreObjectObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			CoreObjectObjectModel core = (objectModel as CoreObjectObjectModel);
			if (core == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			CoreObjectGroup currentGroup = null;

			string lastPropertyKey = null;
			string lastPropertyValue = null;

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				if (line.StartsWith(" "))
				{
					if (lastPropertyValue == null)
					{
						throw new InvalidDataFormatException("Cannot continue a property that hasn't been declared");
					}
					line = line.Substring(1);
					line = line.Replace("\\,", ",");
					lastPropertyValue += line;
					continue;
				}
				else
				{
					if (lastPropertyValue != null)
					{
						string[] lastPropertyValues = lastPropertyValue.Split(new char[] { ';' });
						if (currentGroup != null)
						{
							currentGroup.Properties.Add(lastPropertyKey, lastPropertyValues);
						}
						else
						{
							core.Properties.Add(lastPropertyKey, lastPropertyValues);
						}
					}
				}
				// line = line.Trim();

				if (String.IsNullOrEmpty(line)) continue;

				string[] parts = line.Split(new char[] { ':' }, 2, StringSplitOptions.None);
				
				string key = parts[0];
				string value = parts[1];
				value = value.Replace("\\,", ",");

				if (key.ToUpper() == "BEGIN")
				{
					if (currentGroup != null)
					{
						currentGroup = currentGroup.Groups.Add(value);
					}
					else
					{
						currentGroup = core.Groups.Add(value);
					}
				}
				else if (key.ToUpper() == "END")
				{
					if (currentGroup != null)
					{
						if (currentGroup.Name != value)
						{
							throw new InvalidDataFormatException("Cannot close a group that has not been opened yet ('" + value + "')");
						}
						else
						{
							currentGroup = currentGroup.ParentGroup;
						}
					}
					else
					{
						throw new InvalidDataFormatException("Cannot close a group that has not been opened yet ('" + value + "')");
					}
				}
				else
				{
					lastPropertyKey = key;
					lastPropertyValue = value;
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			CoreObjectObjectModel plom = (objectModel as CoreObjectObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			foreach (CoreObjectProperty property in plom.Properties)
			{
				WriteProperty(writer, property);
			}
			foreach (CoreObjectGroup group in plom.Groups)
			{
				WriteGroup(writer, group);
			}
			writer.Flush();
		}

		private void WriteGroup(Writer writer, CoreObjectGroup group, int indent = 0)
		{
			writer.WriteLine("BEGIN:" + group.Name);
			foreach (CoreObjectProperty property in group.Properties)
			{
				WriteProperty(writer, property, indent + 1);
			}
			foreach (CoreObjectGroup group1 in group.Groups)
			{
				WriteGroup(writer, group1);
			}
			writer.WriteLine("END:" + group.Name);
		}

		private void WriteProperty(Writer writer, CoreObjectProperty property, int indent = 0)
		{
			writer.Write(property.Name);
			foreach (CoreObjectAttribute att in property.Attributes)
			{
				writer.Write(';');
				writer.Write(att.Name);
				if (att.Values.Count > 0)
				{
					writer.Write('=');
					for (int i = 0; i < att.Values.Count; i++)
					{
						writer.Write(att.Values[i]);
						if (i < att.Values.Count - 1) writer.Write(',');
					}
				}
			}
			writer.Write(':');
			for (int i = 0; i < property.Values.Count; i++)
			{
				writer.Write(property.Values[i]);
				if (i < property.Values.Count - 1) writer.Write(';');
			}
			writer.WriteLine();
		}
	}
}
