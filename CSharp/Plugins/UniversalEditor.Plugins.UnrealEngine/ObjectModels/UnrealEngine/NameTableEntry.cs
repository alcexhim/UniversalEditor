using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    [Flags()]
    public enum NameTableEntryFlags
    {
        None = 0
    }
    public class NameTableEntry : ICloneable
    {
        public class NameTableEntryCollection
            : System.Collections.ObjectModel.Collection<NameTableEntry>
        {
            public NameTableEntry Add(string name, NameTableEntryFlags flags = NameTableEntryFlags.None)
            {
                NameTableEntry entry = new NameTableEntry();
                entry.Name = name;
                entry.Flags = flags;
                Add(entry);
                return entry;
            }
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private NameTableEntryFlags mvarFlags = NameTableEntryFlags.None;
        public NameTableEntryFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

        public object Clone()
        {
            NameTableEntry entry = new NameTableEntry();
            entry.Name = (mvarName.Clone() as string);
            entry.Flags = mvarFlags;
            return entry;
        }

        public override string ToString()
        {
            return ToString(true);
        }
        public string ToString(bool includeFlags)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(mvarName);
            if (includeFlags)
            {
                sb.Append(" : ");
                sb.Append(mvarFlags.ToString());
            }
            return sb.ToString();
        }
    }
}
