using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
    public enum CFLEncryptionMethod
    {
        /// <summary>
        /// No encryption.
        /// </summary>
        None      = 0x00000000,
        /// <summary>
        /// Simple XOR crypt (generally stops casual hex-editor), key is one char.
        /// </summary>
        SimpleXor = 0x01000000,
        /// <summary>
        /// XOR's every byte with data from pseudorandom generator, key is the random seed.
        /// </summary>
        PseudoRandomXor = 0x02000000, 
        /// <summary>
        /// XOR's every byte with a letter from entered string. Somewhat easy to crack
        /// if string is short, but is easy way to implement password protection.
        /// </summary>
        StringXor = 0x03000000,
        PGP       = 0x10000000, //!< Pretty Good Privacy
        GPG       = 0x20000000, //!< GPG
        DES       = 0x30000000, //!< Data Encryption Standard
        TripleDES      = 0x40000000, //!< Triple-DES
        BLOWFISH  = 0x50000000, //!< Blowfish
        IDEA      = 0x60000000, //!< IDEA
        RC4       = 0x70000000  //!< RC4
    }
}
