using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.ObjectModels.Lighting.Fixture
{
	public class Capability : ICloneable
	{
		public class CapabilityCollection
			: System.Collections.ObjectModel.Collection<Capability>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private byte mvarMinimumValue = 0;
		public byte MinimumValue { get { return mvarMinimumValue; } set { mvarMinimumValue = value; } }

		private byte mvarMaximumValue = 255;
		public byte MaximumValue { get { return mvarMaximumValue; } set { mvarMaximumValue = value; } }

        private byte mvarDefaultValue = 0;
        public byte DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private Color mvarColor = Color.Empty;
		public Color Color { get { return mvarColor; } set { mvarColor = value; } }

		private string mvarImageFileName = String.Empty;
		public string ImageFileName { get { return mvarImageFileName; } set { mvarImageFileName = value; } }

		public object Clone()
		{
			Capability clone = new Capability();
			clone.Color = mvarColor;
			clone.ImageFileName = (mvarImageFileName.Clone() as string);
			clone.MaximumValue = mvarMaximumValue;
			clone.MinimumValue = mvarMinimumValue;
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}

        private PictureObjectModel mvarImage = null;
        public PictureObjectModel Image { get { return mvarImage; } set { mvarImage = value; } }
    }
}
