using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace UniversalEditor.UserInterface.WindowsForms
{
    public class VirtualFileDescriptor
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private int? mvarLength = null;
        public int? Length { get { return mvarLength; } set { mvarLength = value; } }

        private DateTime? mvarDateModified = null;
        public DateTime? DateModified { get { return mvarDateModified; } set { mvarDateModified = value; } }

        private System.IO.Stream mvarStream = null;
        public System.IO.Stream Stream { get { return mvarStream; } set { mvarStream = value; } }
    }
}
