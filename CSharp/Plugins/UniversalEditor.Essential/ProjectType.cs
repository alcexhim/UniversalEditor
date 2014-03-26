using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class ProjectType
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarLargeIconImageFileName = null;
		public string LargeIconImageFileName { get { return mvarLargeIconImageFileName; } set { mvarLargeIconImageFileName = value; } }

		private string mvarSmallIconImageFileName = null;
		public string SmallIconImageFileName { get { return mvarSmallIconImageFileName; } set { mvarSmallIconImageFileName = value; } }
	}
}
