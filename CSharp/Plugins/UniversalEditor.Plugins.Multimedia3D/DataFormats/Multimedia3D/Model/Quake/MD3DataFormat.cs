using System;
using System.Collections.Generic;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

// http://en.wikipedia.org/wiki/MD3_%28file_format%29

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Quake
{
	public class MD3DataFormat : DataFormat
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

		private int mvarVersion = 15;
		public int Version
		{
			get { return mvarVersion; }
			set { mvarVersion = value; }
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) return;

			Reader br = base.Accessor.Reader;
			string IDP3 = br.ReadFixedLengthString(4);
			this.mvarVersion = br.ReadInt32();
			
			string modelName = br.ReadNullTerminatedString(64);
			if (!model.StringTable.ContainsKey(1033))
			{
				model.StringTable.Add(1033, new ModelStringTableExtension());
			}
			model.StringTable[1033].Title = modelName;

			int flags = br.ReadInt32();
			int numberOfFrames = br.ReadInt32();
			int numberOfTags = br.ReadInt32();
			int numberOfSurfaces = br.ReadInt32();
			int numberOfSkins = br.ReadInt32();
			int offsetToFrames = br.ReadInt32();
			int offsetToTags = br.ReadInt32();
			int offsetToSurfaces = br.ReadInt32();
			int offsetToEndOfFile = br.ReadInt32();

			#region Read the frames
			br.Accessor.Position = offsetToFrames;
			for (int i = 0; i < numberOfFrames; i++)
			{
				// General properties of a single animation frame.

				// First corner of the bounding box.
				float minBoundsX = br.ReadSingle();
				float minBoundsY = br.ReadSingle();
				float minBoundsZ = br.ReadSingle();

				// Second corner of the bounding box.
				float maxBoundsX = br.ReadSingle();
				float maxBoundsY = br.ReadSingle();
				float maxBoundsZ = br.ReadSingle();

				// Local origin, usually (0, 0, 0).
				float localOriginX = br.ReadSingle();
				float localOriginY = br.ReadSingle();
				float localOriginZ = br.ReadSingle();

				// Radius of bounding sphere.
				float radiusOfBoundingSphere = br.ReadSingle();

				// Name of Frame. ASCII character string, NUL-terminated (C-style)
				string frameName = br.ReadNullTerminatedString(16);
			}
			#endregion
			#region Read the tags
			br.Accessor.Position = offsetToTags;
			for (int i = 0; i < numberOfTags; i++)
			{
				// An attachment point for another MD3 model.

				// Name of Tag object. ASCII character string, NUL-terminated (C-style).
				string tagName = br.ReadNullTerminatedString(64);

				// Coordinates of Tag object.
				float originX = br.ReadSingle();
				float originY = br.ReadSingle();
				float originZ = br.ReadSingle();

				// 3x3 rotation matrix associated with the Tag.
				float axisX1 = br.ReadSingle();
				float axisY1 = br.ReadSingle();
				float axisZ1 = br.ReadSingle();
				float axisX2 = br.ReadSingle();
				float axisY2 = br.ReadSingle();
				float axisZ2 = br.ReadSingle();
				float axisX3 = br.ReadSingle();
				float axisY3 = br.ReadSingle();
				float axisZ3 = br.ReadSingle();
			}
			#endregion
			#region Read the surfaces
			br.Accessor.Position = offsetToSurfaces;
			for (int i = 0; i < numberOfSurfaces; i++)
			{
				// An animated triangle mesh.
				ReadSurface(br, ref model);
			}
			#endregion
		}

		private void ReadSurface(Reader br, ref ModelObjectModel model)
		{
			long offsetToStart = br.Accessor.Position;

			ModelSurface surface = new ModelSurface();
			#region Header
			// Magic number. As a string of 4 octets, reads "IDP3"; as unsigned
			// little-endian 860898377 (0x33504449); as unsigned big-endian
			// 1229213747 (0x49445033).
			string magic = br.ReadFixedLengthString(4); // IDP3

			// Name of Surface object. ASCII character string, NUL-terminated
			// (C-style).
			surface.Name = br.ReadNullTerminatedString(64);

			int flags = br.ReadInt32();

			// Number of animation frames. This should match NUM_FRAMES in the
			// MD3 header.
			int numFrames = br.ReadInt32();

			// Number of Shader objects defined in this Surface, with a limit of
			// MD3_MAX_SHADERS. Current value of MD3_MAX_SHADERS is 256.
			int numShaders = br.ReadInt32();

			// Number of Vertex objects defined in this Surface, up to
			// MD3_MAX_VERTS. Current value of MD3_MAX_VERTS is 4096.
			int numVertices = br.ReadInt32();

			// Number of Triangle objects defined in this Surface, maximum of
			// MD3_MAX_TRIANGLES. Current value of MD3_MAX_TRIANGLES is 8192.
			int numTriangles = br.ReadInt32();

			// Relative offset from SURFACE_START where the list of Triangle
			// objects starts.
			int offsetToTriangles = br.ReadInt32();

			// Relative offset from SURFACE_START where the list of Shader
			// objects starts.
			int offsetToShaders = br.ReadInt32();

			// Relative offset from SURFACE_START where the list of ST objects (s-t
			// texture coordinates) starts.
			int offsetToTextureCoordinates = br.ReadInt32();

			// Relative offset from SURFACE_START where the list of Vertex objects
			// (X-Y-Z-N vertices) starts.
			int offsetToVertices = br.ReadInt32();

			// Relative offset from SURFACE_START to where the Surface object ends.
			int offsetToEnd = br.ReadInt32();
			#endregion
			#region Shaders
			// List of Shader objects usually starts immediate after the Surface
			// header, but use OFS_SHADERS (or rather, OFS_SHADERS+SURFACE_START for
			// files).
			br.Accessor.Position = offsetToStart + offsetToShaders;
			for (int i = 0; i < numShaders; i++)
			{
				// Pathname of shader in the PK3. ASCII character string,
				// NUL-terminated (C-style).
				string shaderName = br.ReadNullTerminatedString(64);

				// Shader index number. No idea how this is allocated, but presumably
				// in sequential order of definition.
				int shaderIndex = br.ReadInt32();
			}
			#endregion
			#region Triangles
			// List of Triangle objects usually starts immediately after the list of
			// Shader objects, but use OFS_TRIANGLES (+ SURFACE_START).
			br.Accessor.Position = offsetToStart + offsetToTriangles;

			List<int[]> triangleIndices = new List<int[]>();
			for (int i = 0; i < numTriangles; i++)
			{
				// List of offset values into the list of Vertex objects that
				// constitute the corners of the Triangle object. Vertex numbers are
				// used instead of actual coordinates, as the coordinates are
				// implicit in the Vertex object. The triangles have clockwise
				// winding.
				int vertex1Index = br.ReadInt32();
				int vertex2Index = br.ReadInt32();
				int vertex3Index = br.ReadInt32();

				// save the indices into the list for reference when we actually
				// get to the vertex array
				triangleIndices.Add(new int[] { vertex1Index, vertex2Index, vertex3Index });
			}
			#endregion
			#region Texture Coordinates
			br.Accessor.Position = offsetToTextureCoordinates;
			for (int i = 0; i < numTriangles; i++)
			{
				float s = br.ReadSingle();
				float t = br.ReadSingle();

				// s and t texture coordinates, normalized to the range [0, 1]. Values
				// outside the range indicate wraparounds/repeats. Unlike UV coordinates,
				// the origin for texture coordinates is located in the upper left corner
				// (similar to the coordinate system used for computer screens) whereas,
				// in UV mapping, it is placed in the lower left corner. As such, the t
				// value must be flipped to correspond with UV coordinates.

				// TODO: convert S/T coordinates to UV in order to have a standard
			}
			#endregion
			#region Vertices
			for (int i = 0; i < numVertices; i++)
			{
				ModelVertex vertex = new ModelVertex();
				// x, y, and z coordinates in right-handed 3-space, scaled down by
				// factor 1.0/64. (Multiply by 1.0/64 to obtain original coordinate
				// value.)
				short _vertexX = br.ReadInt16();
				short _vertexY = br.ReadInt16();
				short _vertexZ = br.ReadInt16();

				// Zenith and azimuth angles of normal vector. 255 corresponds to 2
				// pi. See spherical coordinates.
				byte normalZenith = br.ReadByte();
				byte normalAzimuth = br.ReadByte();

				double vertexX = (_vertexX * (1.0 / 64));
				double vertexY = (_vertexY * (1.0 / 64));
				double vertexZ = (_vertexZ * (1.0 / 64));

				vertex.OriginalPosition = new PositionVector3(vertexX, vertexY, vertexZ);
				vertex.Position = new PositionVector3(vertexX, vertexY, vertexZ);

				// The normal vector uses a spherical coordinate system. Since the
				// normal vector is, by definition, a length of one, only the angles
				// need to be recorded. Each angle is constrained between 0 - 255 to
				// fit in one octet. A normal vector encodes into 16 bits.

				// Encoding:
				//      azimuth <- atan2(y, x) * 255 / (2 * pi)
				//      zenith <- acos(z) * 255 / (2 * pi)

				// Decoding:
				//      lat <- zenith * (2 * pi ) / 255
				//      lng <- azimuth * (2 * pi) / 255
				//      x <- cos ( lng ) * sin ( lat )
				//      y <- sin ( lng ) * sin ( lat )
				//      z <- cos ( lat )
				double lat = (double)((double)normalZenith * (2 * Math.PI) / 255);
				double lng = (double)((double)normalAzimuth * (2 * Math.PI) / 255);
				
				double normalX = Math.Cos(lng) * Math.Sin(lat);
				double normalY = Math.Sin(lng) * Math.Sin(lat);
				double normalZ = Math.Cos(lat);
				vertex.Normal = new PositionVector3(normalX, normalY, normalZ);
				surface.Vertices.Add(vertex);
			}
			#endregion

			model.Surfaces.Add(surface);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			ModelObjectModel model = objectModel as ModelObjectModel;
			bw.WriteFixedLengthString("IDP3");
			bw.WriteInt32(mvarVersion);
			string modelTitle = String.Empty;
			if (model.StringTable.ContainsKey(1033))
			{
				modelTitle = model.StringTable[1033].Title;
			}
			else
			{
				// get the first string table in the list
				foreach (System.Collections.Generic.KeyValuePair<int, ModelStringTableExtension> kvp in model.StringTable)
				{
					modelTitle = kvp.Value.Title;
					break;
				}
			}
			bw.WriteNullTerminatedString(modelTitle, 64);
			bw.Flush();
		}
	}
}
