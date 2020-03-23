using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    [Flags()]
    public enum ObjectFlags : uint
    {
        None = 0,
        /// <summary>
        /// Object must be tracked in the editor by the Undo/Redo tracking system.
        /// </summary>
        Transactional = 0x00000001,
        /// <summary>
        /// Object is not reachable on the object graph.
        /// </summary>
        Unreachable = 0x00000002,
        /// <summary>
        /// Object may be imported by other package files.
        /// </summary>
        Public = 0x00000004,
        /// <summary>
        /// Temporary import tag in load/save.
        /// </summary>
        Importing = 0x00000008,
        /// <summary>
        /// Temporary export tag in load/save.
        /// </summary>
        Exporting = 0x00000010,
        /// <summary>
        /// The external data source corresponding to this object has been modified.
        /// </summary>
        SourceModified = 0x00000020,
        /// <summary>
        /// Check during garbage collection.
        /// </summary>
        GarbageCollect = 0x00000040,
        /// <summary>
        /// During load, indicates object needs loading.
        /// </summary>
        RequireLoad = 0x00000200,
        /// <summary>
        /// A hardcoded name which should be syntax-highlighted.
        /// </summary>
        HighlightedName = 0x00000400,
        EliminateObject = 0x00000400,
        RemappedName = 0x00000800,
        /// <summary>
        /// In a singular function.
        /// </summary>
        SingularFunction = 0x00000800,
        /// <summary>
        /// Suppressed log name.
        /// </summary>
        Suppressed = 0x00001000,
        StateChanged = 0x00001000,
        /// <summary>
        /// Within an EndState call.
        /// </summary>
        EndState = 0x00002000,
        /// <summary>
        /// Do not save the object.
        /// </summary>
        Transient = 0x00004000,
        /// <summary>
        /// Data is being preloaded from the file.
        /// </summary>
        Preloading = 0x00008000,
        /// <summary>
        /// Must be loaded for game client.
        /// </summary>
        LoadForClient = 0x00010000,
        /// <summary>
        /// Must be loaded for game server.
        /// </summary>
        LoadForServer = 0x00020000,
        /// <summary>
        /// Must be loaded for editor.
        /// </summary>
        LoadForEditor = 0x00040000,
        /// <summary>
        /// Keep object around (don't garbage collect) for editor even if unreferenced.
        /// </summary>
        Standalone = 0x00080000,
        /// <summary>
        /// Don't load this object for the game client.
        /// </summary>
        NotForClient = 0x00100000,
        /// <summary>
        /// Don't load this object for the game server.
        /// </summary>
        NotForServer = 0x00200000,
        /// <summary>
        /// Don't load this object for the editor.
        /// </summary>
        NotForEdit = 0x00400000,
        /// <summary>
        /// Object Destroy has already been called
        /// </summary>
        Destroyed = 0x00800000,
        /// <summary>
        /// Object needs to be postloaded.
        /// </summary>
        RequirePostLoad = 0x01000000,
        /// <summary>
        /// This object has an execution stack allocated and is ready to execute UnrealScript code.
        /// </summary>
        HasStack = 0x02000000,
        /// <summary>
        /// Class or name is defined in C++ code and must be bound at load-time. (UClass only)
        /// </summary>
        Intrinsic = 0x04000000,
        /// <summary>
        /// Class or name is defined in C++ code and must be bound at load-time. (UClass only)
        /// </summary>
        Native = 0x04000000,
        /// <summary>
        /// Marked (for debugging)
        /// </summary>
        Marked = 0x08000000,
        /// <summary>
        /// ShutdownAfterError called.
        /// </summary>
        ErrorShutdown = 0x10000000,
        /// <summary>
        /// For debugging Serialize calls
        /// </summary>
        DebugPostLoad = 0x20000000,
        /// <summary>
        /// For debugging Serialize calls
        /// </summary>
        DebugSerialize = 0x40000000,
        /// <summary>
        /// For debugging Destroy calls
        /// </summary>
        DebugDestroy = 0x80000000
    }
}
