//
//  PMAXEffectsScriptChunk.cs - represents a Concertroid PMAX patch chunk that adds an effects script to the model
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
	/// Represents a Concertroid PMAX patch chunk that adds an effects script to the model.
	/// </summary>
	public class PMAXEffectsScriptChunk : PMAXPatchChunk
	{
		private string mvarName = "EFXS";
		public override string Name
		{
			get { return mvarName; }
		}

		public System.Collections.Specialized.StringCollection EffectScriptFileNames { get; set; } = new System.Collections.Specialized.StringCollection();

		public override void LoadInternal(Accessor accessor)
		{
			Reader reader = new Reader(accessor);
			int fileNameCount = reader.ReadInt32();
			for (int i = 0; i < fileNameCount; i++)
			{
				string fileName = reader.ReadNullTerminatedString();
				EffectScriptFileNames.Add(fileName);
			}
		}
		public override void SaveInternal(Accessor accessor)
		{
			Writer writer = new Writer(accessor);
			writer.WriteInt32(EffectScriptFileNames.Count);
			foreach (string fileName in EffectScriptFileNames)
			{
				writer.WriteNullTerminatedString(fileName);
			}
			writer.Flush();
		}
		public override object Clone()
		{
			PMAXEffectsScriptChunk clone = new PMAXEffectsScriptChunk();
			foreach (string fileName in EffectScriptFileNames)
			{
				clone.EffectScriptFileNames.Add(fileName);
			}
			return clone;
		}
	}
}
