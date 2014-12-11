using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.GLB
{
	public class GLBDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Ultimate Stunts GLB", new byte?[][] { new byte?[] { (byte)0, (byte)'G', (byte)'L', (byte)'B' } }, new string[] { "*.glb" });
			}
			return _dfr;
		}

        private string mvarObjectName = String.Empty;
        public string ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

        private GLBObjectType mvarObjectType = GLBObjectType.GeometryObjectData0_5_0;
        public GLBObjectType ObjectType { get { return mvarObjectType; } set { mvarObjectType = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) return;

			IO.Reader br = base.Accessor.Reader;
			string magic = br.ReadFixedLengthString(4); // "\0GLB"

			GLBObjectType objectType = (GLBObjectType)br.ReadInt32();
			
            int objectNameSize = br.ReadInt32();
            mvarObjectName = br.ReadFixedLengthString(objectNameSize).TrimNull();

			int objectDataSize = br.ReadInt32();

			switch (objectType)
			{
				case GLBObjectType.GeometryObjectData0_5_0: // Geometry object data 0.5
				{
					// This object type is used in Ultimate Stunts prior to version 0.7.0. It is
					// still supported, but its use is discouraged. Please use the Geometry object data
					// 0.7 instead.

					int materialDataSize = br.ReadInt32();
					int vertexCount = br.ReadInt32();
					int indexCount = br.ReadInt32();

					long offset = br.Accessor.Position;
					#region Material data
					while (br.Accessor.Position <= offset + materialDataSize)
					{
						#region material modulation color
						{
							byte r = br.ReadByte();
							byte g = br.ReadByte();
							byte b = br.ReadByte();
							byte a = br.ReadByte();
							System.Drawing.Color color = System.Drawing.Color.FromArgb(a, r, g, b);
						}
						#endregion
						#region material replacement color
						{
							byte r = br.ReadByte();
							byte g = br.ReadByte();
							byte b = br.ReadByte();
							byte a = br.ReadByte();
							System.Drawing.Color color = System.Drawing.Color.FromArgb(a, r, g, b);
						}
						#endregion
						
						byte lodFlags = br.ReadByte();
						byte reflectance = br.ReadByte();
						byte emissivity = br.ReadByte();
						byte staticFrictionCoefficient = br.ReadByte(); // 1 byte range 0-16
						byte dynamicFrictionCoefficient = br.ReadByte();

						byte unused1 = br.ReadByte();
						byte unused2 = br.ReadByte();
						byte unused3 = br.ReadByte();

						// texture name (null-terminated, aligned to 4 byte size)
						string textureName = br.ReadNullTerminatedString();
						br.Align(4);
					}
					#endregion
					#region Vertex data
					#endregion
					#region Index data

					#endregion
					break;
				}
                case GLBObjectType.GeometryObjectData0_5_1: // Geometry object data 0.5
				{
					// This object type is used in Ultimate Stunts prior to version 0.7.0. It is
					// still supported, but its use is discouraged. Please use the Geometry object data
					// 0.7 instead. 
					break;
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("\0GLB");
            bw.WriteInt32((int)mvarObjectType);

            bw.WriteInt32((int)mvarObjectName.Length);
            bw.WriteFixedLengthString(mvarObjectName);
		}
	}
}
