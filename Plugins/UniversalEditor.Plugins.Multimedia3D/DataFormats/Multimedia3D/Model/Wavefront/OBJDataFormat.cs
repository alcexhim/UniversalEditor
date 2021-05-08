//
//  OBJDataFormat.cs - provides a DataFormat for manipulating 3D models in Wavefront OBJ text format
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
using System.Collections.Generic;

using UniversalEditor.DataFormats.Text.Plain;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
using UniversalEditor.ObjectModels.Text.Plain;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Wavefront
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D models in Wavefront OBJ text format.
	/// </summary>
	public class OBJDataFormat : PlainTextDataFormat
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
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PlainTextObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PlainTextObjectModel ptom = (objectModels.Pop() as PlainTextObjectModel);
			ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			string filetitle = Accessor.GetFileTitle();

			PlainTextObjectModel ptom = new PlainTextObjectModel();
			ptom.Lines.Add("# Universal Editor MM3D v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " OBJ File: '" + Accessor.GetFileName() + "'");
			ptom.Lines.Add("# https://github.com/alcexhim/UniversalEditor");
			ptom.Lines.Add("mtllib " + filetitle + ".mtl");

			for (int i = 0; i < model.Surfaces.Count; i++)
			{
				ptom.Lines.Add("o Object");
				for (int j = 0; j < model.Surfaces[i].Vertices.Count; j++)
				{
					ptom.Lines.Add(String.Format("v {0} {1} {2}", model.Surfaces[0].Vertices[j].Position.X, model.Surfaces[0].Vertices[j].Position.Y, model.Surfaces[0].Vertices[j].Position.Z));
					/*
					ptom.Lines.Add(String.Format("v {0} {1} {2}", model.Surfaces[0].Triangles[j].Vertex1.Position.X, model.Surfaces[0].Triangles[j].Vertex1.Position.Y, model.Surfaces[0].Triangles[j].Vertex1.Position.Z));
					ptom.Lines.Add(String.Format("v {0} {1} {2}", model.Surfaces[0].Triangles[j].Vertex2.Position.X, model.Surfaces[0].Triangles[j].Vertex2.Position.Y, model.Surfaces[0].Triangles[j].Vertex2.Position.Z));
					ptom.Lines.Add(String.Format("v {0} {1} {2}", model.Surfaces[0].Triangles[j].Vertex3.Position.X, model.Surfaces[0].Triangles[j].Vertex3.Position.Y, model.Surfaces[0].Triangles[j].Vertex3.Position.Z));
					*/
				}
				for (int j = 0; j < model.Surfaces[0].Triangles.Count; j++)
				{
					ptom.Lines.Add(String.Format("vt {0} {1}", model.Surfaces[0].Triangles[j].Vertex1.Texture.U, model.Surfaces[0].Triangles[j].Vertex1.Texture.V));
					ptom.Lines.Add(String.Format("vt {0} {1}", model.Surfaces[0].Triangles[j].Vertex2.Texture.U, model.Surfaces[0].Triangles[j].Vertex2.Texture.V));
					ptom.Lines.Add(String.Format("vt {0} {1}", model.Surfaces[0].Triangles[j].Vertex3.Texture.U, model.Surfaces[0].Triangles[j].Vertex3.Texture.V));
				}
				for (int j = 0; j < model.Surfaces[0].Triangles.Count; j++)
				{
					ptom.Lines.Add(String.Format("vn {0} {1} {2}", model.Surfaces[0].Triangles[j].Vertex1.Normal.X, model.Surfaces[0].Triangles[j].Vertex1.Normal.Y, model.Surfaces[0].Triangles[j].Vertex1.Normal.Z));
					ptom.Lines.Add(String.Format("vn {0} {1} {2}", model.Surfaces[0].Triangles[j].Vertex2.Normal.X, model.Surfaces[0].Triangles[j].Vertex2.Normal.Y, model.Surfaces[0].Triangles[j].Vertex2.Normal.Z));
					ptom.Lines.Add(String.Format("vn {0} {1} {2}", model.Surfaces[0].Triangles[j].Vertex3.Normal.X, model.Surfaces[0].Triangles[j].Vertex3.Normal.Y, model.Surfaces[0].Triangles[j].Vertex3.Normal.Z));
				}
				// ptom.Lines.Add("usemtl Material");
				// ptom.Lines.Add("s off");

				for (int k = 0; k < model.Surfaces[i].Triangles.Count; k++)
				{
					ptom.Lines.Add(String.Format("f {0}/{1}/{2} {3}/{4}/{5} {6}/{7}/{8}",
						model.Surfaces[i].Vertices.IndexOf(model.Surfaces[i].Triangles[k].Vertex1),
						model.Surfaces[i].Vertices.IndexOf(model.Surfaces[i].Triangles[k].Vertex2),
						(i + 1).ToString(),
						model.Surfaces[i].Vertices.IndexOf(model.Surfaces[i].Triangles[k].Vertex2),
						model.Surfaces[i].Vertices.IndexOf(model.Surfaces[i].Triangles[k].Vertex3),
						(i + 1).ToString(),
						model.Surfaces[i].Vertices.IndexOf(model.Surfaces[i].Triangles[k].Vertex3),
						model.Surfaces[i].Vertices.IndexOf(model.Surfaces[i].Triangles[k].Vertex1),
						(i + 1).ToString()
					));
				}
			}

			objectModels.Push(ptom);
		}
	}
}
