//
//  MQOTextDataFormat.cs - provides a DataFormat for manipulating 3D models in Metasequoia (MQO) text format
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

using UniversalEditor.ObjectModels.Multimedia3D.Model;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Metasequoia
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D models in Metasequoia (MQO) text format.
	/// </summary>
	public class MQOTextDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private float mvarFormatVersion = 1.0f;
		public float FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			Reader tr = base.Accessor.Reader;
			string MetasequoiaDocument = tr.ReadLine();
			if (MetasequoiaDocument != "MetasequoiaDocument") throw new InvalidDataFormatException("File does not begin with \"MetasequoiaDocument\"");

			string DocumentFormat = tr.ReadLine();
			if (!DocumentFormat.StartsWith("Format Text Ver ")) throw new InvalidDataFormatException("Cannot understand Metasequoia format \"" + DocumentFormat + "\"");

			mvarFormatVersion = Single.Parse(DocumentFormat.Substring(16));

			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				if (String.IsNullOrEmpty(line.Trim())) continue;

				if (line.StartsWith("Scene "))
				{
				}
				else if (line.StartsWith("Material "))
				{
					string materialCountStr = line.Substring(9, line.IndexOf("{") - 9);
					int materialCount = Int32.Parse(materialCountStr);
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			Writer tw = base.Accessor.Writer;
			tw.WriteLine("MetasequoiaDocument");

			tw.WriteLine("Format Text Ver " + mvarFormatVersion.ToString("0.#"));

			tw.WriteLine("Material " + model.Materials.Count.ToString());
			foreach (ModelMaterial mat in model.Materials)
			{
			}
		}
	}
}
