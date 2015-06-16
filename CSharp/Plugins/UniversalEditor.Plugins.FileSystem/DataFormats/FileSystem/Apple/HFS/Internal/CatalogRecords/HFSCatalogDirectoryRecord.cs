using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal.CatalogRecords
{
	internal class HFSCatalogDirectoryRecord : HFSCatalogRecord
	{
		/// <summary>
		/// Directory flags.
		/// </summary>
		public HFSDirectoryFlags flags;
		/// <summary>
		/// The directory valence (the number of files and folders directly contained by this
		/// folder). This is equal to the number of file and folder records whose key's parentID is
		/// equal to this folder's folderID.
		/// </summary>
		/// <remarks>
		/// The current Mac OS File Manager programming interfaces require folders to have a valence
		/// less than 32,767. An implementation must enforce this restriction if it wants the volume
		/// to be usable by Mac OS. Values of 32,768 and larger are problematic; 32,767 and smaller
		/// are OK. It's an implementation restriction for the older Mac OS APIs; items 32,768 and
		/// beyond would be unreachable by PBGetCatInfo. As a practical matter, many programs are
		/// likely to fails with anywhere near that many items in a single folder. So, the volume
		/// format allows more than 32,767 items in a folder, but it's probably not a good idea to
		/// exceed that limit right now.
		/// </remarks>
		public int directoryValence;
		/// <summary>
		/// The directory ID.
		/// </summary>
		public int directoryID;
		/// <summary>
		/// The date and time this directory was created.
		/// </summary>
		public int creationTimestamp;
		/// <summary>
		/// The date and time this directory was last modified.
		/// </summary>
		public int modificationTimestamp;
		/// <summary>
		/// HFS+ only. The date and time at which a named fork of this directory was last modified.
		/// </summary>
		public int attributeModificationTimestamp;
		/// <summary>
		/// HFS+ only.
		/// </summary>
		public int lastAccessTimestamp;
		/// <summary>
		/// The date and time this directory was last backed up.
		/// </summary>
		public int lastBackupTimestamp;
		public HFSPlusPermissions permissions;
		/// <summary>
		/// Information used by the Finder.
		/// </summary>
		public HFSDInfo finderUserInformation;
		/// <summary>
		/// Additional information used by the Finder.
		/// </summary>
		public HFSDXInfo finderAdditionalInformation;
		public HFSPlusTextEncoding textEncoding;
	}
}
