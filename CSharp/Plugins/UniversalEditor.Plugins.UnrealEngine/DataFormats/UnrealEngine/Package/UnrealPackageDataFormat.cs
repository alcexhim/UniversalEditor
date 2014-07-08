using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.UnrealEngine;
using UniversalEditor.Plugins.UnrealEngine;

namespace UniversalEditor.DataFormats.UnrealEngine.Package
{
    public class UnrealPackageDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(UnrealPackageObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Unreal Engine package", new byte?[][] { new byte?[] { 0xC1, 0x83, 0x2A, 0x9E } }, new string[] { "*.u", "*.utx", "*.upk" });
                _dfr.ExportOptions.Add(new CustomOptionText("PackageName", "Package &name:"));
                _dfr.ExportOptions.Add(new CustomOptionNumber("PackageVersion", "Package &version:", 0, UInt16.MinValue, UInt16.MaxValue));
				_dfr.Sources.Add("http://wiki.beyondunreal.com/Legacy:Package_File_Format");
            }
            return _dfr;
        }

        private ushort mvarPackageVersion = 0;
        /// <summary>
        /// Version of the file-format; Unreal1 uses mostly 61-63, UT 67-69; However, note that
        /// quite a few packages are in use with UT that have Unreal1 versions.
        /// </summary>
        public ushort PackageVersion { get { return mvarPackageVersion; } set { mvarPackageVersion = value; } }
        
        private string mvarPackageName = String.Empty;
        public string PackageName { get { return mvarPackageName; } set { mvarPackageName = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            Reader br = base.Accessor.Reader;
            
            UnrealPackageObjectModel upk = (objectModel as UnrealPackageObjectModel);
            #region Header

            // Always "0x9E2A83C1"; use this to verify that you indeed try to read an Unreal-Package
            uint signature = br.ReadUInt32();
            if (signature != 0x9E2A83C1) throw new InvalidDataFormatException("File does not begin with 0x9E2A83C1");

            mvarPackageVersion = br.ReadUInt16();
            upk.LicenseeNumber = br.ReadUInt16();
            upk.PackageFlags = (PackageFlags)br.ReadUInt32(); // 1949392

            if (mvarPackageVersion >= 512)
            {
                uint valueLength = br.ReadUInt32();
                mvarPackageName = br.ReadFixedLengthString(valueLength);
                mvarPackageName = mvarPackageName.TrimNull();

                uint unknown1 = br.ReadUInt32();
                uint unknown2 = br.ReadUInt32();
            }

            // Number of entries in name-table
            uint nameTableEntryCount = br.ReadUInt32();
            // Offset of name-table within the file
            uint nameTableOffset = br.ReadUInt32();

            // Number of entries in export-table
            uint exportTableEntryCount = br.ReadUInt32();
            // Offset of export-table within the file
            uint exportTableOffset = br.ReadUInt32();

            // Number of entries in import-table
            uint importTableEntryCount = br.ReadUInt32();
            // Offset of import-table within the file
            uint importTableOffset = br.ReadUInt32();

            // After the ImportOffset, the header differs between the versions. The only interesting
            // fact, though, is that for fileformat versions => 68, a GUID has been introduced. It can
            // be found right after the ImportOffset:
            if (mvarPackageVersion < 68)
            {
                // number of values in the Heritage Table
                uint heritageTableEntryCount = br.ReadUInt32();
                // offset of the Heritage Table from the beginning of the file
                uint heritageTableOffset = br.ReadUInt32();

                long pos = br.Accessor.Position;
                br.Accessor.Position = heritageTableOffset;

                for (uint i = 0; i < heritageTableEntryCount; i++)
                {
                    upk.PackageGUIDs.Add(br.ReadGuid());
                }

                br.Accessor.Position = pos;
            }
            else if (mvarPackageVersion >= 68)
            {
                upk.PackageGUIDs.Add(br.ReadGuid());

                if (mvarPackageVersion < 512)
                {
                    uint generationCount = br.ReadUInt32();
                    for (uint i = 0; i < generationCount; i++)
                    {
                        Generation generation = new Generation();
                        generation.ExportCount = br.ReadUInt32();
                        generation.NameCount = br.ReadUInt32();
                        upk.Generations.Add(generation);
                    }
                }
            }

            #endregion

            #region Name table
            {
                // The Unreal-Engine introduces two new variable-types. The first one is a rather simple
                // string type, called NAME from now on. The second one is a bit more tricky, these
                // CompactIndices, or INDEX later on, compresses ordinary DWORDs downto one to five BYTEs.

                // The first and most simple one of the three tables is the name-table. The name-table can
                // be considered an index of all unique names used for objects and references within the
                // file. Later on, you'll often find indexes into this table instead of a string
                // containing the object-name.
                br.Accessor.Position = nameTableOffset;
                for (uint i = 0; i < nameTableEntryCount; i++)
                {
                    string name = br.ReadNAME(mvarPackageVersion);
                    if (mvarPackageVersion >= 512)
                    {
                        uint unknown = br.ReadUInt32();
                    }
                    NameTableEntryFlags flags = (NameTableEntryFlags)br.ReadInt32();
                    upk.NameTableEntries.Add(name, flags);
                }
            }
            #endregion

            #region Export table
            {
                // The export-table is an index for all objects within the package. Every object in
                // the body of the file has a corresponding entry in this table, with information like
                // offset within the file etc.
                br.Accessor.Position = exportTableOffset;
                for (uint i = 0; i < exportTableEntryCount; i++)
                {
                    ExportTableEntry entry = new ExportTableEntry();

                    // Class of the object, i.e. "Texture" or "Palette" etc; stored as a
                    // ObjectReference
                    int classIndex = br.ReadINDEX();
                    if (classIndex != 0) entry.ObjectClass = new ObjectReference(classIndex, upk);

                    // Object Parent; again a ObjectReference
                    int objectParentIndex = br.ReadINDEX();
                    if (objectParentIndex != 0) entry.ObjectParent = new ObjectReference(objectParentIndex, upk);

                    // Internal package/group of the object, i.e. ‘Floor’ for floor-textures;
                    // ObjectReference
                    int groupIndex = br.ReadInt32();
                    if (groupIndex != 0) entry.Group = new ObjectReference(groupIndex, upk);

                    // The name of the object; an index into the name-table
                    int objectNameIndex = br.ReadINDEX();
                    if (objectNameIndex >= 0 && objectNameIndex < upk.NameTableEntries.Count)
                    {
                        entry.Name = upk.NameTableEntries[objectNameIndex];
                    }

                    // Flags for the object; described in the appendix
                    entry.Flags = (ObjectFlags)br.ReadInt32();

                    // Total size of the object
                    entry.Size = br.ReadINDEX();

					entry.DataRequest += entry_DataRequest;

                    if (entry.Size != 0)
                    {
                        // Offset of the object; this field only exists if the SerialSize is larger 0
                        entry.Offset = br.ReadINDEX();
                    }
                    upk.ExportTableEntries.Add(entry);
                }
            }
            #endregion

            #region Import table
            {
                br.Accessor.Position = importTableOffset;
                for (uint i = 0; i < importTableEntryCount; i++)
                {
                    // The third table holds references to objects in external packages. For example, a
                    // texture might have a DetailTexture (which makes for the nice structure if have a
                    // very close look at a texture). Now, these DetailTextures are all stored in a
                    // single package (as they are used by many different textures in different package
                    // files). The property of the texture object only needs to store an index into the
                    // import-table then as the entry in the import-table already points to the
                    // DetailTexture in the other package.
                    ImportTableEntry entry = new ImportTableEntry();

                    int classPackageIndex = br.ReadINDEX();
                    if (classPackageIndex >= 0 && classPackageIndex < upk.NameTableEntries.Count)
                    {
                        entry.PackageName = upk.NameTableEntries[classPackageIndex];
                    }

                    int classNameIndex = br.ReadINDEX();
                    if (classNameIndex >= 0 && classNameIndex < upk.NameTableEntries.Count)
                    {
                        entry.ClassName = upk.NameTableEntries[classNameIndex];
                    }

                    int packageReference = br.ReadInt32();
                    entry.Package = new ObjectReference(packageReference, upk);

                    int objectNameIndex = br.ReadINDEX();
                    if (objectNameIndex >= 0 && objectNameIndex < upk.NameTableEntries.Count)
                    {
                        entry.ObjectName = upk.NameTableEntries[objectNameIndex];
                    }
                    upk.ImportTableEntries.Add(entry);
                }
            }
            #endregion
        }

		private void entry_DataRequest(object sender, ObjectModels.FileSystem.DataRequestEventArgs e)
		{
			ExportTableEntry entry = (sender as ExportTableEntry);
			base.Accessor.Seek(entry.Offset, SeekOrigin.Begin);
			e.Data = base.Accessor.Reader.ReadBytes(entry.Size);
		}

        protected override void SaveInternal(ObjectModel objectModel)
        {
            Writer bw = base.Accessor.Writer;
            
            UnrealPackageObjectModel upk = (objectModel as UnrealPackageObjectModel);
            #region Header

            // Always "0x9E2A83C1"; use this to verify that you indeed try to read an Unreal-Package
            bw.WriteUInt32((uint)0x9E2A83C1);
            
            bw.WriteUInt16(mvarPackageVersion);
            bw.WriteUInt16(upk.LicenseeNumber);
            bw.WriteUInt32((uint)upk.PackageFlags); // 1949392

            if (mvarPackageVersion >= 512)
            {
            	bw.WriteUInt32((uint)mvarPackageName.Length);
            	bw.WriteFixedLengthString(mvarPackageName);
            	
                uint unknown1 = 0;
                bw.WriteUInt32(unknown1);
                
                uint unknown2 = 0;
                bw.WriteUInt32(unknown2);
            }

            // Number of entries in name-table
            bw.WriteUInt32((uint)upk.NameTableEntries.Count);
            // Offset of name-table within the file
            uint nameTableOffset = 0;
            bw.WriteUInt32(nameTableOffset);

            // Number of entries in export-table
            bw.WriteUInt32((uint)upk.ExportTableEntries.Count);
            // Offset of export-table within the file
            uint exportTableOffset = 0;
            bw.WriteUInt32(exportTableOffset);

            // Number of entries in import-table
            bw.WriteUInt32((uint)upk.ImportTableEntries.Count);
            // Offset of import-table within the file
            uint importTableOffset = 0;
            bw.WriteUInt32(importTableOffset);

            // After the ImportOffset, the header differs between the versions. The only interesting
            // fact, though, is that for fileformat versions => 68, a GUID has been introduced. It can
            // be found right after the ImportOffset:
            if (mvarPackageVersion < 68)
            {
                // number of values in the Heritage Table
                bw.WriteUInt32((uint)upk.PackageGUIDs.Count);
                // offset of the Heritage Table from the beginning of the file
                uint heritageTableOffset = 0;
                bw.WriteUInt32(heritageTableOffset);

                // TODO: navigate to the heritageTableOffset to write the data
                for (uint i = 0; i < (uint)upk.PackageGUIDs.Count; i++)
                {
                	bw.WriteGuid(upk.PackageGUIDs[(int)i]);
                }
            }
            else if (mvarPackageVersion >= 68)
            {
            	if (upk.PackageGUIDs.Count > 0)
            	{
	            	bw.WriteGuid(upk.PackageGUIDs[0]);
            	}
            	else
            	{
            		Guid guid = Guid.NewGuid();
            		upk.PackageGUIDs.Add(guid);
            		bw.WriteGuid(guid);
            	}
            	
                if (mvarPackageVersion < 512)
                {
                	bw.WriteUInt32((uint)upk.Generations.Count);
                    for (uint i = 0; i < upk.Generations.Count; i++)
                    {
                    	Generation generation = upk.Generations[(int)i];
                        bw.WriteUInt32(generation.ExportCount);
                        bw.WriteUInt32(generation.NameCount);
                    }
                }
            }

            #endregion

            #region Name table
            {
                // The Unreal-Engine introduces two new variable-types. The first one is a rather simple
                // string type, called NAME from now on. The second one is a bit more tricky, these
                // CompactIndices, or INDEX later on, compresses ordinary DWORDs downto one to five BYTEs.

                // The first and most simple one of the three tables is the name-table. The name-table can
                // be considered an index of all unique names used for objects and references within the
                // file. Later on, you'll often find indexes into this table instead of a string
                // containing the object-name.
                
                // TODO: navigate to the name table offset
                // br.BaseStream.Position = nameTableOffset;
                for (uint i = 0; i < upk.NameTableEntries.Count; i++)
                {
                	bw.WriteNAME(upk.NameTableEntries[(int)i].Name, mvarPackageVersion);
                    if (mvarPackageVersion >= 512)
                    {
                        uint unknown = 0;
                        bw.WriteUInt32(unknown);
                    }
                    bw.WriteInt32((int)upk.NameTableEntries[(int)i].Flags);
                }
            }
            #endregion

            #region Export table
            {
                // The export-table is an index for all objects within the package. Every object in
                // the body of the file has a corresponding entry in this table, with information like
                // offset within the file etc.
                // br.BaseStream.Position = exportTableOffset;
                // TODO: navigate to export table offset
                
                for (uint i = 0; i < upk.ExportTableEntries.Count; i++)
                {
                	ExportTableEntry entry = upk.ExportTableEntries[(int)i];

                    // Class of the object, i.e. "Texture" or "Palette" etc; stored as a
                    // ObjectReference
                	if (entry.ObjectClass != null)
                	{
                    	bw.WriteINDEX(entry.ObjectClass.IndexValue);
                	}
                	else
                	{
                		bw.WriteINDEX(0);
                	}

                    // Object Parent; again a ObjectReference
                    if (entry.ObjectParent != null)
                    {
                    	bw.WriteINDEX(entry.ObjectParent.IndexValue);
                    }
                    else
                    {
                    	bw.WriteINDEX(0);
                    }

                    // Internal package/group of the object, i.e. ‘Floor’ for floor-textures;
                    // ObjectReference
                    if (entry.Group != null)
                    {
                    	bw.WriteInt32(entry.Group.IndexValue);
                    }
                    else
                    {
                    	bw.WriteInt32((int)0);
                    }
                    
                    // The name of the object; an index into the name-table
                    bw.WriteINDEX(upk.NameTableEntries.IndexOf(entry.Name));
                    
                    // Flags for the object; described in the appendix
                    bw.WriteInt32((int)entry.Flags);

                    // Total size of the object
                    bw.WriteINDEX(entry.Size);

                    if (entry.Size != 0)
                    {
                        // Offset of the object; this field only exists if the SerialSize is larger 0
                        bw.WriteINDEX(entry.Offset);
                    }
                }
            }
            #endregion

            #region Import table
            {
                for (uint i = 0; i < upk.ImportTableEntries.Count; i++)
                {
                    // The third table holds references to objects in external packages. For example, a
                    // texture might have a DetailTexture (which makes for the nice structure if have a
                    // very close look at a texture). Now, these DetailTextures are all stored in a
                    // single package (as they are used by many different textures in different package
                    // files). The property of the texture object only needs to store an index into the
                    // import-table then as the entry in the import-table already points to the
                    // DetailTexture in the other package.
                    ImportTableEntry entry = upk.ImportTableEntries[(int)i];

                    bw.WriteINDEX(upk.NameTableEntries.IndexOf(entry.PackageName));
                    bw.WriteINDEX(upk.NameTableEntries.IndexOf(entry.ClassName));
                    bw.WriteInt32(entry.Package.IndexValue);
                    bw.WriteINDEX(upk.NameTableEntries.IndexOf(entry.ObjectName));
                }
            }
            #endregion
            bw.Flush();
        }
    }
}
