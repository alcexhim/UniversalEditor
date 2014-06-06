using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.VersatileContainer;
using UniversalEditor.ObjectModels.VersatileContainer.Sections;

namespace UniversalEditor.DataFormats.VersatileContainer
{
    public class VersatileContainerV2DataFormat : DataFormat
    {
        private Version mvarFormatVersion = new Version(2, 0);
        public Version FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(VersatileContainerObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Versatile Container object file", new byte?[][] { new byte?[] { (byte)'V', (byte)'e', (byte)'r', (byte)'s', (byte)'a', (byte)'t', (byte)'i', (byte)'l', (byte)'e', (byte)' ', (byte)'C', (byte)'o', (byte)'n', (byte)'t', (byte)'a', (byte)'i', (byte)'n', (byte)'e', (byte)'r', (byte)' ', (byte)'f', (byte)'i', (byte)'l', (byte)'e', (byte)' ', (byte)'0', (byte)'0', (byte)'0', (byte)'2' } }, new string[] { "*.vco" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            VersatileContainerObjectModel vcom = (objectModel as VersatileContainerObjectModel);
            if (vcom == null) return;

            IO.Reader br = base.Accessor.Reader;
            string signature = br.ReadFixedLengthString(30);    // Versatile Container file 0002
            signature = signature.TrimNull();
            if (signature != "Versatile Container file 0002") throw new InvalidDataFormatException();

            mvarFormatVersion = br.ReadVersion();
            vcom.Title = br.ReadNullTerminatedString();

            uint propertyCount = br.ReadUInt32();
            uint sectionClassCount = br.ReadUInt32();
            uint sectionCount = br.ReadUInt32();

            List<string> sectionClassNames = new List<string>();

            #region Section Class Entries
            for (uint i = 0; i < sectionClassCount; i++)
            {
                string sectionClassName = br.ReadNullTerminatedString();
                sectionClassNames.Add(sectionClassName);
            }
            #endregion
            #region Section Entries
            List<uint> sectionDataSizes = new List<uint>();
            for (uint i = 0; i < sectionCount; i++)
            {
                VersatileContainerSectionType sectionType = (VersatileContainerSectionType)br.ReadUInt32();
                string sectionName = br.ReadNullTerminatedString();

                switch (sectionType)
                {
                    case VersatileContainerSectionType.None:
                    {
                        sectionDataSizes.Add(0);

                        VersatileContainerBlankSection sect = new VersatileContainerBlankSection();
                        vcom.Sections.Add(sect);
                        break;
                    }
                    case VersatileContainerSectionType.Section:
                    {
                        uint sectionDataSize = br.ReadUInt32();
                        sectionDataSizes.Add(sectionDataSize);

                        uint sectionClassIndex = br.ReadUInt32();

                        VersatileContainerContentSection sect = new VersatileContainerContentSection();
                        sect.Name = sectionName;
                        if (sectionClassIndex != 0xFFFFFFFF)
                        {
                            sect.ClassName = sectionClassNames[(int)sectionClassIndex];
                        }
                        else
                        {
                            sect.ClassName = null;
                        }
                        vcom.Sections.Add(sect);
                        break;
                    }
                    case VersatileContainerSectionType.Directory:
                    {
                        sectionDataSizes.Add(0);

                        VersatileContainerDirectorySection sect = new VersatileContainerDirectorySection();
                        vcom.Sections.Add(sect);
                        break;
                    }
                    case VersatileContainerSectionType.Reference:
                    {
                        sectionDataSizes.Add(0);
                        uint sectionIndex = br.ReadUInt32();
                        
                        VersatileContainerReferenceSection sect = new VersatileContainerReferenceSection();
                        sect.Target = vcom.Sections[(int)sectionIndex];
                        vcom.Sections.Add(sect);
                        break;
                    }
                }
            }
            #endregion
            for (uint i = 0; i < sectionCount; i++)
            {
                VersatileContainerSection section = vcom.Sections[(int)i];
                if (section is VersatileContainerContentSection)
                {
                    VersatileContainerContentSection sect = (section as VersatileContainerContentSection);
                    byte[] data = br.ReadBytes(sectionDataSizes[(int)i]);
                    if (sect == null) continue;
                    sect.Data = data;
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            VersatileContainerObjectModel vcom = (objectModel as VersatileContainerObjectModel);
            if (vcom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteNullTerminatedString("Versatile Container file 0002");
            bw.WriteVersion(mvarFormatVersion);
            bw.WriteNullTerminatedString(vcom.Title);

            bw.WriteUInt32((uint)vcom.Properties.Count);

            // get a list of section class names
            List<string> sectionClassNames = new List<string>();
            foreach (VersatileContainerSection section in vcom.Sections)
            {
                if (section is VersatileContainerContentSection)
                {
                    VersatileContainerContentSection sect = (section as VersatileContainerContentSection);
                    if (!sectionClassNames.Contains(sect.ClassName)) sectionClassNames.Add(sect.ClassName);
                }
            }
            bw.WriteUInt32((uint)sectionClassNames.Count);

            bw.WriteUInt32((uint)vcom.Sections.Count);

            #region Section Class Entries
            foreach (string sectionClassName in sectionClassNames)
            {
                bw.WriteNullTerminatedString(sectionClassName);
            }
            #endregion
            #region Section Entries
            List<VersatileContainerSectionType> sectionTypes = new List<VersatileContainerSectionType>();
            List<uint> sectionDataSizes = new List<uint>();
            foreach (VersatileContainerSection section in vcom.Sections)
            {
                VersatileContainerSectionType sectionType = VersatileContainerSectionType.Section; // (VersatileContainerSectionType)br.ReadUInt32();
                bw.WriteUInt32((uint)sectionType);

                bw.WriteNullTerminatedString(section.Name);
                if (section is VersatileContainerBlankSection)
                {
                }
                else if (section is VersatileContainerContentSection)
                {
                    VersatileContainerContentSection sect = (section as VersatileContainerContentSection);
                    bw.WriteUInt32((uint)sect.Data.LongLength);
                    if (!String.IsNullOrEmpty(sect.ClassName))
                    {
                        bw.WriteUInt32((uint)sectionClassNames.IndexOf(sect.ClassName));
                    }
                    else
                    {
                        bw.WriteUInt32((uint)0xFFFFFFFF);
                    }
                }
                else if (section is VersatileContainerDirectorySection)
                {
                    VersatileContainerDirectorySection sect = (section as VersatileContainerDirectorySection);
                }
                else if (section is VersatileContainerReferenceSection)
                {
                    VersatileContainerReferenceSection sect = (section as VersatileContainerReferenceSection);
                    bw.WriteUInt32((uint)vcom.Sections.IndexOf(sect.Target));
                }
            }
            #endregion
            foreach (VersatileContainerSection section in vcom.Sections)
            {
                if (section is VersatileContainerContentSection)
                {
                    VersatileContainerContentSection sect = (section as VersatileContainerContentSection);
                    bw.WriteBytes(sect.Data);
                }
            }
        }
    }
}