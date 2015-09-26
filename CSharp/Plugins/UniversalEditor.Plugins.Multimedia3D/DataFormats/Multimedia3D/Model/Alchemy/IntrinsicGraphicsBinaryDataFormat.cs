using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

using UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Internal;
using UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy
{
	public class IntrinsicGraphicsBinaryDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Alchemy Intrinsic Graphics Binary", new string[] { "*.igb" });
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
			objectSizes.Add("igObject", 0x00);
			objectSizes.Add("igAABox", 0x06);
			objectSizes.Add("igActor", 0x21);
			objectSizes.Add("igActorInfo", 0x15);
			objectSizes.Add("igActorList", 0x09);
			objectSizes.Add("igAnimation", 0x1B);
			objectSizes.Add("igAnimationBinding", 0x0F);
			objectSizes.Add("igAnimationBindingList", 0x09);
			objectSizes.Add("igAnimationCombiner", 0x2D);
			objectSizes.Add("igAnimationCombinerBoneInfo", 0x15);
			objectSizes.Add("igAnimationCombinerBoneInfoList", 0x09);
			objectSizes.Add("igAnimationCombinerBoneInfoListList", 0x09);
			objectSizes.Add("igAnimationCombinerList", 0x09);
			objectSizes.Add("igAnimationDatabase", 0x15);
			objectSizes.Add("igAnimationList", 0x09);
			objectSizes.Add("igAnimationModifierList", 0x09);
			objectSizes.Add("igAnimationState", 0x45);
			objectSizes.Add("igAnimationStateList", 0x09);
			objectSizes.Add("igAnimationSystem", 0x06);
			objectSizes.Add("igAnimationTrack", 0x0C);
			objectSizes.Add("igAnimationTrackList", 0x09);
			objectSizes.Add("igAnimationTransitionDefinitionList", 0x09);
			objectSizes.Add("igAppearance", 0x12);
			objectSizes.Add("igAppearanceList", 0x09);
			objectSizes.Add("igAttr", 0x03);
			objectSizes.Add("igAttrList", 0x09);
			objectSizes.Add("igAttrSet", 0x12);
			objectSizes.Add("igBitMask", 0x0C);
			objectSizes.Add("igBlendFunctionAttr", 0x21);
			objectSizes.Add("igBlendMatrixSelect", 0x1B);
			objectSizes.Add("igBlendStateAttr", 0x06);
			objectSizes.Add("igCamera", 0x27);
			objectSizes.Add("igClut", 0x0F);
			objectSizes.Add("igColorAttr", 0x06);
			objectSizes.Add("igCullFaceAttr", 0x09);
			objectSizes.Add("igDataList", 0x09);
			objectSizes.Add("igDataPump", 0x0C);
			objectSizes.Add("igDataPumpFloatLinearInterface", 0x06);
			objectSizes.Add("igDataPumpFloatSource", 0x12);
			objectSizes.Add("igDataPumpInfo", 0x09);
			objectSizes.Add("igDataPumpInterface", 0x00);
			objectSizes.Add("igDataPumpList", 0x09);
			objectSizes.Add("igDataPumpVec4fLinearInterface", 0x06);
			objectSizes.Add("igDataPumpVec4fSource", 0x12);
			objectSizes.Add("igDataPumpSource", 0x0F);
			objectSizes.Add("igDirEntry", 0x03);
			objectSizes.Add("igDrawableAttr", 0x03);
			objectSizes.Add("igEnbayaAnimationSource", 0x0C);
			objectSizes.Add("igEnbayaTransformSource", 0x06);
			objectSizes.Add("igExternalDirEntry", 0x0C);
			objectSizes.Add("igExternalImageEntry", 0x0C);
			objectSizes.Add("igExternalIndexedEntry", 0x0F);
			objectSizes.Add("igExternalInfoEntry", 0x0C);
			objectSizes.Add("igFloatList", 0x09);
			objectSizes.Add("igGeometry", 0x12);
			objectSizes.Add("igGeometryAttr", 0x1E);
			objectSizes.Add("igGeometryAttr2", 0x1B);
			objectSizes.Add("igGeometryAttr1_5", 0x21);
			objectSizes.Add("igGraphPath", 0x09);
			objectSizes.Add("igGraphPathList", 0x09);
			objectSizes.Add("igGroup", 0x0C);
			objectSizes.Add("igImage", 0x3F);
			objectSizes.Add("igImageMipMapList", 0x09);
			objectSizes.Add("igIndexArray", 0x0C);
			objectSizes.Add("igInfo", 0x06);
			objectSizes.Add("igInfoList", 0x09);
			objectSizes.Add("igIntList", 0x09);
			objectSizes.Add("igIntListList", 0x09);
			objectSizes.Add("igLongList", 0x09);
			objectSizes.Add("igMaterialAttr", 0x15);
			objectSizes.Add("igMaterialModeAttr", 0x06);
			objectSizes.Add("igMatrixObject", 0x03);
			objectSizes.Add("igMatrixObjectList", 0x09);
			objectSizes.Add("igMemoryDirEntry", 0x12);
			objectSizes.Add("igModelViewMatrixBoneSelect", 0x0F);
			objectSizes.Add("igModelViewMatrixBoneSelectList", 0x09);
			objectSizes.Add("igMorphBase", 0x27);
			objectSizes.Add("igMorphedGeometryAttr2", 0x0F);
			objectSizes.Add("igMorphInstance", 0x27);
			objectSizes.Add("igMorphInstance2", 0x1E);
			objectSizes.Add("igMorphVertexArrayList", 0x12);
			objectSizes.Add("igNamedObject", 0x03);
			objectSizes.Add("igNode", 0x09);
			objectSizes.Add("igNodeList", 0x09);
			objectSizes.Add("igObjectDirEntry", 0x09);
			objectSizes.Add("igObjectList", 0x09);
			objectSizes.Add("igPrimLengthArray", 0x09);
			objectSizes.Add("igPrimLengthArray1_1", 0x09);
			objectSizes.Add("igQuaternionfList", 0x09);
			objectSizes.Add("igSceneInfo", 0x1B);
			objectSizes.Add("igShadeModelAttr", 0x06);
			objectSizes.Add("igSkeleton", 0x0F);
			objectSizes.Add("igSkeletonBoneInfo", 0x0C);
			objectSizes.Add("igSkeletonBoneInfoList", 0x09);
			objectSizes.Add("igSkeletonList", 0x09);
			objectSizes.Add("igSkin", 0x09);
			objectSizes.Add("igSkinList", 0x09);
			objectSizes.Add("igStringObjList", 0x09);
			objectSizes.Add("igTextureAttr", 0x27);
			objectSizes.Add("igTextureBindAttr", 0x09);
			objectSizes.Add("igTextureInfo", 0x09);
			objectSizes.Add("igTextureList", 0x09);
			objectSizes.Add("igTextureMatrixStateAttr", 0x09);
			objectSizes.Add("igTextureStateAttr", 0x09);
			objectSizes.Add("igTransform", 0x15);
			objectSizes.Add("igTransformSequence", 0x18);
			objectSizes.Add("igTransformSequence1_5", 0x33);
			objectSizes.Add("igTransformSource", 0x00);
			objectSizes.Add("igUnsignedCharList", 0x09);
			objectSizes.Add("igUnsignedIntList", 0x09);
			objectSizes.Add("igVec2fList", 0x09);
			objectSizes.Add("igVec3fList", 0x09);
			objectSizes.Add("igVec3fListList", 0x09);
			objectSizes.Add("igVec4fList", 0x09);
			objectSizes.Add("igVec4fAlignedList", 0x09);
			objectSizes.Add("igVec4ucList", 0x09);
			objectSizes.Add("igVertexArray", 0x0C);
			objectSizes.Add("igVertexArray1_1", 0x18);
			objectSizes.Add("igVertexArray2", 0x09);
			objectSizes.Add("igVertexArray2List", 0x09);
			objectSizes.Add("igVertexBlendMatrixListAttr", 0x0C);
			objectSizes.Add("igVertexBlendStateAttr", 0x06);
			objectSizes.Add("igVertexData", 0x21);
			objectSizes.Add("igVertexStream", 0x12);
			objectSizes.Add("igVisualAttribute", 0x03);
			objectSizes.Add("igVolume", 0x00);
		}

        private List<string> stringTableEntries = new List<string>();
        private List<IGB_ENTRY> entryTable = new List<IGB_ENTRY>();
        private List<ushort> referenceTable = new List<ushort>();
        private List<Internal.IGB_NODE_INFO> nodeStringTable_params = new List<Internal.IGB_NODE_INFO>();
        private List<igBase> nodes = new List<igBase>();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) return;

			IO.BinaryReader br = base.Stream.BinaryReader;

			
			#region Header
			uint head01 = br.ReadUInt32();
			uint entries_count = br.ReadUInt32();
			uint nodeTable_size = br.ReadUInt32();
			uint nodeTable_count = br.ReadUInt32();
			uint head05 = br.ReadUInt32();
			uint head06 = br.ReadUInt32();
			uint head07 = br.ReadUInt32();
			uint head08 = br.ReadUInt32();
			uint fieldStringTable_size = br.ReadUInt32();
			uint fieldStringTable_count = br.ReadUInt32();
			uint head11 = br.ReadUInt32();
			uint head12 = br.ReadUInt32();
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
			#region Field String Table
			List<uint[]> fieldStringTableParameters = new List<uint[]>();
			List<string> fieldStringTableEntries = new List<string>();
			#region Parameters
			for (uint i = 0; i < fieldStringTable_count; i++)
			{
				uint param1 = br.ReadUInt32();
				uint param2 = br.ReadUInt32();
				uint param3 = br.ReadUInt32();
				fieldStringTableParameters.Add(new uint[] { param1, param2, param3 });
			}
			#endregion
			#region Entries
			for (int i = 0; i < fieldStringTable_count; i++)
			{
				string stringTableEntry = br.ReadFixedLengthString(fieldStringTableParameters[i][0]);
				stringTableEntry = stringTableEntry.TrimNull();

				fieldStringTableEntries.Add(stringTableEntry);
			}
			#endregion
			#endregion
			#region Data String Table
			uint dataStringTable_size = br.ReadUInt32();
			uint dataStringTable_unknown1 = br.ReadUInt32();
			uint dataStringTable_count = br.ReadUInt32();

			List<uint> dataStringTable_entrySizes = new List<uint>();
			for (int i = 0; i < dataStringTable_count; i++)
			{
				uint size = br.ReadUInt32();
				dataStringTable_entrySizes.Add(size);
			}

			List<string> dataStringTable_entries = new List<string>();
			for (int i = 0; i < dataStringTable_count; i++)
			{
				string entry = br.ReadFixedLengthString(dataStringTable_entrySizes[i]);
				entry = entry.TrimNull();
				dataStringTable_entries.Add(entry);
			}
			#endregion
			#region Node String Table
			for (int i = 0; i < nodeTable_count; i++)
			{
				Internal.IGB_NODE_INFO ni = new Internal.IGB_NODE_INFO();
				ni.nameLength = br.ReadUInt32();
				ni.param02 = br.ReadUInt32();
				ni.param03 = br.ReadUInt32();
				ni.param04 = br.ReadUInt32();
				ni.parentNode = br.ReadUInt32();
				ni.childCount = br.ReadUInt32();
				nodeStringTable_params.Add(ni);
			}
			for (int i = 0; i < nodeTable_count; i++)
			{
				Internal.IGB_NODE_INFO ni = nodeStringTable_params[i];
				ni.name = br.ReadFixedLengthString(ni.nameLength);
				ni.name = ni.name.TrimNull();

				// read node data
				int dataLen = 0x00;

				igBase node = null;
                switch (ni.name)
                {
                    case "igObject": node = new igObject(); break;
                    case "igAABox": node = new igAABox(); break;
                    case "igActor": node = new igActor(); break;
                    case "igActorInfo": node = new igActorInfo(); break;
                    case "igActorList": node = new igActorList(); break;
                    case "igAnimation": node = new igAnimation(); break;
                    case "igAnimationBinding": node = new igAnimationBinding(); break;
                    case "igAnimationBindingList": node = new igAnimationBindingList(); break;
                    case "igAnimationCombiner": node = new igAnimationCombiner(); break;
                    case "igAnimationCombinerBoneInfo": node = new igAnimationCombinerBoneInfo(); break;
                    case "igAnimationCombinerBoneInfoList": node = new igAnimationCombinerBoneInfoList(); break;
                    case "igAnimationCombinerBoneInfoListList": node = new igAnimationCombinerBoneInfoListList(); break;
                    case "igAnimationCombinerList": node = new igAnimationCombinerList(); break;
                    case "igAnimationDatabase": node = new igAnimationDatabase(); break;
                    case "igAnimationList": node = new igAnimationList(); break;
                    case "igAnimationModifierList": node = new igAnimationModifierList(); break;
                    case "igAnimationState": node = new igAnimationState(); break;
                    case "igAnimationStateList": node = new igAnimationStateList(); break;
                    case "igAnimationSystem": node = new igAnimationSystem(); break;
                    case "igAnimationTrack": node = new igAnimationTrack(); break;
                    case "igAnimationTrackList": node = new igAnimationTrackList(); break;
                    case "igAnimationTransitionDefinitionList": node = new igAnimationTransitionDefinitionList(); break;
                    case "igAppearance": node = new igAppearance(); break;
                    case "igAppearanceList": node = new igAppearanceList(); break;
                    case "igAttr": node = new igAttr(); break;
                    case "igAttrList": node = new igAttrList(); break;
                    case "igAttrSet": node = new igAttrSet(); break;
                    case "igBitMask": node = new igBitMask(); break;
                    case "igBlendFunctionAttr": node = new igBlendFunctionAttr(); break;
                    case "igBlendMatrixSelect": node = new igBlendMatrixSelect(); break;
                    case "igBlendStateAttr": node = new igBlendStateAttr(); break;
                    case "igCamera": node = new igCamera(); break;
                    case "igClut": node = new igClut(); break;
                    case "igColorAttr": node = new igColorAttr(); break;
                    case "igCullFaceAttr": node = new igCullFaceAttr(); break;
                    case "igDataList": node = new igDataList(); break;
                    case "igDataPump": node = new igDataPump(); break;
                    case "igDataPumpFloatLinearInterface": node = new igDataPumpFloatLinearInterface(); break;
                    case "igDataPumpFloatSource": node = new igDataPumpFloatSource(); break;
                    case "igDataPumpInfo": node = new igDataPumpInfo(); break;
                    case "igDataPumpInterface": node = new igDataPumpInterface(); break;
                    case "igDataPumpList": node = new igDataPumpList(); break;
                    case "igDataPumpVec4fLinearInterface": node = new igDataPumpVec4fLinearInterface(); break;
                    case "igDataPumpVec4fSource": node = new igDataPumpVec4fSource(); break;
                    case "igDataPumpSource": node = new igDataPumpSource(); break;
                    case "igDirEntry": node = new igDirEntry(); break;
                    case "igDrawableAttr": node = new igDrawableAttr(); break;
                    case "igEnbayaAnimationSource": node = new igEnbayaAnimationSource(); break;
                    case "igEnbayaTransformSource": node = new igEnbayaTransformSource(); break;
                    case "igExternalDirEntry": node = new igExternalDirEntry(); break;
                    case "igExternalImageEntry": node = new igExternalImageEntry(); break;
                    case "igExternalIndexedEntry": node = new igExternalIndexedEntry(); break;
                    case "igExternalInfoEntry": node = new igExternalInfoEntry(); break;
                    case "igFloatList": node = new igFloatList(); break;
                    case "igGeometry": node = new igGeometry(); break;
                    case "igGeometryAttr2": node = new igGeometryAttr2(); break;
                    case "igGraphPath": node = new igGraphPath(); break;
                    case "igGraphPathList": node = new igGraphPathList(); break;
                    case "igGroup": node = new igGroup(); break;
                    case "igImage": node = new igImage(); break;
                    case "igImageMipMapList": node = new igImageMipMapList(); break;
                    case "igIndexArray": node = new igIndexArray(); break;
                    case "igInfo": node = new igInfo(); break;
                    case "igInfoList": node = new igInfoList(); break;
                    case "igIntList": node = new igIntList(); break;
                    case "igLongList": node = new igLongList(); break;
                    case "igMaterialAttr": node = new igMaterialAttr(); break;
                    case "igMaterialModeAttr": node = new igMaterialModeAttr(); break;
                    case "igMatrixObject": node = new igMatrixObject(); break;
                    case "igMatrixObjectList": node = new igMatrixObjectList(); break;
                    case "igMemoryDirEntry": node = new igMemoryDirEntry(); break;
                    case "igModelViewMatrixBoneSelect": node = new igModelViewMatrixBoneSelect(); break;
                    case "igModelViewMatrixBoneSelectList": node = new igModelViewMatrixBoneSelectList(); break;
                    case "igMorphedGeometryAttr2": node = new igMorphedGeometryAttr2(); break;
                    case "igMorphInstance2": node = new igMorphInstance2(); break;
                    case "igMorphVertexArrayList": node = new igMorphVertexArrayList(); break;
                    case "igNamedObject": node = new igNamedObject(); break;
                    case "igNode": node = new igNode(); break;
                    case "igNodeList": node = new igNodeList(); break;
                    case "igObjectDirEntry": node = new igObjectDirEntry(); break;
                    case "igObjectList": node = new igObjectList(); break;
                    case "igPrimLengthArray": node = new igPrimLengthArray(); break;
                    case "igPrimLengthArray1_1": node = new igPrimLengthArray1_1(); break;
                    case "igQuaternionfList": node = new igQuaternionfList(); break;
                    case "igSceneInfo": node = new igSceneInfo(); break;
                    case "igSkeleton": node = new igSkeleton(); break;
                    case "igSkeletonBoneInfo": node = new igSkeletonBoneInfo(); break;
                    case "igSkeletonBoneInfoList": node = new igSkeletonBoneInfoList(); break;
                    case "igSkeletonList": node = new igSkeletonList(); break;
                    case "igSkin": node = new igSkin(); break;
                    case "igSkinList": node = new igSkinList(); break;
                    case "igStringObjList": node = new igStringObjList(); break;
                    case "igTextureAttr": node = new igTextureAttr(); break;
                    case "igTextureBindAttr": node = new igTextureBindAttr(); break;
                    case "igTextureInfo": node = new igTextureInfo(); break;
                    case "igTextureList": node = new igTextureList(); break;
                    case "igTextureMatrixStateAttr": node = new igTextureMatrixStateAttr(); break;
                    case "igTextureStateAttr": node = new igTextureStateAttr(); break;
                    case "igTransform": node = new igTransform(); break;
                    case "igTransformSequence": node = new igTransformSequence(); break;
                    case "igTransformSequence1_5": node = new igTransformSequence1_5(); break;
                    case "igTransformSource": node = new igTransformSource(); break;
                    case "igUnsignedCharList": node = new igUnsignedCharList(); break;
                    case "igUnsignedIntList": node = new igUnsignedIntList(); break;
                    case "igVec2fList": node = new igVec2fList(); break;
                    case "igVec3fList": node = new igVec3fList(); break;
                    case "igVec4fList": node = new igVec4fList(); break;
                    case "igVec4fAlignedList": node = new igVec4fAlignedList(); break;
                    case "igVec4ucList": node = new igVec4ucList(); break;
                    case "igVertexArray2": node = new igVertexArray2(); break;
                    case "igVertexArray2List": node = new igVertexArray2List(); break;
                    case "igVertexBlendMatrixListAttr": node = new igVertexBlendMatrixListAttr(); break;
                    case "igVertexBlendStateAttr": node = new igVertexBlendStateAttr(); break;
                    case "igVertexData": node = new igVertexData(); break;
                    case "igVertexStream": node = new igVertexStream(); break;
                    case "igVisualAttribute": node = new igVisualAttribute(); break;
                    case "igVolume": node = new igVolume(); break;
                    default: throw new InvalidOperationException("Unknown node name " + ni.name);
                }
				node.TypeName = ni.name;

				if (objectSizes.ContainsKey(node.TypeName))
				{
					dataLen = objectSizes[node.TypeName];
				}
				else
				{
					throw new InvalidOperationException("Unknown object type " + node.TypeName + "!");
				}
				node.Data = br.ReadUInt16Array(dataLen);
                nodes.Add(node);
			}
			#endregion
			#region Type String Table
			uint test_type = br.ReadUInt32();
			switch (test_type)
			{
				case 0x0239:
				{
					// read type string table properties
					uint typetable_type = test_type; // 0x0239
					uint typetable_elem = br.ReadUInt32(); // number of strings

					// read type string table
					List<string> items = new List<string>();
					for(uint i = 0; i < typetable_elem; i++)
					{
						string item = br.ReadNullTerminatedString();
						items.Add(item);
					}
					break;
				}
				case 0x17:
				case 0x2E:
				{
					// read list properties
					uint list_type = test_type; // 0x17, 0x2E
					uint list_unk1 = br.ReadUInt32(); // 0x01
					uint list_elem = br.ReadUInt32(); // 0x01, 0x02
   
					// read a list of lengths
					List<uint> lengths = new List<uint>();
					for(uint i = 0; i < list_elem; i++)
					{
						uint n = br.ReadUInt32();
						lengths.Add(n);
					}
   
					// read a list of strings
					List<string> items = new List<string>();
					for(int i = 0; i < list_elem; i++)
					{
						string item = br.ReadFixedLengthString(lengths[i]);
						item = item.TrimNull();
						items.Add(item);
					}
   
					// read type string table properties
					uint typetable_type = br.ReadUInt32(); // 0x0239
					uint typetable_elem = br.ReadUInt32(); // number of strings
   
					// read type string table
					List<string> typeStringTableEntries = new List<string>();
					for(uint i = 0; i < typetable_elem; i++)
					{
						string item = br.ReadNullTerminatedString();
						typeStringTableEntries.Add(item);
					}
					break;
				}
			}
			#endregion
			#region Entries
			for (uint i = 0; i < entries_count; i++)
			{
				uint entry_type = br.ReadUInt32();
				uint entry_size = br.ReadUInt32();

				// find entry name
				if (entry_type > nodes.Count)
				{
					throw new IndexOutOfRangeException("Failed to find entry type name.");
				}

				string entry_name = nodes[(int)entry_type].TypeName;

				switch (entry_type)
				{
					case 0x03:
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
						break;
					}
					case 0x04:
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
						break;
					}
					case 0x08:
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
						break;
					}
					default:
					{
						throw new InvalidOperationException("Unknown entry type 0x" + entry_type.ToString("X").PadLeft(4, '0'));
					}
				}
			}
			// read entry table size
			uint entryTableSize = br.ReadUInt32();
			
			// read number of elements
			uint entryTableCount = br.ReadUInt32();

			// read indices
			for (uint i = 0; i < entryTableCount; i++)
			{
				ushort index = br.ReadUInt16();
				referenceTable.Add(index);
			}
			#endregion
			#region Tree
            uint treeType = br.ReadUInt32();

            for (int i = 0; i < referenceTable.Count; i++)
            {
                // directory reference
                ushort directoryReference = referenceTable[i];

                if (directoryReference >= entryTable.Count) throw new IndexOutOfRangeException("Invalid directory reference");

                IGB_ENTRY entry = entryTable[directoryReference];
                if (entry.type >= nodes.Count) throw new IndexOutOfRangeException("Invalid directory entry type");

                string entryType = nodes[(int)entry.type].TypeName;
                // process entry
                switch (entryType)
                {
                    case "igObjectDirEntry":
                    {
                        // retrieve node name
                        uint NT_index = entry.entries[1];
                        if(NT_index >= nodes.Count) throw new IndexOutOfRangeException("Invalid node index.");

                        igBase ig = nodes[(int)NT_index];
                        
                        // read chunk header
                        uint chunktype = br.ReadUInt32();
                        uint chunksize = br.ReadUInt32();

                        // validate chunk header
                        if (chunktype != NT_index)
                        {
                            throw new InvalidOperationException("Unexpected node " + ig.TypeName);
                        }
                        if (chunksize < 0x10)
                        {
                            throw new ArgumentOutOfRangeException("chunksize", chunksize, "Invalid " + ig.TypeName + " chunk. Chunksize is too small.");
                        }
                        if ((chunksize % 0x04) != 0)
                        {
                            throw new ArgumentOutOfRangeException("chunksize", chunksize, "Invalid " + ig.TypeName + " chunk. Chunksize not divisible by four.");
                        }
           
                        // read chunk data
                        uint attrsize = chunksize - 0x08;
                        byte[] attrdata = br.ReadBytes(attrsize);

                        IO.BinaryReader br1 = new IO.BinaryReader(attrdata);
                        // process nodes
                        switch (ig.TypeName)
                        {
                            #region igAABox
                            case "igAABox":
                            {
                                igAABox node = (ig as igAABox);
                                if (br1.BaseStream.Length != 0x18) throw new ArgumentOutOfRangeException("igAABox.size", br1.BaseStream.Length, "must be 0x18 (24)");

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
                                if (br1.BaseStream.Length != 0x68) throw new ArgumentOutOfRangeException("igActor.size", br1.BaseStream.Length, "must be 0x68 (104)");

                                node.Name = ReadName32(br1);
                                node.Bound = ReadEntry(br1);
                                node.Flags = br1.ReadUInt32();
                                node.ChildList = ReadEntry(br1);
                                node.AnimationSystem = ReadEntry(br1);
                                node.BoneMatrixCacheArray = ReadEntry(br1);
                                node.BlendMatrixCacheArray = ReadEntry(br1);
                                node.Appearance = ReadEntry(br1);
                                node.AnimationDatabase = ReadEntry(br1);
                                node.ModifierList = ReadEntry(br1);
                                node.Transform = br1.ReadSingleArray(16); // 4x4 matrix
                                break;
                            }
                            #endregion
                            #region igActorInfo
                            case "igActorInfo":
                            {
                                igActorInfo node = (ig as igActorInfo);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x1C) throw new ArgumentOutOfRangeException("igActorInfo.size", br1.BaseStream.Length, "must be 0x1C (28)");

                                node.Name = ReadName32(br1);
                                node.ResolveState = br1.ReadUInt32();

                                uint actorID = br1.ReadUInt32();
                                node.Actor = nodes[actorID];

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
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igActorList.size", br1.BaseStream.Length, "must be 0x0C (12)");

                                uint count = br1.ReadUInt32();
                                node.Capacity = br1.ReadUInt32();

                                uint data = br1.ReadUInt32();

                                throw new Exception("break here to see what we can do about this...");

                                // assuming that data points to the start of the first Actor node in the list...
                                // and that Actors in a list are placed in the nodes array contiguously...
                                for (uint i = 0; i < count; i++)
                                {
                                    // we can loop through and read each one from nodes[data + i]!
                                    node.Items.Add(nodes[data + i] as igActor);
                                }
                                break;
                            }
                            #endregion
                            #region igAnimation
                            case "igAnimation":
                            {
                                igAnimation node = (ig as igAnimation);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x30) throw new ArgumentOutOfRangeException("igAnimation.size", br1.BaseStream.Length, "must be 0x30 (48)");

                                node.Name = ReadName32(br1);
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
                                if (br1.BaseStream.Length != 0x14) throw new ArgumentOutOfRangeException("igAnimationBinding.size", br1.BaseStream.Length, "must be 0x14 (20)");

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
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationBindingList.size", br1.BaseStream.Length, "must be 0x0C (12)");

                                uint count = br1.ReadUInt32();
                                node.Capacity = br1.ReadUInt32();

                                throw new Exception("break here to see what we can do about this...");

                                // assuming that data points to the start of the first Actor node in the list...
                                // and that Actors in a list are placed in the nodes array contiguously...
                                for (uint i = 0; i < count; i++)
                                {
                                    // we can loop through and read each one from nodes[data + i]!
                                    node.Items.Add(nodes[data + i] as igAnimationBinding);
                                }
                                break;
                            }
                            #endregion
                            #region igAnimationCombiner
                            case "igAnimationCombiner":
                            {
                                igAnimationCombiner node = (ig as igAnimationCombiner);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x50) throw new ArgumentOutOfRangeException("igAnimationCombiner.size", br1.BaseStream.Length, "must be 0x50 (80)");

                                node.Name = ReadName32(br1);
                                node.Skeleton = ReadEntry<igSkeleton>(br1);
                                node.BoneInfoListList = ReadEntry<igAnimationCombinerBoneInfoListList>(br1);
                                node.BoneInfoListBase = ReadEntry<igAnimationCombinerBoneInfoList>(br1);
                                node.AnimationStateList = ReadEntry<igAnimationStateList>(br1);
                                node.ResultQuaternionArray = ReadEntry<igQuaternionfList>(br1);
                                // node.AnimationCacheMatrixArray =
                                igBase unknown1 = ReadEntry(br1);

                                node.CacheTime = br1.ReadUInt64();
                                node.CacheValid = br1.ReadUInt32();

                                igBase boneMatrixArray = ReadEntry(br1);
                                igBase blendMatrixArray = ReadEntry(br1);
                                node.AnimationStateTime = br1.ReadUInt64();
                                node.LastCleanStateTime = br1.ReadUInt64();
                                node.CleanStateTimeThreshold = br1.ReadUInt64();
                                node.CleanStatesTransitionMargin = br1.ReadUInt64();
                                break;
                            }
                            #endregion
                            #region igAnimationCombinerBoneInfo
                            case "igAnimationCombinerBoneInfo":
                            {
                                igAnimationCombinerBoneInfo node = (ig as igAnimationCombinerBoneInfo);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x30) throw new ArgumentOutOfRangeException("igAnimationCombinerBoneInfo.size", br1.BaseStream.Length, "must be 0x30 (48)");

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
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationCombinerBoneInfoList.size", br1.BaseStream.Length, "must be 0x0C (12)");

                                uint count = br1.ReadUInt32();
                                node.Capacity = br1.ReadUInt32();

                                throw new Exception("break here to see what we can do about this...");

                                // assuming that data points to the start of the first item node in the list...
                                // and that items in a list are placed in the nodes array contiguously...
                                for (uint i = 0; i < count; i++)
                                {
                                    // we can loop through and read each one from nodes[data + i]!
                                    node.Items.Add(nodes[data + i] as igAnimationCombinerBoneInfo);
                                }
                                break;
                            }
                            #endregion
                            #region igAnimationCombinerBoneInfoListList
                            case "igAnimationCombinerBoneInfoListList":
                            {
                                igAnimationCombinerBoneInfoListList node = (ig as igAnimationCombinerBoneInfoListList);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationCombinerBoneInfoListList.size", br1.BaseStream.Length, "must be 0x0C (12)");

                                uint count = br1.ReadUInt32();
                                node.Capacity = br1.ReadUInt32();

                                throw new Exception("break here to see what we can do about this...");

                                // assuming that data points to the start of the first item node in the list...
                                // and that items in a list are placed in the nodes array contiguously...
                                for (uint i = 0; i < count; i++)
                                {
                                    // we can loop through and read each one from nodes[data + i]!
                                    node.Items.Add(nodes[data + i] as igAnimationCombinerBoneInfoList);
                                }
                                break;
                            }
                            #endregion
                            #region igAnimationCombinerList
                            case "igAnimationCombinerList":
                            {
                                igAnimationCombinerList node = (ig as igAnimationCombinerList);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationCombinerList.size", br1.BaseStream.Length, "must be 0x0C (12)");

                                uint count = br1.ReadUInt32();
                                node.Capacity = br1.ReadUInt32();

                                throw new Exception("break here to see what we can do about this...");

                                // assuming that data points to the start of the first item node in the list...
                                // and that items in a list are placed in the nodes array contiguously...
                                for (uint i = 0; i < count; i++)
                                {
                                    // we can loop through and read each one from nodes[data + i]!
                                    node.Items.Add(nodes[data + i] as igAnimationCombiner);
                                }
                                break;
                            }
                            #endregion
                            #region igAnimationDatabase
                            case "igAnimationDatabase":
                            {
                                igAnimationDatabase node = (ig as igAnimationDatabase);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x1C) throw new ArgumentOutOfRangeException("igAnimationDatabase.size", br1.BaseStream.Length, "must be 0x1C (28)");

                                node.Name = ReadName32(br1);
                                node.ResolveState = br1.ReadUInt32();

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
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationList.size", br1.BaseStream.Length, "must be 0x0C (12)");

                                uint count = br1.ReadUInt32();
                                node.Capacity = br1.ReadUInt32();

                                throw new Exception("break here to see what we can do about this...");

                                // assuming that data points to the start of the first item node in the list...
                                // and that items in a list are placed in the nodes array contiguously...
                                for (uint i = 0; i < count; i++)
                                {
                                    // we can loop through and read each one from nodes[data + i]!
                                    node.Items.Add(nodes[data + i] as igAnimation);
                                }
                                break;
                            }
                            #endregion
                            #region igAnimationModifierList
                            case "igAnimationModifierList":
                            {
                                igAnimationModifierList node = (ig as igAnimationModifierList);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationModifierList.size", br1.BaseStream.Length, "must be 0x0C (12)");

                                uint count = br1.ReadUInt32();
                                node.Capacity = br1.ReadUInt32();

                                throw new Exception("break here to see what we can do about this...");

                                // TODO: what kind of nodes does igAnimationModifierList store?
                                // there is no such thing (yet) as an igAnimationModifier

                                // assuming that data points to the start of the first item node in the list...
                                // and that items in a list are placed in the nodes array contiguously...
                                for (uint i = 0; i < count; i++)
                                {
                                    // we can loop through and read each one from nodes[data + i]!
                                    node.Items.Add(nodes[data + i]);
                                }
                                break;
                            }
                            #endregion
                            #region igAnimationState
                            case "igAnimationState":
                            {
                                igAnimationState node = (ig as igAnimationState);
                                // validate chunksize
                                if (br1.BaseStream.Length != 0x0C) throw new ArgumentOutOfRangeException("igAnimationList.size", br1.BaseStream.Length, "must be 0x0C (12)");

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
                        }
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
                        throw new InvalidOperationException("Unknown directory entry type " + entryType + ".");
                    }
                }
            }
			#endregion
		}

        private igBase ReadEntry(IO.BinaryReader br)
        {
            return ReadEntry<igBase>(br);
        }
        private T ReadEntry<T>(IO.BinaryReader br) where T : igBase
        {
            uint item = br.ReadUInt32();
            if (item != 0xFFFFFFFF)
            {
                // test reference #1
                uint referenceTableIndex = item;
                uint entryTableIndex = referenceTable[referenceTableIndex];
                if (entryTableIndex >= entryTable.Count) throw new ArgumentOutOfRangeException("entryTableIndex", entryTableIndex, "must be less than entryTable.Count (" + entryTable.Count + ")");

                IGB_ENTRY e = entryTable[entryTableIndex];

                // test reference #2
                switch (entryTable[entryTableIndex].type)
                {
                    #region igObjectDirEntry
                    case 0x03: // igObjectDirEntry
                    {
                        // test reference #3
                        if (e.entries[1] >= nodeStringTable_params.Count) throw new InvalidOperationException("Invalid NT reference.");

                        // must be correct entry type
                        if (nodeStringTable_params[e.type].name != "igObjectDirEntry")
                        {
                            throw new InvalidOperationException("Entry is not an igObjectDirEntry.");
                        }

                        // extra data to display
                        uint NT_index = e.entries[1];
                        break;
                    }
                    #endregion
                    #region igMemoryDirEntry
                    case 0x04: // igMemoryDirEntry
                    {
                        if (nodeStringTable_params[e.type].name != "igMemoryDirEntry")
                        {
                            throw new InvalidOperationException("Entry is not an igMemoryDirEntry.");
                        }
                        break;
                    }
                    #endregion
                    #region igExternalInfoEntry
                    case 0x08: // igExternalInfoEntry
                    {
                        if (nodeStringTable_params[e.type].name != "igExternalInfoEntry")
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
            return nodes[item];
        }

        private string ReadName32(IO.BinaryReader br)
        {
            uint nameIndex = br.ReadUInt32();
            if (nameIndex >= stringTableEntries.Count) throw new ArgumentOutOfRangeException("nameIndex", nameIndex, "must be less than number of string table entries (" + stringTableEntries.Count + ")");
            return stringTableEntries[nameIndex];
        }

        protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
