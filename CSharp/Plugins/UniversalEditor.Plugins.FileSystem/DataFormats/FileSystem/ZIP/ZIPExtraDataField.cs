using System;
namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	public class ZIPExtraDataField
	{
		public class ZIPExtraDataFieldCollection
			: System.Collections.ObjectModel.Collection<ZIPExtraDataField>
		{
		}

		public byte [] LocalData {
			get { return GetLocalDataInternal (); }
			set { SetLocalDataInternal (value); }
		}
		public byte [] CentralData {
			get { return GetCentralDataInternal (); }
			set { SetCentralDataInternal (value); }
		}

		private byte [] _LocalData = new byte [0];
		protected virtual byte [] GetLocalDataInternal ()
		{
			return _LocalData;
		}
		protected virtual void SetLocalDataInternal (byte[] value)
		{
			_LocalData = value;
		}

		private byte [] _CentralData = new byte [0];
		protected virtual byte [] GetCentralDataInternal ()
		{
			return _CentralData;
		}
		protected virtual void SetCentralDataInternal (byte [] value)
		{
			_CentralData = value;
		}

		public short TypeCode { get; set; }
		public ZIPExtraDataFieldType Type { get { return (ZIPExtraDataFieldType)TypeCode; } set { TypeCode = (short)value; } }
	}
}
