using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.InstallShield;

namespace UniversalEditor.DataFormats.InstallShield.Script
{
    public class InstallShieldCompiledScriptDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Filters.Add("InstallShield compiled script", new byte?[][] { new byte?[] { null, null, null, null, null, null, null, null, null, null, null, null, 48, 13, 10, (byte)'S', (byte)'t', (byte)'i', (byte)'r', (byte)'l', (byte)'i', (byte)'n', (byte)'g', (byte)' ', (byte)'T', (byte)'e', (byte)'c', (byte)'h', (byte)'n', (byte)'o', (byte)'l', (byte)'o', (byte)'g', (byte)'i', (byte)'e', (byte)'s', (byte)',', (byte)' ', (byte)'I', (byte)'n', (byte)'c', (byte)'.', (byte)' ', (byte)' ', (byte)'(', (byte)'c', (byte)')', (byte)' ', (byte)'1', (byte)'9', (byte)'9', (byte)'0', (byte)'-', (byte)'1', (byte)'9', (byte)'9', (byte)'4' } }, new string[] { "*.ins" });
                _dfr.Capabilities.Add(typeof(InstallShieldScriptObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            Reader br = base.Accessor.Reader;
            short u0 = br.ReadInt16();
            short u1 = br.ReadInt16();
            short u2 = br.ReadInt16();
            short u3 = br.ReadInt16();
            short u4 = br.ReadInt16();
            short u5 = br.ReadInt16();
            string comment = br.ReadLengthPrefixedString();

            short u6 = br.ReadInt16();
            short u7 = br.ReadInt16();

            byte[] unknowns = br.ReadBytes(102);

            List<string> variableNames1 = new List<string>();

            short variableCount = br.ReadInt16();
            for (short i = 0; i < variableCount; i++)
            {
                short variableIndex = br.ReadInt16();
                
                short variableNameLength = br.ReadInt16();
                string variableName = br.ReadFixedLengthString(variableNameLength);
                variableNames1.Add(variableName);
            }

            short u8 = br.ReadInt16();

            List<string> variableNames2 = new List<string>();

            short u9 = br.ReadInt16();
            for (short i = 0; i < u9; i++)
            {
                short variableIndex = br.ReadInt16();

                short variableNameLength = br.ReadInt16();
                string variableName = br.ReadFixedLengthString(variableNameLength);
                variableNames2.Add(variableName);
            }

            byte[] unknown_3 = br.ReadBytes(170);
            short u_ct0 = br.ReadInt16();
            for (short i = 0; i < u_ct0; i++)
            {
                short index = br.ReadInt16();

                short nameLength = br.ReadInt16();
                string name = br.ReadFixedLengthString(nameLength);

                short valueLength = br.ReadInt16();
                string value = br.ReadFixedLengthString(valueLength);
            }

            short u12 = br.ReadInt16();
            short u13 = br.ReadInt16();
            short u14 = br.ReadInt16();
            short u15 = br.ReadInt16();
            short u16 = br.ReadInt16();

            short u_ct1 = br.ReadInt16();
            for (short i = 0; i < u_ct1; i++)
            {
                short index = br.ReadInt16();

                short nameLength = br.ReadInt16();
                string name = br.ReadFixedLengthString(nameLength);

                short valueLength = br.ReadInt16();
                string value = br.ReadFixedLengthString(valueLength);
            }

            byte[] unknown1000 = br.ReadBytes(80);

            while (!br.EndOfStream)
            {
                byte j0 = br.ReadByte();
                if (j0 == 0) break;
                short len = br.ReadInt16();
                string val = br.ReadFixedLengthString(len);
            }
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
