using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.StructuredStorage.Internal
{
    internal struct StructuredStorageDirectoryEntry
    {
        public string Name;
        public StructuredStorageDirectoryType EntryType;
        public Common.RBTree.RBTreeColor rbTreeColor;
        public int leftIndex;
        public int rightIndex;
        public int childIndex;
        public Guid guid;
        public int stateBits;
        public DateTime creationDate;
        public DateTime modifyDate;
        public int startSect;
        public int size;

        public StructuredStorageDirectoryEntry(string Name, StructuredStorageDirectoryType entryType, Common.RBTree.RBTreeColor rBTreeColor, int leftIndex, int rightIndex, int childIndex, Guid guid, int stateBits, DateTime creationDate, DateTime modifyDate, int startSect, int size)
        {
            this.Name = Name;
            this.EntryType = entryType;
            this.rbTreeColor = rBTreeColor;
            this.leftIndex = leftIndex;
            this.rightIndex = rightIndex;
            this.childIndex = childIndex;
            this.guid = guid;
            this.stateBits = stateBits;
            this.creationDate = creationDate;
            this.modifyDate = modifyDate;
            this.startSect = startSect;
            this.size = size;
        }

    }
}
