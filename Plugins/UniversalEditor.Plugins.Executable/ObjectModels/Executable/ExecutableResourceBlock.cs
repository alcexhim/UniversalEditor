using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	public enum ExecutableResourceType
	{
		Custom = -1,
		// NewResource = 0x2000,
		// Error = 0x7fff,
		Cursor = 1,
		Bitmap =2,
		Icon = 3,
		Menu = 4,
		Dialog = 5,
		String =6,
		FontDir = 7,
		Font = 8,
		Accelerator = 9,
		RCData = 10,
		MessageTable = 11,
		GroupCursor = 12,
		GroupIcon = 14,
		Version = 16 // ,
		// NewBitmap = (Bitmap | NewResource),
		// NewMenu = (Menu | NewResource),
		// NewDialog = (Dialog | NewResource)
	}
	public abstract class ExecutableResourceBlock : ICloneable
	{
		public class ExecutableResourceBlockCollection
			: System.Collections.ObjectModel.Collection<ExecutableResourceBlock>
		{
		}

		public abstract ExecutableResourceType Type { get; }

		private int mvarID = 0;
		public int ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public abstract object Clone();
	}
}
