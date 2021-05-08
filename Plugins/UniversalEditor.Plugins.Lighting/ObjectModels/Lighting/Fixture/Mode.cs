using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Fixture
{
	public class Mode : ICloneable
	{
		public class ModeCollection
			: System.Collections.ObjectModel.Collection<Mode>
		{
			public Mode this[Guid ID]
			{
				get
				{
					foreach (Mode mode in this)
					{
						if (mode.ID == ID) return mode;
					}
					return null;
				}
			}
		}

		public override string ToString()
		{
			return mvarName;
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private ModeChannel.ModeChannelCollection mvarChannels = new ModeChannel.ModeChannelCollection();
		public ModeChannel.ModeChannelCollection Channels { get { return mvarChannels; } }

		private int mvarBulbLumens = 0;
		public int BulbLumens { get { return mvarBulbLumens; } set { mvarBulbLumens = value; } }

		private string mvarBulbType = String.Empty;
		public string BulbType { get { return mvarBulbType; } set { mvarBulbType = value; } }

		private int mvarBulbColorTemperature = 0;
		public int BulbColorTemperature { get { return mvarBulbColorTemperature; } set { mvarBulbColorTemperature = value; } }

		private int mvarPhysicalWidth = 0;
		public int PhysicalWidth { get { return mvarPhysicalWidth; } set { mvarPhysicalWidth = value; } }

		private int mvarPhysicalHeight = 0;
		public int PhysicalHeight { get { return mvarPhysicalHeight; } set { mvarPhysicalHeight = value; } }

		private int mvarPhysicalWeight = 0;
		public int PhysicalWeight { get { return mvarPhysicalWeight; } set { mvarPhysicalWeight = value; } }

		private int mvarPhysicalDepth = 0;
		public int PhysicalDepth { get { return mvarPhysicalDepth; } set { mvarPhysicalDepth = value; } }

		private int mvarLensDegreesMaximum = 0;
		public int LensDegreesMax { get { return mvarLensDegreesMaximum; } set { mvarLensDegreesMaximum = value; } }

		private int mvarLensDegreesMinimum = 0;
		public int LensDegreesMinimum { get { return mvarLensDegreesMinimum; } set { mvarLensDegreesMinimum = value; } }

		private string mvarLensName = String.Empty;
		public string LensName { get { return mvarLensName; } set { mvarLensName = value; } }

		private int mvarFocusPanMaximum = 0;
		public int FocusPanMaximum { get { return mvarFocusPanMaximum; } set { mvarFocusPanMaximum = value; } }

		private int mvarFocusTiltMaximum = 0;
		public int FocusTiltMaximum { get { return mvarFocusTiltMaximum; } set { mvarFocusTiltMaximum = value; } }

		private string mvarFocusType = String.Empty;
		public string FocusType { get { return mvarFocusType; } set { mvarFocusType = value; } }

		private int mvarPowerConsumption = 0;
		public int PowerConsumption { get { return mvarPowerConsumption; } set { mvarPowerConsumption = value; } }

		private DMXConnectorType mvarDMXConnector = DMXConnectorType.DMX3Pin;
		public DMXConnectorType DMXConnector { get { return mvarDMXConnector; } set { mvarDMXConnector = value; } }

		public object Clone()
		{
			Mode clone = new Mode();
			clone.BulbColorTemperature = mvarBulbColorTemperature;
			clone.BulbLumens = mvarBulbLumens;
			clone.BulbType = (mvarBulbType.Clone() as string);
			clone.DMXConnector = mvarDMXConnector;
			clone.FocusPanMaximum = mvarFocusPanMaximum;
			clone.FocusTiltMaximum = mvarFocusTiltMaximum;
			clone.FocusType = (mvarFocusType.Clone() as string);
			clone.LensDegreesMax = mvarLensDegreesMaximum;
			clone.LensDegreesMinimum = mvarLensDegreesMinimum;
			clone.LensName = (mvarLensName.Clone() as string);
			clone.Name = (mvarName.Clone() as string);
			clone.PhysicalDepth = mvarPhysicalDepth;
			clone.PhysicalHeight = mvarPhysicalHeight;
			clone.PhysicalWeight = mvarPhysicalWeight;
			clone.PhysicalWidth = mvarPhysicalWidth;
			clone.PowerConsumption = mvarPowerConsumption;
			return clone;
		}
	}
}
