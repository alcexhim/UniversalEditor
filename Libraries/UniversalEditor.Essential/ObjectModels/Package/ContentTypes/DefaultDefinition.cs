//
//  DefaultDefinition.cs - defines a content type for all files of a particular extension in an Open Packaging Convention document
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
using System.Text;

namespace UniversalEditor.ObjectModels.Package.ContentTypes
{
	/// <summary>
	/// Defines a content type for all files of a particular extension in an Open Packaging Convention document.
	/// </summary>
	public class DefaultDefinition : ICloneable
	{
		public class DefaultDefinitionCollection
			: System.Collections.ObjectModel.Collection<DefaultDefinition>
		{
			public DefaultDefinition Add (string extension, string contentType)
			{
				DefaultDefinition item = new DefaultDefinition ();
				item.Extension = extension;
				item.ContentType = contentType;
				Add (item);
				return item;
			}
		}

		public string Extension { get; set; } = String.Empty;
		public string ContentType { get; set; } = String.Empty;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("*.");
			sb.Append(Extension);
			sb.Append("; ");
			sb.Append(ContentType);
			return sb.ToString();
		}

		public object Clone()
		{
			DefaultDefinition clone = new DefaultDefinition();
			clone.ContentType = (ContentType.Clone() as string);
			clone.Extension = (Extension.Clone() as string);
			return clone;
		}
	}
}
