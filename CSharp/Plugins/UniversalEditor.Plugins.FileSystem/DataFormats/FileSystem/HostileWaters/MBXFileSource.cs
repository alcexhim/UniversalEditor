using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HostileWaters
{
	/// <summary>
	/// Allows accessing data from an MBX file associated with a DAT file.
	/// </summary>
	public class MBXFileSource : FileSource
	{
		private string mvarMBXFileName = null;
		public string MBXFileName { get { return mvarMBXFileName; } set { mvarMBXFileName = value; } }

		private uint mvarOffset = 0;
		public uint Offset { get { return mvarOffset; } set { mvarOffset = value; } }

		private uint mvarLength = 0;
		public uint Length { get { return mvarLength; } set { mvarLength = value; } }

		private Reader mbxReader = null;

		public override byte[] GetData(long offset, long length)
		{
			if (mbxReader == null)
			{
				mbxReader = new Reader(new FileAccessor(MBXFileName, false, false));
			}

			mbxReader.Seek(mvarOffset, SeekOrigin.Begin);
			byte[] data = mbxReader.ReadBytes(mvarLength);
			return data;
		}
		public override long GetLength()
		{
			return mvarLength;
		}

		public MBXFileSource(string MBXFileName, uint offset, uint length)
		{
			mvarMBXFileName = MBXFileName;
			mvarOffset = offset;
			mvarLength = length;
		}
	}
}
