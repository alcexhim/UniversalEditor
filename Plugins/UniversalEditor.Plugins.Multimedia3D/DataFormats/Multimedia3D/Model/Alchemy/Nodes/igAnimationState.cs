//
//  igAnimationState.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igAnimationState : igBase
	{
		public igAnimationState()
		{
		}

		public igAnimation Animation { get; internal set; }
		public uint CombineMode { get; internal set; }
		public uint TransitionMode { get; internal set; }
		public uint Status { get; internal set; }
		public uint BaseState { get; internal set; }
		public uint New { get; internal set; }
		public float CurrentBlendRatio { get; internal set; }
		public ulong LocalTime { get; internal set; }
		public ulong BaseTransitionTime { get; internal set; }
		public float TimeScale { get; internal set; }
		public ulong TimeBias { get; internal set; }
		public ulong BlendStartTime { get; internal set; }
		public float BlendStartRatio { get; internal set; }
		public float BlendRatioRange { get; internal set; }
		public ulong BlendDuration { get; internal set; }
		public ulong AnimationStartTime { get; internal set; }
		public uint CycleMatchTargetState { get; internal set; }
		public uint IsCycleMatchTarget { get; internal set; }
		public uint CycleMatchDisable { get; internal set; }
		public uint ManualCycleMatch { get; internal set; }
		public ulong CycleMatchDuration { get; internal set; }
		public uint CycleMatchDurationRange { get; internal set; }
		public ulong CycleMatchTargetDuration { get; internal set; }
		public uint FastCacheDecodingState { get; internal set; }
	}
}
