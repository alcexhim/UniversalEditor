//
//  MotionFrame.cs - describes animation actions for a single frame in a 3D animation
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
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Motion
{
	/// <summary>
	/// Describes animation actions for a single frame in a 3D animation.
	/// </summary>
	public class MotionFrame : ICloneable
	{
		public class MotionFrameCollection
			: System.Collections.ObjectModel.Collection<MotionFrame>
		{
		}

		/// <summary>
		/// Gets or sets the frame index.
		/// </summary>
		/// <value>The frame index.</value>
		public uint Index { get; set; } = 0;
		/// <summary>
		/// Gets a collection of <see cref="MotionAction" /> instances representing the actions to be applied during this frame.
		/// </summary>
		/// <value>The actions to be applied during this frame.</value>
		public MotionAction.MotionActionCollection Actions { get; } = new MotionAction.MotionActionCollection();

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Frame ");
			sb.Append(Index);
			sb.Append(" (");
			sb.Append(Actions.Count);
			sb.Append(" actions)");
			return sb.ToString();
		}

		public object Clone()
		{
			MotionFrame clone = new MotionFrame();
			clone.Index = Index;
			foreach (MotionAction action in Actions)
			{
				clone.Actions.Add(action.Clone() as MotionAction);
			}
			return clone;
		}
	}
}
