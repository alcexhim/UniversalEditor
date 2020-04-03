using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    public class ImportTableEntry : ICloneable
    {
        public class ImportTableEntryCollection
            : System.Collections.ObjectModel.Collection<ImportTableEntry>
        {
        }

        public object Clone()
        {
            ImportTableEntry clone = new ImportTableEntry();
            return clone;
        }

        private NameTableEntry mvarPackageName = null;
        /// <summary>
        /// Package file in which the class of the object is defined
        /// </summary>
        public NameTableEntry PackageName { get { return mvarPackageName; } set { mvarPackageName = value; } }

        private NameTableEntry mvarClassName = null;
        /// <summary>
        /// Class of the object, i.e. "Texture", "Palette", "Package", etc.
        /// </summary>
        public NameTableEntry ClassName { get { return mvarClassName; } set { mvarClassName = value; } }

        private ObjectReference mvarPackage = null;
        /// <summary>
        /// Reference where the object resides
        /// </summary>
        public ObjectReference Package { get { return mvarPackage; } set { mvarPackage = value; } }

        private NameTableEntry mvarObjectName = null;
        /// <summary>
        /// The name of the object
        /// </summary>
        public NameTableEntry ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (mvarPackageName != null)
            {
                sb.Append(mvarPackageName.ToString(false));
                sb.Append(".");
            }
            if (mvarObjectName == null)
            {
                sb.Append("(invalid name)");
            }
            else
            {
                sb.Append(mvarObjectName.ToString(false));
            }
			/*
            if (mvarClassName != null)
            {
                sb.Append(" (");
                sb.Append(mvarClassName.ToString(false));
                sb.Append(")");
            }
            */
            return sb.ToString();
        }
    }
}
