using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    [Flags()]
    public enum PackageFlags
    {
        None = 0,
        /// <summary>
        /// The package is allowed to be downloaded to clients freely.
        /// </summary>
        AllowDownload = 0x0001,
        /// <summary>
        /// All objects in the package are optional (i.e. skins, textures) and it's up to the client whether he wants
        /// to download them or not. Not yet implemented; currently ignored.
        /// </summary>
        ClientOptional = 0x0002,
        /// <summary>
        /// This package is only needed on the server side, and the client shouldn't be informed of its presence. This
        /// is used with packages like IpDrv so that it can be updated frequently on the server side without requiring
        /// downloading stuff to the client.
        /// </summary>
        ServerSideOnly = 0x0004,
        /// <summary>
        /// Loaded from linker with broken import links
        /// </summary>
        BrokenLinks = 0x0008,
        /// <summary>
        /// Not trusted
        /// </summary>
        Untrusted = 0x0010,
        /// <summary>
        /// Client needs to download this package
        /// </summary>
        Required = 0x8000
    }
}
