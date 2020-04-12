//
//  SceneModelReference.cs - represents a reference to a 3D model in a 3D scene
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
using System.Collections.ObjectModel;

namespace UniversalEditor.ObjectModels.Multimedia3D.Scene
{
	/// <summary>
	/// Represents a reference to a 3D model in a 3D scene.
	/// </summary>
	public class SceneModelReference : ICloneable
	{
		public class SceneModelReferenceCollection : Collection<SceneModelReference>
		{
			public SceneModelReference Add(string ModelName)
			{
				SceneModelReference model = new SceneModelReference();
				model.ModelName = ModelName;
				base.Add(model);
				return model;
			}
		}

		/// <summary>
		/// Gets or sets the name of the model referenced by this <see cref="SceneModelReference" />.
		/// </summary>
		/// <value>The name of the model referenced by this <see cref="SceneModelReference" />.</value>
		public string ModelName { get; set; } = string.Empty;
		/// <summary>
		/// Gets or sets the full path of the model file referenced by this <see cref="SceneModelReference" />.
		/// </summary>
		/// <value>The full path of the model file referenced by this <see cref="SceneModelReference" />.</value>
		public string FileName { get; set; } = string.Empty;

		public object Clone()
		{
			return new SceneModelReference
			{
				FileName = this.FileName,
				ModelName = this.ModelName
			};
		}
	}
}
