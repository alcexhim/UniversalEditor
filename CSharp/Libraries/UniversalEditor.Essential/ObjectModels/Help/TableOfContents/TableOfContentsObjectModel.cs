using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Help.TableOfContents
{
	public class TableOfContentsObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Table of Contents";
				_omr.Path = new string[] { "Help", "Table of Contents" };
			}
			return _omr;
		}

		private TOCNode.TOCNodeCollection mvarNodes = new TOCNode.TOCNodeCollection();
		public TOCNode.TOCNodeCollection Nodes { get { return mvarNodes; } }

		public override void Clear()
		{
			mvarNodes.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			TableOfContentsObjectModel clone = (where as TableOfContentsObjectModel);
			if (where == null) throw new ObjectModelNotSupportedException();

			foreach (TOCNode node in mvarNodes)
			{
				clone.Nodes.Add(node);
			}
		}
	}
}
