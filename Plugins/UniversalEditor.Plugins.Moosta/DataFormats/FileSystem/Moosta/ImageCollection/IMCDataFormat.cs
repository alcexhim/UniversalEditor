//
//  IMCDataFormat.cs - provides a DataFormat for manipulating Moosta OMP image collection files
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

using System.Collections.Generic;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Moosta.ImageCollection
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Moosta OMP image collection files.
	/// </summary>
	public class IMCDataFormat : DataFormat
    {
        private static DataFormatReference _dfr;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            List<File> files = new List<File>();
            IO.Reader br = base.Accessor.Reader;
            int fileCount = br.ReadInt32();
            for (int i = 0; i < fileCount; i++)
            {
                string fileName = br.ReadLengthPrefixedString();
                files.Add(fsom.AddFile(fileName));
            }
            for (int i = 0; i < fileCount; i++)
            {
                int length = br.ReadInt32();
                int unknown1 = br.ReadInt32();
                long offset = br.Accessor.Position;

                files[i].Properties.Add("length", length);
                files[i].Properties.Add("offset", offset);
                files[i].Properties.Add("reader", br);
                files[i].Size = length;
                files[i].DataRequest += IMCDataFormat_DataRequest;

                br.Accessor.Seek(length, SeekOrigin.Current);
            }
        }

        private void IMCDataFormat_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.Reader br = (IO.Reader)file.Properties["reader"];
            int length = (int)file.Properties["length"];
            long offset = (long)file.Properties["offset"];
            br.Accessor.Seek(offset, SeekOrigin.Begin);
            e.Data = br.ReadBytes(length);
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            IO.Writer bw = base.Accessor.Writer;

            File[] files = fsom.GetAllFiles();
            bw.WriteInt32(files.Length);
            foreach (File file in files)
            {
                bw.Write(file.Name);
            }
            foreach (File file in files)
            {
                bw.WriteInt64(file.Size);
                bw.WriteBytes(file.GetData());
            }
            bw.Flush();
        }
    }
}
