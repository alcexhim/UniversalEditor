﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace UniversalEditor.DataFormats.Programming
{
    public class BooCodeDataFormat : CodeDataFormat
    {
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Filters.Add("Boo code file", new string[] { "*.boo" });
            return dfr;
        }
        protected internal override string GenerateCode(object obj, int indentCount)
        {
            throw new NotImplementedException();
        }
    }
}
