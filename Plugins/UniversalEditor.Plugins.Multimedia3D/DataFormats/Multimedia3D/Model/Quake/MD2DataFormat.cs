using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
namespace UniversalEditor.DataFormats.Multimedia3D.Model.Quake
{
	public class MD2DataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		private int mvarVersion = 8;
		public int Version
		{
			get { return mvarVersion; }
			set { mvarVersion = value; }
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			ModelObjectModel mom = objectModel as ModelObjectModel;
			string IDP2 = br.ReadFixedLengthString(4);
			if (IDP2 != "IDP2") throw new InvalidDataFormatException("File does not begin with 'IDP2'");

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
			Writer bw = base.Accessor.Writer;
			ModelObjectModel mom = objectModel as ModelObjectModel;
			bw.WriteFixedLengthString("IDP2");
			bw.WriteInt32(mvarVersion);
			int skinwidth = 128;
			int skinheight = 128;
			bw.WriteInt32(skinwidth);
			bw.WriteInt32(skinheight);
			int framesize = 0;
			bw.WriteInt32(framesize);
			int numberOfSkins = 0;
			bw.WriteInt32(numberOfSkins);
			int numberOfVertices = 0;
			bw.WriteInt32(numberOfVertices);
			int numberOfTextureCoordinates = 0;
			bw.WriteInt32(numberOfTextureCoordinates);
			int numberOfTriangles = 0;
			bw.WriteInt32(numberOfTriangles);
			int numberOfOpenGLCommands = 0;
			bw.WriteInt32(numberOfOpenGLCommands);
			int numberOfFrames = 0;
			bw.WriteInt32(numberOfFrames);
			int offsetToSkinNames = 0;
			bw.WriteInt32(offsetToSkinNames);
			int offsetToTextureCoordinates = 0;
			bw.WriteInt32(offsetToTextureCoordinates);
			int offsetToTriangles = 0;
			bw.WriteInt32(offsetToTriangles);
			int offsetToFrameData = 0;
			bw.WriteInt32(offsetToFrameData);
			int offsetToOpenGLCommands = 0;
			bw.WriteInt32(offsetToOpenGLCommands);
			int offsetToEndOfFile = 0;
			bw.WriteInt32(offsetToEndOfFile);
			bw.Flush();
		}
	}
}
