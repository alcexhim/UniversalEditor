using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Fixture
{
	public class FixtureObjectModel : ObjectModel
	{
		private ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "Fixture";
				_omr.Path = new string[] { "Lighting", "Fixture" };
			}
			return _omr;
        }

        private Guid mvarID = Guid.Empty;
        public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarManufacturer = String.Empty;
		public string Manufacturer { get { return mvarManufacturer; } set { mvarManufacturer = value; } }

		private string mvarModel = String.Empty;
		public string Model { get { return mvarModel; } set { mvarModel = value; } }

		private string mvarType = String.Empty;
		public string Type { get { return mvarType; } set { mvarType = value; } }

		private Channel.ChannelCollection mvarChannels = new Channel.ChannelCollection();
		public Channel.ChannelCollection Channels { get { return mvarChannels; } }

		private Mode.ModeCollection mvarModes = new Mode.ModeCollection();
		public Mode.ModeCollection Modes { get { return mvarModes; } }

        private FixtureTask.FixtureTaskCollection mvarTasks = new FixtureTask.FixtureTaskCollection();
        public FixtureTask.FixtureTaskCollection Tasks { get { return mvarTasks; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(mvarManufacturer);
            sb.Append(" ");
            sb.Append(mvarModel);
            return sb.ToString();
        }

		public override void Clear()
		{
            mvarID = Guid.Empty;
			mvarManufacturer = String.Empty;
			mvarModel = String.Empty;
			mvarType = String.Empty;
			mvarChannels.Clear();
			mvarModes.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			FixtureObjectModel clone = (where as FixtureObjectModel);
            clone.ID = mvarID;
			clone.Manufacturer = (mvarManufacturer.Clone() as string);
			clone.Model = (mvarModel.Clone() as string);
			clone.Type = (mvarType.Clone() as string);
			foreach (Channel channel in mvarChannels)
			{
				clone.Channels.Add(channel.Clone() as Channel);
			}
			foreach (Mode mode in mvarModes)
			{
				clone.Modes.Add(mode.Clone() as Mode);
			}
		}
    }
}
