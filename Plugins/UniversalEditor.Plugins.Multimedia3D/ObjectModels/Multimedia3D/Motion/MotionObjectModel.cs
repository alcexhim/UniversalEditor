//
//  MotionObjectModel.cs - provides an ObjectModel for manipulating 3D animation data
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
	/// Provides an <see cref="ObjectModel" /> for manipulating 3D animation data.
	/// </summary>
	public class MotionObjectModel : ObjectModel
	{
		/// <summary>
		/// The name(s) of the model(s) which are compatible with this motion data.
		/// </summary>
		public System.Collections.Specialized.StringCollection CompatibleModelNames { get; } = new System.Collections.Specialized.StringCollection();
		public MotionFrame.MotionFrameCollection Frames { get; } = new MotionFrame.MotionFrameCollection();

		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Path = new string[] { "Multimedia", "3D Multimedia", "Motion capture data" };
			omr.Description = "Motion capture data that can be used to animate a model in 3D space.";
			return omr;
		}

		public override void Clear()
		{
			Frames.Clear();
		}
		public override void CopyTo(ObjectModel destination)
		{
			MotionObjectModel clone = (destination as MotionObjectModel);
			foreach (MotionFrame frame in Frames)
			{
				clone.Frames.Add(frame.Clone() as MotionFrame);
			}
		}

		public void ReplaceBoneNames(string FindBoneName, string ReplaceBoneName)
		{
			foreach (MotionFrame frame in Frames)
			{
				foreach (MotionAction act in frame.Actions)
				{
					if (act is MotionBoneRepositionAction)
					{
						MotionBoneRepositionAction repos = (act as MotionBoneRepositionAction);
						if (repos.BoneName == FindBoneName)
						{
							repos.BoneName = ReplaceBoneName;
						}
					}
				}
			}
		}

		public void RemoveAllBoneReferences(string FindBoneName)
		{
			System.Collections.Generic.List<MotionFrame> framesToDelete = new System.Collections.Generic.List<MotionFrame>();
			foreach (MotionFrame frame in Frames)
			{
				for (int i = 0; i < frame.Actions.Count; i++)
				{
					MotionBoneRepositionAction repos = (frame.Actions[i] as MotionBoneRepositionAction);
					if (repos != null)
					{
						if (repos.BoneName == FindBoneName)
						{
							frame.Actions.Remove(repos);
							i--;
						}
					}
				}
				if (frame.Actions.Count == 0)
				{
					framesToDelete.Add(frame);
				}
			}
			foreach (MotionFrame frame in framesToDelete)
			{
				Frames.Remove(frame);
			}
		}
	}
}
