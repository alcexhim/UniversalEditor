using System;
using System.Collections.Generic;
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.Genealogy.DataFormats.FamilyTreeMaker.Windows
{
	internal struct INDGROUPSRecord
	{
		public string name;

		public INDGROUPSRecord(string name)
		{
			this.name = name;
		}
	}
	internal class INDGROUPSObjectModel : ObjectModel
	{
		public List<INDGROUPSRecord> Items { get; } = new List<INDGROUPSRecord>();

		public override void Clear()
		{
			Items.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
		}
	}
	internal class INDGROUPSDataFormat : DataFormat
	{
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			INDGROUPSObjectModel om = (objectModel as INDGROUPSObjectModel);
			Reader r = base.Accessor.Reader;
			r.Seek(114, SeekOrigin.Begin);

			int count = r.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				string v = r.ReadNullTerminatedString(Encoding.UTF16LittleEndian);
				byte nul = r.ReadByte();

				v = v.TrimNull();
				if (String.IsNullOrEmpty(v))
					break;
				om.Items.Add(new INDGROUPSRecord(v));
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
