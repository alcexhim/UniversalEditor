using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodeCommentElement : CodeElement
    {
        private string mvarContent = String.Empty;
        public string Content { get { return mvarContent; } set { mvarContent = value; } }

        private bool mvarMultiline = false;
        public bool Multiline { get { return mvarMultiline; } set { mvarMultiline = value; } }

        private bool mvarIsDocumentationComment = false;
        public bool IsDocumentationComment { get { return mvarIsDocumentationComment; } set { mvarIsDocumentationComment = value; } }
    }
}
