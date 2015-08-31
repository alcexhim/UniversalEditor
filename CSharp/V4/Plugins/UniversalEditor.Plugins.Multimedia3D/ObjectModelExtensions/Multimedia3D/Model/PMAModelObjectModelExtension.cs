using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace UniversalEditor.ObjectModelExtensions.Multimedia3D.Model
{
    public class PMAModelObjectModelExtension // : ObjectModelExtension
	{
		/*
        public override void Clear()
        {
            mvarEnabled = false;
            mvarVersion = new Version(1, 0, 0, 0);
            mvarTextureFlipping = new TextureFlippingInformation();
        }
        public override void CopyTo(ObjectModelExtension where)
        {
            PMAModelObjectModelExtension clone = (where as PMAModelObjectModelExtension);
            if (clone == null) return;

            clone.Enabled = mvarEnabled;
            clone.Version = (mvarVersion.Clone() as Version);
            clone.mvarTextureFlipping = (mvarTextureFlipping.Clone() as TextureFlippingInformation);
        }
		*/

		private bool mvarEnabled = false;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private Version mvarVersion = new Version(1, 0, 0, 0);
        public Version Version { get { return mvarVersion; } set { mvarVersion = value; } }

		private TextureFlippingInformation mvarTextureFlipping = new TextureFlippingInformation();
        public TextureFlippingInformation TextureFlipping { get { return mvarTextureFlipping; } }
	}
}
