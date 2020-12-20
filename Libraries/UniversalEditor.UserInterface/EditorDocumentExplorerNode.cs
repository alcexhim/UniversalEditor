//
//  EditorDocumentExplorerNode.cs - represents information about a node in a Document Explorer window for a particular Editor
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Collections.Generic;
using MBS.Framework;

namespace UniversalEditor.UserInterface
{
	public class EditorDocumentExplorerNode : ISupportsExtraData
	{
		public class EditorDocumentExplorerNodeCollection
			: System.Collections.ObjectModel.Collection<EditorDocumentExplorerNode>
		{
			public EditorDocumentExplorerNode Add(string text)
			{
				EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(text);
				Add(node);
				return node;
			}
		}

		public EditorDocumentExplorerNode(string text)
		{
			Text = text;
			Nodes = new EditorDocumentExplorerNodeCollection();
		}

		public EditorDocumentExplorerNode.EditorDocumentExplorerNodeCollection Nodes { get; private set; } = null;
		public string Text { get; set; }

		private Dictionary<string, object> _ExtraData = new Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey(key))
			{
				if (_ExtraData[key] is T)
					return (T)_ExtraData[key];
			}
			return defaultValue;
		}
		public void SetExtraData<T>(string key, T value)
		{
			_ExtraData[key] = value;
		}
		public object GetExtraData(string key, object defaultValue = null)
		{
			if (_ExtraData.ContainsKey(key))
				return _ExtraData[key];
			return defaultValue;
		}
		public void SetExtraData(string key, object value)
		{
			_ExtraData[key] = value;
		}
	}
}
