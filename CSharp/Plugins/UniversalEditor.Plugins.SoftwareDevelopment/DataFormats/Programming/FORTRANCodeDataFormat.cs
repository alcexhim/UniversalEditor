using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace UniversalEditor.DataFormats.Programming
{
    public class FORTRANCodeDataFormat : CodeDataFormat
    {
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Fortran code file", new string[] { "*.for" });
            return dfr;
        }
        protected internal override string GenerateCode(object obj, int indentCount)
        {
            throw new NotImplementedException();
        }
    }
}
