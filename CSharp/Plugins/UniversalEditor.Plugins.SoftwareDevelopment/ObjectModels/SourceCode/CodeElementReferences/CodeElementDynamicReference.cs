using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElementReferences
{
    /// <summary>
    /// Represents a dynamic reference to an Object, Property, Method, Field, or similar element whose
    /// target is calculated by the scripting engine at runtime.
    /// </summary>
    public class CodeElementDynamicReference : CodeElementReference
    {
        public CodeElementDynamicReference(string name, string[] objectName)
        {
            mvarName = name;
            mvarObjectName = objectName;
        }

        private string mvarName = String.Empty;
        /// <summary>
        /// The name of the object, property, method, field, or similar element whose target should be
        /// dynamically located.
        /// </summary>
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string[] mvarObjectName = new string[0];
        public string[] ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (mvarObjectName.Length > 0)
            {
                sb.Append(String.Join(".", mvarObjectName));
                sb.Append('.');
            }
            sb.Append(mvarName);
            return sb.ToString();
        }

    }
}
