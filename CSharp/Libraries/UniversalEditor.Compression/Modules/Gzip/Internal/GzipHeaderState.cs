using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.Modules.Gzip.Internal
{
	internal enum GzipHeaderState
	{
		ReadingID1,
		ReadingID2,
		ReadingCM,
		ReadingFLG,
		ReadingMMTime,
		ReadingXFL,
		ReadingOS,
		ReadingXLen1,
		ReadingXLen2,
		ReadingXLenData,
		ReadingFileName,
		ReadingComment,
		ReadingCRC16Part1,
		ReadingCRC16Part2,
		Done,
		ReadingCRC,
		ReadingFileSize
	}
}
