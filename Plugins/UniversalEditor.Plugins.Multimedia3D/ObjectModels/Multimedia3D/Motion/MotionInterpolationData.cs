//
//  MotionInterpolationData.cs - describes interpolation data for a 3D animation
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion
{
	/// <summary>
	/// Describes interpolation data for a 3D animation.
	/// </summary>
	public class MotionInterpolationData
	{
		public MotionInterpolationData()
		{
			XAxis.Name = "X-axis interpolation bezier curve";
			YAxis.Name = "Y-axis interpolation bezier curve";
			ZAxis.Name = "Z-axis interpolation bezier curve";
			Rotation.Name = "Rotation interpolation bezier curve";
		}
		/// <summary>
		/// Gets the X-axis interpolation bezier curve.
		/// </summary>
		/// <value>The X-axis interpolation bezier curve.</value>
		public Neo.BezierCurve XAxis { get; } = new Neo.BezierCurve();
		/// <summary>
		/// Gets the Y-axis interpolation bezier curve.
		/// </summary>
		/// <value>The Y-axis interpolation bezier curve.</value>
		public Neo.BezierCurve YAxis { get; } = new Neo.BezierCurve();
		/// <summary>
		/// Gets the Z-axis interpolation bezier curve.
		/// </summary>
		/// <value>The Z-axis interpolation bezier curve.</value>
		public Neo.BezierCurve ZAxis { get; } = new Neo.BezierCurve();
		/// <summary>
		/// Gets the rotation interpolation bezier curve.
		/// </summary>
		/// <value>The rotation interpolation bezier curve.</value>
		public Neo.BezierCurve Rotation { get; } = new Neo.BezierCurve();
	}
}
