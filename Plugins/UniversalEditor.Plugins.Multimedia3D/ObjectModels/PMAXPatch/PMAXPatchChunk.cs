//
//  PMAXPatchChunk.cs - the abstract base class from which all Concertroid PMAX patch chunks are derived
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

using System;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
	/// <summary>
	/// The abstract base class from which all Concertroid PMAX patch chunks are derived.
	/// </summary>
	public abstract class PMAXPatchChunk : ICloneable
	{
		/// <summary>
		/// 4-letter identifier for this chunk.
		/// </summary>
		public abstract string Name { get; }

		public abstract void SaveInternal(Accessor accessor);
		public abstract void LoadInternal(Accessor accessor);

		public abstract object Clone();

		public int Size { get; set; } = 0;

		public class PMAXPatchChunkCollection
			: System.Collections.ObjectModel.Collection<PMAXPatchChunk>
		{

		}
	}
}
