//
//  FileNameShortener.cs - truncates a file name with the MS-DOS style filena~1.ext
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
using System.Collections.Generic;

namespace UniversalEditor.IO
{
	public class FileNameShortener
	{
		private Dictionary<string, int> _TruncatedCount = new Dictionary<string, int>();

		public int MaxFileNameLength { get; set; } = 8;
		public int MaxExtensionLength { get; set; } = 3;
		public int Count { get; set; } = 0;

		public string Shorten(string value)
		{
			string filename = System.IO.Path.GetFileNameWithoutExtension(value);
			string extension = System.IO.Path.GetExtension(value);
			if (extension.StartsWith("."))
				extension = extension.Substring(1);

			filename = filename.Replace(".", String.Empty);

			if (filename.Length <= MaxFileNameLength && extension.Length <= MaxExtensionLength)
				return filename + '.' + extension;

			extension = extension.Substring(0, MaxExtensionLength);

			int len = Math.Min(filename.Length, MaxFileNameLength) - 1 - Count.ToString().Length;// with ~xx.xxx
			string truncated = filename.Substring(0, len);
			string truncatedWithExtension = truncated + '.' + extension;
			if (!_TruncatedCount.ContainsKey(truncatedWithExtension))
			{
				_TruncatedCount[truncatedWithExtension] = 0;
			}
			_TruncatedCount[truncatedWithExtension]++;

			return truncated + '~' + _TruncatedCount[truncatedWithExtension].ToString() + '.' + extension;
		}
	}
}
