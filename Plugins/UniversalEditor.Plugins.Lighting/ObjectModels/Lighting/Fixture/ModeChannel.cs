using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Fixture
{
	public class ModeChannel : ICloneable
	{
		public class ModeChannelCollection
			: System.Collections.ObjectModel.Collection<ModeChannel>
		{
			public ModeChannel this[Guid ID]
			{
				get
				{
					foreach (ModeChannel channel in this)
					{
						if (channel.Channel.ID == ID) return channel;
					}
					return null;
				}
			}
		}

		private int mvarRelativeAddress = 0;
		public int RelativeAddress { get { return mvarRelativeAddress; } set { mvarRelativeAddress = value; } }

		private Channel mvarChannel = null;
		public Channel Channel { get { return mvarChannel; } set { mvarChannel = value; } }

		public object Clone()
		{
			ModeChannel clone = new ModeChannel();
			clone.RelativeAddress = mvarRelativeAddress;
			clone.Channel = mvarChannel;
			return clone;
		}
	}
}
