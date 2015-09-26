using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
	public abstract class PMAXPatchChunk : ICloneable
	{
		/// <summary>
		/// 4-letter identifier for this chunk.
		/// </summary>
		public abstract string Name { get; }

		public abstract void SaveInternal(Accessor accessor);
		public abstract void LoadInternal(Accessor accessor);

		public abstract object Clone();

		private int mvarSize = 0;
		public int Size
		{
			get { return mvarSize; }
			set { mvarSize = value; }
		}

		public class PMAXPatchChunkCollection
			: System.Collections.ObjectModel.Collection<PMAXPatchChunk>
		{

		}
	}
}
