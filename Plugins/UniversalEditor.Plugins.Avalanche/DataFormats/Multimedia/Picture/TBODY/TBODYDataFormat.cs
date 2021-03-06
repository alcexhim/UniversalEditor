using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.DirectDraw;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.DirectDraw.Internal;

namespace UniversalEditor.DataFormats.Multimedia.Picture.TBODY
{
	public class TBODYDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			// A TBODY file is just a DDS file without a header
			IO.Reader br = base.Accessor.Reader;
			byte[] data = br.ReadToEnd();

			// We have to add the DDS header
			MemoryAccessor ma = new MemoryAccessor();
			IO.Writer bw = new IO.Writer(ma);
			bw.WriteUInt32(DirectDrawSurfaceDataFormat.DDS_MAGIC);
			bw.WriteUInt32((uint)124); // data size

			bw.WriteUInt32((uint)528391);

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

			byte[] data1 = ma.ToArray();
			ma = new MemoryAccessor(data1);

			ContinueLoading(ref objectModel, new DirectDrawSurfaceDataFormat());
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
