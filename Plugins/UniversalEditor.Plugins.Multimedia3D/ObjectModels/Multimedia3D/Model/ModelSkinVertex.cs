//
//  ModelSkinVertex.cs - represents a skin vertex for a 3D model
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
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a skin vertex for a 3D model.
	/// </summary>
	public class ModelSkinVertex : ICloneable
	{
		public class ModelSkinVertexCollection : Collection<ModelSkinVertex>
		{
		}

		/// <summary>
		/// Gets or sets the target vertex associated with the skin.
		/// </summary>
		/// <value>The target vertex associated with the skin.</value>
		public ModelVertex TargetVertex { get; set; } = null;
		/// <summary>
		/// Gets or sets the maximum position for the skin vertex.
		/// </summary>
		/// <value>The maximum position for the skin vertex.</value>
		public PositionVector3 MaximumPosition { get; set; } = new PositionVector3();

		public object Clone()
		{
			ModelSkinVertex clone = new ModelSkinVertex();
			clone.TargetVertex = (TargetVertex.Clone() as ModelVertex);
			clone.MaximumPosition = (PositionVector3)MaximumPosition.Clone();
			return clone;
		}
	}
}
