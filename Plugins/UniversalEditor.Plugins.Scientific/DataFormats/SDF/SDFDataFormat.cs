//
//  SDFDataFormat.cs - a platform-independent data format that works in Fortran, C, and IDL
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
using UniversalEditor.IO;
using UniversalEditor.Plugins.Scientific.ObjectModels.DataSetCollection;

namespace UniversalEditor.Plugins.Scientific.DataFormats.SDF
{
	/// <summary>
	/// A platform-independent data format that works in Fortran, C, and IDL.
	/// </summary>
	public class SDFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(DataSetCollectionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			DataSetCollectionObjectModel dsc = (objectModel as DataSetCollectionObjectModel);
			if (dsc == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			string signature = reader.ReadFixedLengthString(11);

			if (signature != "SDF format\0")
				throw new InvalidDataFormatException("File does not begin with 'SDF format', 0x00");

			reader.Endianness = Endianness.BigEndian;

			// location of next available byte in header
			ulong hdrlen = reader.ReadUInt64();
			// location of next available byte in data area. This is also equal to the file size.
			ulong datalen = reader.ReadUInt64();
			// number of datasets currently in file
			uint ndatasets = reader.ReadUInt32();

			// The current size of the header (initially set to HINITSZ, and incremented in blocks of HINITSZ as
			// necessary.Default value of HINITSZ is 2000, but can be set by user.
			ulong hdr_alloc_size = reader.ReadUInt64();

			for (uint i = 0; i < ndatasets; i++)
			{
				string dataset_desc = reader.ReadLine();
				string[] tokenized = dataset_desc.Split(new char[] { ' ' });

				if (tokenized.Length < 6) throw new InvalidDataFormatException("dataset description (tokenized) does not contain at least 6 elements");

				DataSet ds = new DataSet();

				int iorder = Int32.Parse(tokenized[0]); // the order of this dataset in the file
				ds.Order = iorder;

				string label = tokenized[1];
				ds.Name = label;

				SDFDataSetDataType dataType = (SDFDataSetDataType)(byte)Char.Parse(tokenized[2]); // a single character denoting the type of data; i.e., 'f', 'i', 'c', or 'b'
				switch (dataType)
				{
					case SDFDataSetDataType.Byte: ds.DataType = typeof(byte); break;
					case SDFDataSetDataType.Complex: ds.DataType = typeof(long); break;
					case SDFDataSetDataType.Float: ds.DataType = typeof(float); break;
					case SDFDataSetDataType.Integer: ds.DataType = typeof(long); break;
				}

				int nbpw = Int32.Parse(tokenized[3]); // the number of bytes per word
				int ndim = Int32.Parse(tokenized[4]); // the number of dimensions of the dataset or array
				ds.Dimensions = ndim;
				ds.Sizes = new int[ndim];
				for (int j = 5; j < 5 + ndim; j++)
				{
					int dim = Int32.Parse(tokenized[j]); // the ndim values of the array dimensions
					ds.Sizes[j - 5] = dim;
				}

				dsc.DataSets.Add(ds);
			}

			reader.Seek((long)hdr_alloc_size, SeekOrigin.Begin);
			reader.Seek(19, SeekOrigin.Current); // idk???

			for (int i = 0; i < dsc.DataSets.Count; i++)
			{
				for (int j = 0; j < dsc.DataSets[i].Dimensions; j++)
				{
					for (int k = 0; k < dsc.DataSets[i].Sizes[j]; k++)
					{
						float w = reader.ReadSingle();
						dsc.DataSets[i].SetValue(j, k, w);
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
