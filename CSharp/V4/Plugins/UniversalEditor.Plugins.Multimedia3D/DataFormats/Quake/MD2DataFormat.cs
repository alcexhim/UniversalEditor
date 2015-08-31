using System;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Quake
{
	public class MD2DataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("id software MD2 model", new byte?[][] { new byte?[] { new byte?(73), new byte?(68), new byte?(80), new byte?(50) } }, new string[] { "*.md2" });
            dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
            return dfr;
        }
		private int mvarVersion = 8;
        public int Version
        {
            get { return mvarVersion; }
            set { mvarVersion = value; }
        }
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			BinaryReader br = base.Stream.BinaryReader;
			ModelObjectModel mom = objectModel as ModelObjectModel;
			string IDP2 = br.ReadFixedLengthString(4);
			this.mvarVersion = br.ReadInt32();
			int skinwidth = br.ReadInt32();
			int skinheight = br.ReadInt32();
			int framesize = br.ReadInt32();
			int numberOfSkins = br.ReadInt32();
			int numberOfVertices = br.ReadInt32();
			int numberOfTextureCoordinates = br.ReadInt32();
			int numberOfTriangles = br.ReadInt32();
			int numberOfOpenGLCommands = br.ReadInt32();
			int numberOfFrames = br.ReadInt32();
			int offsetToSkinNames = br.ReadInt32();
			int offsetToTextureCoordinates = br.ReadInt32();
			int offsetToTriangles = br.ReadInt32();
			int offsetToFrameData = br.ReadInt32();
			int offsetToOpenGLCommands = br.ReadInt32();
			int offsetToEndOfFile = br.ReadInt32();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			BinaryWriter bw = base.Stream.BinaryWriter;
			ModelObjectModel mom = objectModel as ModelObjectModel;
			bw.WriteFixedLengthString("IDP2");
			bw.Write(this.mvarVersion);
			int skinwidth = 128;
			int skinheight = 128;
			bw.Write(skinwidth);
			bw.Write(skinheight);
			int framesize = 0;
			bw.Write(framesize);
			int numberOfSkins = 0;
			bw.Write(numberOfSkins);
			int numberOfVertices = 0;
			bw.Write(numberOfVertices);
			int numberOfTextureCoordinates = 0;
			bw.Write(numberOfTextureCoordinates);
			int numberOfTriangles = 0;
			bw.Write(numberOfTriangles);
			int numberOfOpenGLCommands = 0;
			bw.Write(numberOfOpenGLCommands);
			int numberOfFrames = 0;
			bw.Write(numberOfFrames);
			int offsetToSkinNames = 0;
			bw.Write(offsetToSkinNames);
			int offsetToTextureCoordinates = 0;
			bw.Write(offsetToTextureCoordinates);
			int offsetToTriangles = 0;
			bw.Write(offsetToTriangles);
			int offsetToFrameData = 0;
			bw.Write(offsetToFrameData);
			int offsetToOpenGLCommands = 0;
			bw.Write(offsetToOpenGLCommands);
			int offsetToEndOfFile = 0;
			bw.Write(offsetToEndOfFile);
			bw.Flush();
		}
	}
}
