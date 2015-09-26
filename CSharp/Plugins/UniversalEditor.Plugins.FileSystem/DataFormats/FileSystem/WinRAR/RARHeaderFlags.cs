using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
    [Flags()]
    public enum RARHeaderFlags
    {
        ArchiveVolume = 0x0001,
        /// <summary>
        /// The archive comment is present. RAR 3.x uses the separate comment block
        /// and does not set this flag.
        /// </summary>
        CommentPresent = 0x0002,
        /// <summary>
        /// Archive lock attribute
        /// </summary>
        Lock = 0x0004,
        /// <summary>
        /// Solid attribute (solid archive)
        /// </summary>
        Solid = 0x0008,
        /// <summary>
        /// New volume naming scheme ('volname.partN.rar')
        /// </summary>
        NewVolumeNames = 0x0010,
        /// <summary>
        /// Authenticity information present. RAR 3.x does not set this flag.
        /// </summary>
        AuthenticityPresent = 0x0020,
        /// <summary>
        /// Recovery record present
        /// </summary>
        RecoveryRecordPresent = 0x0040,
        /// <summary>
        /// Block headers are encrypted
        /// </summary>
        EncryptedBlockHeaders = 0x0080,
        /// <summary>
        /// First volume (set only by RAR 3.0 and later)
        /// </summary>
        FirstVolume = 0x0100
    }
}
