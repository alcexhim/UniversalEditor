//
//  GLBDataFormat.cs - provides a DataFormat for manipulating 3D models in Ultimate Stunts GLB format
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

namespace UniversalEditor.DataFormats.Multimedia3D.Model.GLB
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D models in Ultimate Stunts GLB format.
	/// </summary>
	/// <remarks>
	/// This is NOT the same format as the OpenGL Transmission Format (glTF) which has the same file extension.
	/// </remarks>
	public class GLBDataFormat : DataFormat
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

		/// <summary>
		/// Gets or sets the name of the object represented by this model.
		/// </summary>
		/// <value>The name of the object represented by this model.</value>
		public string ObjectName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the format version of this model file.
		/// </summary>
		/// <value>The format version of this model file.</value>
		public GLBObjectType ObjectType { get; set; } = GLBObjectType.GeometryObjectData0_5_0;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string magic = br.ReadFixedLengthString(4); // "\0GLB"
			if (magic != "\0GLB") throw new InvalidDataFormatException("File does not begin with 0, 'GLB'");

			GLBObjectType objectType = (GLBObjectType)br.ReadInt32();

			int objectNameSize = br.ReadInt32();
			ObjectName = br.ReadFixedLengthString(objectNameSize).TrimNull();

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
							Color color = Color.FromRGBAByte(r, g, b, a);
						}
						#endregion
						#region material replacement color
						{
							byte r = br.ReadByte();
							byte g = br.ReadByte();
							byte b = br.ReadByte();
							byte a = br.ReadByte();
							Color color = Color.FromRGBAByte(r, g, b, a);
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
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("\0GLB");
			bw.WriteInt32((int)ObjectType);

			bw.WriteInt32((int)ObjectName.Length);
			bw.WriteFixedLengthString(ObjectName);
		}
	}
}
