//
//  Reference.cs - represents a reference to another project file in a ProjectObjectModel
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

namespace UniversalEditor.ObjectModels.Project
{
	/// <summary>
	/// Represents a reference to another project file in a <see cref="ProjectObjectModel" />.
	/// </summary>
	public class Reference
	{
		public class ReferenceCollection
			: System.Collections.ObjectModel.Collection<Reference>
		{
			public Reference Add(string title, string fileName, Version version, Guid id)
			{
				Reference refer = new Reference();
				refer.Title = title;
				refer.FileName = fileName;
				refer.Version = version;
				refer.ID = id;
				base.Add(refer);
				return refer;
			}
		}

		/// <summary>
		/// Gets or sets the globally-unique identifier for this <see cref="Reference" />.
		/// </summary>
		/// <value>The globally-unique identifier for this <see cref="Reference" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the title of this <see cref="Reference" />.
		/// </summary>
		/// <value>The title of this <see cref="Reference" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the full path to the file pointed to by this <see cref="Reference" />.
		/// </summary>
		/// <value>The full path to the file pointed to by this <see cref="Reference" />.</value>
		public string FileName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the version of the referenced object pointed to by this <see cref="Reference" />.
		/// </summary>
		/// <value>The version of the referenced object pointed to by this <see cref="Reference" />.</value>
		public Version Version { get; set; } = new Version();

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Title);
			sb.Append(", ");
			sb.Append(Version.ToString());
			sb.Append(", ");
			sb.Append(ID.ToString("B"));
			sb.Append(", ");
			sb.Append(FileName);
			return sb.ToString();
		}
	}
}
