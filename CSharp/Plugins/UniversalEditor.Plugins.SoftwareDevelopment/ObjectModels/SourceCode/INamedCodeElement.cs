using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode
{
    public interface INamedCodeElement
    {
        string Name { get; set; }
        string GetFullName(string separator);
    }
    public interface IMultipleNamedCodeElement
    {
        string[] Name { get; set; }
        string GetFullName(string separator);
    }
}
