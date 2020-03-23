using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Help.TableOfContents
{
	public class TOCNode
	{
		public class TOCNodeCollection
			: System.Collections.ObjectModel.Collection<TOCNode>
		{

		}

		private string mvarLocation = String.Empty;
		/// <summary>
		/// The location of the Help topic pointed to by this <see cref="TOCNode" />.
		/// </summary>
		public string Location { get { return mvarLocation; } set { mvarLocation = value; } }

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the Help topic pointed to by this <see cref="TOCNode" />.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private TOCNode.TOCNodeCollection mvarNodes = new TOCNode.TOCNodeCollection();
		public TOCNode.TOCNodeCollection Nodes { get { return mvarNodes; } }
	}
}
