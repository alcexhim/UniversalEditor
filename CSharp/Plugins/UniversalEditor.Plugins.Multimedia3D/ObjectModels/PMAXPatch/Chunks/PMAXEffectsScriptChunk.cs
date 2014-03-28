using System;
using System.Collections.Generic;
using System.Linq;

using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.PMAXPatch.Chunks
{
	public class PMAXEffectsScriptChunk : PMAXPatchChunk
	{
		private string mvarName = "EFXS";
		public override string Name
		{
			get { return mvarName; }
		}

		private System.Collections.Specialized.StringCollection mvarEffectScriptFileNames = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection EffectScriptFileNames { get { return mvarEffectScriptFileNames; } set { mvarEffectScriptFileNames = value; } }

		public override void LoadInternal(Accessor accessor)
		{
			Reader reader = new Reader(accessor);
			int fileNameCount = reader.ReadInt32();
			for (int i = 0; i < fileNameCount; i++)
			{
				string fileName = reader.ReadNullTerminatedString();
				mvarEffectScriptFileNames.Add(fileName);
			}
		}
		public override void SaveInternal(Accessor accessor)
		{
			Writer writer = new Writer(accessor);
			writer.WriteInt32(mvarEffectScriptFileNames.Count);
			foreach (string fileName in mvarEffectScriptFileNames)
			{
				writer.WriteNullTerminatedString(fileName);
			}
			writer.Flush();
		}
		public override object Clone()
		{
			PMAXEffectsScriptChunk clone = new PMAXEffectsScriptChunk();
			foreach (string fileName in mvarEffectScriptFileNames)
			{
				clone.EffectScriptFileNames.Add(fileName);
			}
			return clone;
		}
	}
}
