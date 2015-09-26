using System;
using System.Collections.Generic;
using System.Linq;
using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.PMAXPatch.Chunks
{
	public class PMAXMaterialNamesChunk : PMAXPatchChunk
	{
		private string mvarName = "MTLN";
		public override string Name
		{
			get { return mvarName; }
		}

		private System.Collections.Specialized.StringCollection mvarMaterialNames = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection MaterialNames
		{
			get { return mvarMaterialNames; }
		}

		internal int MaterialNameCount = 0;

		public override void LoadInternal(Accessor accessor)
		{
			Reader reader = accessor.Reader;
			long streamPos = accessor.Position;
			while ((reader.Accessor.Position - streamPos) < base.Size)
			{
				string materialName = reader.ReadNullTerminatedString();
				mvarMaterialNames.Add(materialName);
			}
		}
		public override void SaveInternal(Accessor accessor)
		{
			Writer writer = accessor.Writer;
			foreach (string materialName in mvarMaterialNames)
			{
				writer.WriteNullTerminatedString(materialName);
			}
			writer.Flush();
		}
		public override object Clone()
		{
			PMAXMaterialNamesChunk clone = new PMAXMaterialNamesChunk();
			foreach (string materialName in mvarMaterialNames)
			{
				clone.MaterialNames.Add(materialName);
			}
			clone.MaterialNameCount = MaterialNameCount;
			return clone;
		}
	}
}
