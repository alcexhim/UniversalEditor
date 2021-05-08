//
//  ModelBoneType.cs - indicates the type of bone in a 3D model
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Indicates the type of bone in a 3D model.
	/// </summary>
	public enum ModelBoneType
	{
		/// <summary>
		/// The bone type is unknown.
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// The bone allows rotation only.
		/// </summary>
		Rotate = 0,
		/// <summary>
		/// The bone allows rotation and movement.
		/// </summary>
		RotateMove = 1,
		/// <summary>
		/// The bone is an inverse kinematics bone.
		/// </summary>
		InverseKinematics = 2,
		/// <summary>
		/// The bone is a blank bone.
		/// </summary>
		Blank = 3,
		/// <summary>
		/// The bone is an inverse kinematics-influenced rotation bone.
		/// </summary>
		IKInfluencedRotation = 4,
		/// <summary>
		/// The bone is an influenced rotation bone.
		/// </summary>
		InfluencedRotation = 5,
		/// <summary>
		/// The bone is an inverse kinematics connection bone.
		/// </summary>
		IKConnect = 6,
		/// <summary>
		/// The bone is hidden.
		/// </summary>
		Hidden = 7,
		/// <summary>
		/// The bone is a twist bone.
		/// </summary>
		Twist = 8,
		/// <summary>
		/// The bone is a revolution bone.
		/// </summary>
		Revolution = 9
	}
}
