using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Fixture
{
    public class FixtureTaskChannel : ICloneable
    {
        private Channel mvarChannel = null;
        public Channel Channel { get { return mvarChannel; } set { mvarChannel = value; } }

        private byte mvarValue = 0;
        public byte Value { get { return mvarValue; } set { mvarValue = value; } }

        public object Clone()
        {
            FixtureTaskChannel clone = new FixtureTaskChannel();
            clone.Channel = mvarChannel;
            clone.Value = mvarValue;
            return clone;
        }

        public class FixtureTaskChannelCollection
            : System.Collections.ObjectModel.Collection<FixtureTaskChannel>
        {
            public FixtureTaskChannel Add(Channel channel)
            {
                return Add(channel, 0);
            }
            public FixtureTaskChannel Add(Channel channel, byte value)
            {
                FixtureTaskChannel item = new FixtureTaskChannel();
                item.Channel = channel;
                item.Value = value;
                Add(item);
                return item;
            }
        }
    }
}
