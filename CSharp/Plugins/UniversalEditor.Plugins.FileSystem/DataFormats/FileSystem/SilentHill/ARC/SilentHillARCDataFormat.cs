using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.SilentHill.ARC
{
    public class SilentHillARCDataFormat
    {
        private static string[] mvarFileNameList = new string[0];
        public static int GetHashFromFileName(string FileName)
        {
            int hash = 0;
            FileName = FileName.ToLower();
            for (int i = 0; i < FileName.Length; i++)
            {
                hash *= 33;
                hash ^= (int)(FileName[i]);
            }
            return hash;
        }
        public static string GetFileNameFromHash(int HashValue)
        {
            foreach (string fileName in mvarFileNameList)
            {
                if (GetHashFromFileName(fileName) == HashValue) return fileName;
            }
            return null;
        }

    }
}
