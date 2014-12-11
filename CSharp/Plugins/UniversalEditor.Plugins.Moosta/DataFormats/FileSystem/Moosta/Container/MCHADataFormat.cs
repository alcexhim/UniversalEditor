using System;
using System.Collections.Generic;
using System.Linq;
using UniversalEditor.Accessors;
using UniversalEditor.Compression.Modules.Gzip;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.FileSystem.Moosta.Container
{
	public class MCHADataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Moosta character animation", new byte?[][] { new byte?[] { (byte)'M', (byte)'c', (byte)'h', (byte)'a' } }, new string[] { "*.mcha" });
			}
			return _dfr;
		}

		private List<string> mvarTitles = new List<string>();
		public List<string> Titles { get { return mvarTitles; } }

		private List<string> mvarCopyrights = new List<string>();
		public List<string> Copyrights { get { return mvarCopyrights; } }

		private PictureObjectModel mvarThumbnail = null;
		public PictureObjectModel Thumbnail { get { return mvarThumbnail; } set { mvarThumbnail = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);

			// The definition for this data format is in OMPTab!OMP.OmpDanceModel

			IO.Reader br = base.Accessor.Reader;
			base.Accessor.DefaultEncoding = Encoding.UTF16LittleEndian;

			if (br.Remaining < 14) throw new InvalidDataFormatException("File must be at least 14 bytes");

			int size = 0;
			byte[] magic = br.ReadBytes(14);
			if (!UniversalEditor.Plugins.Moosta.Common.CheckMoostaFileType(magic, Plugins.Moosta.MoostaFileType.Dmcha, out size))
			{
				throw new InvalidDataFormatException("DRM-protected Mcha is not supported by this DataFormat");
			}
			if (!UniversalEditor.Plugins.Moosta.Common.CheckMoostaFileType(magic, Plugins.Moosta.MoostaFileType.Mcha | Plugins.Moosta.MoostaFileType.Dmcha, out size))
			{
				throw new InvalidDataFormatException("File does not begin with Mcha or is not a DRM-protected Mcha");
			}
			br.Accessor.Position -= (14 - size);

			float formatVersion = br.ReadSingle();

			int meshCount = br.ReadInt32();
			int vertexCount = br.ReadInt32();				// 11578
			int faceCount = br.ReadInt32();				// 15852
			int boneCount = br.ReadInt32();				// 53
			int materialCount = br.ReadInt32();				// 9
			int textureCount = br.ReadInt32();				// 5

			if (formatVersion >= 1.3f)
			{
				int titleCount = br.ReadInt32();
				for (int i = 0; i < titleCount; i++)
				{
					int languageID = br.ReadInt32();
					string title = br.ReadInt16String();
					mvarTitles.Add(title);
				}
			}
			else
			{
				string title = br.ReadInt16String();
			}

			int copyrightCount = br.ReadInt32();
			for (int i = 0; i < copyrightCount; i++)
			{
				string copyright = br.ReadInt16String();
				mvarCopyrights.Add(copyright);
			}

			int imageThumbnailWidth = br.ReadInt32();
			int imageThumbnailHeight = br.ReadInt32();

			// HEY! This works.. and I just guessed at the format! ;-)
			UniversalEditor.ObjectModels.Multimedia.Picture.PictureObjectModel pic = new ObjectModels.Multimedia.Picture.PictureObjectModel();

			pic.Width = imageThumbnailWidth;
			pic.Height = imageThumbnailHeight;
			for (int y = 0; y < imageThumbnailHeight; y++)
			{
				for (int x = 0; x < imageThumbnailWidth; x++)
				{
					byte a = br.ReadByte();
					byte r = br.ReadByte();
					byte g = br.ReadByte();
					byte b = br.ReadByte();
					Color color = Color.FromRGBA(r, g, b, a);
					pic.SetPixel(color, x, y);
				}
			}
			mvarThumbnail = pic;

			byte compressionMode = br.ReadByte();
			if (formatVersion >= 1.3f)
			{
				byte b = br.ReadByte();
				byte[] u = br.ReadBytes(b);

				IO.Reader br1 = new IO.Reader(new MemoryAccessor(u));
				uint permissionFlags = br1.ReadUInt32();
			}

			if (compressionMode == 1)
			{
				int dataLength = br.ReadInt32();
				int unknown0 = br.ReadInt32();
				dataLength -= 4;

				byte[] compressedData = br.ReadBytes(dataLength);
				byte[] decompressedData = (new GzipCompressionModule()).Decompress(compressedData);
				br = new IO.Reader(new MemoryAccessor(decompressedData));
			}


			// br.Accessor.Position = 0;
			// model.Surfaces.Clear();
			#region Load Surfaces
			{
				for (int i = 0; i < meshCount; i++)
				{
					ModelSurface mesh = new ModelSurface();

					int unknown1 = br.ReadInt32();
					int unknown2 = br.ReadInt32();
					#region Load Vertices
					uint vertexCountAgain = br.ReadUInt32();
					for (uint j = 0; j < vertexCountAgain; j++)
					{
						ModelVertex vertex = new ModelVertex();

						float positionX = br.ReadSingle();
						float positionY = br.ReadSingle();
						float positionZ = br.ReadSingle();
						vertex.Position = new PositionVector3(positionX, positionY, positionZ);
						vertex.OriginalPosition = new PositionVector3(positionX, positionY, positionZ);

						float normalX = br.ReadSingle();
						float normalY = br.ReadSingle();
						float normalZ = br.ReadSingle();
						vertex.Normal = new PositionVector3(positionX, positionY, positionZ);
						vertex.OriginalNormal = new PositionVector3(positionX, positionY, positionZ);

						float textureU = br.ReadSingle();
						float textureV = br.ReadSingle();
						vertex.Texture = new TextureVector2(textureU, textureV);

						ushort unknown4 = br.ReadUInt16();
						ushort unknown5 = br.ReadUInt16();
						ushort unknown6 = br.ReadUInt16();
						ushort unknown7 = br.ReadUInt16();

						float bone0Weight = br.ReadSingle();
						vertex.Bone0Weight = bone0Weight;

						float unknown9 = br.ReadSingle();
						float unknown10 = br.ReadSingle();
						float unknown11 = br.ReadSingle();
						float unknown12 = 1f;
						mesh.Vertices.Add(vertex);
					}
					#endregion
					#region Load Faces/Triangles
					uint faceCountAgain = br.ReadUInt32();
					for (uint j = 0; j < faceCountAgain; j++)
					{
						int unknown13 = br.ReadInt32();
						uint vertex1Index = br.ReadUInt32();
						uint vertex2Index = br.ReadUInt32();
						uint vertex3Index = br.ReadUInt32();

						if (vertex1Index >= vertexCountAgain || vertex2Index >= vertexCountAgain || vertex3Index >= vertexCountAgain)
						{
							throw new IndexOutOfRangeException("vertex index out of range");
						}
						mesh.Triangles.Add(mesh.Vertices[(int)vertex1Index], mesh.Vertices[(int)vertex2Index], mesh.Vertices[(int)vertex3Index]);
					}
					#endregion
					#region Load Deformers/Skins
					uint deformerCount = br.ReadUInt32();
					for (uint j = 0; j < deformerCount; j++)
					{
						ModelSkin skin = new ModelSkin();
						skin.Name = br.ReadInt16String();
						uint deformerVertexCount = br.ReadUInt32();
						for (uint k = 0; k < deformerVertexCount; k++)
						{
							ModelSkinVertex vtx = new ModelSkinVertex();
							uint dfvtxTargetIndex = br.ReadUInt32();
							if (dfvtxTargetIndex < mesh.Vertices.Count)
							{
								vtx.TargetVertex = mesh.Vertices[(int)dfvtxTargetIndex];
							}
							float dfvtxMaximumPositionX = br.ReadSingle();
							float dfvtxMaximumPositionY = br.ReadSingle();
							float dfvtxMaximumPositionZ = br.ReadSingle();
							vtx.MaximumPosition = new PositionVector3(dfvtxMaximumPositionX, dfvtxMaximumPositionY, dfvtxMaximumPositionZ);
							skin.Vertices.Add(vtx);
						}
						model.Skins.Add(skin);
					}
					#endregion
					model.Surfaces.Add(mesh);
				}
			}
			#endregion
			#region Load Materials
			{
				for (int i = 0; i < materialCount; i++)
				{
					ModelMaterial mat = new ModelMaterial();
					int unknown1 = br.ReadInt32();
					int unknown2 = br.ReadInt32();
					mat.Name = br.ReadInt16String();
					mat.AmbientColor = br.ReadColorARGBSingle();
					mat.DiffuseColor = br.ReadColorARGBSingle();
					mat.SpecularColor = br.ReadColorARGBSingle();
					mat.Shininess = br.ReadSingle();

					string textureFileName = br.ReadInt16String();
					string toonFileName = br.ReadInt16String();
					string unknown5 = br.ReadInt16String();
					string unknown6 = br.ReadInt16String();
					string unknown7 = br.ReadInt16String();
					// mat.ToonFileName = toonFileName;
					mat.Textures.Add(textureFileName, null, ModelTextureFlags.Texture);
					model.Materials.Add(mat);
				}
			}
			#endregion
			#region Load Bones
			{
				uint unknown1 = br.ReadUInt32();
				uint unknown2 = br.ReadUInt32();
				uint boneCountAgain = br.ReadUInt32();
				for (uint i = 0; i < boneCount; i++)
				{
					ModelBone bone = new ModelBone();
					bone.Name = br.ReadInt16String();

					#region Bone Matrix #1
					{
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
					}
					#endregion
					#region Bone Matrix #2
					{
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
					}
					#endregion

					int unknown3 = br.ReadInt32();
					if (unknown3 >= 0)
					{

					}
					else
					{

					}

					model.Bones.Add(bone);
				}
			}
			#endregion
			#region Load Textures
			{
				for (int i = 0; i < textureCount; i++)
				{
					ModelTexture texture = new ModelTexture();

					int textureType = br.ReadInt32();
					int unknown1 = br.ReadInt32();
					texture.TextureFileName = br.ReadInt16String();

					if (textureType == 10)
					{
						// formatted bitmap data
						uint dataSize = br.ReadUInt32();
						byte[] data = br.ReadBytes(dataSize);

					}
					else if (textureType == 3)
					{
						// raw bitmap data, ARGB format
						int width = br.ReadInt32();
						int height = br.ReadInt32();

						PictureObjectModel picture = new PictureObjectModel(width, height);
						for (int y = 0; y < height; y++)
						{
							for (int x = 0; x < width; x++)
							{
								byte a = br.ReadByte();
								byte r = br.ReadByte();
								byte g = br.ReadByte();
								byte b = br.ReadByte();
								Color color = Color.FromRGBA(r, g, b, a);
								picture.SetPixel(color, x, y);
							}
						}
						texture.TexturePicture = picture;
					}
					
					model.Textures.Add(texture);
				}
			}
			#endregion

			Console.WriteLine("TODO: complete implementation of MCHA format");
			return;

			// throw new InvalidDataFormatException("Format version " + formatVersion.ToString() + " not supported");
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
