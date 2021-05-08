//
//  PMDExtensionDataFormat.cs - implements an internal DataFormat for processing PMAX extensions to the PMD data format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

using UniversalEditor.ObjectModels.AniMiku.PMDExtension;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.AniMiku.PMDExtension
{
	/// <summary>
	/// Implements an internal <see cref="DataFormat" /> for processing PMAX extensions to the PMD data
	/// format.
	/// </summary>
	internal class PMDExtensionDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PMDExtensionObjectModel), DataFormatCapabilities.All);
				// _dfr.Filters.Add("AniMiku PMD extension");
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PMDExtensionObjectModel pmdo = (objectModel as PMDExtensionObjectModel);
			if (pmdo == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			foreach (ModelMaterial mat in mvarModel.Materials)
			{
				mat.AlwaysLight = br.ReadBoolean();
				mat.EnableAnimation = br.ReadBoolean();
				mat.EnableGlow = br.ReadBoolean();

				if (mat.EnableAnimation)
				{
					int textureCount = br.ReadInt32();
					string archiveFileName = br.ReadNullTerminatedString(100);

					PMDExtensionTextureGroup file = new PMDExtensionTextureGroup();
					file.Material = mat;
					file.ArchiveFileName = archiveFileName;
					for (int i = 0; i < textureCount; i++)
					{
						string textureFileName = br.ReadNullTerminatedString(256);
						file.TextureImageFileNames.Add(textureFileName);
					}
					pmdo.ArchiveFiles.Add(file);
				}
			}

			int originalModelLength = br.ReadInt32();
			string END = br.ReadFixedLengthString(3);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
#if READYTOSAVE
			IO.Writer bw = base.Accessor.Writer;
			foreach (ModelMaterial mat in mvarModel.Materials)
			{
				bw.Write(mat.AlwaysLight);
				bw.Write(mat.EnableAnimation);
				bw.Write(mat.EnableGlow);

				if (mat.EnableAnimation)
				{
					// TODO: figure out how to get texture count for the model
					int textureCount = br.ReadInt32();
					string archiveFileName = br.ReadNullTerminatedString(100);

					PMDExtensionTextureGroup file = new PMDExtensionTextureGroup();
					file.Material = mat;
					file.ArchiveFileName = archiveFileName;
					for (int i = 0; i < textureCount; i++)
					{
						string textureFileName = br.ReadNullTerminatedString(256);
						file.TextureImageFileNames.Add(textureFileName);
					}
					pmdo.ArchiveFiles.Add(file);
				}
			}

			int originalModelLength = br.ReadInt32();
			string END = br.ReadFixedLengthString(3);
#endif
			throw new NotImplementedException();
		}

		private ModelObjectModel mvarModel = null;
		public ModelObjectModel Model { get { return mvarModel; } set { mvarModel = value; } }
	}
}
