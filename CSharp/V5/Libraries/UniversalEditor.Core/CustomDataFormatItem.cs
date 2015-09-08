using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor
{
	public abstract class CustomDataFormatItem
	{
		public class CustomDataFormatItemCollection
			: System.Collections.ObjectModel.Collection<CustomDataFormatItem>
		{
		}

		private string mvarName = null;
		public string Name { get { return mvarName; } set { mvarName = value; } }
	}
	public class CustomDataFormatItemField : CustomDataFormatItem
	{
		private string mvarExportTarget = null;
		public string ExportTarget { get { return mvarExportTarget; } set { mvarExportTarget = value; } }

		private string mvarDataType = String.Empty;
		public string DataType { get { return mvarDataType; } set { mvarDataType = value; } }

		private int? mvarLength = null;
		public int? Length { get { return mvarLength; } set { mvarLength = value; } }

		private IO.Encoding mvarEncoding = null;
		public IO.Encoding Encoding { get { return mvarEncoding; } set { mvarEncoding = value; } }

		private string mvarValue = null;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }
	}
	public class CustomDataFormatItemArray : CustomDataFormatItem
	{
		private string mvarDataType = String.Empty;
		public string DataType { get { return mvarDataType; } set { mvarDataType = value; } }

		private int mvarLength = 0;
		public int Length { get { return mvarLength; } set { mvarLength = value; } }
	}
}
