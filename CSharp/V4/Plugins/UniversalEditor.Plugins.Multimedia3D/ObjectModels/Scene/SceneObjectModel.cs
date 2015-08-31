using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Scene
{
	public class SceneObjectModel : ObjectModel
	{
        public override ObjectModelReference MakeReference()
        {
            ObjectModelReference omr = base.MakeReference();
            omr.Title = "Animation scene";
            omr.Path = new string[] { "Multimedia", "3D Multimedia", "3D Scene" };
            omr.Description = "Stores model settings and camera settings for an animated or static scene in 3D space.";
            return omr;
        }

        private uint mvarImageWidth = 512;
        public uint ImageWidth { get { return mvarImageWidth; } set { mvarImageWidth = value; } }
        private uint mvarImageHeight = 384;
        public uint ImageHeight { get { return mvarImageHeight; } set { mvarImageHeight = value; } }

        #region Application Settings
        private bool mvarFPSVisible = false;
        public bool FPSVisible { get { return mvarFPSVisible; } set { mvarFPSVisible = value; } }
        private bool mvarCoordinateAxisVisible = true;
        public bool CoordinateAxisVisible { get { return mvarCoordinateAxisVisible; } set { mvarCoordinateAxisVisible = value; } }
        private bool mvarGroundShadowVisible = true;
        public bool GroundShadowVisible { get { return mvarGroundShadowVisible; } set { mvarGroundShadowVisible = value; } }
        private bool mvarGroundShadowTransparent = false;
        public bool GroundShadowTransparent { get { return mvarGroundShadowTransparent; } set { mvarGroundShadowTransparent = value; } }
        private SceneScreenCaptureMode mvarScreenCaptureMode = SceneScreenCaptureMode.None;
        public SceneScreenCaptureMode ScreenCaptureMode { get { return mvarScreenCaptureMode; } set { mvarScreenCaptureMode = value; } }
        private float mvarGroundShadowBrightness = 1.0f;
        public float GroundShadowBrightness { get { return mvarGroundShadowBrightness; } set { mvarGroundShadowBrightness = value; } }
        #endregion

        private SceneModelReference.SceneModelReferenceCollection mvarModels = new SceneModelReference.SceneModelReferenceCollection();
        public SceneModelReference.SceneModelReferenceCollection Models { get { return mvarModels; } }

		public override void CopyTo(ObjectModel destination)
		{
			SceneObjectModel clone = destination as SceneObjectModel;
			foreach (SceneModelReference smr in this.mvarModels)
			{
				clone.Models.Add(smr.Clone() as SceneModelReference);
			}

            clone.ImageWidth = mvarImageWidth;
            clone.ImageHeight = mvarImageHeight;
		}
		public override void Clear()
		{
			mvarModels.Clear();

            mvarImageWidth = 512;
            mvarImageHeight = 384;
		}

        private float mvarFPSLimit = 60.0f;
        /// <summary>
        /// The frame rate limit 
        /// </summary>
        public float FPSLimit { get { return mvarFPSLimit; } set { mvarFPSLimit = value; } }
    }
}
