//
//  InstallShieldScriptV2DataFormat.cs - provides a DataFormat for manipulating InstallShield format version 2 script files
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

using System.Linq;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.InstallShield;

namespace UniversalEditor.DataFormats.InstallShield.Script
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating InstallShield format version 2 script files.
	/// </summary>
    public class InstallShieldScriptV2DataFormat : DataFormat
    {
        #region DataFormat members
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
            
            string aLuZ = br.ReadFixedLengthString(4);
            if (aLuZ != "aLuZ") throw new InvalidDataFormatException("File does not begin with \"aLuZ\"");

            short u0 = br.ReadInt16();
            mvarComment = br.ReadFixedLengthString(98);
            if (mvarComment.Contains('\0')) mvarComment = mvarComment.Substring(0, mvarComment.IndexOf('\0'));

            int u1 = br.ReadInt32();
            int u2 = br.ReadInt32();
            int u3 = br.ReadInt32();
            int u4 = br.ReadInt32();
            int u5 = br.ReadInt32();
            int u6 = br.ReadInt32();

            short w0 = br.ReadInt16();

        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("aLuZ");
            bw.WriteInt16(0);
            bw.WriteFixedLengthString(mvarComment, 98);

            
        }
        #endregion

        public const string COMMENT_V1 = "Copyright (c) 1990-1999 Stirling Technologies, Ltd. All Rights Reserved.";
        public const string COMMENT_V2 = "Copyright (c) 1990-2002 InstallShield Software Corp. All Rights Reserved.";

        private string mvarComment = COMMENT_V1;
        public string Comment
        {
            get { return mvarComment; }
            set { mvarComment = value; }
        }
        
    }
}
