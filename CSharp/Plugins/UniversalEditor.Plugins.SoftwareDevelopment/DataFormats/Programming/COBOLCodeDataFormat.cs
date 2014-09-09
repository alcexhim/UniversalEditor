using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace UniversalEditor.DataFormats.Programming
{
    public class COBOLCodeDataFormat : CodeDataFormat
    {
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Cobol code file", new string[] { "*.cob", "*.cbl", "*.cobol" });
            return dfr;
        }
        protected internal override string GenerateCode(object obj, int indentCount)
        {
            throw new NotImplementedException();
        }
    }
}
