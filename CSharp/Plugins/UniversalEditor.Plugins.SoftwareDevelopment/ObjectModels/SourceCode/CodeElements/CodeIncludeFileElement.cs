using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    /// <summary>
    /// Represents a file to be included into the current code file
    /// </summary>
    public class CodeIncludeFileElement : CodeElement
    {
        public CodeIncludeFileElement()
        {
        }
        public CodeIncludeFileElement(string FileName) : this(FileName, true)
        {
        }
        public CodeIncludeFileElement(string FileName, bool isRelativePath)
        {
            mvarFileName = FileName;
            mvarIsRelativePath = isRelativePath;
        }

        private string mvarFileName = String.Empty;
        public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

        private bool mvarIsRelativePath = true;
        public bool IsRelativePath { get { return mvarIsRelativePath; } set { mvarIsRelativePath = value; } }
    }
}
