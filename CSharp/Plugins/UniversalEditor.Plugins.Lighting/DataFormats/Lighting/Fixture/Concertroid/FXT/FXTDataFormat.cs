using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Lighting;
using UniversalEditor.ObjectModels.Lighting.Fixture;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Lighting.Fixture.Concertroid.FXT
{
    public class FXTDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
            }
            return _dfr;
        }

        private Version m_FormatVersion = new Version(1, 0);
        protected Version FormatVersion { get { return m_FormatVersion; } }

        private Guid mvarFixtureID = Guid.Empty;
        public Guid FixtureID { get { return mvarFixtureID; } set { mvarFixtureID = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FixtureObjectModel fixture = (objectModel as FixtureObjectModel);
            if (fixture == null) throw new ObjectModelNotSupportedException();

            IO.BinaryReader br = base.Stream.BinaryReader;
            string FIXT = br.ReadFixedLengthString(4);
            if (FIXT != "FIXT") throw new InvalidDataFormatException("File does not begin with \"FIXT\"");

            ushort versionMajor = br.ReadUInt16();
            ushort versionMinor = br.ReadUInt16();
            m_FormatVersion = new Version(versionMajor, versionMinor);

            mvarFixtureID = br.ReadGuid();

            int channelCount = br.ReadInt32();
            int modeCount = br.ReadInt32();

            for (int i = 0; i < channelCount; i++)
            {
                Channel channel = new Channel();
                channel.Name = br.ReadFixedLengthString(256);

                int channelType = br.ReadInt32();

                int capabilityCount = br.ReadInt32();
                for (int j = 0; j < capabilityCount; j++)
                {
                    Capability capability = new Capability();
                    capability.Title = br.ReadFixedLengthString(256);
                    capability.MaximumValue = br.ReadByte();
                    capability.MinimumValue = br.ReadByte();
                    capability.DefaultValue = br.ReadByte();

                    capability.Color = br.ReadColorRGBA8888();

                    capability.ImageFileName = br.ReadFixedLengthString(256);
                    int imageThumbnailWidth = br.ReadInt32();
                    int imageThumbnailHeight = br.ReadInt32();
                    if (imageThumbnailWidth > 0 && imageThumbnailHeight > 0)
                    {
                        byte[] imageData = new byte[imageThumbnailWidth * imageThumbnailHeight];
                        for (int y = 0; y < imageThumbnailHeight; y++)
                        {
                            for (int x = 0; x < imageThumbnailWidth; x++)
                            {
                                imageData[x + (y * imageThumbnailWidth)] = br.ReadByte();
                            }
                        }
                        capability.Image = PictureObjectModel.FromByteArray(imageData, imageThumbnailWidth, imageThumbnailHeight);
                    }
                    else
                    {
                        capability.Image = null;
                    }
                    channel.Capabilities.Add(capability);
                }
                fixture.Channels.Add(channel);
            }
            for (int i = 0; i < modeCount; i++)
            {
                Mode mode = new Mode();
                mode.Name = br.ReadFixedLengthString(256);

                #region Bulb
                mode.BulbLumens = br.ReadInt32();
                mode.BulbType = br.ReadFixedLengthString(64);
                mode.BulbColorTemperature = br.ReadInt32();
                #endregion
                #region Physical
                mode.PhysicalWidth = br.ReadInt32();
                mode.PhysicalHeight = br.ReadInt32();
                mode.PhysicalWeight = br.ReadInt32();
                mode.PhysicalDepth = br.ReadInt32();
                #endregion
                #region Lens
                mode.LensDegreesMax = br.ReadInt32();
                mode.LensDegreesMinimum = br.ReadInt32();
                mode.LensName = br.ReadFixedLengthString(64);
                #endregion
                #region Focus
                mode.FocusPanMaximum = br.ReadInt32();
                mode.FocusTiltMaximum = br.ReadInt32();
                mode.FocusType = br.ReadFixedLengthString(64);
                #endregion
                #region Power
                mode.PowerConsumption = br.ReadInt32();
                mode.DMXConnector = (DMXConnectorType)br.ReadInt32();
                #endregion

                int modeChannelCount = br.ReadInt32();
                for (int j = 0; j < modeCount; j++)
                {
                    ModeChannel channel = new ModeChannel();
                    channel.RelativeAddress = br.ReadInt32();

                    int modeChannelIndex = br.ReadInt32();
                    channel.Channel = fixture.Channels[modeChannelIndex];
                    mode.Channels.Add(channel);
                }
                fixture.Modes.Add(mode);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
