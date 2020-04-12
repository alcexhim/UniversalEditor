//
//  CDDADataFormat.cs - provides a DataFormat for manipulating playlist files in CDDA format
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

using System.Collections.Generic;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Playlist;

namespace UniversalEditor.DataFormats.Multimedia.Playlist.CDDA
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating playlist files in CDDA format.
	/// </summary>
	public class CDDADataFormat : RIFFDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			ChunkedObjectModel rom = new ChunkedObjectModel();
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			RIFFDataChunk fmtChunk = new RIFFDataChunk();
			fmtChunk.ID = "fmt ";
			Accessors.MemoryAccessor ms = new Accessors.MemoryAccessor();
			IO.Writer bw = new IO.Writer(ms);
			ushort CDAFileVersion = 1;
			ushort CDATrackNumber = 1;
			uint CDADiscSerialNumber = 0u;
			uint CDATrackStartHSG = 0u;
			uint CDATrackLengthHSG = 0u;
			byte CDATrackStartRBFFrame = 0;
			byte CDATrackStartRBFSecond = 0;
			byte CDATrackStartRBFMinute = 0;
			byte CDATrackStartRBFUnused = 0;
			byte CDATrackLengthRBFFrame = 0;
			byte CDATrackLengthRBFSecond = 0;
			byte CDATrackLengthRBFMinute = 0;
			byte CDATrackLengthRBFUnused = 0;
			if (pom.Entries.Count > 0)
			{
				PlaylistEntry entry = pom.Entries[0];
			}
			bw.WriteUInt16(CDAFileVersion);
			bw.WriteUInt16(CDATrackNumber);
			bw.WriteUInt32(CDADiscSerialNumber);
			bw.WriteUInt32(CDATrackStartHSG);
			bw.WriteUInt32(CDATrackLengthHSG);
			bw.WriteBytes(new byte[]
			{
				CDATrackStartRBFFrame, 
				CDATrackStartRBFSecond, 
				CDATrackStartRBFMinute, 
				CDATrackStartRBFUnused
			});
			bw.WriteBytes(new byte[]
			{
				CDATrackLengthRBFFrame, 
				CDATrackLengthRBFSecond, 
				CDATrackLengthRBFMinute, 
				CDATrackLengthRBFUnused
			});
			ms.Close();

			fmtChunk.Data = ms.ToArray();
			rom.Chunks.Add(fmtChunk);
			objectModels.Push(rom);
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			ChunkedObjectModel rom = objectModels.Pop() as ChunkedObjectModel;
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			RIFFDataChunk fmtChunk = (rom.Chunks["fmt "] as RIFFDataChunk);
			IO.Reader br = new IO.Reader(new Accessors.MemoryAccessor(fmtChunk.Data));
			ushort CDAFileVersion = br.ReadUInt16();
			ushort CDATrackNumber = br.ReadUInt16();
			uint CDADiscSerialNumber = br.ReadUInt32();
			uint CDATrackStartHSG = br.ReadUInt32();
			uint CDATrackLengthHSG = br.ReadUInt32();
			byte CDATrackStartRBFFrame = br.ReadByte();
			byte CDATrackStartRBFSecond = br.ReadByte();
			byte CDATrackStartRBFMinute = br.ReadByte();
			byte CDATrackStartRBFUnused = br.ReadByte();
			byte CDATrackLengthRBFFrame = br.ReadByte();
			byte CDATrackLengthRBFSecond = br.ReadByte();
			byte CDATrackLengthRBFMinute = br.ReadByte();
			byte CDATrackLengthRBFUnused = br.ReadByte();
			PlaylistEntry entry = new PlaylistEntry();
			entry.Title = "Track" + CDATrackNumber.ToString().PadLeft(2, '0');
			entry.Length = (long)((ulong)CDATrackLengthHSG);
			entry.Offset = (long)((ulong)CDATrackStartHSG);
			pom.Entries.Add(entry);
		}
	}
}
