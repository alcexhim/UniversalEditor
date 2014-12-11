using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Shaiya
{
	public class Shaiya3DCDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Shaiya Online 3DC model", new string[] { "*.3dc" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			
			IO.Reader br = base.Accessor.Reader;
			uint constant00 = br.ReadUInt32();
			uint boneCount = br.ReadUInt32();
			for (uint i = 0; i < boneCount; i++)
			{
				// bone matrices
				ModelBone bone = new ModelBone();
				// TODO: what do??

				float matrix1x1 = br.ReadSingle();
				float matrix1x2 = br.ReadSingle();
				float matrix1x3 = br.ReadSingle();
				float matrix1x4 = br.ReadSingle();
				float matrix2x1 = br.ReadSingle();
				float matrix2x2 = br.ReadSingle();
				float matrix2x3 = br.ReadSingle();
				float matrix2x4 = br.ReadSingle();
				float matrix3x1 = br.ReadSingle();
				float matrix3x2 = br.ReadSingle();
				float matrix3x3 = br.ReadSingle();
				float matrix3x4 = br.ReadSingle();
				float matrix4x1 = br.ReadSingle();
				float matrix4x2 = br.ReadSingle();
				float matrix4x3 = br.ReadSingle();
				float matrix4x4 = br.ReadSingle();

				model.Bones.Add(bone);
			}

			ModelSurface surf = new ModelSurface();

			uint vertexCount = br.ReadUInt32();
			for (uint i = 0; i < vertexCount; i++)
			{
				ModelVertex vertex = new ModelVertex();
				float positionX = br.ReadSingle();
				float positionY = br.ReadSingle();
				float positionZ = br.ReadSingle();
				vertex.Position = new PositionVector3(positionX, positionY, positionZ);
				vertex.OriginalPosition = new PositionVector3(positionX, positionY, positionZ);

				vertex.Bone0Weight = br.ReadSingle();

				byte bone0 = br.ReadByte();
				vertex.Bone0 = model.Bones[bone0];
				byte bone1 = br.ReadByte();
				vertex.Bone1 = model.Bones[bone1];

				byte unknown1 = br.ReadByte();
				byte unknown2 = br.ReadByte();

				float normalX = br.ReadSingle();
				float normalY = br.ReadSingle();
				float normalZ = br.ReadSingle();
				vertex.Normal = new PositionVector3(normalX, normalY, normalZ);
				vertex.OriginalNormal = new PositionVector3(normalX, normalY, normalZ);

				float textureU = br.ReadSingle();
				float textureV = br.ReadSingle();
				vertex.Texture = new TextureVector2(textureU, textureV);
				surf.Vertices.Add(vertex);
			}

			uint triangleCount = br.ReadUInt32();
			for (uint i = 0; i < triangleCount; i++)
			{
				ModelTriangle triangle = new ModelTriangle();
				ushort vertexIndex1 = br.ReadUInt16();
				ushort vertexIndex2 = br.ReadUInt16();
				ushort vertexIndex3 = br.ReadUInt16();
				surf.Triangles.Add(triangle);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteUInt32(0);

			bw.WriteUInt32((uint)model.Bones.Count);
			foreach (ModelBone bone in model.Bones)
			{
				// bone matrices
				// TODO: what do??

				float matrix1x1 = 0;
				bw.WriteSingle(matrix1x1);
				float matrix1x2 = 0;
				bw.WriteSingle(matrix1x2);
				float matrix1x3 = 0;
				bw.WriteSingle(matrix1x3);
				float matrix1x4 = 0;
				bw.WriteSingle(matrix1x4);
				float matrix2x1 = 0;
				bw.WriteSingle(matrix2x1);
				float matrix2x2 = 0;
				bw.WriteSingle(matrix2x2);
				float matrix2x3 = 0;
				bw.WriteSingle(matrix2x3);
				float matrix2x4 = 0;
				bw.WriteSingle(matrix2x4);
				float matrix3x1 = 0;
				bw.WriteSingle(matrix3x1);
				float matrix3x2 = 0;
				bw.WriteSingle(matrix3x2);
				float matrix3x3 = 0;
				bw.WriteSingle(matrix3x3);
				float matrix3x4 = 0;
				bw.WriteSingle(matrix3x4);
				float matrix4x1 = 0;
				bw.WriteSingle(matrix4x1);
				float matrix4x2 = 0;
				bw.WriteSingle(matrix4x2);
				float matrix4x3 = 0;
				bw.WriteSingle(matrix4x3);
				float matrix4x4 = 0;
				bw.WriteSingle(matrix4x4);
			}

			ModelSurface surf = model.Surfaces[0];

			bw.WriteUInt32((uint)surf.Vertices.Count);
			foreach (ModelVertex vertex in surf.Vertices)
			{
				bw.WriteSingle((float)vertex.Position.X);
				bw.WriteSingle((float)vertex.Position.Y);
				bw.WriteSingle((float)vertex.Position.Z);

				bw.WriteSingle(vertex.Bone0Weight);

				bw.WriteByte((byte)(model.Bones.IndexOf(vertex.Bone0)));
				bw.WriteByte((byte)(model.Bones.IndexOf(vertex.Bone1)));

				bw.WriteByte((byte)0);
				bw.WriteByte((byte)0);

				bw.WriteSingle((float)vertex.Normal.X);
				bw.WriteSingle((float)vertex.Normal.Y);
				bw.WriteSingle((float)vertex.Normal.Z);

				bw.WriteSingle((float)vertex.Texture.U);
				bw.WriteSingle((float)vertex.Texture.V);
			}

			bw.WriteUInt32((uint)surf.Triangles.Count);
			foreach (ModelTriangle triangle in surf.Triangles)
			{
				bw.WriteUInt16((ushort)surf.Vertices.IndexOf(triangle.Vertex1));
				bw.WriteUInt16((ushort)surf.Vertices.IndexOf(triangle.Vertex2));
				bw.WriteUInt16((ushort)surf.Vertices.IndexOf(triangle.Vertex3));
			}
		}
	}
}
