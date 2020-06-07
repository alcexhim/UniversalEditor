//
//  TEDDataFormat.cs - provides a DataFormat for manipulating 3D models in Triangle Editor TED format
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

using MBS.Framework.Drawing;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.TriangleEditor
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D models in Triangle Editor TED format.
	/// </summary>
	public class TEDDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
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

			IO.Reader br = base.Accessor.Reader;

			byte zeroX14 = br.ReadByte();
			if (zeroX14 != 0x14) throw new InvalidDataFormatException();

			string signature = br.ReadFixedLengthString(20);
			if (signature != "TriangleEditor_V.1.0") throw new InvalidDataFormatException();

			ushort triangleCount = br.ReadUInt16();
			if (triangleCount == 0xFFFF) return;

			triangleCount++;

			ModelSurface surf = new ModelSurface();
			for (ushort i = 0; i < triangleCount; i++)
			{
				ModelTriangle tri = new ModelTriangle();
				for (int j = 0; j < 3; j++)
				{
					// read the next vertex for the triangle
					double positionY = br.ReadDouble();
					double positionX = br.ReadDouble();
					double positionZ = br.ReadDouble();

					ModelVertex vtx = new ModelVertex();
					vtx.Position = new PositionVector3(positionX, positionY, positionZ);
					vtx.OriginalPosition = new PositionVector3(positionX, positionY, positionZ);

					surf.Vertices.Add(vtx);

					switch (j)
					{
						case 0: tri.Vertex1 = vtx; break;
						case 1: tri.Vertex2 = vtx; break;
						case 2: tri.Vertex3 = vtx; break;
					}
				}
				surf.Triangles.Add(tri);
			}
			model.Surfaces.Add(surf);


			ModelMaterial mat = new ModelMaterial();
			mat.Name = "dummy";
			mat.IndexCount = (uint)surf.Vertices.Count;
			foreach (ModelTriangle tri in surf.Triangles)
			{
				mat.Triangles.Add(tri);
			}
			mat.AmbientColor = Colors.White;
			mat.DiffuseColor = Colors.White;
			mat.EmissiveColor = Colors.White;
			mat.SpecularColor = Colors.White;
			model.Materials.Add(mat);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
