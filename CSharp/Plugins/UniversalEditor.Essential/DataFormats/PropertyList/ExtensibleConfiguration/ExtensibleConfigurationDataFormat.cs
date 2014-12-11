using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration
{
	public class ExtensibleConfigurationDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		
		private int mvarIndentLength = 4;
		public int IndentLength { get { return this.mvarIndentLength; } set { this.mvarIndentLength = value; } }

		private ExtensibleConfigurationSettings mvarSettings = new ExtensibleConfigurationSettings();
		protected ExtensibleConfigurationSettings Settings { get { return mvarSettings; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = objectModel as PropertyListObjectModel;
			Reader tr = base.Accessor.Reader;
			string nextString = string.Empty;
			string nextPropertyName = string.Empty;
			bool insideQuotedString = false;
			bool escaping = false;
			Group nextGroup = null;
			while (!tr.EndOfStream)
			{
				if (nextString.StartsWith(mvarSettings.SingleLineCommentStart))
				{
					string comment = tr.ReadLine();
				}
				if (nextString.StartsWith(mvarSettings.MultiLineCommentStart))
				{
					string comment = tr.ReadUntil(mvarSettings.MultiLineCommentEnd);
					string cmntend = tr.ReadFixedLengthString(mvarSettings.MultiLineCommentEnd.Length);
					nextString = String.Empty;
					continue;
				}

				char nextChar = tr.ReadChar();
				if (insideQuotedString)
				{
					if (nextChar == '"')
					{
						if (!escaping)
						{
							insideQuotedString = false;
							continue;
						}
					}
					else
					{
						if (nextChar == '\\')
						{
							if (!escaping)
							{
								escaping = true;
								continue;
							}
						}
					}
					nextString += nextChar;
					escaping = false;
				}
				else
				{
					char c = nextChar;
					string cw = c.ToString();
					if (c == '"')
					{
						insideQuotedString = !insideQuotedString;
						continue;
					}
					else if (cw == mvarSettings.PropertyNameValueSeparator)
					{
						nextPropertyName = nextString;
						nextString = string.Empty;
					}
					else if (cw == mvarSettings.PropertySeparator)
					{
						if (nextPropertyName != null)
						{
							nextPropertyName = nextPropertyName.Trim();
							nextString = nextString.Trim();

							if (nextGroup != null)
							{
								nextGroup.Properties.Add(nextPropertyName, nextString);
							}
							else
							{
								plom.Properties.Add(nextPropertyName, nextString);
							}
						}
						nextPropertyName = string.Empty;
						nextString = string.Empty;
					}
					else if (cw == mvarSettings.GroupStart)
					{
						Group group = new Group();
						group.Name = nextString.Trim();
						nextString = string.Empty;
						if (nextGroup != null)
						{
							nextGroup.Groups.Add(group);
						}
						else
						{
							plom.Groups.Add(group);
						}
						nextGroup = group;
					}
					else if (cw == mvarSettings.GroupEnd)
					{
						if (nextGroup != null)
						{
							nextGroup = nextGroup.Parent;
						}
					}
					else
					{
						nextString += nextChar;
						continue;
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = objectModel as PropertyListObjectModel;
			Writer tw = base.Accessor.Writer;
			foreach (Property p in plom.Properties)
			{
				tw.Write(mvarSettings.PropertyNamePrefix);
				tw.Write(p.Name);
				tw.Write(mvarSettings.PropertyNameSuffix);
				tw.Write(mvarSettings.PropertyNameValueSeparator);
				tw.Write(mvarSettings.PropertyValuePrefix);
				tw.WriteFixedLengthString(p.Value.ToString());
				tw.Write(mvarSettings.PropertyValueSuffix);
			}
			foreach (Group g in plom.Groups)
			{
				this.WriteGroup(tw, g, 0);
			}
			tw.Flush();
			tw.Close();
		}
		private void WriteGroup(Writer tw, Group group, int indent)
		{
			string indents = new string(' ', indent * this.mvarIndentLength);
			tw.WriteLine(indents + group.Name);
			tw.WriteLine(indents + "{");
			foreach (Property p in group.Properties)
			{
				tw.WriteLine(string.Concat(new object[]
				{
					indents, 
					new string(' ', this.mvarIndentLength), 
					p.Name, 
					"=\"", 
					p.Value, 
					"\";"
				}));
			}
			foreach (Group g in group.Groups)
			{
				this.WriteGroup(tw, g, indent + 1);
			}
			tw.WriteLine(indents + "}");
		}
	}
}
