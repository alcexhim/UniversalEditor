using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    public class UnrealPackageObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null)
            {
                _omr = base.MakeReferenceInternal();
                _omr.Title = "Unreal Engine package";
                _omr.Path = new string[] { "Game development", "Unreal Engine", "Package" };
            }
            return _omr;
        }

        private List<Guid> mvarPackageGUIDs = new List<Guid>();
        /// <summary>
        /// Unique identifiers used for package downloading from servers.
        /// </summary>
        public List<Guid> PackageGUIDs { get { return mvarPackageGUIDs; } set { mvarPackageGUIDs = value; } }

        private ushort mvarLicenseeNumber = 0;
        /// <summary>
        /// This is the licensee number, different for each game.
        /// </summary>
        public ushort LicenseeNumber { get { return mvarLicenseeNumber; } set { mvarLicenseeNumber = value; } }

        private PackageFlags mvarPackageFlags = PackageFlags.None;
        /// <summary>
        /// Global package flags; i.e., if a package may be downloaded from a game server, etc.
        /// </summary>
        public PackageFlags PackageFlags { get { return mvarPackageFlags; } set { mvarPackageFlags = value; } }

        private NameTableEntry.NameTableEntryCollection mvarNameTableEntries = new NameTableEntry.NameTableEntryCollection();
        /// <summary>
        /// The name-table can be considered an index of all unique names used for objects and
        /// references within the file. Later on, you'll often find indexes into this table instead of
        /// a string containing the object-name.
        /// </summary>
        public NameTableEntry.NameTableEntryCollection NameTableEntries { get { return mvarNameTableEntries; } }

        private ExportTableEntry.ExportTableEntryCollection mvarExportTableEntries = new ExportTableEntry.ExportTableEntryCollection();
        /// <summary>
        /// The export-table is an index for all objects within the package. Every object in the body
        /// of the file has a corresponding entry in this table, with information like offset within
        /// the file etc.
        /// </summary>
        public ExportTableEntry.ExportTableEntryCollection ExportTableEntries { get { return mvarExportTableEntries; } }

        private ImportTableEntry.ImportTableEntryCollection mvarImportTableEntries = new ImportTableEntry.ImportTableEntryCollection();
        public ImportTableEntry.ImportTableEntryCollection ImportTableEntries { get { return mvarImportTableEntries; } }

        private Generation.GenerationCollection mvarGenerations = new Generation.GenerationCollection();
        public Generation.GenerationCollection Generations { get { return mvarGenerations; } }

        public override void Clear()
        {
            mvarLicenseeNumber = 0;
            mvarPackageFlags = UnrealEngine.PackageFlags.None;
            mvarPackageGUIDs.Clear();
            mvarNameTableEntries.Clear();
            mvarExportTableEntries.Clear();
            mvarImportTableEntries.Clear();
        }

        public override void CopyTo(ObjectModel where)
        {
            UnrealPackageObjectModel clone = (where as UnrealPackageObjectModel);
            clone.LicenseeNumber = mvarLicenseeNumber;
            clone.PackageFlags = mvarPackageFlags;
            foreach (Guid guid in mvarPackageGUIDs)
            {
                clone.PackageGUIDs.Add(guid);
            }
            foreach (NameTableEntry entry in mvarNameTableEntries)
            {
                clone.NameTableEntries.Add(entry.Clone() as NameTableEntry);
            }
            foreach (ExportTableEntry entry in mvarExportTableEntries)
            {
                clone.ExportTableEntries.Add(entry.Clone() as ExportTableEntry);
            }
            foreach (ImportTableEntry entry in mvarImportTableEntries)
            {
                clone.ImportTableEntries.Add(entry.Clone() as ImportTableEntry);
            }
        }
    }
}
