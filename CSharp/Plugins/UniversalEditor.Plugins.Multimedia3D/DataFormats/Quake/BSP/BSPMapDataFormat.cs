using System;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Scene;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Quake.BSP
{
	public class BSPMapDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Binary Space Partitioned 3D scene", new byte?[][] { new byte?[] { (byte)'I', (byte)'B', (byte)'S', (byte)'P' } }, new string[] { "*.bsp" });
            dfr.Capabilities.Add(typeof(SceneObjectModel), DataFormatCapabilities.All);
            return dfr;
        }
		[ImplementationStatus(ImplementationStatus.NotImplemented)]
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
            IO.BinaryReader br = base.Stream.BinaryReader;
            string magic = br.ReadFixedLengthString(4);
            if (magic != "IBSP") throw new DataFormatException(Localization.StringTable.ErrorDataFormatInvalid);

            int version = br.ReadInt32();
            for (int i = 0; i < 17; i++)
            {
                int directoryEntryOffset = br.ReadInt32();
                int directoryEntryLength = br.ReadInt32();

                long currentPos = br.BaseStream.Position;

                br.BaseStream.Seek(directoryEntryOffset, System.IO.SeekOrigin.Begin);



                br.BaseStream.Seek(currentPos, System.IO.SeekOrigin.Begin);
            }
		}
        [ImplementationStatus(ImplementationStatus.NotImplemented)]
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
