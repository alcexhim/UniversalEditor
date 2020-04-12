//
//  PMAXMaterialNamesChunk.cs - represents a PMAX patch chunk that contains a list of material names for the model
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.PMAXPatch.Chunks
{
	/// <summary>
	/// Represents a PMAX patch chunk that contains a list of material names for the model.
	/// </summary>
	public class PMAXMaterialNamesChunk : PMAXPatchChunk
	{
		private string mvarName = "MTLN";
		public override string Name
		{
			get { return mvarName; }
		}

		public System.Collections.Specialized.StringCollection MaterialNames { get; } = new System.Collections.Specialized.StringCollection();

		internal int MaterialNameCount = 0;

		public override void LoadInternal(Accessor accessor)
		{
			Reader reader = accessor.Reader;
			long streamPos = accessor.Position;
			while ((reader.Accessor.Position - streamPos) < base.Size)
			{
				string materialName = reader.ReadNullTerminatedString();
				MaterialNames.Add(materialName);
			}
		}
		public override void SaveInternal(Accessor accessor)
		{
			Writer writer = accessor.Writer;
			foreach (string materialName in MaterialNames)
			{
				writer.WriteNullTerminatedString(materialName);
			}
			writer.Flush();
		}
		public override object Clone()
		{
			PMAXMaterialNamesChunk clone = new PMAXMaterialNamesChunk();
			foreach (string materialName in MaterialNames)
			{
				clone.MaterialNames.Add(materialName);
			}
			clone.MaterialNameCount = MaterialNameCount;
			return clone;
		}
	}
}
