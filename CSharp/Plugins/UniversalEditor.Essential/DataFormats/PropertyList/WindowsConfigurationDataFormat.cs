using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.PropertyList
{
	public class WindowsConfigurationDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Windows Configuration document", new string[] { "*.ini", "*.inf" });
			dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private string[] mvarCommentSignals = new string[] { ";", "#" };
		public string[] CommentSignals { get { return mvarCommentSignals; } set { mvarCommentSignals = value; } }

		private string mvarPropertyValuePrefix = "\"";
		public string PropertyValuePrefix { get { return mvarPropertyValuePrefix; } set { mvarPropertyValuePrefix = value; } }

		private string mvarPropertyValueSuffix = "\"";
        public string PropertyValueSuffix { get { return mvarPropertyValueSuffix; } set { mvarPropertyValueSuffix = value; } }

        private string mvarPropertyNameValueSeparator = "=";
        public string PropertyNameValueSeparator { get { return mvarPropertyNameValueSeparator; } set { mvarPropertyNameValueSeparator = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
            if (plom == null) throw new ObjectModelNotSupportedException();

			Reader tr = base.Accessor.Reader;
			Group CurrentGroup = null;
            
			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				if (String.IsNullOrEmpty(line)) continue;

				if (!line.StartsWith(";") && !line.StartsWith("#"))
				{
					if (line.StartsWith("[") && line.EndsWith("]"))
					{
						string groupName = line.Substring(1, line.Length - 2);
						CurrentGroup = plom.Groups.Add(groupName);
					}
					else
					{
						if (line.Contains(mvarPropertyNameValueSeparator))
						{
							string[] nvp = line.Split(new string[]
							{
								mvarPropertyNameValueSeparator
							}, 2, StringSplitOptions.None);
							string propertyName = nvp[0].Trim();
							string propertyValue = string.Empty;
							if (nvp.Length == 2)
							{
								propertyValue = nvp[1].Trim();
							}
							if (propertyValue.StartsWith(this.mvarPropertyValuePrefix) && propertyValue.EndsWith(this.mvarPropertyValueSuffix))
							{
								propertyValue = propertyValue.Substring(this.mvarPropertyValuePrefix.Length, propertyValue.Length - this.mvarPropertyValuePrefix.Length - this.mvarPropertyValueSuffix.Length);
							}
							else
							{
								if (propertyValue.ContainsAny(this.mvarCommentSignals))
								{
									propertyValue = propertyValue.Substring(0, propertyValue.IndexOfAny(this.mvarCommentSignals));
								}
							}
							if (CurrentGroup != null)
							{
								CurrentGroup.Properties.Add(propertyName, propertyValue);
							}
							else
							{
								plom.Properties.Add(propertyName, propertyValue);
							}
						}
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer tw = base.Accessor.Writer;
		    // tw.BaseStream.SetLength(0L); // why is this here??
			if (objectModel is PropertyListObjectModel)
			{
				PropertyListObjectModel objm = objectModel as PropertyListObjectModel;
				foreach (Property p in objm.Properties)
				{
					tw.Write(p.Name + "=");
					tw.Write(this.mvarPropertyValuePrefix);
					if (p.Value != null)
					{
						tw.Write(p.Value.ToString());
					}
					tw.Write(this.mvarPropertyValueSuffix);
					if (objm.Properties.IndexOf(p) < objm.Properties.Count - 1)
					{
						tw.WriteLine();
					}
				}
				if (objm.Groups.Count > 0 && objm.Properties.Count > 0)
				{
					tw.WriteLine();
				}
				foreach (Group g in objm.Groups)
				{
					tw.WriteLine("[" + g.Name + "]");
					foreach (Property p in g.Properties)
					{
						tw.Write(p.Name + "=");
						tw.Write(this.mvarPropertyValuePrefix);
						if (p.Value != null)
						{
							tw.Write(p.Value.ToString());
						}
						tw.Write(this.mvarPropertyValueSuffix);
						if (g.Properties.IndexOf(p) < g.Properties.Count - 1)
						{
							tw.WriteLine();
						}
					}
					if (objm.Groups.IndexOf(g) < objm.Groups.Count - 1)
					{
						tw.WriteLine();
					}
				}
			}
			tw.Flush();
		}
	}
}
