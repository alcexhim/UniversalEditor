//
//  ModelParameterAttachment.cs - represents a parameter attachment for a 3D model
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a parameter attachment for a 3D model.
	/// </summary>
	public class ModelParameterAttachment : ICloneable
	{
		public class ModelParameterAttachmentCollection
			: System.Collections.ObjectModel.Collection<ModelParameterAttachment>
		{
		}

		/// <summary>
		/// Gets or sets the 3D model object to which this <see cref="ModelParameterAttachment" /> is attached.
		/// </summary>
		/// <value>The 3D model object to which this <see cref="ModelParameterAttachment" /> is attached.</value>
		public IModelObject AttachedObject { get; set; } = null;

		/// <summary>
		/// Gets or sets the name of the property to which this <see cref="ModelParameterAttachment" /> applies.
		/// </summary>
		/// <value>The name of the property to which this <see cref="ModelParameterAttachment" /> applies.</value>
		public string PropertyName { get; set; } = String.Empty;

		public object Clone()
		{
			ModelParameterAttachment clone = new ModelParameterAttachment();
			clone.AttachedObject = AttachedObject;
			clone.PropertyName = (PropertyName.Clone() as string);
			return clone;
		}
	}
}
