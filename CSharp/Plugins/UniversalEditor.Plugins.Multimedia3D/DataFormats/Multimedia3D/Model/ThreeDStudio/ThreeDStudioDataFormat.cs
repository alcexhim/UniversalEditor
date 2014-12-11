using System;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

// base this data format from /Binaries/salmon-viewer-0.2.1
#if ZZYZX
namespace UniversalEditor.DataFormats.Multimedia3D.Model.ThreeDStudio
{
	public class ThreeDStudioDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Filters.Add("3D Studio model", new string[] { "*.3ds" });
            dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
            return dfr;
        }
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			// 3ds models can use additional files which are expected in the same directory
			string baseDirectory = System.IO.Path.GetDirectoryName(base.FileName);

			Reader br = base.Accessor.Reader;

			ThreeDStudioChunk chunk = ReadChunk(br);

		}

		private ThreeDStudioChunk ReadChunk(Reader br)
		{
			ushort chunkID = br.ReadUInt16();
			if (chunkID != 0x4D4D)
			{
				throw new InvalidDataFormatException("Could not read 3DS chunk 0x4D4D");
			}

			uint chunkLength = br.ReadUInt32();

			ThreeDStudioChunk chunk = new ThreeDStudioChunk();
			chunk.ID = chunkID;
			chunk.Length = chunkLength;

			uint childBytesRead = 0;

			while (childBytesRead < chunk.Length)
			{
				ThreeDStudioChunk child = new ThreeDStudioChunk();
				chunk.ID = br.ReadUInt16();
				chunk.Length = br.ReadUInt32();

				// process based on ID
				switch (child.ID)
				{
					case 0x0002: //C_VERSION
					{
						int version = br.ReadInt32();
						childBytesRead += 4;

						break;
					}
					case 0x3D3D: // C_OBJECTINFO
					{
						// not sure what's up with this chunk
						//SkipChunk ( obj_chunk );
						//child.BytesRead += obj_chunk.BytesRead;
						//ProcessChunk ( child );

						// blender 3ds export (others?) uses this
						// in the hierarchy of objects and materials
						// so lets process the next (child) chunk

						break;
					}
					case 0xAFFF: // C_MATERIAL
					{
						ProcessMaterialChunk(child);
						break;
					}
					case 0x4000: // C_OBJECT
					{
						// string name = 
						ProcessString(child);

						Entity e = ProcessObjectChunk(child);
						e.CalculateNormals();
						model.Entities.Add(e);

						break;
					}
					default:
					{
						SkipChunk(child);
						break;
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
#endif