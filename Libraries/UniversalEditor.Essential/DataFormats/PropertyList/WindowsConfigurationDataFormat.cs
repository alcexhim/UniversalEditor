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
using System.Linq;
using System.Collections.Generic;
using MBS.Framework.Settings;

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
			dfr.ImportOptions.SettingsGroups[0].Settings.Add(new TextSetting("GroupHierarchySeparator", "Group _hierarchy separator", "."));
			dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting("GroupHierarchySeparator", "Group _hierarchy separator", "."));
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
		/// <summary>
		/// Gets or sets the <see cref="string" /> with which to separate hierarchical group names.
		/// </summary>
		/// <value>The group hierarchy separator.</value>
		public string GroupHierarchySeparator { get; set; } = null;

		public static string DefaultGroupHierarchySeparator { get; set; } = ".";

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			Reader tr = base.Accessor.Reader;
			Group CurrentGroup = null;

			if (!tr.EndOfStream)
			{
				long pos = tr.Accessor.Position;

				// determine BOM
				// FIXME: this is super inefficient as it makes more than a single pass through
				string line = tr.ReadLine();
				if (line.StartsWith("\xff\xfe"))
				{
					tr.Endianness = Endianness.LittleEndian;
					tr.Accessor.DefaultEncoding = Encoding.UTF16LittleEndian;
					pos += 2;
				}
				else if (line.StartsWith("\xfe\xff"))
				{
					tr.Endianness = Endianness.BigEndian;
					tr.Accessor.DefaultEncoding = Encoding.UTF16BigEndian;
					pos += 2;
				}
				else if (line.StartsWith("\xef\xbb\xbf"))
				{
					tr.Accessor.DefaultEncoding = Encoding.UTF8;
					pos += 3;
				}

				tr.Accessor.Position = pos;
			}

			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				line = line.Trim();

				if (String.IsNullOrEmpty(line)) continue;

				if (!line.StartsWith(";") && !line.StartsWith("#"))
				{
					if (line.StartsWith("[") && line.EndsWith("]"))
					{
						string groupName = line.Substring(1, line.Length - 2);
						CurrentGroup = plom.Items.AddGroup(groupName, GroupHierarchySeparator);
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
								CurrentGroup.Items.AddProperty(propertyName, propertyValue);
							}
							else
							{
								plom.Items.AddProperty(propertyName, propertyValue);
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
				foreach (PropertyListItem p in objm.Items)
				{
					if (p is Property)
					{
						WriteProperty(tw, p as Property, objm.Items.IndexOf(p) < objm.Items.Count - 1);
					}
					else if (p is Group)
					{
						WriteGroup(tw, p as Group, objm.Items.IndexOf(p) < objm.Items.Count - 1);
					}
				}
			}
			tw.Flush();
		}

		private void WriteGroup(Writer tw, Group g, bool endline, string prefix = null)
		{
			string groupHierarchySeparator = GroupHierarchySeparator;
			if (groupHierarchySeparator == null)
				groupHierarchySeparator = DefaultGroupHierarchySeparator;

			string fullName = String.Format("{0}{1}", prefix == null ? String.Empty : String.Format("{0}{1}", prefix, groupHierarchySeparator), g.Name);
			tw.WriteLine(String.Format("[{0}]", fullName));

			IEnumerable<Property> properties = g.Items.OfType<Property>();
			foreach (Property p in properties)
			{
				WriteProperty(tw, p, g.Items.IndexOf(p) < g.Items.Count - 1);
			}

			IEnumerable<Group> groups = g.Items.OfType<Group>();
			if (groups.Any())
			{
				tw.WriteLine();
				foreach (Group g2 in groups)
				{
					WriteGroup(tw, g2, endline, fullName);
				}
			}

			if (endline)
			{
				tw.WriteLine();
			}
		}

		private void WriteProperty(Writer tw, Property p, bool endline)
		{
			tw.Write(p.Name + "=");
			tw.Write(this.PropertyValuePrefix);
			if (p.Value != null)
			{
				tw.Write(p.Value.ToString());
			}
			tw.Write(this.PropertyValueSuffix);
			if (endline)
			{
				tw.WriteLine();
			}
		}
	}
}
