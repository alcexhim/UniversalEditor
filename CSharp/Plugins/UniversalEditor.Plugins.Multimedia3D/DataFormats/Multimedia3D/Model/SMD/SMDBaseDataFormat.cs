using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.SMD;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.SMD
{
    public class SMDBaseDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(SMDObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Half-Life 2 SMD", new byte?[][] { new byte?[] { (byte)'v', (byte)'e', (byte)'r', (byte)'s', (byte)'i', (byte)'o', (byte)'n', (byte)' ', (byte)'1' } }, new string[] { "*.smd" });
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            SMDObjectModel smd = (objectModel as SMDObjectModel);
            if (smd == null) return;

            IO.Reader tr = base.Accessor.Reader;
            string version = tr.ReadLine();
            if (version != "version 1") throw new InvalidDataFormatException("File does not begin with \"version 1\"");

            string nextLine = String.Empty;
            while (!tr.EndOfStream)
            {
                SMDSection section = new SMDSection();
                section.Name = tr.ReadLine();
                while (!tr.EndOfStream)
                {
                    nextLine = tr.ReadLine();
                    if (nextLine == "end") break;

                    section.Lines.Add(nextLine);
                }
                smd.Sections.Add(section);
            }
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
