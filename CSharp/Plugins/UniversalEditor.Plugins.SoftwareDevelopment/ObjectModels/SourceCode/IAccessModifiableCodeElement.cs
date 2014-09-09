using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode
{
    public interface IAccessModifiableCodeElement
    {
        CodeAccessModifiers AccessModifiers { get; set; }
    }
}
