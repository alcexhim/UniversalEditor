using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.CoreObject
{
    public class CoreObjectDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Core Object", new string[] { "*.vcs", "*.ics", "*.vcf" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
            if (plom == null) throw new ObjectModelNotSupportedException();

            Reader reader = base.Accessor.Reader;

            Group currentGroup = null;
            Property lastProperty = null;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.StartsWith(" "))
                {
                    if (lastProperty == null)
                    {
                        throw new InvalidDataFormatException("Cannot continue a property that hasn't been declared");
                    }
                    line = line.Substring(1);
                    line = line.Replace("\\,", ",");
                    lastProperty.Value += line;
                    continue;
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
                        currentGroup = plom.Groups.Add(value);
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
                            currentGroup = currentGroup.Parent;
                        }
                    }
                    else
                    {
                        throw new InvalidDataFormatException("Cannot close a group that has not been opened yet ('" + value + "')");
                    }
                }
                else
                {
                    if (currentGroup != null)
                    {
                        lastProperty = currentGroup.Properties.Add(key, value);
                    }
                    else
                    {
                        lastProperty = plom.Properties.Add(key, value);
                    }
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
            if (plom == null) throw new ObjectModelNotSupportedException();

            Writer writer = base.Accessor.Writer;
            foreach (Property property in plom.Properties)
            {
                WriteProperty(writer, property);
            }
            foreach (Group group in plom.Groups)
            {
                WriteGroup(writer, group);
            }
            writer.Flush();
        }

        private void WriteGroup(Writer writer, Group group, int indent = 0)
        {
            writer.WriteLine("BEGIN:" + group.Name);
            foreach (Property property in group.Properties)
            {
                WriteProperty(writer, property, indent + 1);
            }
            foreach (Group group1 in group.Groups)
            {
                WriteGroup(writer, group1);
            }
            writer.WriteLine("END:" + group.Name);
        }

        private void WriteProperty(Writer writer, Property property, int indent = 0)
        {
            writer.WriteLine(property.Name + ":" + property.Value.ToString());
        }
    }
}
