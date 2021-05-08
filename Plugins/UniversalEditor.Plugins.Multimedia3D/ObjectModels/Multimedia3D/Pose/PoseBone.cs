//
//  PoseBone.cs - describes bone positioning information for a 3D model pose
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
	/// Describes bone positioning information for a 3D model pose.
	/// </summary>
	public class PoseBone
	{
		public class PoseBoneCollection
			: System.Collections.ObjectModel.Collection<PoseBone>
		{
		}

		/// <summary>
		/// Gets or sets the bone identifier.
		/// </summary>
		/// <value>The bone identifier.</value>
		public string BoneID { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the name of the bone.
		/// </summary>
		/// <value>The name of the bone.</value>
		public string BoneName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the position of the bone.
		/// </summary>
		/// <value>The position of the bone.</value>
		public PositionVector3 Position { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Gets or sets the rotation of the bone.
		/// </summary>
		/// <value>The rotation of the bone.</value>
		public PositionVector4 Quaternion { get; set; } = new PositionVector4(0, 0, 0, 0);
	}
}
