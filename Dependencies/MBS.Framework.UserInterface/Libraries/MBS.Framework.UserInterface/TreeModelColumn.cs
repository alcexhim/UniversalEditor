using System;
namespace MBS.Framework.UserInterface
{
	public class TreeModelColumn
	{
		public class TreeModelColumnCollection
			: System.Collections.ObjectModel.Collection<TreeModelColumn>
		{

		}

		private Type mvarDataType = null;
		public Type DataType {  get { return mvarDataType;  } set { mvarDataType = value; } }

		public TreeModelColumn(Type dataType)
		{
			mvarDataType = dataType;
		}
	}
}
