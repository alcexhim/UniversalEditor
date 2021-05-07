//
//  MS3DDataFormat.cs - provides a DataFormat for reading and writing 3D models in Milkshape 3D MS3D binary format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com> (DataFormat)
//       Mete Ciragan (MilkShape 3D, original specification)
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Milkshape3D
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for reading and writing 3D models in Milkshape 3D MS3D binary format.
	/// </summary>
	public class MS3DDataFormat : DataFormat
	{
		//
		// max values
		//
		public const int MAX_VERTICES = 65534;
		public const int MAX_TRIANGLES = 65534;
		public const int MAX_GROUPS = 255;
		public const int MAX_MATERIALS = 128;
		public const int MAX_JOINTS = 128;



		//
		// flags
		//
		public const int DIRTY = 8;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			string signature = reader.ReadFixedLengthString(10); // "MS3D000000"
			if (signature != "MS3D000000")
				throw new InvalidDataFormatException("File does not begin with 'MS3D000000");

			int version = reader.ReadInt32();
			if (version != 4)
			{
				// FIXME
			}


			ushort nNumVertices = reader.ReadUInt16();
			for (ushort i = 0; i < nNumVertices; i++)
			{
				MS3DObjectFlags flags = (MS3DObjectFlags)reader.ReadByte(); // SELECTED | SELECTED2 | HIDDEN
				float[] vertices = reader.ReadSingleArray(3);
				byte boneId = reader.ReadByte(); // -1(255?) = no bone
				byte referenceCount = reader.ReadByte();
			}

			ushort nNumTriangles = reader.ReadUInt16();
			for (ushort i = 0; i < nNumTriangles; i++)
			{
				MS3DObjectFlags flags = (MS3DObjectFlags)reader.ReadByte(); // SELECTED | SELECTED2 | HIDDEN
				ushort[] vertexIndices = reader.ReadUInt16Array(3);
				for (int j = 0; j < 3; j++)
				{
					float[] vertexNormals = reader.ReadSingleArray(3);
				}
				float[] s = reader.ReadSingleArray(3);
				float[] t = reader.ReadSingleArray(3);
				byte smoothingGroup = reader.ReadByte(); // 1 - 32
				byte groupIndex = reader.ReadByte();
			}

			ushort nNumGroups = reader.ReadUInt16();
			for (ushort i = 0; i < nNumGroups; i++)
			{
				MS3DObjectFlags flags = (MS3DObjectFlags)reader.ReadByte(); // SELECTED | HIDDEN
				string name = reader.ReadFixedLengthString(32).TrimNull();
				ushort numtriangles = reader.ReadUInt16();
				ushort[] triangleIndices = reader.ReadUInt16Array(numtriangles); // the groups group the triangles
				byte materialIndex = reader.ReadByte(); // -1(255?) = no material
			}

			ushort nNumMaterials = reader.ReadUInt16();
			for (ushort i = 0; i < nNumMaterials; i++)
			{
				ModelMaterial mat = new ModelMaterial();
				mat.Name = reader.ReadFixedLengthString(32).TrimNull();
				float[] ambient = reader.ReadSingleArray(4); // RGBA?
				mat.AmbientColor = MBS.Framework.Drawing.Color.FromRGBASingle(ambient[0], ambient[1], ambient[2], ambient[3]);
				float[] diffuse = reader.ReadSingleArray(4); // RGBA?
				mat.DiffuseColor = MBS.Framework.Drawing.Color.FromRGBASingle(diffuse[0], diffuse[1], diffuse[2], diffuse[3]);
				float[] specular = reader.ReadSingleArray(4); // RGBA?
				mat.SpecularColor = MBS.Framework.Drawing.Color.FromRGBASingle(specular[0], specular[1], specular[2], specular[3]);
				float[] emissive = reader.ReadSingleArray(4); // RGBA?
				mat.EmissiveColor = MBS.Framework.Drawing.Color.FromRGBASingle(emissive[0], emissive[1], emissive[2], emissive[3]);
				float shininess = reader.ReadSingle(); // 0.0f - 128.0f
				mat.Shininess = shininess;
				float transparency = reader.ReadSingle(); // 0.0f - 1.0f
														  // mat.Transparency = transparency;
				byte mode = reader.ReadByte(); // 0, 1, 2 is unused now
				string textureFileName = reader.ReadFixedLengthString(128).TrimNull();
				string alphaMapFileName = reader.ReadFixedLengthString(128).TrimNull();
				mat.Textures.Add(textureFileName, alphaMapFileName, ModelTextureFlags.Texture);
			}

			float fAnimationFPS = reader.ReadSingle();
			float fCurrentTime = reader.ReadSingle();
			int iTotalFrames = reader.ReadInt32();

			ushort nNumJoints = reader.ReadUInt16();
			for (ushort i = 0; i < nNumJoints; i++)
			{
				MS3DObjectFlags flags = (MS3DObjectFlags)reader.ReadByte(); // SELECTED | DIRTY
				string name = reader.ReadFixedLengthString(32).TrimNull();
				string parentName = reader.ReadFixedLengthString(32).TrimNull();

				float[] rotation = reader.ReadSingleArray(3); // local reference matrix
				float[] position = reader.ReadSingleArray(3);

				ushort numKeyFramesRot = reader.ReadUInt16();
				ushort numKeyFramesTrans = reader.ReadUInt16();
				for (ushort j = 0; j < numKeyFramesRot; j++)
				{
					// ms3d_keyframe_rot_t
					float time = reader.ReadSingle(); // time in seconds
					float[] keyframe_rotation = reader.ReadSingleArray(3); // x, y, z angles
				}
				for (ushort j = 0; j < numKeyFramesTrans; j++)
				{
					// ms3d_keyframe_pos_t
					float time = reader.ReadSingle(); // time in seconds
					float[] keyframe_position = reader.ReadSingleArray(3); // x, y, z angles
				}
			}

			//
			// Then comes the subVersion of the comments part, which is not available in older files
			//
			int subVersion = reader.ReadInt32(); // subVersion is = 1, 4 bytes
												 // FIXME: figure out if we need to test this part if subVersion != 1

			// Then comes the numer of group comments
			uint nNumGroupComments = reader.ReadUInt32(); // 4 bytes

			//
			// Then come nNumGroupComments times group comments, which are dynamic, because the comment can be any length
			//
			for (uint i = 0; i < nNumGroupComments; i++)
			{
				int index = reader.ReadInt32();                                          // index of group, material or joint
				int commentLength = reader.ReadInt32();                                  // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				string comment = reader.ReadFixedLengthString(commentLength);                        // comment
			}

			// Then comes the number of material comments
			int nNumMaterialComments = reader.ReadInt32(); // 4 bytes
														   //
														   // Then come nNumMaterialComments times material comments, which are dynamic, because the comment can be any length
														   //
			for (uint i = 0; i < nNumMaterialComments; i++)
			{
				int index = reader.ReadInt32();                                          // index of group, material or joint
				int commentLength = reader.ReadInt32();                                  // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				string comment = reader.ReadFixedLengthString(commentLength);                        // comment
			}

			// Then comes the number of joint comments
			int nNumJointComments = reader.ReadInt32(); // 4 bytes
														//
														// Then come nNumJointComments times joint comments, which are dynamic, because the comment can be any length
														//
			for (uint i = 0; i < nNumJointComments; i++)
			{
				int index = reader.ReadInt32();                                          // index of group, material or joint
				int commentLength = reader.ReadInt32();                                  // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				string comment = reader.ReadFixedLengthString(commentLength);                        // comment
			}

			// Then comes the number of model comments, which is always 0 or 1
			int nHasModelComment = reader.ReadInt32(); // 4 bytes
													   //
													   // Then come nHasModelComment times model comments, which are dynamic, because the comment can be any length
													   //
			for (uint i = 0; i < nHasModelComment; i++)
			{
				int index = reader.ReadInt32();                                          // index of group, material or joint
				int commentLength = reader.ReadInt32();                                  // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				string comment = reader.ReadFixedLengthString(commentLength);                        // comment
			}

			// Then comes the subversion of the vertex extra information like bone weights, extra etc.
			int subVersionVertexExtra = reader.ReadInt32();     // subVersion is = 2, 4 bytes

			// Then comes nNumVertices times ms3d_vertex_ex_t structs (sizeof(ms3d_vertex_ex_t) == 10)
			for (int i = 0; i < nNumVertices; i++)
			{
				if (subVersionVertexExtra == 1)
				{
					// ms3d_vertex_ex_t for subVersion == 1
					byte[] boneIds = reader.ReadBytes(3);                                    // index of joint or -1, if -1, then that weight is ignored, since subVersion 1
					byte[] weights = reader.ReadBytes(3);                                    // vertex weight ranging from 0 - 255, last weight is computed by 1.0 - sum(all weights), since subVersion 1
																							 // weight[0] is the weight for boneId in ms3d_vertex_t
																							 // weight[1] is the weight for boneIds[0]
																							 // weight[2] is the weight for boneIds[1]
																							 // 1.0f - weight[0] - weight[1] - weight[2] is the weight for boneIds[2]
				}
				else if (subVersionVertexExtra == 2)
				{
					// ms3d_vertex_ex_t for subVersion == 2
					byte[] boneIds = reader.ReadBytes(3);                                    // index of joint or -1, if -1, then that weight is ignored, since subVersion 1
					byte[] weights = reader.ReadBytes(3);                                    // vertex weight ranging from 0 - 100, last weight is computed by 1.0 - sum(all weights), since subVersion 1
																							 // weight[0] is the weight for boneId in ms3d_vertex_t
																							 // weight[1] is the weight for boneIds[0]
																							 // weight[2] is the weight for boneIds[1]
																							 // 1.0f - weight[0] - weight[1] - weight[2] is the weight for boneIds[2]
					uint extra = reader.ReadUInt32();                                   // vertex extra, which can be used as color or anything else, since subVersion 2
				}
			}

			// Then comes the subversion of the joint extra information like color etc.
			int subVersionJointExtra = reader.ReadInt32();     // subVersion is = 2, 4 bytes
			if (subVersionJointExtra == 1)
			{
				// ms3d_joint_ex_t for subVersion == 1
				for (ushort i = 0; i < nNumJoints; i++)
				{
					float[] color = reader.ReadSingleArray(3);   // joint color, since subVersion == 1
				}
			}

			// Then comes nNumJoints times ms3d_joint_ex_t structs (sizeof(ms3d_joint_ex_t) == 12)

			// Then comes the subversion of the model extra information
			int subVersionModelExtra = reader.ReadInt32();     // subVersion is = 1, 4 bytes
			if (subVersionModelExtra == 1)
			{
				// ms3d_model_ex_t for subVersion == 1
				float jointSize = reader.ReadSingle();    // joint size, since subVersion == 1
				int transparencyMode = reader.ReadInt32(); // 0 = simple, 1 = depth buffered with alpha ref, 2 = depth sorted triangles, since subVersion == 1
				float alphaRef = reader.ReadSingle(); // alpha reference value for transparencyMode = 1, since subVersion == 1
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;

			writer.WriteFixedLengthString("MS3D000000");
			writer.WriteInt32(4);

			ushort nNumVertices = (ushort)model.Surfaces[0].Vertices.Count;
			writer.WriteUInt16(nNumVertices);

			for (ushort i = 0; i < nNumVertices; i++)
			{
				MS3DObjectFlags flags = MS3DObjectFlags.None;
				writer.WriteByte((byte)flags);

				writer.WriteSingle((float)model.Surfaces[0].Vertices[i].Position.X);
				writer.WriteSingle((float)model.Surfaces[0].Vertices[i].Position.Y);
				writer.WriteSingle((float)model.Surfaces[0].Vertices[i].Position.Z);

				if (model.Surfaces[0].Vertices[i].Bone0 == null)
				{
					writer.WriteByte(255);// -1(255?) = no bone
				}
				else
				{
					writer.WriteByte((byte)model.Bones.IndexOf(model.Surfaces[0].Vertices[i].Bone0));
				}

				byte referenceCount = 0;
				writer.WriteByte(referenceCount);
			}

			ushort nNumTriangles = (ushort)model.Surfaces[0].Triangles.Count;
			writer.WriteUInt16(nNumTriangles);

			for (ushort i = 0; i < nNumTriangles; i++)
			{
				MS3DObjectFlags flags = MS3DObjectFlags.None;
				writer.WriteByte((byte)flags); // SELECTED | SELECTED2 | HIDDEN

				writer.WriteUInt16((ushort)model.Surfaces[0].Vertices.IndexOf(model.Surfaces[0].Triangles[i].Vertex1));
				writer.WriteUInt16((ushort)model.Surfaces[0].Vertices.IndexOf(model.Surfaces[0].Triangles[i].Vertex2));
				writer.WriteUInt16((ushort)model.Surfaces[0].Vertices.IndexOf(model.Surfaces[0].Triangles[i].Vertex3));

				// vertexNormals
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex1.Normal.X);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex1.Normal.Y);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex1.Normal.Z);

				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex2.Normal.X);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex2.Normal.Y);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex2.Normal.Z);

				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex3.Normal.X);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex3.Normal.Y);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex3.Normal.Z);

				// texture s-coords
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex1.Texture.U);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex2.Texture.U);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex3.Texture.U);

				// texture t-coords
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex1.Texture.V);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex2.Texture.V);
				writer.WriteSingle((float)model.Surfaces[0].Triangles[i].Vertex3.Texture.V);

				byte smoothingGroup = 1;  // 1 - 32
				writer.WriteByte(smoothingGroup);

				byte groupIndex = 0;
				writer.WriteByte(groupIndex);
			}

			ushort nNumGroups = 0;
			writer.WriteUInt16(nNumGroups);

			for (ushort i = 0; i < nNumGroups; i++)
			{
				MS3DObjectFlags flags = MS3DObjectFlags.None; // SELECTED | HIDDEN
				writer.WriteByte((byte)flags);

				string name = String.Empty;
				writer.WriteFixedLengthString(name, 32);

				ushort numtriangles = 0;
				writer.WriteUInt16(numtriangles);

				ushort[] triangleIndices = new ushort[0]; // the groups group the triangles
				writer.WriteUInt16Array(triangleIndices);

				byte materialIndex = 255; // -1(255?) = no material
				writer.WriteByte(materialIndex);
			}

			ushort nNumMaterials = (ushort)model.Materials.Count;
			for (ushort i = 0; i < nNumMaterials; i++)
			{
				writer.WriteFixedLengthString(model.Materials[i].Name, 32);

				writer.WriteSingle((float)model.Materials[i].AmbientColor.R);
				writer.WriteSingle((float)model.Materials[i].AmbientColor.G);
				writer.WriteSingle((float)model.Materials[i].AmbientColor.B);
				writer.WriteSingle((float)model.Materials[i].AmbientColor.A);

				writer.WriteSingle((float)model.Materials[i].DiffuseColor.R);
				writer.WriteSingle((float)model.Materials[i].DiffuseColor.G);
				writer.WriteSingle((float)model.Materials[i].DiffuseColor.B);
				writer.WriteSingle((float)model.Materials[i].DiffuseColor.A);

				writer.WriteSingle((float)model.Materials[i].SpecularColor.R);
				writer.WriteSingle((float)model.Materials[i].SpecularColor.G);
				writer.WriteSingle((float)model.Materials[i].SpecularColor.B);
				writer.WriteSingle((float)model.Materials[i].SpecularColor.A);

				writer.WriteSingle((float)model.Materials[i].EmissiveColor.R);
				writer.WriteSingle((float)model.Materials[i].EmissiveColor.G);
				writer.WriteSingle((float)model.Materials[i].EmissiveColor.B);
				writer.WriteSingle((float)model.Materials[i].EmissiveColor.A);

				writer.WriteSingle(model.Materials[i].Shininess);
				writer.WriteSingle(0.0f); // writer.WriteSingle(model.Materials[i].Transparency);

				byte mode = 0;  // reader.ReadByte(); // 0, 1, 2 is unused now
				writer.WriteByte(mode);

				string textureFileName = String.Empty;
				if (model.Materials[i].Textures.Count > 0)
					textureFileName = model.Materials[i].Textures[0].TextureFileName;
				writer.WriteFixedLengthString(textureFileName, 128);

				string alphaMapFileName = String.Empty;
				if (model.Materials[i].Textures.Count > 0)
					alphaMapFileName = model.Materials[i].Textures[0].MapFileName;
				writer.WriteFixedLengthString(alphaMapFileName, 128);
			}

			float fAnimationFPS = 24.0f;
			writer.WriteSingle(fAnimationFPS);
			float fCurrentTime = 0.0f;
			writer.WriteSingle(fCurrentTime);
			int iTotalFrames = 0;
			writer.WriteInt32(iTotalFrames);

			ushort nNumJoints = (ushort)model.Joints.Count;
			for (ushort i = 0; i < nNumJoints; i++)
			{
				MS3DObjectFlags flags = MS3DObjectFlags.None;
				writer.WriteByte((byte)flags); // SELECTED | DIRTY
				writer.WriteFixedLengthString(model.Joints[i].Name, 32);
				writer.WriteFixedLengthString(String.Empty, 32); // parentName

				writer.WriteSingleArray(model.Joints[i].Rotation.ToFloatArray()); // local reference matrix
				writer.WriteSingleArray(model.Joints[i].Position.ToFloatArray()); // local reference matrix

				ushort numKeyFramesRot = 0;
				writer.WriteUInt16(numKeyFramesRot);
				ushort numKeyFramesTrans = 0;
				writer.WriteUInt16(numKeyFramesTrans);
				for (ushort j = 0; j < numKeyFramesRot; j++)
				{
					// ms3d_keyframe_rot_t
					float time = 0.0f; // time in seconds
					writer.WriteSingle(time);
					float[] keyframe_rotation = { 0.0f, 0.0f, 0.0f }; // x, y, z angles
					writer.WriteSingleArray(keyframe_rotation);
				}
				for (ushort j = 0; j < numKeyFramesTrans; j++)
				{
					// ms3d_keyframe_pos_t
					float time = 0.0f; // time in seconds
					writer.WriteSingle(time);
					float[] keyframe_position = { 0.0f, 0.0f, 0.0f }; // x, y, z angles
					writer.WriteSingleArray(keyframe_position);
				}
			}

			//
			// Then comes the subVersion of the comments part, which is not available in older files
			//
			int subVersion = 1; // subVersion is = 1, 4 bytes
			writer.WriteInt32(subVersion);

			// Then comes the numer of group comments
			uint nNumGroupComments = 0; // 4 bytes
			writer.WriteUInt32(nNumGroupComments);

			//
			// Then come nNumGroupComments times group comments, which are dynamic, because the comment can be any length
			//
			for (uint i = 0; i < nNumGroupComments; i++)
			{
				int index = 0;
				writer.WriteInt32(index);                                          // index of group, material or joint
				string comment = String.Empty;
				writer.WriteInt32(comment.Length); // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				writer.WriteFixedLengthString(comment); // comment
			}

			// Then comes the number of material comments
			uint nNumMaterialComments = 0; // 4 bytes
										   //
										   // Then come nNumMaterialComments times material comments, which are dynamic, because the comment can be any length
										   //
			writer.WriteUInt32(nNumMaterialComments);
			for (uint i = 0; i < nNumMaterialComments; i++)
			{
				int index = 0;                                          // index of group, material or joint
				writer.WriteInt32(index);
				string comment = String.Empty;
				writer.WriteInt32(comment.Length); // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				writer.WriteFixedLengthString(comment);                        // comment
			}

			// Then comes the number of joint comments
			uint nNumJointComments = 0; // 4 bytes
										//
										// Then come nNumJointComments times joint comments, which are dynamic, because the comment can be any length
										//
			for (uint i = 0; i < nNumJointComments; i++)
			{
				int index = 0;                                          // index of group, material or joint
				writer.WriteInt32(index);
				string comment = String.Empty;
				writer.WriteInt32(comment.Length);                                  // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				writer.WriteFixedLengthString(comment);                        // comment
			}

			// Then comes the number of model comments, which is always 0 or 1
			uint nHasModelComment = 0; // 4 bytes
									   //
									   // Then come nHasModelComment times model comments, which are dynamic, because the comment can be any length
									   //
			for (uint i = 0; i < nHasModelComment; i++)
			{
				int index = 0;                                          // index of group, material or joint
				writer.WriteInt32(index);
				string comment = String.Empty;
				writer.WriteInt32(comment.Length); // length of comment (terminating '\0' is not saved), "MC" has comment length of 2 (not 3)
				writer.WriteFixedLengthString(comment); // comment
			}

			// Then comes the subversion of the vertex extra information like bone weights, extra etc.
			int subVersionVertexExtra = 2;     // subVersion is = 2, 4 bytes
			writer.WriteInt32(subVersionVertexExtra);

			// Then comes nNumVertices times ms3d_vertex_ex_t structs (sizeof(ms3d_vertex_ex_t) == 10)
			for (int i = 0; i < nNumVertices; i++)
			{
				if (subVersionVertexExtra == 1)
				{
					// ms3d_vertex_ex_t for subVersion == 1
					byte[] boneIds = new byte[] { 255, 255, 255 }; // 3 of 'em
					writer.WriteBytes(boneIds);                                    // index of joint or -1, if -1, then that weight is ignored, since subVersion 1
					byte[] weights = new byte[] { 255, 255, 255 };                                    // vertex weight ranging from 0 - 255, last weight is computed by 1.0 - sum(all weights), since subVersion 1
																									  // weight[0] is the weight for boneId in ms3d_vertex_t
																									  // weight[1] is the weight for boneIds[0]
																									  // weight[2] is the weight for boneIds[1]
																									  // 1.0f - weight[0] - weight[1] - weight[2] is the weight for boneIds[2]
					writer.WriteBytes(weights);
				}
				else if (subVersionVertexExtra == 2)
				{
					// ms3d_vertex_ex_t for subVersion == 2
					byte[] boneIds = { 255, 255, 255 };                                    // index of joint or -1, if -1, then that weight is ignored, since subVersion 1
					writer.WriteBytes(boneIds);
					byte[] weights = { 255, 255, 255 };                                    // vertex weight ranging from 0 - 100, last weight is computed by 1.0 - sum(all weights), since subVersion 1
																						   // weight[0] is the weight for boneId in ms3d_vertex_t
																						   // weight[1] is the weight for boneIds[0]
																						   // weight[2] is the weight for boneIds[1]
																						   // 1.0f - weight[0] - weight[1] - weight[2] is the weight for boneIds[2]
					writer.WriteBytes(weights);
					uint extra = 0;                                   // vertex extra, which can be used as color or anything else, since subVersion 2
					writer.WriteUInt32(extra);
				}
			}

			// Then comes the subversion of the joint extra information like color etc.
			int subVersionJointExtra = 1;     // subVersion is = 2, 4 bytes
			writer.WriteInt32(subVersionJointExtra);
			if (subVersionJointExtra == 1)
			{
				// ms3d_joint_ex_t for subVersion == 1
				for (ushort i = 0; i < nNumJoints; i++)
				{
					float[] color = { 0.0f, 0.0f, 0.0f };   // joint color, since subVersion == 1
					writer.WriteSingleArray(color);
				}
			}

			// Then comes nNumJoints times ms3d_joint_ex_t structs (sizeof(ms3d_joint_ex_t) == 12)

			// Then comes the subversion of the model extra information
			int subVersionModelExtra = 1;     // subVersion is = 1, 4 bytes
			writer.WriteInt32(subVersionModelExtra);
			if (subVersionModelExtra == 1)
			{
				// ms3d_model_ex_t for subVersion == 1
				float jointSize = 0.0f;    // joint size, since subVersion == 1
				writer.WriteSingle(jointSize);
				int transparencyMode = 0; // 0 = simple, 1 = depth buffered with alpha ref, 2 = depth sorted triangles, since subVersion == 1
				writer.WriteInt32(transparencyMode);
				float alphaRef = 0.0f; // alpha reference value for transparencyMode = 1, since subVersion == 1
				writer.WriteSingle(alphaRef);
			}
		}
	}
}
