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
			public CustomDataFormatItem this[string name]
			{
				get
				{
					foreach (CustomDataFormatItem item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
		}
		public class CustomDataFormatItemReadOnlyCollection
			: System.Collections.ObjectModel.ReadOnlyCollection<CustomDataFormatItem>
		{
			public CustomDataFormatItemReadOnlyCollection(IList<CustomDataFormatItem> list) : base(list) { }
			
			public CustomDataFormatItem this[string name]
			{
				get
				{
					foreach (CustomDataFormatItem item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
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

		private Guid mvarStructureID = Guid.Empty;
		public Guid StructureID { get { return mvarStructureID; } set { mvarStructureID = value; } }

		private int? mvarLength = null;
		public int? Length { get { return mvarLength; } set { mvarLength = value; } }

		private IO.Encoding mvarEncoding = null;
		public IO.Encoding Encoding { get { return mvarEncoding; } set { mvarEncoding = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		private CustomDataFormatFieldCondition mvarFieldCondition = null;
		public CustomDataFormatFieldCondition FieldCondition { get { return mvarFieldCondition; } set { mvarFieldCondition = value; } }

		public CustomDataFormatItemField()
		{

		}
		public CustomDataFormatItemField(string name, string dataType)
		{
			Name = name;
			DataType = dataType;
		}
		public CustomDataFormatItemField(string name, string dataType, int length)
		{
			Name = name;
			DataType = dataType;
			Length = length;
		}
	}
	public class CustomDataFormatItemArray : CustomDataFormatItem
	{
		private string mvarDataType = String.Empty;
		public string DataType { get { return mvarDataType; } set { mvarDataType = value; } }

		private Guid mvarStructureID = Guid.Empty;
		public Guid StructureID { get { return mvarStructureID; } set { mvarStructureID = value; } }

		private string mvarLength = null;
		public string Length { get { return mvarLength; } set { mvarLength = value; } }

		private string mvarMaximumSize = null;
		public string MaximumSize { get { return mvarMaximumSize; } set { mvarMaximumSize = value; } }

	}
}
