using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal;

namespace UniversalEditor.DataFormats.Multimedia.Picture.TBODY
{
	public class TBODYDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Avalanche TBODY texture", new string[] { "*.tbody" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Stream.Reader;
			byte[] data = br.ReadToEnd();

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			IO.Writer bw = new IO.Writer(ms);
			bw.Write(DirectDrawSurfaceDataFormat.DDS_MAGIC);
			bw.Write((uint)124); // data size

			bw.Write((uint)528391);

			uint width = 256;
			uint height = 256;
			bw.WriteUInt32(height); // height
			bw.WriteUInt32(width); // width
			bw.WriteUInt32((uint)(width * height)); // pitch or linear size
			bw.WriteUInt32((uint)0); // depth
			bw.WriteUInt32((uint)0); // mipmap count
			for (int i = 0; i < 11; i++)
			{
				bw.WriteUInt32((uint)0); //reserved
			}

			bw.WriteUInt32((uint)32);
			bw.WriteUInt32((uint)4);
			bw.WriteUInt32((uint)894720068);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)4096);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)0);

			bw.WriteBytes(data);
			bw.Close();

			byte[] data1 = ms.ToArray();
			ms = new System.IO.MemoryStream(data1);

			DirectDrawSurfaceDataFormat dds = new DirectDrawSurfaceDataFormat();
			StreamAccessor saa = new StreamAccessor(objectModel, dds);
			saa.Open(ms);
			saa.Load();
			saa.Close();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
