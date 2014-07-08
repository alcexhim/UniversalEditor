using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    public enum FunctionFlags
    {
        None = 0x00000000,
        /// <summary>
        /// Function is final (prebindable, non-overridable function).
        /// </summary>
        Final = 0x00000001,
        /// <summary>
        /// Function has been defined (not just declared). Not used in source code.
        /// </summary>
        Defined = 0x00000002,
        /// <summary>
        /// Function is an iterator.
        /// </summary>
        Iterator = 0x00000004,
        /// <summary>
        /// Function is a latent state function.
        /// </summary>
        Latent = 0x00000008,
        /// <summary>
        /// Unary operator is a prefix operator.
        /// </summary>
        PrefixOperator = 0x00000010,
        /// <summary>
        /// Function cannot be reentered.
        /// </summary>
        Singular = 0x00000020,
        /// <summary>
        /// Function is network-replicated. Not used in source code.
        /// </summary>
        NetworkReplicated = 0x00000040,
        /// <summary>
        /// Function should be sent reliably on the network. Not used in source code.
        /// </summary>
        NetworkReliable = 0x00000080,
        /// <summary>
        /// Function executed on the client side.
        /// </summary>
        Simulated = 0x00000100,
        /// <summary>
        /// Executable from command line.
        /// </summary>
        CommandLine = 0x00000200,
        /// <summary>
        /// Native function.
        /// </summary>
        Native = 0x00000400,
        /// <summary>
        /// Event function.
        /// </summary>
        Event = 0x00000800,
        /// <summary>
        /// Operator function.
        /// </summary>
        Operator = 0x00001000,
        /// <summary>
        /// Static function.
        /// </summary>
        Static = 0x00002000,
        /// <summary>
        /// Don't export intrinsic function to C++.
        /// </summary>
        DoNotExport = 0x00004000,
        /// <summary>
        /// Function doesn't modify this object.
        /// </summary>
        Constant = 0x00008000,
        /// <summary>
        /// Return value is purely dependent on parameters; no state dependencies or internal state changes.
        /// </summary>
        Invariant = 0x00010000
    }
}
