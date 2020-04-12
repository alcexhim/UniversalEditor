//
//  TOCNode.cs - represents a node in a TableOfContentsObjectModel
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

using System;

namespace UniversalEditor.ObjectModels.Help.TableOfContents
{
	/// <summary>
	/// Represents a node in a <see cref="TableOfContentsObjectModel" />.
	/// </summary>
	public class TOCNode
	{
		public class TOCNodeCollection
			: System.Collections.ObjectModel.Collection<TOCNode>
		{

		}

		/// <summary>
		/// The location of the Help topic pointed to by this <see cref="TOCNode" />.
		/// </summary>
		public string Location { get; set; } = String.Empty;
		/// <summary>
		/// The title of the Help topic pointed to by this <see cref="TOCNode" />.
		/// </summary>
		public string Title { get; set; } = String.Empty;

		/// <summary>
		/// Gets a collection of <see cref="TOCNode" /> instances representing the child nodes contained by this <see cref="TOCNode" />.
		/// </summary>
		/// <value>The child nodes contained by this <see cref="TOCNode" />.</value>
		public TOCNodeCollection Nodes { get; } = new TOCNodeCollection();
	}
}
