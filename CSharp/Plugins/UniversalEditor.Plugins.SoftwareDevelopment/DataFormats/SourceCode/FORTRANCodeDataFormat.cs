using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace UniversalEditor.DataFormats.SourceCode
{
    public class FORTRANCodeDataFormat : CodeDataFormat
    {
		private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
			}
            return _dfr;
        }
        protected internal override string GenerateCode(object obj, int indentCount)
        {
            throw new NotImplementedException();
        }
    }
}
