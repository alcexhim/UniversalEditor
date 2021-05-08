//
//  RawMotionFrameComparer.cs - internal comparer for comparing RawMotionFrame instances
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

using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion.Internal
{
	/// <summary>
	/// Internal comparer for comparing <see cref="RawMotionFrame" /> instances.
	/// </summary>
	internal class RawMotionFrameComparer : Comparer<RawMotionFrame>
	{
		public override int Compare(RawMotionFrame x, RawMotionFrame y)
		{
			return x.Index.CompareTo(y.Index);
		}
	}
}
