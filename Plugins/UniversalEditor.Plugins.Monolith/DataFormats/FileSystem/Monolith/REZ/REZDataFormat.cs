//
//  REZDataFormat.cs - provides a DataFormat for manipulating Monolith Productions' REZ archive files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.Monolith.DataFormats.FileSystem.Monolith.REZ
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Monolith Productions' REZ archive files.
	/// </summary>
	public class REZDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.ExportOptions.Add(new CustomOptionText(nameof(Description), "_Description", String.Empty, 127));
                _dfr.Sources.Add("http://wiki.xentax.com/index.php?title=Monolith_REZ");
                // _dfr.Filters.Add("Monolith Productions REZ archive", new string[] { "*.rez" });
            }
            return _dfr;
        }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        private uint mvarFormatVersion = 1;
        public uint FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            IO.Reader reader = base.Accessor.Reader;
            mvarDescription = reader.ReadFixedLengthString(127);
            mvarFormatVersion = reader.ReadUInt32();

            uint diroffset = reader.ReadUInt32();
            uint dirsize = reader.ReadUInt32();
			uint unknown1 = reader.ReadUInt32();
			

            throw new NotImplementedException();
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            File[] allfiles = fsom.GetAllFiles();

            IO.Writer writer = base.Accessor.Writer;
            writer.WriteFixedLengthString(mvarDescription, 127);
            writer.WriteUInt32(mvarFormatVersion);

            uint diroffset = 184; // 127 + (11 * 4) + 13
            uint dirsize = 0;

            foreach (File file in fsom.Files)
            {
                diroffset += (uint)file.Size;

            }
            throw new NotImplementedException();
        }
    }
}
