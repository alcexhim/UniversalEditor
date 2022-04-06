using System;
using System.Collections.Generic;

using UniversalEditor.ObjectModels.Multimedia3D.Scene;

using UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Internal;
using UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy
{
	public class IntrinsicGraphicsBinaryDataFormat : DataFormat
	{
		//skin29.igb statistics from Finalizer
		// Primary object type: igAnimationDatabase
		// inherits from: igObject, igNamedObject, igInfo, igAnimationDatabase
		// object size: 40
		//memory pool: Main.Default
		// Allocated instances: 1

		// igAnimationDatabase("miku_anim_db") - 0x037cef70
		// -- _name: miku_anim_db
		// -- _resolveState: true
		// -- _skeletonList: igSkeletonList(0 object)
		// -- _animationList: igAnimationList(0 object)
		// -- _skinList: igSkinList(1 object)
		// -- -- igSkin("miku_skin") - 0x03059310
		// -- -- -- _name: miku_skin
		// -- -- -- _skinnedGraph: igAttrSet("sceneGraph") - 0x038aea98
		// -- -- -- -- ...
		// -- -- -- _bound: NULL
		// -- _appearanceList: igAppearanceList(0 object)
		// -- _combinerList: igAnimationCombinerList(0 object)

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(SceneObjectModel), DataFormatCapabilities.All);
				// _dfr.Filters.Add("Alchemy Intrinsic Graphics Binary", new string[] { "*.igb" });
				_dfr.Sources.Add("http://www.siliconstudio.co.jp/middleware/alchemy/index.html");
			}
			return _dfr;
		}

		public IntrinsicGraphicsBinaryDataFormat()
		{
			InitializeObjectSizes();
		}

		private Dictionary<string, int> objectSizes = new Dictionary<string, int>();
		public void InitializeObjectSizes()
		{
			objectSizes["igObject"] = 0x00;
			objectSizes["igAABox"] = 0x06;
			objectSizes["igActor"] = 0x21;
			objectSizes["igActorInfo"] = 0x15;
			objectSizes["igActorList"] = 0x09;
			objectSizes["igAnimation"] = 0x1B;
			objectSizes["igAnimationBinding"] = 0x0F;
			objectSizes["igAnimationBindingList"] = 0x09;
			objectSizes["igAnimationCombiner"] = 0x2D;
			objectSizes["igAnimationCombinerBoneInfo"] = 0x15;
			objectSizes["igAnimationCombinerBoneInfoList"] = 0x09;
			objectSizes["igAnimationCombinerBoneInfoListList"] = 0x09;
			objectSizes["igAnimationCombinerList"] = 0x09;
			objectSizes["igAnimationDatabase"] = 0x15;
			objectSizes["igAnimationList"] = 0x09;
			objectSizes["igAnimationModifierList"] = 0x09;
			objectSizes["igAnimationState"] = 0x45;
			objectSizes["igAnimationStateList"] = 0x09;
			objectSizes["igAnimationSystem"] = 0x06;
			objectSizes["igAnimationTrack"] = 0x0C;
			objectSizes["igAnimationTrackList"] = 0x09;
			objectSizes["igAnimationTransitionDefinitionList"] = 0x09;
			objectSizes["igAppearance"] = 0x12;
			objectSizes["igAppearanceList"] = 0x09;
			objectSizes["igAttr"] = 0x03;
			objectSizes["igAttrList"] = 0x09;
			objectSizes["igAttrSet"] = 0x12;
			objectSizes["igBitMask"] = 0x0C;
			objectSizes["igBlendFunctionAttr"] = 0x21;
			objectSizes["igBlendMatrixSelect"] = 0x1B;
			objectSizes["igBlendStateAttr"] = 0x06;
			objectSizes["igCamera"] = 0x27;
			objectSizes["igClut"] = 0x0F;
			objectSizes["igColorAttr"] = 0x06;
			objectSizes["igCullFaceAttr"] = 0x09;
			objectSizes["igDataList"] = 0x09;
			objectSizes["igDataPump"] = 0x0C;
			objectSizes["igDataPumpFloatLinearInterface"] = 0x06;
			objectSizes["igDataPumpFloatSource"] = 0x12;
			objectSizes["igDataPumpInfo"] = 0x09;
			objectSizes["igDataPumpInterface"] = 0x00;
			objectSizes["igDataPumpList"] = 0x09;
			objectSizes["igDataPumpVec4fLinearInterface"] = 0x06;
			objectSizes["igDataPumpVec4fSource"] = 0x12;
			objectSizes["igDataPumpSource"] = 0x0F;
			objectSizes["igDirEntry"] = 0x03;
			objectSizes["igDrawableAttr"] = 0x03;
			objectSizes["igEnbayaAnimationSource"] = 0x0C;
			objectSizes["igEnbayaTransformSource"] = 0x06;
			objectSizes["igExternalDirEntry"] = 0x0C;
			objectSizes["igExternalImageEntry"] = 0x0C;
			objectSizes["igExternalIndexedEntry"] = 0x0F;
			objectSizes["igExternalInfoEntry"] = 0x0C;
			objectSizes["igFloatList"] = 0x09;
			objectSizes["igGeometry"] = 0x12;
			objectSizes["igGeometryAttr"] = 0x1E;
			objectSizes["igGeometryAttr2"] = 0x1B;
			objectSizes["igGeometryAttr1_5"] = 0x21;
			objectSizes["igGraphPath"] = 0x09;
			objectSizes["igGraphPathList"] = 0x09;
			objectSizes["igGroup"] = 0x0C;
			objectSizes["igImage"] = 0x3F;
			objectSizes["igImageMipMapList"] = 0x09;
			objectSizes["igIndexArray"] = 0x0C;
			objectSizes["igInfo"] = 0x06;
			objectSizes["igInfoList"] = 0x09;
			objectSizes["igIntList"] = 0x09;
			objectSizes["igIntListList"] = 0x09;
			objectSizes["igLongList"] = 0x09;
			objectSizes["igMaterialAttr"] = 0x15;
			objectSizes["igMaterialModeAttr"] = 0x06;
			objectSizes["igMatrixObject"] = 0x03;
			objectSizes["igMatrixObjectList"] = 0x09;
			objectSizes["igMemoryDirEntry"] = 0x12;
			objectSizes["igModelViewMatrixBoneSelect"] = 0x0F;
			objectSizes["igModelViewMatrixBoneSelectList"] = 0x09;
			objectSizes["igMorphBase"] = 0x27;
			objectSizes["igMorphedGeometryAttr2"] = 0x0F;
			objectSizes["igMorphInstance"] = 0x27;
			objectSizes["igMorphInstance2"] = 0x1E;
			objectSizes["igMorphVertexArrayList"] = 0x12;
			objectSizes["igNamedObject"] = 0x03;
			objectSizes["igNode"] = 0x09;
			objectSizes["igNodeList"] = 0x09;
			objectSizes["igObjectDirEntry"] = 0x09;
			objectSizes["igObjectList"] = 0x09;
			objectSizes["igPrimLengthArray"] = 0x09;
			objectSizes["igPrimLengthArray1_1"] = 0x09;
			objectSizes["igQuaternionfList"] = 0x09;
			objectSizes["igSceneInfo"] = 0x1B;
			objectSizes["igShadeModelAttr"] = 0x06;
			objectSizes["igSkeleton"] = 0x0F;
			objectSizes["igSkeletonBoneInfo"] = 0x0C;
			objectSizes["igSkeletonBoneInfoList"] = 0x09;
			objectSizes["igSkeletonList"] = 0x09;
			objectSizes["igSkin"] = 0x09;
			objectSizes["igSkinList"] = 0x09;
			objectSizes["igStringObjList"] = 0x09;
			objectSizes["igTextureAttr"] = 0x27;
			objectSizes["igTextureBindAttr"] = 0x09;
			objectSizes["igTextureInfo"] = 0x09;
			objectSizes["igTextureList"] = 0x09;
			objectSizes["igTextureMatrixStateAttr"] = 0x09;
			objectSizes["igTextureStateAttr"] = 0x09;
			objectSizes["igTransform"] = 0x15;
			objectSizes["igTransformSequence"] = 0x18;
			objectSizes["igTransformSequence1_5"] = 0x33;
			objectSizes["igTransformSource"] = 0x00;
			objectSizes["igUnsignedCharList"] = 0x09;
			objectSizes["igUnsignedIntList"] = 0x09;
			objectSizes["igVec2fList"] = 0x09;
			objectSizes["igVec3fList"] = 0x09;
			objectSizes["igVec3fListList"] = 0x09;
			objectSizes["igVec4fList"] = 0x09;
			objectSizes["igVec4fAlignedList"] = 0x09;
			objectSizes["igVec4ucList"] = 0x09;
			objectSizes["igVertexArray"] = 0x0C;
			objectSizes["igVertexArray1_1"] = 0x18;
			objectSizes["igVertexArray2"] = 0x09;
			objectSizes["igVertexArray2List"] = 0x09;
			objectSizes["igVertexBlendMatrixListAttr"] = 0x0C;
			objectSizes["igVertexBlendStateAttr"] = 0x06;
			objectSizes["igVertexData"] = 0x21;
			objectSizes["igVertexStream"] = 0x12;
			objectSizes["igVisualAttribute"] = 0x03;
			objectSizes["igVolume"] = 0x00;
		}

		private List<string> stringTableEntries = new List<string>();
		private List<IGB_ENTRY> entryTable = new List<IGB_ENTRY>();
		private string[] memoryPoolNames = null;

		/// <summary>
		/// Classes (nodes) in the IGB schema.
		/// </summary>
		private IGBNodeTypeInfo[] nodeTable = null;
		private List<IGB_ENTRY> objectEntryTable = new List<IGB_ENTRY>();
		private List<IGB_ENTRY> memoryEntryTable = new List<IGB_ENTRY>();
		private List<IGB_ENTRY> externalEntryTable = new List<IGB_ENTRY>();

		private igBase[] objrefs = null;

		private igBase creat(string name)
		{
			igBase node = null;

			System.Reflection.Assembly thisAsm = typeof(igBase).Assembly;
			string nodeTypeName = String.Format("UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes.{0}", name);
			node = (igBase)thisAsm.CreateInstance(nodeTypeName);

			if (node == null)
			{
				throw new TypeLoadException(String.Format("unknown node type name {0}", name));
			}

			node.TypeName = name;
			return node;
		}
		private T creat<T>(string name) where T : igBase
		{
			return (T)creat(name);
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SceneObjectModel scene = (objectModel as SceneObjectModel);
			if (scene == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = Accessor.Reader;


			#region Header
			uint entries_size = br.ReadUInt32();				// data
			uint entries_count = br.ReadUInt32();		// count

			uint nodeTable_size = br.ReadUInt32();		// capacity
			uint nodeTable_count = br.ReadUInt32();

			uint unknown1_size = br.ReadUInt32(); // node table offset?
			uint unknown1_count = br.ReadUInt32();

			uint unknown2_size = br.ReadUInt32();
			uint unknown2_count = br.ReadUInt32();

			uint fieldStringTable_size = br.ReadUInt32();
			uint fieldStringTable_count = br.ReadUInt32();

			uint igbMagic = br.ReadUInt32(); // 0xFADA
			if (igbMagic != 0xFADA)
			{
				throw new InvalidDataFormatException("file does not contain igbMagic 0xFADA at offset 0x28");
			}
			uint igbVersion = br.ReadUInt32(); // 0x00000009
			if (false) // igbVersion != 0x00000009)
			{
				throw new InvalidDataFormatException("file does not contain igbVersion 0x00000009 at offset 0x2C");
			}

			uint stringTable_size = br.ReadUInt32();
			uint stringTable_count = br.ReadUInt32();
			#endregion
			#region String Table
			for (uint i = 0; i < stringTable_count; i++)
			{
				uint stringTableEntryLength = br.ReadUInt32();
				string stringTableEntry = br.ReadFixedLengthString(stringTableEntryLength);
				stringTableEntry = stringTableEntry.TrimNull();

				stringTableEntries.Add(stringTableEntry);
			}
			#endregion
			#region Field Table
			IGB_FIELD_ENTRY[] fieldTypes = new IGB_FIELD_ENTRY[fieldStringTable_count];
			for (uint i = 0; i < fieldStringTable_count; i++)
			{
				fieldTypes[i].nameLength = br.ReadUInt32();
				fieldTypes[i].param2 = br.ReadUInt32();
				fieldTypes[i].param3 = br.ReadUInt32();
			}
			for (int i = 0; i < fieldStringTable_count; i++)
			{
				string stringTableEntry = br.ReadFixedLengthString(fieldTypes[i].nameLength);
				stringTableEntry = stringTableEntry.TrimNull();
				fieldTypes[i].name = stringTableEntry;
			}
			#endregion
			#region Data String Table
			uint dataStringTable_size = br.ReadUInt32();
			uint dataStringTable_unknown1 = br.ReadUInt32();
			uint dataStringTable_count = br.ReadUInt32();

			uint[] dataStringTable_entrySizes = new uint[dataStringTable_count];
			string[] dataTypes = new string[dataStringTable_count];
			for (int i = 0; i < dataStringTable_count; i++)
			{
				dataStringTable_entrySizes[i] = br.ReadUInt32();
			}
			for (int i = 0; i < dataStringTable_count; i++)
			{
				string entry = br.ReadFixedLengthString(dataStringTable_entrySizes[i]);
				entry = entry.TrimNull();
				dataTypes[i] = entry;
			}
			#endregion
			#region Node Type Info
			nodeTable = new IGBNodeTypeInfo[nodeTable_count];
			for (int i = 0; i < nodeTable_count; i++)
			{
				Internal.IGBNodeTypeInfo ni = new Internal.IGBNodeTypeInfo();
				ni.nameLength = br.ReadUInt32();
				ni.param02 = br.ReadUInt32();
				ni.param03 = br.ReadUInt32();
				ni.fieldCount = br.ReadUInt32();
				ni.parentNodeindex = br.ReadUInt32();
				ni.childCount = br.ReadUInt32();
				nodeTable[i] = ni;
			}

			// bind nodes to parents
			for (int i = 0; i < nodeTable_count; i++)
			{
				if (nodeTable[i].parentNodeindex != UInt32.MaxValue)
				{
					nodeTable[i].parentNode = nodeTable[nodeTable[i].parentNodeindex];
				}
			}

			ushort[][] nodeData = new ushort[nodeTable_count][];

			for (int i = 0; i < nodeTable_count; i++)
			{
				Internal.IGBNodeTypeInfo ni = nodeTable[i];
				ni.name = br.ReadFixedLengthString(ni.nameLength);
				ni.name = ni.name.TrimNull();
				// read node data
				int dataLen = 0x00;

				if (objectSizes.ContainsKey(ni.name))
				{
					dataLen = objectSizes[ni.name];
				}
				else
				{
					throw new InvalidOperationException("Unknown object type " + ni.name + "!");
				}
				nodeData[i] = br.ReadUInt16Array(dataLen);
				nodeTable[i] = ni;
			}
			#endregion
			#region Type String Table
			uint systemTableLength = br.ReadUInt32();

			// read list properties
			uint list_count = br.ReadUInt32(); // 0x01
			uint list_capacity = br.ReadUInt32(); // 0x01, 0x02

			// all lengths first, followed by all strings
			uint[] lengths = new uint[list_count];
			for (uint i = 0; i < list_count; i++)
			{
				uint n = br.ReadUInt32();
				lengths[i] = n;
			}
			string[] items = new string[list_count];
			for (int i = 0; i < list_count; i++)
			{
				string item = br.ReadFixedLengthString(lengths[i]);
				item = item.TrimNull();
				items[i] = item;
			}

			// read type string table properties
			long memoryPoolNameTableOffset = br.Accessor.Position;
			uint memoryPoolNameTableSize = br.ReadUInt32(); // 0x0239
			uint memoryPoolNameTableCount = br.ReadUInt32(); // number of strings

			// read type string table
			memoryPoolNames = new string[memoryPoolNameTableCount];
			for (uint i = 0; i < memoryPoolNameTableCount; i++)
			{
				string item = br.ReadNullTerminatedString();
				memoryPoolNames[i] = item;
			}

			if (br.Accessor.Position - memoryPoolNameTableSize != memoryPoolNameTableOffset)
			{
				throw new InvalidDataFormatException("sanity check failed - memory pool name table");
			}
			#endregion
			#region Entries
			for (uint i = 0; i < entries_count; i++)
			{
				IGBNodeEntryType entry_type = (IGBNodeEntryType)br.ReadUInt32();
				uint entry_size = br.ReadUInt32();

				// find entry name
				if ((uint)entry_type > nodeTable_count)
				{
					throw new IndexOutOfRangeException("Failed to find entry type name.");
				}

				string entry_name = nodeTable[(int)entry_type].name;

				switch (entry_type)
				{
					case IGBNodeEntryType.Object:
					{
						// read entry
						uint p01 = br.ReadUInt32();
						uint p02 = br.ReadUInt32();
						uint p03 = br.ReadUInt32();

						// save entry
						IGB_ENTRY e;
						e.type = entry_type;
						e.entries = new uint[] { p01, p02, p03 };
						entryTable.Add(e);
						objectEntryTable.Add(e);
						break;
					}
					case IGBNodeEntryType.Memory:
					{
						// read entry
						uint p01 = br.ReadUInt32();
						uint p02 = br.ReadUInt32();
						uint p03 = br.ReadUInt32();
						uint p04 = br.ReadUInt32();
						uint p05 = br.ReadUInt32();
						uint p06 = br.ReadUInt32();

						// save entry
						IGB_ENTRY e;
						e.type = entry_type;
						e.entries = new uint[] { p01, p02, p03, p04, p05, p06 };
						entryTable.Add(e);
						memoryEntryTable.Add(e);
						break;
					}
					case IGBNodeEntryType.External:
					{
						// read entry
						uint p01 = br.ReadUInt32();
						uint p02 = br.ReadUInt32();
						uint p03 = br.ReadUInt32();
						uint p04 = br.ReadUInt32();

						// save entry
						IGB_ENTRY e;
						e.type = entry_type;
						e.entries = new uint[] { p01, p02, p03, p04 };
						entryTable.Add(e);
						externalEntryTable.Add(e);
						break;
					}
					default:
					{
						throw new InvalidOperationException("Unknown entry type 0x" + entry_type.ToString("X").PadLeft(4, '0'));
					}
				}
			}

			// read entry table size
			uint referenceTableSize = br.ReadUInt32();

			// read number of elements
			uint referenceTableCount = br.ReadUInt32();

			referenceTable = new ushort[referenceTableCount];
			// read indices
			for (uint i = 0; i < referenceTableCount; i++)
			{
				ushort index = br.ReadUInt16();
				referenceTable[i] = index;
			}
			#endregion
			#region Tree
			uint treeType = br.ReadUInt32();

			objrefs = new igBase[referenceTableCount];

			for (int i = 0; i < referenceTableCount; i++)
			{
				// directory reference
				ushort directoryReference = referenceTable[i];

				if (directoryReference >= entryTable.Count) throw new IndexOutOfRangeException("Invalid directory reference");

				IGB_ENTRY entry = entryTable[directoryReference];
				if ((uint)entry.type >= nodeTable_count) throw new IndexOutOfRangeException("Invalid directory entry type");

				string entryTypeName = nodeTable[(int)entry.type].name;
				// process entry
				switch (entryTypeName)
				{
					case "igObjectDirEntry":
					{
						// retrieve node name
						uint NT_index = entry.entries[1];
						if (NT_index >= nodeTable_count) throw new IndexOutOfRangeException("Invalid node index.");

						string igTypeName = nodeTable[(int)NT_index].name;

						ReadObjectDirEntry(br, entry, NT_index, igTypeName, i);
						break;
					}
					case "igMemoryDirEntry":
					{
						// don't read anything when you encounter these nodes
						// they appear to act as tree level terminators
						break;
					}
					case "igExternalInfoEntry":
					{
						// don't read anything when you encounter these nodes?
						break;
					}
					default:
					{
						throw new InvalidOperationException("Unknown directory entry type " + entryTypeName + ".");
					}
				}
			}
			#endregion

			uint[] tests = new uint[memoryEntryTable.Count];
			for (uint i = 0; i < memoryEntryTable.Count; i++)
			{
				tests[i] = br.ReadUInt32();
			}
			for (uint i = 0; i < memoryEntryTable.Count; i++)
			{
				IGB_ENTRY entry = entryTable[(int)tests[i]];
				byte[] memdata = br.ReadBytes(entry.entries[1]);

			}
		}

		private ushort[] referenceTable = null;
		private void ReadObjectDirEntry(IO.Reader br, IGB_ENTRY entry, uint NT_index, string igTypeName, int memoryIndex)
		{
			// read chunk header
			uint chunktype = br.ReadUInt32();
			uint chunksize = br.ReadUInt32();

			uint memoryPoolIndex = entry.entries[2];
			string memoryPoolName = null;
			if (memoryPoolIndex != UInt32.MaxValue)
			{
				memoryPoolName = memoryPoolNames[memoryPoolIndex];
			}

			// validate chunk header
			if (chunktype != NT_index)
			{
				throw new InvalidOperationException("Unexpected node " + igTypeName);
			}
			if (chunksize < 0x10)
			{
				throw new ArgumentOutOfRangeException("chunksize", chunksize, "Invalid " + igTypeName + " chunk. Chunksize is too small.");
			}
			if ((chunksize % 0x04) != 0)
			{
				throw new ArgumentOutOfRangeException("chunksize", chunksize, "Invalid " + igTypeName + " chunk. Chunksize not divisible by four.");
			}

			// read chunk data
			uint attrsize = chunksize - 0x08;
			byte[] attrdata = br.ReadBytes(attrsize);

			Accessors.MemoryAccessor attracc = new Accessors.MemoryAccessor(attrdata);
			IO.Reader br1 = attracc.Reader;

			igBase ig = objrefs[memoryIndex];
			if (ig == null)
			{
				ig = creat(igTypeName);
				objrefs[memoryIndex] = ig;
			}
			else
			{
				// instance already allocated with igAllocateObject()
			}

			if (br1.Accessor.Length != objectSizes[igTypeName])
			{

			}

			Type t = ig.GetType();
			igLoadAttributes(t, br1, ig);

			return;

			// process
			switch (igTypeName)
			{
				#region igAABox
				case "igAABox":
				{
					igAABox node = (ig as igAABox);
					if (br1.Accessor.Length != 0x18) throw new ArgumentOutOfRangeException("igAABox.size", br1.Accessor.Length, "must be 0x18 (24)");

					float[] min = br1.ReadSingleArray(3);
					float[] max = br1.ReadSingleArray(3);

					node.Minimum = new PositionVector3(min[0], min[1], min[2]);
					node.Maximum = new PositionVector3(max[0], max[1], max[2]);
					break;
				}
				#endregion
				#region igActor
				case "igActor":
				{
					igActor node = (ig as igActor);
					// validate chunksize
					if (br1.Accessor.Length != 0x68) throw new ArgumentOutOfRangeException("igActor.size", br1.Accessor.Length, "must be 0x68 (104)");

					igLoadAttributes<igNamedObject>(br1, node);

					node.Bound = ReadEntry(br1);
					node.Flags = (igActor.igActorFlags)br1.ReadUInt32();
					node.ChildList = ReadEntry<igList>(br1);
					node.AnimationSystem = ReadEntry<igAnimationSystem>(br1);
					node.BoneMatrixCacheArray = ReadEntry(br1);
					node.BlendMatrixCacheArray = ReadEntry(br1);
					node.Appearance = ReadEntry<igAppearance>(br1);
					node.AnimationDatabase = ReadEntry<igAnimationDatabase>(br1);
					node.ModifierList = ReadEntry<igAnimationModifierList>(br1);
					node.Transform = br1.ReadSingleArray(16); // 4x4 matrix
					break;
				}
				#endregion
				#region igActorInfo
				case "igActorInfo":
				{
					igActorInfo node = (ig as igActorInfo);
					// validate chunksize
					if (br1.Accessor.Length != 0x1C) throw new ArgumentOutOfRangeException("igActorInfo.size", br1.Accessor.Length, "must be 0x1C (28)");

					igLoadAttributes<igNamedObject>(br1, node);
					igLoadAttributes<igInfo>(br1, node);

					uint actorID = br1.ReadUInt32();
					node.Actor = igAllocateObject<igActor>(actorID);

					node.ActorList = ReadEntry<igActorList>(br1);
					node.AnimationDatabase = ReadEntry<igAnimationDatabase>(br1);
					node.CombinerList = ReadEntry<igAnimationCombinerList>(br1);
					node.AppearanceList = ReadEntry<igAppearanceList>(br1);
					break;
				}
				#endregion
				#region igActorList
				case "igActorList":
				{
					igActorList node = (ig as igActorList);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igActorList.size", br1.Accessor.Length, "must be 0x0C (12)");

					uint count = br1.ReadUInt32();
					node.Capacity = br1.ReadUInt32();

					uint data = br1.ReadUInt32();

					throw new Exception("break here to see what we can do about this...");

					/*
					// assuming that data points to the start of the first Actor node in the list...
					// and that Actors in a list are placed in the nodes array contiguously...
					for (uint j = 0; j < count; j++)
					{
						// we can loop through and read each one from nodes[data + i]!
						node.Items.Add(nodes[(int)(data + j)] as igActor);
					}
					*/
					break;
				}
				#endregion
				#region igAnimation
				case "igAnimation":
				{
					igAnimation node = (ig as igAnimation);
					// validate chunksize
					if (br1.Accessor.Length != 0x30) throw new ArgumentOutOfRangeException("igAnimation.size", br1.Accessor.Length, "must be 0x30 (48)");

					igLoadAttributes<igNamedObject>(br1, node);

					node.Priority = br1.ReadUInt32();
					node.BindingList = ReadEntry<igAnimationBindingList>(br1);
					node.TrackList = ReadEntry<igAnimationTrackList>(br1);
					node.TransitionList = ReadEntry<igAnimationTransitionDefinitionList>(br1);
					node.KeyFrameTimeOffset = br1.ReadUInt64();
					node.StartTime = br1.ReadUInt64();
					node.Duration = br1.ReadUInt64();
					node.UseAnimationTransBoolArray = br1.ReadUInt32();
					break;
				}
				#endregion
				#region igAnimationBinding
				case "igAnimationBinding":
				{
					// NOTES:
					//      _chainSwapList and _reflectTrack are typically 0xFFFFFFFF.
					//      are they MEM or DIR entries?

					igAnimationBinding node = (ig as igAnimationBinding);
					// validate chunksize
					if (br1.Accessor.Length != 0x14) throw new ArgumentOutOfRangeException("igAnimationBinding.size", br1.Accessor.Length, "must be 0x14 (20)");

					node.Skeleton = ReadEntry<igSkeleton>(br1);
					node.BoneTrackIndexArray = br1.ReadUInt32(); // TODO: this is an Entry, what do we do?
					node.BindCount = br1.ReadUInt32();
					node.ChainSwapList = br1.ReadUInt32();
					node.ReflectTrack = br1.ReadUInt32();
					break;
				}
				#endregion
				#region igAnimationBindingList
				case "igAnimationBindingList":
				{
					igAnimationBindingList node = (ig as igAnimationBindingList);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationBindingList.size", br1.Accessor.Length, "must be 0x0C (12)");

					uint count = br1.ReadUInt32();
					node.Capacity = br1.ReadUInt32();

					throw new Exception("break here to see what we can do about this...");

					// assuming that data points to the start of the first Actor node in the list...
					// and that Actors in a list are placed in the nodes array contiguously...
					for (uint j = 0; j < count; j++)
					{
						// we can loop through and read each one from nodes[data + i]!
						// node.Items.Add(nodes[(int)(data + j)] as igAnimationBinding);
					}
					break;
				}
				#endregion
				#region igAnimationCombiner
				case "igAnimationCombiner":
				{
					igAnimationCombiner node = (ig as igAnimationCombiner);
					// validate chunksize
					if (br1.Accessor.Length != 0x50) throw new ArgumentOutOfRangeException("igAnimationCombiner.size", br1.Accessor.Length, "must be 0x50 (80)");

					igLoadAttributes<igNamedObject>(br1, node);

					node.Skeleton = ReadEntry<igSkeleton>(br1);
					node.BoneInfoListList = ReadEntry<igAnimationCombinerBoneInfoListList>(br1);
					node.BoneInfoListBase = ReadEntry<igAnimationCombinerBoneInfoList>(br1);
					node.AnimationStateList = ReadEntry<igAnimationStateList>(br1);
					node.ResultQuaternionArray = ReadEntry<igQuaternionfList>(br1);
					// node.AnimationCacheMatrixArray =
					igBase unknown1 = ReadEntry(br1);

					node.CacheTime = br1.ReadUInt64();
					node.CacheValid = br1.ReadInt32();

					igBase boneMatrixArray = ReadEntry(br1);
					igBase blendMatrixArray = ReadEntry(br1);
					node.AnimationStateTime = br1.ReadUInt64();
					node.LastCleanStateTime = (int)br1.ReadUInt64();
					node.CleanStateTimeThreshold = br1.ReadUInt64();
					node.CleanStatesTransitionMargin = (int)br1.ReadUInt64();
					break;
				}
				#endregion
				#region igAnimationCombinerBoneInfo
				case "igAnimationCombinerBoneInfo":
				{
					igAnimationCombinerBoneInfo node = (ig as igAnimationCombinerBoneInfo);
					// validate chunksize
					if (br1.Accessor.Length != 0x30) throw new ArgumentOutOfRangeException("igAnimationCombinerBoneInfo.size", br1.Accessor.Length, "must be 0x30 (48)");

					node.AnimationState = ReadEntry<igAnimationState>(br1);
					igBase transformSource = ReadEntry(br1);

					float[] constantQuaternion = br1.ReadSingleArray(4);
					node.ConstantQuaternion = new PositionVector4(constantQuaternion[0], constantQuaternion[1], constantQuaternion[2], constantQuaternion[3]);

					float[] constantTranslation = br1.ReadSingleArray(3);
					node.ConstantTranslation = new PositionVector3(constantTranslation[0], constantTranslation[1], constantTranslation[2]);

					node.Priority = br1.ReadUInt32();
					node.AnimationDrivenChannels = br1.ReadUInt32();
					node.Reflect = br1.ReadUInt32();
					break;
				}
				#endregion
				#region igAnimationCombinerBoneInfoList
				case "igAnimationCombinerBoneInfoList":
				{
					igAnimationCombinerBoneInfoList node = (ig as igAnimationCombinerBoneInfoList);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationCombinerBoneInfoList.size", br1.Accessor.Length, "must be 0x0C (12)");

					uint count = br1.ReadUInt32();
					node.Capacity = br1.ReadUInt32();

					throw new Exception("break here to see what we can do about this...");

					// assuming that data points to the start of the first item node in the list...
					// and that items in a list are placed in the nodes array contiguously...
					for (uint j = 0; j < count; j++)
					{
						// we can loop through and read each one from nodes[data + i]!
						// node.Items.Add(nodes[data + i] as igAnimationCombinerBoneInfo);
					}
					break;
				}
				#endregion
				#region igAnimationCombinerBoneInfoListList
				case "igAnimationCombinerBoneInfoListList":
				{
					igAnimationCombinerBoneInfoListList node = (ig as igAnimationCombinerBoneInfoListList);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationCombinerBoneInfoListList.size", br1.Accessor.Length, "must be 0x0C (12)");

					uint count = br1.ReadUInt32();
					node.Capacity = br1.ReadUInt32();

					throw new Exception("break here to see what we can do about this...");

					// assuming that data points to the start of the first item node in the list...
					// and that items in a list are placed in the nodes array contiguously...
					for (uint j = 0; j < count; j++)
					{
						// we can loop through and read each one from nodes[data + i]!
						// node.Items.Add(nodes[data + j] as igAnimationCombinerBoneInfoList);
					}
					break;
				}
				#endregion
				#region igAnimationCombinerList
				case "igAnimationCombinerList":
				{
					igAnimationCombinerList node = (ig as igAnimationCombinerList);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationCombinerList.size", br1.Accessor.Length, "must be 0x0C (12)");

					uint count = br1.ReadUInt32();
					node.Capacity = br1.ReadUInt32();

					throw new Exception("break here to see what we can do about this...");

					// assuming that data points to the start of the first item node in the list...
					// and that items in a list are placed in the nodes array contiguously...
					for (uint j = 0; j < count; j++)
					{
						// we can loop through and read each one from nodes[data + i]!
						// node.Items.Add(nodes[(int)(data + j)] as igAnimationCombiner);
					}
					break;
				}
				#endregion
				#region igAnimationDatabase
				case "igAnimationDatabase":
				{
					igAnimationDatabase node = (ig as igAnimationDatabase);
					// validate chunksize
					if (br1.Accessor.Length != 0x1C) throw new ArgumentOutOfRangeException("igAnimationDatabase.size", br1.Accessor.Length, "must be 0x1C (28)");

					igLoadAttributes<igNamedObject>(br1, node);
					igLoadAttributes<igInfo>(br1, node);

					throw new Exception("figure out what these children are");

					igBase child1 = ReadEntry(br1);
					igBase child2 = ReadEntry(br1);
					igBase child3 = ReadEntry(br1);
					igBase child4 = ReadEntry(br1);
					igBase child5 = ReadEntry(br1);
					break;
				}
				#endregion
				#region igAnimationList
				case "igAnimationList":
				{
					igAnimationList node = (ig as igAnimationList);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationList.size", br1.Accessor.Length, "must be 0x0C (12)");

					uint count = br1.ReadUInt32();
					node.Capacity = br1.ReadUInt32();

					throw new Exception("break here to see what we can do about this...");

					// assuming that data points to the start of the first item node in the list...
					// and that items in a list are placed in the nodes array contiguously...
					for (uint j = 0; j < count; j++)
					{
						// we can loop through and read each one from nodes[data + i]!
						// node.Items.Add(nodes[(int)(data + i)] as igAnimation);
					}
					break;
				}
				#endregion
				#region igAnimationModifierList
				case "igAnimationModifierList":
				{
					igAnimationModifierList node = (ig as igAnimationModifierList);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationModifierList.size", br1.Accessor.Length, "must be 0x0C (12)");

					uint count = br1.ReadUInt32();
					node.Capacity = br1.ReadUInt32();

					throw new Exception("break here to see what we can do about this...");

					// TODO: what kind of nodes does igAnimationModifierList store?
					// there is no such thing (yet) as an igAnimationModifier

					/*
					// assuming that data points to the start of the first item node in the list...
					// and that items in a list are placed in the nodes array contiguously...
					for (uint j = 0; j < count; i++)
					{
						// we can loop through and read each one from nodes[data + i]!
						// node.Items.Add(nodes[(int)(data + j)]);
					}
					*/
					break;
				}
				#endregion
				#region igAnimationState
				case "igAnimationState":
				{
					igAnimationState node = (ig as igAnimationState);
					// validate chunksize
					if (br1.Accessor.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationList.size", br1.Accessor.Length, "must be 0x0C (12)");

					node.Animation = ReadEntry<igAnimation>(br1);
					node.CombineMode = br1.ReadUInt32();
					node.TransitionMode = br1.ReadUInt32();
					node.Status = br1.ReadUInt32();
					node.BaseState = br1.ReadUInt32();
					node.New = br1.ReadUInt32();
					node.CurrentBlendRatio = br1.ReadSingle();
					node.LocalTime = br1.ReadUInt64();
					node.BaseTransitionTime = br1.ReadUInt64();
					node.TimeScale = br1.ReadSingle();
					node.TimeBias = br1.ReadUInt64();
					node.BlendStartTime = br1.ReadUInt64();
					node.BlendStartRatio = br1.ReadSingle();
					node.BlendRatioRange = br1.ReadSingle();
					node.BlendDuration = br1.ReadUInt64();
					node.AnimationStartTime = br1.ReadUInt64();
					node.CycleMatchTargetState = br1.ReadUInt32();
					node.IsCycleMatchTarget = br1.ReadUInt32();
					node.CycleMatchDisable = br1.ReadUInt32();
					node.ManualCycleMatch = br1.ReadUInt32();
					node.CycleMatchDuration = br1.ReadUInt64();
					node.CycleMatchDurationRange = br1.ReadUInt32();
					node.CycleMatchTargetDuration = br1.ReadUInt64();
					node.FastCacheDecodingState = br1.ReadUInt32();
					break;
				}
				#endregion
				#region igGraphPath
				case "igGraphPath":
				{
					igGraphPath node = (ig as igGraphPath);

					// igGraphPath members
					uint hPath = br1.ReadUInt32();
					igNodeList path = igAllocateObject<igNodeList>(hPath);

					break;
				}
				#endregion
				#region igGraphPathList
				case "igGraphPathList":
				{
					igGraphPathList node = (ig as igGraphPathList);

					// igObjectList members
					uint count = br1.ReadUInt32();
					uint capacity = br1.ReadUInt32();
					uint data = br1.ReadUInt32();

					break;
				}
				#endregion
				#region igAttrSet
				case "igAttrSet":
				{
					igAttrSet node = (ig as igAttrSet);

					igLoadAttributes<igNamedObject>(br1, node);


					uint hChildList = br1.ReadUInt32();
					uint hAttributeList = br1.ReadUInt32();

					node.ChildList = igAllocateObject<igNodeList>(hChildList);
					node.AttributeList = igAllocateObject<igAttrList>(hAttributeList);
					node.RushState = ReadBoolean32(br1);
					break;
				}
				#endregion
				#region igAttrList
				case "igAttrList":
				{
					// igObjectList fields
					uint count = br1.ReadUInt32();
					uint capacity = br1.ReadUInt32();
					uint data = br1.ReadUInt32();
					break;
				}
				#endregion
				#region igNodeList
				case "igNodeList":
				{
					igNodeList node = (ig as igNodeList);

					// igObjectList fields
					uint count = br1.ReadUInt32();
					uint capacity = br1.ReadUInt32();
					uint data = br1.ReadUInt32();

					node.Capacity = capacity;
					// node.Data = fieldRefs[data];
					break;
				}
				#endregion
				#region igTextureList
				case "igTextureList":
				{
					igTextureList node = (ig as igTextureList);

					// igObjectList fields
					uint count = br1.ReadUInt32();
					uint capacity = br1.ReadUInt32();
					node.Capacity = capacity;

					int data = br1.ReadInt32();

					break;
				}
				#endregion
				#region igGeometryAttr1_5
				case "igGeometryAttr1_5":
				{
					igGeometryAttr1_5 node = (ig as igGeometryAttr1_5);
					igLoadAttributes<igAttr>(br1, node);
					igLoadAttributes<igGeometryAttr>(br1, node);
					igLoadAttributes<igGeometryAttr1_5>(br1, node);
					break;
				}
				#endregion
				default:
				{
					break;
				}
			}
		}

		private void igReflectionLoadAttributes(Type t, Reader br1, igBase ig)
		{
			throw new NotImplementedException();
		}

		private bool ReadBoolean32(Reader br1)
		{
			return br1.ReadUInt32() != 0;
		}

		private void igLoadAttributes(Type nodeType, Reader br1, igBase node)
		{
			if (nodeType == typeof(igNamedObject))
			{
				// igNamedObject members
				string objectName = ReadName32(br1);
				(node as igNamedObject).Name = objectName;
			}
			else if (nodeType == typeof(igAABox))
			{
				float[] min = br1.ReadSingleArray(3);
				float[] max = br1.ReadSingleArray(3);

				(node as igAABox).Minimum = new PositionVector3(min[0], min[1], min[2]);
				(node as igAABox).Maximum = new PositionVector3(max[0], max[1], max[2]);
			}
			else if (nodeType == typeof(igAttr))
			{
				(node as igAttr).CachedUnitID = br1.ReadUInt32();
			}
			else if (nodeType == typeof(igAttrSet))
			{
				igLoadAttributes<igGroup>(br1, node as igGroup);

				uint hChildList = br1.ReadUInt32();
				uint hAttributeList = br1.ReadUInt32();

				(node as igAttrSet).ChildList = igAllocateObject<igNodeList>(hChildList);
				(node as igAttrSet).AttributeList = igAllocateObject<igAttrList>(hAttributeList);
				(node as igAttrSet).RushState = ReadBoolean32(br1);
			}
			else if (nodeType == typeof(igAnimationState))
			{
				(node as igAnimationState).Animation = ReadEntry<igAnimation>(br1);
				(node as igAnimationState).CombineMode = br1.ReadUInt32();
				(node as igAnimationState).TransitionMode = br1.ReadUInt32();
				(node as igAnimationState).Status = br1.ReadUInt32();
				(node as igAnimationState).BaseState = br1.ReadUInt32();
				(node as igAnimationState).New = br1.ReadUInt32();
				(node as igAnimationState).CurrentBlendRatio = br1.ReadSingle();
				(node as igAnimationState).LocalTime = br1.ReadUInt64();
				(node as igAnimationState).BaseTransitionTime = br1.ReadUInt64();
				(node as igAnimationState).TimeScale = br1.ReadSingle();
				(node as igAnimationState).TimeBias = br1.ReadUInt64();
				(node as igAnimationState).BlendStartTime = br1.ReadUInt64();
				(node as igAnimationState).BlendStartRatio = br1.ReadSingle();
				(node as igAnimationState).BlendRatioRange = br1.ReadSingle();
				(node as igAnimationState).BlendDuration = br1.ReadUInt64();
				(node as igAnimationState).AnimationStartTime = br1.ReadUInt64();
				(node as igAnimationState).CycleMatchTargetState = br1.ReadUInt32();
				(node as igAnimationState).IsCycleMatchTarget = br1.ReadUInt32();
				(node as igAnimationState).CycleMatchDisable = br1.ReadUInt32();
				(node as igAnimationState).ManualCycleMatch = br1.ReadUInt32();
				(node as igAnimationState).CycleMatchDuration = br1.ReadUInt64();
				(node as igAnimationState).CycleMatchDurationRange = br1.ReadUInt32();
				(node as igAnimationState).CycleMatchTargetDuration = br1.ReadUInt64();
				(node as igAnimationState).FastCacheDecodingState = br1.ReadUInt32();
			}
			else if (nodeType == typeof(igClut))
			{
				((igClut)node).Fmt = br1.ReadUInt32();
				((igClut)node).NumEntries = br1.ReadUInt32();
				((igClut)node).Stride = br1.ReadUInt32();
				uint hData = br1.ReadUInt32();
				((igClut)node).ClutSize = br1.ReadUInt32();
			}
			else if (nodeType == typeof(igGeometryAttr))
			{
				igLoadAttributes<igAttr>(br1, ((igGeometryAttr)node));

				uint hVertexArray = br1.ReadUInt32();
				(node as igGeometryAttr).VertexArray = igAllocateObject<igDx9VertexArray1_1>(hVertexArray);
				uint hIndexArray = br1.ReadUInt32();

				(node as igGeometryAttr).PrimitiveType = (IGBPrimitiveType)br1.ReadUInt32();
				(node as igGeometryAttr).PrimitiveCount = br1.ReadUInt32();
				(node as igGeometryAttr).Offset = br1.ReadUInt32();
				(node as igGeometryAttr).PrimitiveLengths = br1.ReadUInt32();
				(node as igGeometryAttr).Handle = br1.ReadUInt32();
				(node as igGeometryAttr).dPds = br1.ReadUInt32();
				(node as igGeometryAttr).dPdt = br1.ReadUInt32();
			}
			else if (nodeType == typeof(igGeometryAttr1_5))
			{
				igLoadAttributes<igGeometryAttr>(br1, ((igGeometryAttr1_5)node));
				(node as igGeometryAttr1_5).StripLengths = igAllocateObject<igPrimLengthArray1_1>(br1.ReadUInt32());
			}
			else if (nodeType == typeof(igGroup))
			{
				igLoadAttributes<igNode>(br1, ((igGroup)node));
			}
			else if (nodeType == typeof(igInfo))
			{
				igLoadAttributes<igNamedObject>(br1, ((igInfo)node));

				// igInfo members
				(node as igInfo).ResolveState = ReadBoolean32(br1);
			}
			else if (nodeType == typeof(igNode))
			{
				igLoadAttributes<igNamedObject>(br1, ((igGroup)node));

				uint hBoundingBox = br1.ReadUInt32();
				(node as igNode).BoundingBox = igAllocateObject<igAABox>(hBoundingBox);
				(node as igNode).Flags = (IGBObjectFlags)br1.ReadUInt32();
			}
			else if (nodeType == typeof(igSceneInfo))
			{
				igLoadAttributes<igInfo>(br1, ((igSceneInfo)node));

				// igSceneInfo members
				uint hSceneGraph = br1.ReadUInt32(); // igAttrSet
				(node as igSceneInfo).SceneGraph = igAllocateObject<igAttrSet>(hSceneGraph);

				uint hTextures = br1.ReadUInt32(); // igTextureList
				(node as igSceneInfo).Textures = igAllocateObject<igTextureList>(hTextures);

				uint hCameras = br1.ReadUInt32(); // igGraphPathList
				(node as igSceneInfo).Cameras = igAllocateObject<igGraphPathList>(hCameras);

				ulong animationBegin = br1.ReadUInt64();
				ulong animationEnd = br1.ReadUInt64();
				float upVectorX = br1.ReadSingle();
				float upVectorY = br1.ReadSingle();
				float upVectorZ = br1.ReadSingle();
				(node as igSceneInfo).UpVector = new PositionVector3(upVectorX, upVectorY, upVectorZ);

				uint hSceneGraphList = br1.ReadUInt32(); // igNodeList
				(node as igSceneInfo).SceneGraphList = igAllocateObject<igNodeList>(hSceneGraphList);
			}
			else if (nodeType == typeof(igShadeModelAttr))
			{
				igLoadAttributes<igAttr>(br1, ((igShadeModelAttr)node));

				((igShadeModelAttr)node).Mode = br1.ReadUInt32();
			}
			else if (nodeType == typeof(igTextureAttr))
			{
				igLoadAttributes<igAttr>(br1, ((igTextureAttr)node));

				((igTextureAttr)node).bColor = br1.ReadUInt32();
				((igTextureAttr)node).MagFilter = (IGBTextureFilter)br1.ReadUInt32();
				((igTextureAttr)node).MinFilter = (IGBTextureFilter)br1.ReadUInt32();
				((igTextureAttr)node).WrapS = (IGBTextureWrap)br1.ReadUInt32();
				((igTextureAttr)node).WrapT = (IGBTextureWrap)br1.ReadUInt32();
				((igTextureAttr)node).MipmapMode = (IGBTextureMipmapMode)br1.ReadUInt32();
				((igTextureAttr)node).Source = (IGBTextureSource)br1.ReadUInt32();
				((igTextureAttr)node).Image = igAllocateObject<igImage>(br1.ReadUInt32());
				((igTextureAttr)node).Paging = ReadBoolean32(br1);
				uint tu = br1.ReadUInt32(); // ???
				((igTextureAttr)node).ImageCount = br1.ReadUInt32();
				((igTextureAttr)node).ImageMipmaps = igAllocateObject<igImageMipMapList>(br1.ReadUInt32());
			}


			else if (nodeType.IsSubclassOf(typeof(igList)))
			{
				uint count = br1.ReadUInt32();
				uint capacity = br1.ReadUInt32();
				uint data = br1.ReadUInt32();
				if (data != UInt32.MaxValue)
				{

				}
			}
			else
			{
				Console.WriteLine(String.Format("unhandled node type '{0}'", nodeType.Name));
			}
		}
		private void igLoadAttributes<T>(Reader br1, T node) where T : igBase
		{
			igLoadAttributes(typeof(T), br1, node);
		}

		private Dictionary<uint, igBase> _preallocatedObjects = new Dictionary<uint, igBase>();

		private T igAllocateObject<T>(uint index) where T : igBase, new()
		{
			if (index == UInt32.MaxValue)
				return null;

			System.Diagnostics.Contracts.Contract.Assert(index >= 0 && index < objrefs.Length);

			/*
			if (objrefs[index] == null)
			{
				if (_preallocatedObjects.ContainsKey(index))
				{
					// FIXME this
					T obj = new T();
					_preallocatedObjects[index];
				}
				_preallocatedObjects[index] = new T();
				// objrefs[index] = new T();
				return (T)_preallocatedObjects[index];
			}
			*/
			if (objrefs[index] == null)
			{
				objrefs[index] = new T();
			}
			return (T)objrefs[index];
		}

		private igBase ReadEntry(IO.Reader br)
		{
			return ReadEntry<igBase>(br);
		}
		private T ReadEntry<T>(IO.Reader br) where T : igBase
		{
			uint item = br.ReadUInt32();
			if (item != 0xFFFFFFFF)
			{
				// test reference #1
				int referenceTableIndex = (int)item;
				int entryTableIndex = referenceTable[referenceTableIndex];
				if (entryTableIndex >= entryTable.Count) throw new ArgumentOutOfRangeException("entryTableIndex", entryTableIndex, "must be less than entryTable.Count (" + entryTable.Count + ")");

				IGB_ENTRY e = entryTable[entryTableIndex];

				// test reference #2
				switch (entryTable[entryTableIndex].type)
				{
					#region igObjectDirEntry
					case IGBNodeEntryType.Object: // igObjectDirEntry
					{
						// test reference #3
						if (e.entries[1] >= nodeTable.Length) throw new InvalidOperationException("Invalid NT reference.");

						// must be correct entry type
						if (nodeTable[(int)e.type].name != "igObjectDirEntry")
						{
							throw new InvalidOperationException("Entry is not an igObjectDirEntry.");
						}

						// extra data to display
						uint NT_index = e.entries[1];
						break;
					}
					#endregion
					#region igMemoryDirEntry
					case IGBNodeEntryType.Memory: // igMemoryDirEntry
					{
						if (nodeTable[(int)e.type].name != "igMemoryDirEntry")
						{
							throw new InvalidOperationException("Entry is not an igMemoryDirEntry.");
						}
						break;
					}
					#endregion
					#region igExternalInfoEntry
					case IGBNodeEntryType.External: // igExternalInfoEntry
					{
						if (nodeTable[(int)e.type].name != "igExternalInfoEntry")
						{
							throw new InvalidOperationException("Entry is not an igExternalInfoEntry.");
						}
						break;
					}
					#endregion
					default:
					{
						throw new InvalidOperationException("Unknown entry type");
					}
				}
			}
			return null;  // (T)nodes[(int)item];
		}

		private string ReadName32(IO.Reader br)
		{
			uint nameIndex = br.ReadUInt32();
			if (nameIndex >= stringTableEntries.Count) throw new ArgumentOutOfRangeException("nameIndex", nameIndex, "must be less than number of string table entries (" + stringTableEntries.Count + ")");
			return stringTableEntries[(int)nameIndex];
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
