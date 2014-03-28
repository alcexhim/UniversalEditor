using System;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Scene;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker
{
	public class SceneDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Polygon Movie Maker scene data", new byte?[][] { new byte?[] { new byte?(80), new byte?(111), new byte?(108), new byte?(121), new byte?(103), new byte?(111), new byte?(110), new byte?(32), new byte?(77), new byte?(111), new byte?(118), new byte?(105), new byte?(101), new byte?(32), new byte?(109), new byte?(97), new byte?(107), new byte?(101), new byte?(114), new byte?(32), new byte?(48), new byte?(48), new byte?(48), new byte?(49) } }, new string[] { "*.pmm" });
            dfr.Capabilities.Add(typeof(SceneObjectModel), DataFormatCapabilities.All);
            return dfr;
        }
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			if (objectModel is SceneObjectModel)
			{
				SceneObjectModel som = objectModel as SceneObjectModel;
				som.Clear();
				BinaryReader br = base.Stream.BinaryReader;
				Encoding encoding = Encoding.GetEncoding("shift-jis");
				string Polygon_Movie_maker_ = br.ReadFixedLengthString(24);
				int unknown = br.ReadInt32();
				int unknown2 = br.ReadInt32();
				int unknown3 = br.ReadInt32();
				int unknown4 = br.ReadInt32();
				int unknown5 = br.ReadInt32();
				string sz4B = br.ReadFixedLengthString(2);
				byte[] flags = br.ReadBytes(8u);
				byte wModelCount = flags[7];
				for (byte i = 0; i < wModelCount; i += 1)
				{
					string modelName = br.ReadNullTerminatedString(20);
					SceneModelReference pmd = new SceneModelReference();
					pmd.ModelName = modelName;
					som.Models.Add(pmd);
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
