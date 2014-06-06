using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.VersatileContainer
{
	public enum VersatileContainerSectionType : uint
	{
        None = 0,
		/// <summary>
		/// A section that contains data stored within the Versatile Container.
		/// Multiple named Sections can point to the same data location.
		/// </summary>
		Section = 1,
		/// <summary>
		/// A section that contains another list of sections. Directories can be
		/// nested within one another.
		/// </summary>
		Directory = 2,
        /// <summary>
        /// A section that doesn't contain any data, but points to the data
        /// contained within another indexed Section.
        /// </summary>
        Reference = 3
	}
}
