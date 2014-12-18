using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;

using UniversalEditor.ObjectModels.AniMiku.PMDExtension;
using UniversalEditor.DataFormats.AniMiku.PMDExtension;

using UniversalEditor.DataFormats.Multimedia3D.Model.PolygonMovieMaker;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.AniMiku.ExtendedPMD
{
	/// <summary>
	/// Implements the AniMiku extended PMD (APMD) data format. This is different than the PMAX (PMD by
	/// ALCEproject Extended) data format implemented by Concertroid.
	/// </summary>
	public class ExtendedPMDDataFormat : PMDModelDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Priority = 1;
			}
			return _dfr;
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);

			// attempt to load more
			IO.Reader br = base.Accessor.Reader;
			if (br.EndOfStream) return;
			byte[] datas = br.ReadUntil("END", false);

			PMDExtensionObjectModel pmdo = new PMDExtensionObjectModel();
			PMDExtensionDataFormat pmdf = new PMDExtensionDataFormat();
			pmdf.Model = model;
			
			Document.Load(pmdo, pmdf, new MemoryAccessor(datas), true);
			
			foreach (PMDExtensionTextureGroup file in pmdo.ArchiveFiles)
			{
				foreach (string fileName in file.TextureImageFileNames)
				{
					file.Material.Textures.Add(file.ArchiveFileName + "::/" + fileName, null, ModelTextureFlags.Texture);
				}
			}
		}
	}
}
