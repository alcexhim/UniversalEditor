//
//  PMAXAdvancedTextureBlockImage.cs - describes information related to an image frame for an animated texture in a Concertroid PMAX patched model
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

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.ObjectModels.PMAXPatch
{
	/// <summary>
	/// Describes information related to an image frame for an animated texture in a Concertroid PMAX patched model.
	/// </summary>
	public class PMAXAdvancedTextureBlockImage : ICloneable
	{
		public class PMAXAdvancedTextureBlockImageCollection
			: System.Collections.ObjectModel.Collection<PMAXAdvancedTextureBlockImage>
		{
		}

		public long Timestamp { get; set; } = 0;
		public string FileName { get; set; } = String.Empty;
		public ModelTextureFlags TextureFlags { get; set; } = ModelTextureFlags.None;

		public object Clone()
		{
			PMAXAdvancedTextureBlockImage clone = new PMAXAdvancedTextureBlockImage();
			clone.FileName = FileName;
			clone.Timestamp = Timestamp;
			clone.TextureFlags = TextureFlags;
			return clone;
		}
	}
}
