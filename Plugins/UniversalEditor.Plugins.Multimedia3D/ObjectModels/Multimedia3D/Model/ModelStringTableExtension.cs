//
//  ModelStringTableExtension.cs - describes translatable string information for a 3D model
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Describes translatable string information for a 3D model.
	/// </summary>
	public class ModelStringTableExtension : ICloneable
	{
		/// <summary>
		/// Gets or sets the title of the model.
		/// </summary>
		/// <value>The title of the model.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the name of the individual or organization that created the model.
		/// </summary>
		/// <value>The name of the individual or organization that created the model.</value>
		public string Author { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the user-defined comments for the model.
		/// </summary>
		/// <value>The user-defined comments for the model.</value>
		public string Comments { get; set; } = String.Empty;
		/// <summary>
		/// Gets a list of bone names for the model.
		/// </summary>
		/// <value>The bone names for the model.</value>
		public List<string> BoneNames { get; } = new List<string>();
		/// <summary>
		/// Gets a list of skin names for the model.
		/// </summary>
		/// <value>The skin names for the model.</value>
		public List<string> SkinNames { get; } = new List<string>();
		/// <summary>
		/// Gets a list of material names for the model.
		/// </summary>
		/// <value>The material names for the model.</value>
		public List<string> MaterialNames { get; } = new List<string>();
		/// <summary>
		/// Gets a list of node names for the model.
		/// </summary>
		/// <value>The node names for the model.</value>
		public List<string> NodeNames { get; } = new List<string>();

		public object Clone()
		{
			ModelStringTableExtension clone = new ModelStringTableExtension();
			clone.Title = Title.Clone() as string;
			clone.Author = Author.Clone() as string;
			clone.Comments = Comments.Clone() as string;
			foreach (string s in BoneNames)
			{
				clone.BoneNames.Add(s.Clone() as string);
			}
			foreach (string s in SkinNames)
			{
				clone.SkinNames.Add(s.Clone() as string);
			}
			foreach (string s in NodeNames)
			{
				clone.NodeNames.Add(s.Clone() as string);
			}
			foreach (string s in MaterialNames)
			{
				clone.MaterialNames.Add(s.Clone() as string);
			}
			return clone;
		}
	}
}
