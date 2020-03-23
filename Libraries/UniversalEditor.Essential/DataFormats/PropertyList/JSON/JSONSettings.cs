using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.DataFormats.PropertyList.JSON
{
    public class JSONSettings
    {
        private string mvarObjectNameValueSeparator = ":";
        public string ObjectNameValueSeparator { get { return mvarObjectNameValueSeparator; } set { mvarObjectNameValueSeparator = value; } }

        private string mvarFieldNamePrefix = "\"";
        public string FieldNamePrefix { get { return mvarFieldNamePrefix; } set { mvarFieldNamePrefix = value; } }
        private string mvarFieldNameSuffix = "\"";
        public string FieldNameSuffix { get { return mvarFieldNameSuffix; } set { mvarFieldNameSuffix = value; } }

        private string mvarArrayPrefix = "[";
        public string ArrayPrefix { get { return mvarArrayPrefix; } set { mvarArrayPrefix = value; } }
        private string mvarArraySuffix = "]";
        public string ArraySuffix { get { return mvarArraySuffix; } set { mvarArraySuffix = value; } }

        private string mvarArrayValuePrefix = "\"";
        public string ArrayValuePrefix { get { return mvarArrayValuePrefix; } set { mvarArrayValuePrefix = value; } }
        private string mvarArrayValueSuffix = "\"";
        public string ArrayValueSuffix { get { return mvarArrayValueSuffix; } set { mvarArrayValueSuffix = value; } }

        private bool mvarAppendSpaceAfterObjectName = false;
        public bool AppendSpaceAfterObjectName { get { return mvarAppendSpaceAfterObjectName; } set { mvarAppendSpaceAfterObjectName = value; } }
        private bool mvarAppendLineAfterObjectName = true;
        public bool AppendLineAfterObjectName { get { return mvarAppendLineAfterObjectName; } set { mvarAppendLineAfterObjectName = value; } }

        private bool mvarAppendLineAfterStartArray = true;
        public bool AppendLineAfterStartArray { get { return mvarAppendLineAfterStartArray; } set { mvarAppendLineAfterStartArray = value; } }
        private bool mvarAppendLineAfterArrayValue = true;
        public bool AppendLineAfterArrayValue { get { return mvarAppendLineAfterArrayValue; } set { mvarAppendLineAfterArrayValue = value; } }
        private bool mvarAppendLineAfterField = true;
        public bool AppendLineAfterField { get { return mvarAppendLineAfterField; } set { mvarAppendLineAfterField = value; } }
        private bool mvarAppendLineAfterFieldName = false;
        public bool AppendLineAfterFieldName { get { return mvarAppendLineAfterFieldName; } set { mvarAppendLineAfterFieldName = value; } }

        private string mvarFieldSeparator = ",";
        public string FieldSeparator { get { return mvarFieldSeparator; } set { mvarFieldSeparator = value; } }

        private bool mvarIndentChildFields = true;
        public bool IndentChildFields { get { return mvarIndentChildFields; } set { mvarIndentChildFields = value; } }
        private bool mvarIndentArrayValues = true;
        public bool IndentArrayValues { get { return mvarIndentArrayValues; } set { mvarIndentArrayValues = value; } }

        private string mvarObjectNamePrefix = "\"";
        public string ObjectNamePrefix { get { return mvarObjectNamePrefix; } set { mvarObjectNamePrefix = value; } }
        private string mvarObjectNameSuffix = "\"";
        public string ObjectNameSuffix { get { return mvarObjectNameSuffix; } set { mvarObjectNameSuffix = value; } }

        private string mvarObjectPrefix = "{";
        public string ObjectPrefix { get { return mvarObjectPrefix; } set { mvarObjectPrefix = value; } }
        private string mvarObjectSuffix = "}";
        public string ObjectSuffix { get { return mvarObjectSuffix; } set { mvarObjectSuffix = value; } }

        private string mvarFieldNameValueSeparator = ":";
        public string FieldNameValueSeparator { get { return mvarFieldNameValueSeparator; } set { mvarFieldNameValueSeparator = value; } }

        private string mvarStringLiteralPrefix = "\"";
        public string StringLiteralPrefix { get { return mvarStringLiteralPrefix; } set { mvarStringLiteralPrefix = value; } }
        private string mvarStringLiteralSuffix = "\"";
        public string StringLiteralSuffix { get { return mvarStringLiteralSuffix; } set { mvarStringLiteralSuffix = value; } }
    }
}
