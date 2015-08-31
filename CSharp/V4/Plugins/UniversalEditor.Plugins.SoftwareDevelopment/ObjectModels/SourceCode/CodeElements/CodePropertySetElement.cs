using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodePropertySetElement : CodeElement
    {
        private string mvarObjectName = String.Empty;
        public string ObjectName
        {
            get { return mvarObjectName; }
            set { mvarObjectName = value; }
        }
        private string mvarPropertyName = String.Empty;
        public string PropertyName
        {
            get { return mvarPropertyName; }
            set { mvarPropertyName = value; }
        }
        private object mvarValue = null;
        /// <summary>
        /// The value to which to set this property. Note that value can
        /// be a CodeElement such as a method call or another complex
        /// expression.
        /// </summary>
        public object Value
        {
            get { return mvarValue; }
            set { mvarValue = value; }
        }
    }
}
