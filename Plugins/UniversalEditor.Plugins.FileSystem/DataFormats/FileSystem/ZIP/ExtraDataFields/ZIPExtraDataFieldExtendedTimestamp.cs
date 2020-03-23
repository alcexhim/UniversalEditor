using System;
using UniversalEditor.Accessors;

namespace UniversalEditor.DataFormats.FileSystem.ZIP.ExtraDataFields
{
	public class ZIPExtraDataFieldExtendedTimestamp : ZIPExtraDataField
	{
		public ZIPExtraDataFieldExtendedTimestamp (DateTime modificationTimestamp = default(DateTime), DateTime accessTimestamp = default (DateTime), DateTime creationTimestamp = default (DateTime))
		{
			base.Type = ZIPExtraDataFieldType.ExtendedTimestamp;

			ModificationTimestamp = modificationTimestamp;
			AccessTimestamp = accessTimestamp;
			CreationTimestamp = creationTimestamp;
		}

		public DateTime ModificationTimestamp { get; set; } = DateTime.Now;
		public DateTime AccessTimestamp { get; set; } = DateTime.Now;
		public DateTime CreationTimestamp { get; set; } = DateTime.Now;

		protected override byte [] GetLocalDataInternal ()
		{
			/*
			 * Local-header version:

			Value         Size        Description
			-----         ----        -----------
			(time)  0x5455        Short       tag for this extra block type ("UT")
			TSize         Short       total data size for this block
			Flags         Byte        info bits
			(ModTime)     Long        time of last modification (UTC/GMT)
			(AcTime)      Long        time of last access (UTC/GMT)
			(CrTime)      Long        time of original creation (UTC/GMT)
			*/
			MemoryAccessor ma = new MemoryAccessor ();
			ma.Writer.WriteByte (7);
			ma.Writer.WriteDOSFileTime (ModificationTimestamp);
			ma.Writer.WriteDOSFileTime (AccessTimestamp);
			ma.Writer.WriteDOSFileTime (CreationTimestamp);
			return ma.ToArray ();
		}
		protected override byte[] GetCentralDataInternal ()
		{
			/*
			 * Central-header version:

			Value         Size        Description
			-----         ----        -----------
			(time)  0x5455        Short       tag for this extra block type ("UT")
			TSize         Short       total data size for this block
			Flags         Byte        info bits (refers to local header!)
			(ModTime)     Long        time of last modification (UTC/GMT)
			*/

			MemoryAccessor ma = new MemoryAccessor ();
			ma.Writer.WriteByte (7);
			ma.Writer.WriteDOSFileTime (ModificationTimestamp);
			return ma.ToArray ();
		}
	}
}
