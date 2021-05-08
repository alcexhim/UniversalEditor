//
//  ExtendedPMDDataFormat.cs - implements the AniMiku extended PMD (APMD) data format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2014-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
		private static DataFormatReference _dfr;
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
			byte[] datas = br.ReadUntil(Encoding.ASCII.GetBytes("END"), false);

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
