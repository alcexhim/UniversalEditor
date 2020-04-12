//
//  SmackerDataFormat.cs - provides a DataFormat for manipulating images in Smacker (SMK) format
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Video;
namespace UniversalEditor.DataFormats.Multimedia.Video.RAD.Smacker
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in Smacker (SMK) format.
	/// </summary>
	public class SmackerDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			dfr.Sources.Add("http://wiki.multimedia.cx/index.php?title=Smacker");
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(4);
			int width = br.ReadInt32();
			int height = br.ReadInt32();
			int frames = br.ReadInt32();
			int frameRate = br.ReadInt32();
			if (frameRate > 0)
			{
				int framesPerSecond = 1000 / frameRate;
			}
			else
			{
				if (frameRate < 0)
				{
					int framesPerSecond = 100000 / -frameRate;
				}
			}
			int flags = br.ReadInt32();
			int[] audioSize = br.ReadInt32Array(7);
			int TreesSize = br.ReadInt32();
			int MMap_Size = br.ReadInt32();
			int MClr_Size = br.ReadInt32();
			int Full_Size = br.ReadInt32();
			int Type_Size = br.ReadInt32();
			int[] audioRate = br.ReadInt32Array(7);
			int dummy = br.ReadInt32();
			int[] frameSizes = br.ReadInt32Array(frames);
			byte[] frameTypes = br.ReadBytes(frames);
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
