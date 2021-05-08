using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public class Perspective
	{
		public class PerspectiveCollection
			: System.Collections.ObjectModel.Collection<Perspective>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private List<ObjectModel> mvarObjectModels = new List<ObjectModel>();
		public List<ObjectModel> ObjectModels { get { return mvarObjectModels; } }
	}
}
