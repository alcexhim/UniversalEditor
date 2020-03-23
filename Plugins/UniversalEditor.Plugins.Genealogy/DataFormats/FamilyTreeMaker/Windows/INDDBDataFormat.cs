using System;
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.Genealogy.DataFormats.FamilyTreeMaker.Windows
{
	internal struct INDDBRecord
	{
		public string name;
		public DateTime testdt;

		public INDDBRecord(string name, DateTime testdt)
		{
			this.name = name;
			this.testdt = testdt;
		}
	}
	internal class INDDBObjectModel : ObjectModel
	{
		public System.Collections.Generic.List<INDDBRecord> Items { get; } = new System.Collections.Generic.List<INDDBRecord>();

		public override void Clear()
		{
		}

		public override void CopyTo(ObjectModel where)
		{
		}
	}
	internal class INDDBDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(INDDBObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			INDDBObjectModel objm = (objectModel as INDDBObjectModel);
			Reader r = base.Accessor.Reader;

			byte[] junk = r.ReadBytes(128); // is this short sector garbage?
			long unk1 = r.ReadInt64(); // bluh

			int test = r.ReadInt32();
			byte[] xx = r.ReadBytes(64);

			for (int i = 0; i < 133; i++)
			{
				// each record is 128 bytes long
				long recordStart = r.Accessor.Position;

				ushort u0 = r.ReadUInt16();
				ushort u1 = r.ReadUInt16();
				ushort u2 = r.ReadUInt16();
				ushort u3 = r.ReadUInt16();
				ushort u4 = r.ReadUInt16();

				string name = r.ReadFixedLengthString(40, Encoding.UTF16LittleEndian); // r.ReadFixedLengthString(40, Encoding.UTF16LittleEndian);
				name = name.TrimNull();

				ushort unknown = r.ReadUInt16();
				long dt = r.ReadInt64();

				DateTime testdt = new DateTime(dt);

				long recordEnd = r.Accessor.Position;

				long posNext = recordEnd - recordStart;
				r.Accessor.Seek(128 - posNext, SeekOrigin.Current);

				// 132 names in file
				objm.Items.Add(new INDDBRecord(name, testdt));
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
