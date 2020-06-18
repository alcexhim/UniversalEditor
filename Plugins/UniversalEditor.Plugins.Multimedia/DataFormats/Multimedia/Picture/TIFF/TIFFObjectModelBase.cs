//
//  TIFFObjectModelBase.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public class TIFFObjectModelBase : ObjectModel
	{
		public TIFFImageFileDirectory.TIFFImageFileDirectoryCollection ImageFileDirectories { get; } = new TIFFImageFileDirectory.TIFFImageFileDirectoryCollection();

		public override void Clear()
		{
			ImageFileDirectories.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			TIFFObjectModelBase clone = (where as TIFFObjectModelBase);
			if (clone == null) throw new ObjectModelNotSupportedException();

			for (int i = 0; i < ImageFileDirectories.Count; i++)
			{
				clone.ImageFileDirectories.Add(ImageFileDirectories[i].Clone() as TIFFImageFileDirectory);
			}
		}
	}
}
