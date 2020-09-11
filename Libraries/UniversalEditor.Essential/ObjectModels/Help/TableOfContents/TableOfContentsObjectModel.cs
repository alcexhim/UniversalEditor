//
//  TableOfContentsObjectModel.cs - provides an ObjectModel for manipulating a Table of Contents for a help file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.ObjectModels.Help.TableOfContents
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating a Table of Contents for a help file.
	/// </summary>
	public class TableOfContentsObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Help", "Table of Contents" };
			}
			return _omr;
		}

		/// <summary>
		/// Gets a collection of <see cref="TOCNode" /> instances representing the nodes in this <see cref="TableOfContentsObjectModel" />.
		/// </summary>
		/// <value>The nodes in this <see cref="TableOfContentsObjectModel" />.</value>
		public TOCNode.TOCNodeCollection Nodes { get; } = new TOCNode.TOCNodeCollection();

		public override void Clear()
		{
			Nodes.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			TableOfContentsObjectModel clone = (where as TableOfContentsObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (TOCNode node in Nodes)
			{
				clone.Nodes.Add(node);
			}
		}
	}
}
