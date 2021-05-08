using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Fixture
{
	public class Channel : ICloneable
	{
		public class ChannelCollection
			: System.Collections.ObjectModel.Collection<Channel>
		{
			protected override void InsertItem(int index, Channel item)
			{
				base.InsertItem(index, item);
				_channelsByName.Add(item.Name, item);
				_channelsByID.Add(item.ID, item);
				item._parentCollection = this;
			}
			protected override void RemoveItem(int index)
			{
				_channelsByName.Remove(this[index].Name);
				_channelsByID.Remove(this[index].ID);
				this[index]._parentCollection = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, Channel item)
			{
				_channelsByName.Remove(this[index].Name);
				_channelsByID.Remove(this[index].ID);
				base.SetItem(index, item);
				_channelsByName.Add(item.Name, item);
				_channelsByID.Add(item.ID, item);
				item._parentCollection = this;
			}

			protected override void ClearItems()
			{
				_channelsByName.Clear();
				_channelsByID.Clear();
				base.ClearItems();
			}

			private Dictionary<string, Channel> _channelsByName = new Dictionary<string, Channel>();
			public Channel this[string Name]
			{
				get
				{
					if (_channelsByName.ContainsKey(Name)) return _channelsByName[Name];
					return null;
				}
			}

			private Dictionary<Guid, Channel> _channelsByID = new Dictionary<Guid, Channel>();
			public Channel this[Guid ID]
			{
				get
				{
					if (_channelsByID.ContainsKey(ID)) return _channelsByID[ID];
					return null;
				}
			}

			internal void UpdateName(Channel item, string newName)
			{
				_channelsByName.Remove(item.Name);
				_channelsByName.Add(newName, item);
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private ChannelCollection _parentCollection = null;

		private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set
			{
				if (_parentCollection != null)
				{
					_parentCollection.UpdateName(this, value);
				}
				mvarName = value;
			}
		}

		private string mvarGroup = String.Empty;
		public string Group { get { return mvarGroup; } set { mvarGroup = value; } }

		private Capability.CapabilityCollection mvarCapabilities = new Capability.CapabilityCollection();
		public Capability.CapabilityCollection Capabilities { get { return mvarCapabilities; } }

		public override string ToString()
		{
			return mvarName + " (" + mvarGroup + ")";
		}

		public object Clone()
		{
			Channel clone = new Channel();
			foreach (Capability capability in mvarCapabilities)
			{
				clone.Capabilities.Add(capability.Clone() as Capability);
			}
			clone.Group = (mvarGroup.Clone() as string);
			clone.Name = (mvarName.Clone() as string);
			return clone;
		}
	}
}
