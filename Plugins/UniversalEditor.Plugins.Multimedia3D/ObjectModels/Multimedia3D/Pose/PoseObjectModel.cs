//
//  PoseObjectModel.cs - provides an ObjectModel for manipulating 3D model poses
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Pose
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating 3D model poses.
	/// </summary>
	public class PoseObjectModel : ObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Path = new string[] { "Multimedia", "3D Multimedia", "Pose" };
			omr.Description = "A pose that can be applied to a model in 3D space.";
			return omr;
		}

		public override void Clear()
		{
		}
		public override void CopyTo(ObjectModel where)
		{
		}

		/// <summary>
		/// The name of the model to which this pose applies. If not null, and this pose is applied to a model
		/// whose name does not match the model name of the pose, a warning will be issued and the bones may not
		/// match correctly.
		/// </summary>
		public string ModelName { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="PoseBone" /> instances representing position and rotation information for bones in this pose.
		/// </summary>
		/// <value>The position and rotation information for bones in this pose.</value>
		public PoseBone.PoseBoneCollection Bones { get; } = new PoseBone.PoseBoneCollection();
	}
}
