//
//  ModelParameter.cs - the abstract base class from which all named parameters that control various aspects of a 3D model derive
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
	/// The abstract base class from which all named parameters that control various aspects of a 3D model derive.
	/// </summary>
	public abstract class ModelParameter : ICloneable
	{
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="ModelParameterAttachment" /> instances describing how this <see cref="ModelParameter" /> corresponds to named properties of a 3D model.
		/// </summary>
		/// <value>The attachments associated with this <see cref="ModelParameter" />.</value>
		public ModelParameterAttachment.ModelParameterAttachmentCollection Attachments { get; } = new ModelParameterAttachment.ModelParameterAttachmentCollection();

		public abstract object Clone();
	}
}
