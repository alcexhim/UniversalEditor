//
//  MVDBoneData.cs - internal structure describing bone data in a MikuMikuMoving Motion Vector Data (MVD) animation file
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

namespace UniversalEditor.DataFormats.Multimedia3D.Motion.MotionVectorData.Internal
{
	/// <summary>
	/// Internal structure describing bone data in a MikuMikuMoving Motion Vector Data (MVD) animation file.
	/// </summary>
	public class MVDBoneData
	{
		/// <summary>
		/// Gets or sets the index of the associated frame.
		/// </summary>
		/// <value>The index of the associated frame.</value>
		public uint FrameIndex { get; set; } = 0;
		/// <summary>
		/// Gets or sets the name of the associated bone.
		/// </summary>
		/// <value>The name of the associated bone.</value>
		public string BoneName { get; set; } = String.Empty;

		public override string ToString()
		{
			return FrameIndex.ToString() + ": " + BoneName;
		}
	}
}
