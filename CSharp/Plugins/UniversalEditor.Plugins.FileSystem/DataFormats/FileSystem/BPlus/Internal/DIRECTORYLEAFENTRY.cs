using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.BPlus.Internal
{
    internal struct DIRECTORYLEAFENTRY
    {
        /// <summary>
        /// Varying-length NUL-terminated string.
        /// </summary>
        public string FileName;
        /// <summary>
        /// Offset of FILEHEADER of internal file FileName relative to beginning of help file.
        /// </summary>
        public int FileOffset;

        public override string ToString()
        {
            return FileName + " (at " + FileOffset.ToString() + ")";
        }
    }
}
