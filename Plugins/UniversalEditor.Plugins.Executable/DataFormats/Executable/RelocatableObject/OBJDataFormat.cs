using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.RelocatableObject
{
	public class OBJDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			while (!reader.EndOfStream)
			{
				OBJRecordType recordType = (OBJRecordType)reader.ReadByte();
				ushort dataLength = reader.ReadUInt16();
				byte[] data = reader.ReadBytes(dataLength);
				byte checksum = reader.ReadByte();

				switch (recordType)
				{
					case OBJRecordType.CodeDataText0xA0:
					case OBJRecordType.CodeDataText0xA1:
					{
						break;
					}
					case OBJRecordType.Comment:
					{
						break;
					}
					case OBJRecordType.CommonDataInitialized0xC2:
					case OBJRecordType.CommonDataInitialized0xC3:
					{
						break;
					}
					case OBJRecordType.CommonDataUninitialized:
					{
						break;
					}
					case OBJRecordType.ExternalReference:
					{
						break;
					}
					case OBJRecordType.ExternalSymbols0x90:
					case OBJRecordType.ExternalSymbols0x91:
					{
						break;
					}
					case OBJRecordType.ModuleEnd0x8A:
					case OBJRecordType.ModuleEnd0x8B:
					{
						break;
					}
					case OBJRecordType.Relocation0x9C:
					case OBJRecordType.Relocation0x9D:
					{
						break;
					}
					case OBJRecordType.Segment0x98:
					case OBJRecordType.Segment0x99:
					{
						break;
					}
					case OBJRecordType.SegmentGroup:
					{
						break;
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
