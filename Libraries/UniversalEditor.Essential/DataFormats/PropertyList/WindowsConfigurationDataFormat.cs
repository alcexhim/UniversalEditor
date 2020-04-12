//
//  WindowsConfigurationDataFormat.cs - provides a DataFormat for manipulating Microsoft Windows configuration (INI) files
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

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.PropertyList
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Windows configuration (INI) files.
	/// </summary>
	public class WindowsConfigurationDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		/// <summary>
		/// Gets or sets an array of <see cref="string" />s which indicate the start of a comment line.
		/// </summary>
		/// <value>The <see cref="string" />s which indicate the start of a comment line.</value>
		public string[] CommentSignals { get; set; } = new string[] { ";", "#" };
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to prefix a property value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to prefix a property value.</value>
		public string PropertyValuePrefix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to suffix a property value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to suffix a property value.</value>
		public string PropertyValueSuffix { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to separate a property name from a property value.
		/// </summary>
		/// <value>The <see cref="string" /> with which to separate a property name from a property value.</value>
		public string PropertyNameValueSeparator { get; set; } = "=";

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
						if (line.Contains(PropertyNameValueSeparator))
						{
							string[] nvp = line.Split(new string[]
							{
								PropertyNameValueSeparator
							}, 2, StringSplitOptions.None);
							string propertyName = nvp[0].Trim();
							string propertyValue = string.Empty;
							if (nvp.Length == 2)
							{
								propertyValue = nvp[1].Trim();
							}
							if (propertyValue.StartsWith(this.PropertyValuePrefix) && propertyValue.EndsWith(this.PropertyValueSuffix))
							{
								propertyValue = propertyValue.Substring(this.PropertyValuePrefix.Length, propertyValue.Length - this.PropertyValuePrefix.Length - this.PropertyValueSuffix.Length);
							}
							else
							{
								if (propertyValue.ContainsAny(this.CommentSignals))
								{
									propertyValue = propertyValue.Substring(0, propertyValue.IndexOfAny(this.CommentSignals));
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
					tw.Write(this.PropertyValuePrefix);
					if (p.Value != null)
					{
						tw.Write(p.Value.ToString());
					}
					tw.Write(this.PropertyValueSuffix);
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
						tw.Write(this.PropertyValuePrefix);
						if (p.Value != null)
						{
							tw.Write(p.Value.ToString());
						}
						tw.Write(this.PropertyValueSuffix);
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
