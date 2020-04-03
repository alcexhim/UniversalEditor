using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    public class ExportTableEntry : ICloneable
    {
        public class ExportTableEntryCollection
            : System.Collections.ObjectModel.Collection<ExportTableEntry>
        {
        }

		internal Accessor _acc = null;

        public event UniversalEditor.ObjectModels.FileSystem.DataRequestEventHandler DataRequest;
        protected virtual void OnDataRequest(UniversalEditor.ObjectModels.FileSystem.DataRequestEventArgs e)
        {
            if (DataRequest != null)
            {
                DataRequest(this, e);
            }
        }

        public byte[] GetData()
        {
            UniversalEditor.ObjectModels.FileSystem.DataRequestEventArgs e = new FileSystem.DataRequestEventArgs();
            OnDataRequest(e);
            return e.Data;
        }

        private NameTableEntry mvarName = null;
        public NameTableEntry Name { get { return mvarName; } set { mvarName = value; } }
        private ObjectReference mvarObjectClass = null;
        public ObjectReference ObjectClass { get { return mvarObjectClass; } set { mvarObjectClass = value; } }
        private ObjectReference mvarObjectParent = null;
        public ObjectReference ObjectParent { get { return mvarObjectParent; } set { mvarObjectParent = value; } }
        private ObjectReference mvarGroup = null;
        public ObjectReference Group { get { return mvarGroup; } set { mvarGroup = value; } }
        private ObjectFlags mvarFlags = ObjectFlags.None;
        public ObjectFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

        private int mvarSize = 0;
        public int Size { get { return mvarSize; } set { mvarSize = value; } }

        private int mvarOffset = 0;
        public int Offset { get { return mvarOffset; } set { mvarOffset = value; } }

        public object Clone()
        {
            ExportTableEntry clone = new ExportTableEntry();
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (mvarGroup != null)
            {
                sb.Append(mvarGroup.ToString());
                sb.Append('.');
            }
            if (mvarName == null)
            {
                sb.Append("(invalid name)");
            }
            else
            {
                sb.Append(mvarName.ToString(false));
            }
			/*
            if (mvarObjectClass != null)
            {
                sb.Append(" (");
                sb.Append(mvarObjectClass.ToString());
                sb.Append(")");
            }
            if (mvarObjectParent != null)
            {
                sb.Append(" extends ");
                sb.Append(mvarObjectParent.ToString());
            }
            */
			/*
            sb.Append(" : ");
            sb.Append(mvarFlags.ToString());
            */		
            return sb.ToString();
        }
    }
}
