using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Scene;

namespace UniversalEditor.DataFormats.Multimedia3D.Scene.PolygonMovieMaker
{
	public class PMMSceneDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(SceneObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SceneObjectModel som = (objectModel as SceneObjectModel);
			if (som == null) return;

			Reader br = base.Accessor.Reader;
			Encoding encoding = Encoding.ShiftJIS;
			string signature = br.ReadFixedLengthString(30);
			if (signature != "Polygon Movie maker 0001") throw new InvalidDataFormatException("File does not begin with 'Polygon Movie maker 0001'");

			som.ImageWidth = br.ReadUInt32();
			som.ImageHeight = br.ReadUInt32();
			int unknown3 = br.ReadInt32();
			int unknown4 = br.ReadInt32();
			int unknown5 = br.ReadInt32();
			string sz4B = br.ReadFixedLengthString(2);
			byte[] flags = br.ReadBytes(7);
			byte wModelCount = br.ReadByte();
			for (byte i = 0; i < wModelCount; i++)
			{
				string modelName = br.ReadNullTerminatedString(20);
				SceneModelReference pmd = new SceneModelReference();
				pmd.ModelName = modelName;
				som.Models.Add(pmd);
			}

			byte[] junk1 = br.ReadBytes(21);
			for (byte i = 0; i < wModelCount; i++)
			{
				string modelFileName = br.ReadNullTerminatedString(256);
				som.Models[(int)i].FileName = modelFileName;

				byte[] n = br.ReadBytes(6);
				int u0001 = br.ReadInt32();
				int u0002 = br.ReadInt32();
				int u0003 = br.ReadInt32();
				int u0004 = br.ReadInt32();
				int u0005 = br.ReadInt32();

				byte[] unknown = br.ReadBytes(10225);
			}

			// HACK: ensure we are at the end of the file before continuing
			// remove this hack when entire file processing is completed
			br.Accessor.Seek(-92, SeekOrigin.End);
			som.FPSVisible = br.ReadBoolean();
			som.CoordinateAxisVisible = br.ReadBoolean();
			som.GroundShadowVisible = br.ReadBoolean();
			float fpsLimit = br.ReadSingle();
			if (fpsLimit == 1000)
			{
				// no limit on the FPS (as exported by MikuMikuDance), so convert it to -1
				som.FPSLimit = -1;
			}
			else
			{
				// limit on the FPS, valid values from MMD are 30 or 60, but we'll support others
				som.FPSLimit = fpsLimit;
			}
			som.ScreenCaptureMode = (SceneScreenCaptureMode)br.ReadInt32();
			int u0006 = br.ReadInt32();

			som.GroundShadowBrightness = br.ReadSingle();
			float uuuu = br.ReadSingle();

			som.GroundShadowTransparent = br.ReadBoolean();

			bool flag3 = br.ReadBoolean();
			bool flag4 = br.ReadBoolean();

			ScenePhysicalOperationMode physicalOperationMode = (ScenePhysicalOperationMode)br.ReadByte();
			float gravityAcceleration = br.ReadSingle();
			int gravityNoize = br.ReadInt32();
			float gravityDirectionX = br.ReadSingle();
			float gravityDirectionY = br.ReadSingle();
			float gravityDirectionZ = br.ReadSingle();
			bool enableNoize = br.ReadBoolean();

			// 47 bytes skipped
			br.Accessor.Seek(47, SeekOrigin.Current);

			int edgeLineColorR = br.ReadInt32();
			int edgeLineColorG = br.ReadInt32();
			int edgeLineColorB = br.ReadInt32();

			short u = br.ReadInt16();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
