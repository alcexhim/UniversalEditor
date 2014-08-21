using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.InstallShield;

namespace UniversalEditor.DataFormats.InstallShield.Script
{
    public class InstallShieldScriptV2DataFormat : DataFormat
    {
        #region DataFormat members
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(InstallShieldScriptObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("InstallShield script (INX)", new byte?[][] { new byte?[] { (byte)'a', (byte)'L', (byte)'u', (byte)'Z' } }, new string[] { "*.inx" });
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
