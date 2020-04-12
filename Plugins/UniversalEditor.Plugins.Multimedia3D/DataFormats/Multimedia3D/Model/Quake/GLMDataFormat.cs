//
//  GLMDataFormat.cs - provides a DataFormat for manipulating 3D character models in Ghoul2 GLM format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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
using System.Collections.Generic;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Quake
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D character models in Ghoul2 GLM format.
	/// </summary>
	public class GLMDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;

			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) return;

			string signature = br.ReadFixedLengthString(4);
			if (signature != "2LGM") throw new InvalidDataFormatException("File does not begin with '2LGM'");

			int version = br.ReadInt32();

			string fileName = br.ReadNullTerminatedString(64);
			string animationName = br.ReadNullTerminatedString(64);

			int animIndex = br.ReadInt32();
			int numBones = br.ReadInt32();
			int numLODs = br.ReadInt32();
			int ofsLODs = br.ReadInt32();
			int numSurfaces = br.ReadInt32();
			int ofsSurfHierarchy = br.ReadInt32();
			int ofsEnd = br.ReadInt32();

			System.Collections.Generic.List<int> arySurfaceHierarchyOffsets = new System.Collections.Generic.List<int>();
			for (int i = 0; i < numSurfaces; i++)
			{
				int surfaceHierarchyOffset = br.ReadInt32();
				arySurfaceHierarchyOffsets.Add(surfaceHierarchyOffset);
			}

			for (int i = 0; i < numSurfaces; i++)
			{
				/* surface hierarchy */
				string surfaceName = br.ReadNullTerminatedString(64);
				int flags = br.ReadInt32();
				string shaderName = br.ReadNullTerminatedString(64);
				int shaderIndex = br.ReadInt32();
				int parentIndex = br.ReadInt32();
				int numChildren = br.ReadInt32();
				System.Collections.Generic.List<int> aryChildIndices = new System.Collections.Generic.List<int>();
				for (int j = 0; j < numChildren; j++)
				{
					int childIndex = br.ReadInt32();
					aryChildIndices.Add(childIndex);
				}
			}

			for (int i = 0; i < numLODs; i++)
			{
				int lodOfsEnd = br.ReadInt32();
				for (int j = 0; j < numSurfaces; j++)
				{
					int lodSurfaceOffset = br.ReadInt32();
				}
			}
			for (int i = 0; i < numSurfaces; i++)
			{
				ModelSurface surf = new ModelSurface();

				int ident = br.ReadInt32();
				int thisSurfaceIndex = br.ReadInt32();
				int surfaceOfsHeader = br.ReadInt32();
				int surfaceNumVerts = br.ReadInt32();
				int surfaceOfsVerts = br.ReadInt32();
				int surfaceNumTriangles = br.ReadInt32();
				int surfaceOfsTriangles = br.ReadInt32();
				int surfaceNumBoneReferences = br.ReadInt32();
				int surfaceOfsBoneReferences = br.ReadInt32();
				int surfaceOfsEnd = br.ReadInt32();

				List<float[]> triangleIndices = new List<float[]>();
				for (int j = 0; j < surfaceNumTriangles; j++)
				{
					float triangleX = br.ReadSingle();
					float triangleY = br.ReadSingle();
					float triangleZ = br.ReadSingle();
					triangleIndices.Add(new float[] { triangleX, triangleY, triangleZ });
				}

				for (int j = 0; j < surfaceNumVerts; j++)
				{
					ModelVertex vtx = new ModelVertex();
					float normalX = br.ReadSingle();
					float normalY = br.ReadSingle();
					float normalZ = br.ReadSingle();
					vtx.Normal = new PositionVector3(normalX, normalY, normalZ);
					float positionX = br.ReadSingle();
					float positionY = br.ReadSingle();
					float positionZ = br.ReadSingle();
					vtx.Position = new PositionVector3(positionX, positionY, positionZ);

					uint numWeightsAndBoneIndices = br.ReadUInt32();
					byte[] boneWeightings = br.ReadBytes(3);
					surf.Vertices.Add(vtx);
				}

				for (int j = 0; j < surfaceNumVerts; j++)
				{
					float textureU = br.ReadSingle();
					float textureV = br.ReadSingle();
					surf.Vertices[j].Texture = new TextureVector2(textureU, textureV);
				}
				for (int j = 0; j < surfaceNumBoneReferences; j++)
				{
					int boneReference = br.ReadInt32();
				}

				model.Surfaces.Add(surf);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
