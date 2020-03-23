using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Motion
{
	public class MotionObjectModel : ObjectModel
    {
        private System.Collections.Specialized.StringCollection mvarCompatibleModelNames = new System.Collections.Specialized.StringCollection();
        /// <summary>
        /// The name(s) of the model(s) which are compatible with this motion data.
        /// </summary>
        public System.Collections.Specialized.StringCollection CompatibleModelNames { get { return mvarCompatibleModelNames; } }

		private MotionFrame.MotionFrameCollection mvarFrames = new MotionFrame.MotionFrameCollection();
		public MotionFrame.MotionFrameCollection Frames { get { return mvarFrames; } }

		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Motion capture data";
            omr.Path = new string[] { "Multimedia", "3D Multimedia", "Motion Capture Data" };
            omr.Description = "Motion capture data that can be used to animate a model in 3D space.";
			return omr;
		}

		public override void Clear()
		{
			mvarFrames.Clear();
		}
		public override void CopyTo(ObjectModel destination)
		{
			MotionObjectModel clone = (destination as MotionObjectModel);
			foreach (MotionFrame frame in mvarFrames)
			{
				clone.Frames.Add(frame.Clone() as MotionFrame);
			}
		}

        public void ReplaceBoneNames(string FindBoneName, string ReplaceBoneName)
        {
            foreach (MotionFrame frame in mvarFrames)
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
            foreach (MotionFrame frame in mvarFrames)
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
                mvarFrames.Remove(frame);
            }
        }
    }
}
