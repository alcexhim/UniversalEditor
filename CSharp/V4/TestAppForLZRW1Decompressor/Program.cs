using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestAppForLZRW1Decompressor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            byte[] input = System.IO.File.ReadAllBytes(@"C:\Temp\lzrw1.dat");
            System.IO.MemoryStream msi = new System.IO.MemoryStream(input);
            System.IO.MemoryStream mso = new System.IO.MemoryStream();
            
            UniversalEditor.Compression.Modules.LZRW1.LZRW1CompressionModule module = new UniversalEditor.Compression.Modules.LZRW1.LZRW1CompressionModule();
            module.Decompress(msi, mso);

            System.IO.File.WriteAllBytes(@"C:\Temp\lzrw1.txt", mso.ToArray());
        }
    }
}
