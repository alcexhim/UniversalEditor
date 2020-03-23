using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.RIFF;
using UniversalEditor.ObjectModels.RIFF;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats
{
	public class RIFFExtensibleModelDataFormat : RIFFDataFormat
	{
		protected override bool IsObjectModelSupported(ObjectModel omb)
		{
			RIFFObjectModel riff = (omb as RIFFObjectModel);
			if (riff != null)
			{
				if (riff.Chunks["MODL"] != null)
				{
					return true;
				}
			}
			return false;
		}
		private DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("RIFF extensible model data", new byte?[][] { new byte?[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F', null, null, null, null, (byte)'M', (byte)'O', (byte)'D', (byte)'L' } }, new string[] { "*.rmd" });
			}
			return _dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new RIFFObjectModel());
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);
			RIFFObjectModel riff = new RIFFObjectModel();

			RIFFGroupChunk chunkMODL = new RIFFGroupChunk();

			RIFFDataChunk chunkVRTX = new RIFFDataChunk();
			System.IO.MemoryStream msVRTX = new System.IO.MemoryStream();
			IO.BinaryWriter bwVRTX = new IO.BinaryWriter(msVRTX);

			bwVRTX.Close();
			chunkVRTX.Data = msVRTX.ToArray();

			chunkMODL.Chunks.Add(chunkVRTX);

			riff.Chunks.Add(chunkMODL);

			objectModels.Push(riff);
		}
	}
}
