using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    public class ObjectReference
    {
        private UnrealPackageObjectModel mvarParent = null;
        public UnrealPackageObjectModel Parent { get { return mvarParent; } }

        private int mvarIndexValue = 0;
        public int IndexValue { get { return mvarIndexValue; } set { mvarIndexValue = value; } }

        public static readonly ObjectReference Empty = new ObjectReference(0, null);

        public ObjectReference(int indexValue = 0, UnrealPackageObjectModel parent = null)
        {
            mvarParent = parent;
            mvarIndexValue = indexValue;
        }

        public object Value
        {
            get
            {
                if (mvarParent != null)
                {
                    if (mvarIndexValue < 0)
                    {
                        // pointer to an entry of the ImportTable
                        int index = -mvarIndexValue - 1;
                        if (index >= 0 && index < mvarParent.ImportTableEntries.Count)
                        {
                            return mvarParent.ImportTableEntries[index];
                        }
                    }
                    else if (mvarIndexValue > 0)
                    {
                        // pointer to an entry in the ExportTable
                        int index = mvarIndexValue - 1;
                        if (index >= 0 && index < mvarParent.ExportTableEntries.Count)
                        {
                            return mvarParent.ExportTableEntries[index];
                        }
                    }
                }
                return null;
            }
        }

		public NameTableEntry Name
		{
			get
			{
				if (Value is ExportTableEntry)
				{
					return (Value as ExportTableEntry).Name;
				}
				else if (Value is ImportTableEntry)
				{
					return (Value as ImportTableEntry).ClassName;
				}
				return null;
			}
		}

		public override string ToString()
        {
            if (mvarIndexValue == 0) return "(null)";
            if (mvarParent != null)
            {
                if (mvarIndexValue < 0)
                {
                    // pointer to an entry of the ImportTable
                    int index = -mvarIndexValue - 1;
                    if (index >= 0 && index < mvarParent.ImportTableEntries.Count)
                    {
                        return mvarParent.ImportTableEntries[index].ToString();
                    }
                    return "(unknown: " + mvarIndexValue.ToString() + ")";
                }
                else if (mvarIndexValue > 0)
                {
                    // pointer to an entry in the ExportTable
                    int index = mvarIndexValue - 1;
                    if (index >= 0 && index < mvarParent.ExportTableEntries.Count)
                    {
                        return mvarParent.ExportTableEntries[index].ToString();
                    }
                    return "(unknown: " + mvarIndexValue.ToString() + ")";
                }
            }
            return "(unknown: " + mvarIndexValue.ToString() + ")";
        }
    }
}
