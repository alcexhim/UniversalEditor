//
//  A3DADataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Linq;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.Plugins.Sega.DataFormats.PropertyList.A3DA
{
	public class A3DADataFormat : DataFormat
	{
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

		public string OriginalFileName { get; set; } = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			Accessor.DefaultEncoding = Encoding.UTF8;

			string signature = reader.ReadLine();
			if (signature != "#A3DA__________")
				throw new InvalidDataFormatException("File does not begin with '#A3DA__________'");

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				line = line.Trim();
				if (line.Contains("#"))
					line = line.Substring(0, line.IndexOf('#'));

				if (String.IsNullOrEmpty(line))
					continue;

				string[] values = line.Split(new char[] { '=' }, 2);
				string key = values[0];
				string value = values[1];
				string[] paths = key.Split(new char[] { '.' });

				Group parent = null;
				for (int i = 0; i < paths.Length - 1; i++)
				{
					if (parent == null)
					{
						Group ng = plom.Items.OfType<Group>(paths[i]);
						if (ng == null)
							ng = plom.Items.AddGroup(paths[i]);

						parent = ng;
					}
					else
					{
						Group ng = parent.Items.OfType<Group>(paths[i]);
						if (ng == null)
							ng = parent.Items.AddGroup(paths[i]);

						parent = ng;
					}
				}

				if (parent != null)
				{
					parent.Items.AddProperty(paths[paths.Length - 1], value);
				}
				else
				{
					plom.Items.AddProperty(paths[paths.Length - 1], value);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteLine("#A3DA__________");
			writer.WriteLine(String.Format("#{0}", DateTime.Now.ToString("ddd MMM dd HH:mm:ss YYYY")));
			writer.WriteLine("_.converter.version=20050823");
			writer.WriteLine(String.Format("_.file_name={0}", OriginalFileName == null ? System.IO.Path.GetFileName(Accessor.GetFileName()) : OriginalFileName));
			writer.WriteLine("_.property.version=20050706");
			foreach (Group g in plom.Items.OfType<Group>())
			{
				WriteGroupRecursive(writer, g);
			}
			foreach (Property p in plom.Items.OfType<Property>())
			{
				WritePropertyRecursive(writer, p);
			}
		}

		private void WriteGroupRecursive(Writer writer, Group group, string prefix = "")
		{
			foreach (Group g in group.Items.OfType<Group>())
			{
				WriteGroupRecursive(writer, g);
			}
			foreach (Property p in group.Items.OfType<Property>())
			{
				WritePropertyRecursive(writer, p);
			}
		}

		private void WritePropertyRecursive(Writer writer, Property property, string prefix = "")
		{
			writer.WriteLine(String.Format("{0}={1}", prefix + property.Name, property.Value));
		}
	}
}
