using System;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Scene;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Quake.BSP
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

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();

            IO.Reader br = base.Accessor.Reader;
            string magic = br.ReadFixedLengthString(4);
            if (magic != "IBSP") throw new InvalidDataFormatException("File does not begin with \"IBSP\"");

            int version = br.ReadInt32();
            for (int i = 0; i < 17; i++)
            {
                int directoryEntryOffset = br.ReadInt32();
                int directoryEntryLength = br.ReadInt32();

                long currentPos = br.Accessor.Position;

                br.Accessor.Seek(directoryEntryOffset, SeekOrigin.Begin);



                br.Accessor.Seek(currentPos, SeekOrigin.Begin);
            }
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
