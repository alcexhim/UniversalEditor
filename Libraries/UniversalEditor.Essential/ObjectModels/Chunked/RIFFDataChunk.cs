//
//  RIFFDataChunk.cs - represents a data chunk in a Resource Interchange File Format file
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

namespace UniversalEditor.ObjectModels.Chunked
{
	/// <summary>
	/// Represents a data chunk in a Resource Interchange File Format file.
	/// </summary>
	public class RIFFDataChunk : RIFFChunk
	{
		private byte[] mvarData = new byte[0];
		public byte[] Data
		{
			get { return mvarData; }
			set { mvarData = value; }
		}
		public override int Size
		{
			get
			{
				int result;
				if (this.mvarData == null)
				{
					result = 0;
				}
				else
				{
					result = this.mvarData.Length;
				}
				return result;
			}
		}
		public void Extract(string FileName)
		{
			System.IO.File.WriteAllBytes(FileName, mvarData);
		}

		public override object Clone()
		{
			RIFFDataChunk clone = new RIFFDataChunk();
			clone.Data = mvarData;
			clone.ID = base.ID;
			return clone;
		}

		public override string ToString()
		{
			return base.ID + " [" + mvarData.Length.ToString() + "]";
		}
	}
}
