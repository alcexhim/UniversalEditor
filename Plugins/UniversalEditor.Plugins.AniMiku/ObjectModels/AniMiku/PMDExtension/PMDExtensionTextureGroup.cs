//
//  PMDExtensionTextureGroup.cs - represents a group of textures for texture animation
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2014-2020 Mike Becker's Software
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
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.ObjectModels.AniMiku.PMDExtension
{
	/// <summary>
	/// Represents a group of textures for texture animation.
	/// </summary>
	public class PMDExtensionTextureGroup : ICloneable
	{
		public class PMDExtensionTextureGroupCollection
			: System.Collections.ObjectModel.Collection<PMDExtensionTextureGroup>
		{
		}

		private string mvarName = String.Empty;
		/// <summary>
		/// The name of the archive file associated with this PMD extension.
		/// </summary>
		public string ArchiveFileName { get { return mvarName; } set { mvarName = value; } }

		private System.Collections.Specialized.StringCollection mvarTextureImageFileNames = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// The file name(s) of texture(s) associated with this PMD extension.
		/// </summary>
		public System.Collections.Specialized.StringCollection TextureImageFileNames { get { return mvarTextureImageFileNames; } }

		public object Clone()
		{
			PMDExtensionTextureGroup clone = new PMDExtensionTextureGroup();
			clone.ArchiveFileName = (mvarName.Clone() as string);
			foreach (string s in mvarTextureImageFileNames)
			{
				clone.TextureImageFileNames.Add(s.Clone() as string);
			}
			return clone;
		}

		private ModelMaterial mvarMaterial = null;
		public ModelMaterial Material { get { return mvarMaterial; } set { mvarMaterial = value; } }
	}
}
