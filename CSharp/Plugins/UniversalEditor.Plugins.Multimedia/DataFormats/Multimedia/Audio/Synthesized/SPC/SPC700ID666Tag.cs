// one line to give the program's name and an idea of what it does.
// Copyright (C) yyyy  name of author
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC
{
	public enum SPC700ID666Format
	{
		None = 0,
		Binary = 1,
		Text = 2
	}
	
	/// <summary>
	/// Description of SPC700ID666Tag.
	/// </summary>
	public class SPC700ID666Tag
	{
		
		private SPC700ID666Format mvarFormat = SPC700ID666Format.Text;
		public SPC700ID666Format Format { get { return mvarFormat; } set { mvarFormat = value; } }
		
		private string mvarSongTitle = "";
		public string SongTitle { get { return mvarSongTitle; } set { mvarSongTitle = value; } }
		
		private string mvarGameTitle = "";
		public string GameTitle { get { return mvarGameTitle; } set { mvarGameTitle = value; } }
		
		private string mvarDumperName = "";
		public string DumperName { get { return mvarDumperName; } set { mvarDumperName = value; } }
		
		private string mvarComments = "";
		public string Comments { get { return mvarComments; } set { mvarComments = value; } }
		
		private DateTime mvarDumpDate = DateTime.Now;
		public DateTime DumpDate { get { return mvarDumpDate; } set { mvarDumpDate = value; } }
		
		private byte[] _fFadeOutSeconds = new byte[3];
		public byte[] fFadeOutSeconds { get { return _fFadeOutSeconds; } set { _fFadeOutSeconds = value; } }
		
		private byte[] _fFadeLength = new byte[5];
		public byte[] fFadeLength { get { return _fFadeLength; } set { _fFadeLength = value; } }
		
		private string mvarSongArtist = "";
		public string SongArtist { get { return mvarSongArtist; } set { mvarSongArtist = value; } }
		
		private bool mvarDefaultDisables = false;
		public bool DefaultDisables { get { return mvarDefaultDisables; } set { mvarDefaultDisables = value; } }
		
		private SPC700Emulator mvarEmulator = SPC700Emulator.Unknown;
		public SPC700Emulator Emulator { get { return mvarEmulator; } set { mvarEmulator = value; } }
		
		private byte[] _fReserved2 = new byte[45];
		public byte[] fReserved2 { get { return _fReserved2; } set { _fReserved2 = value; } }
		
	}
}
