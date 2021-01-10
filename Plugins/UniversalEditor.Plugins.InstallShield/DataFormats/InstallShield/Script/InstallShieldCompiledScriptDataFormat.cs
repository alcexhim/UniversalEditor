//
//  InstallShieldCompiledScriptDataFormat.cs - provides a DataFormat for manipulating compiled InstallShield script files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.InstallShield;

namespace UniversalEditor.DataFormats.InstallShield.Script
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating compiled InstallShield script files.
	/// </summary>
	public class InstallShieldCompiledScriptDataFormat : DataFormat
    {
        private static DataFormatReference _dfr;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
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
