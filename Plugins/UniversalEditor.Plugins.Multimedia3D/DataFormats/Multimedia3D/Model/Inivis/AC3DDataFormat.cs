//
//  AC3DDataFormat.cs - provides a DataFormat for manipulating 3D model files in Inivis AC3D format
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

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Inivis
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D model files in Inivis AC3D format.
	/// </summary>
	public class AC3DDataFormat : DataFormat
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

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			IO.Writer tw = base.Accessor.Writer;
			tw.WriteLine("AC3Db");
			tw.WriteLine("OBJECT world");
			tw.WriteLine("kids 1");
			tw.WriteLine("OBJECT poly");
			tw.WriteLine("name \"" + model.Name + "\"");
			tw.WriteLine("loc 0 0 0");
			tw.WriteLine("crease 45.000000");
			tw.WriteLine("numvert " + model.Surfaces[0].Vertices.Count);
			foreach (ModelVertex vert in model.Surfaces[0].Vertices)
			{
				tw.WriteLine(vert.Position.X.ToString() + " " + vert.Position.Y.ToString() + " " + vert.Position.Z.ToString());
			}

			int surfcount = ((int)((double)model.Surfaces[0].Vertices.Count / 3));
			tw.WriteLine("numsurf " + surfcount.ToString());
			for (int i = 0; i < model.Surfaces[0].Vertices.Count; i += 3)
			{
				tw.WriteLine("SURF 0x30");
				tw.WriteLine("mat 0");
				tw.WriteLine("refs 3");

				tw.WriteLine(i.ToString() + " " + model.Surfaces[0].Vertices[i].Texture.U.ToString() + " " + model.Surfaces[0].Vertices[i].Texture.V.ToString());
				tw.WriteLine((i + 1).ToString() + " " + model.Surfaces[0].Vertices[i + 1].Texture.U.ToString() + " " + model.Surfaces[0].Vertices[i + 1].Texture.V.ToString());
				tw.WriteLine((i + 2).ToString() + " " + model.Surfaces[0].Vertices[i + 2].Texture.U.ToString() + " " + model.Surfaces[0].Vertices[i + 2].Texture.V.ToString());
			}
			tw.Flush();
		}
	}
}
