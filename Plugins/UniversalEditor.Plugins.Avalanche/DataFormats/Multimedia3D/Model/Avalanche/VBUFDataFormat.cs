using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Avalanche
{
	public class VBUFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://forum.xentax.com/viewtopic.php?f=16&t=5983");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			FileAccessor facc = (base.Accessor as FileAccessor);
			if (facc == null) throw new InvalidOperationException("Input must be a file");

			string filename = System.IO.Path.ChangeExtension(facc.FileName, "ibuf");
			IO.Reader br = base.Accessor.Reader;
			IO.Reader brIBUF = new IO.Reader(new FileAccessor(filename));
			br.Endianness = IO.Endianness.LittleEndian;
			brIBUF.Endianness = IO.Endianness.BigEndian;

			//br.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
			ModelSurface surf = new ModelSurface();

			int count = (int)(brIBUF.Accessor.Length / 2);
			for (int i = 0; i < count; i++)
			{
				float vx = br.ReadSingle();
				float vz = br.ReadSingle();
				float vy = br.ReadSingle();
				vx *= 100;
				vy *= 100;
				vz *= 100;
				vz *= -1;

				float tu = br.ReadHalf();
				float tv = br.ReadHalf();
				tv = 1 - tv;

				uint unknown1 = br.ReadUInt32();
				uint vertexColor = br.ReadUInt32();

				ModelVertex vtx = new ModelVertex(vx, vy, vz, tu, tv); // vx, vz, vy in original script??
				surf.Vertices.Add(vtx);
			}
			/*
			for (int x = 0; x < surf.Vertices.Count; x++)
			{
				float nx = br.ReadSingle();
				float ny = br.ReadSingle();
				float nz = br.ReadSingle();
				nz *= -1;
				float un = br.ReadSingle();

				surf.Vertices[x].Normal = new PositionVector3(nx, ny, nz);
				surf.Vertices[x].OriginalNormal = new PositionVector3(nx, ny, nz);

				// not sure why this needs to be here...
				br.BaseStream.Seek(10, System.IO.SeekOrigin.Current);
			}
			while (!brIBUF.EndOfStream)
			{
				ushort fa = (ushort)(brIBUF.ReadUInt16() + 1);
				ushort fb = (ushort)(brIBUF.ReadUInt16() + 1);
				ushort fc = (ushort)(brIBUF.ReadUInt16() + 1);
				surf.Triangles.Add(surf.Vertices[fa], surf.Vertices[fb], surf.Vertices[fc]);
			}
			*/

			for (int i = 0; i < surf.Vertices.Count - 3; i += 3)
			{
				surf.Triangles.Add(surf.Vertices[i], surf.Vertices[i + 1], surf.Vertices[i + 2]);
			}

			ModelMaterial matDefault = new ModelMaterial();
			matDefault.Name = "default";
			matDefault.EmissiveColor = Color.FromRGBAByte(255, 255, 255, 255);
			foreach (ModelTriangle tri in surf.Triangles)
			{
				matDefault.Triangles.Add(tri);
			}
			model.Materials.Add(matDefault);

			model.Surfaces.Add(surf);
			/*
			 // wtf...?
			msh = mesh vertices:Vert_array faces:Face_array
			msh.numTVerts = UV_array.count
			buildTVFaces msh
			msh.name=fname
			for j = 1 to UV_array.count do setTVert msh j UV_array[j]
			for j = 1 to Face_array.count do setTVFace msh j Face_array[j]
			--===========================================================================
			gc()
			fclose f
			fclose g
			enableSceneRedraw()
			Print ("Done! ("+((((timestamp())-st)/60)as string)+" Seconds)") --print time to finish
			)))) else (Print "Aborted.")
		}
			*/
			brIBUF.Close();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
