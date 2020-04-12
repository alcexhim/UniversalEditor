//
//  ModelAngleLimit.cs - defines upper and lower limits for an angle in a 3D model
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
	/// Defines upper and lower limits for an angle in a 3D model.
	/// </summary>
	public class ModelAngleLimit
	{
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.Multimedia3D.Model.ModelAngleLimit"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled { get; set; } = false;
		/// <summary>
		/// Gets or sets the lower bound of this <see cref="ModelAngleLimit" />.
		/// </summary>
		/// <value>The lower bound of this <see cref="ModelAngleLimit" />.</value>
		public PositionVector3 Lower { get; set; } = PositionVector3.Empty;
		/// <summary>
		/// Gets or sets the upper bound of this <see cref="ModelAngleLimit" />.
		/// </summary>
		/// <value>The upper bound of this <see cref="ModelAngleLimit" />.</value>
		public PositionVector3 Upper { get; set; } = PositionVector3.Empty;
	}
}
