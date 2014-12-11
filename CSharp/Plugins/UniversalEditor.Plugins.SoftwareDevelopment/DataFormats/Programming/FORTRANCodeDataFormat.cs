using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace UniversalEditor.DataFormats.Programming
{
    public class FORTRANCodeDataFormat : CodeDataFormat
    {
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Filters.Add("Fortran code file", new string[] { "*.for" });
            return dfr;
        }
        protected internal override string GenerateCode(object obj, int indentCount)
        {
            throw new NotImplementedException();
        }
    }
}
