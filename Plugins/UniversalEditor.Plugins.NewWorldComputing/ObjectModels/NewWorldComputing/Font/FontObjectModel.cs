//
//  FontObjectModel.cs - provides an ObjectModel for manipulating fonts used in New World Computing (Heroes of Might and Magic II) games
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

using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.ObjectModels.NewWorldComputing.Font
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating fonts used in New World Computing (Heroes of Might and Magic II) games.
	/// </summary>
	public class FontObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Gaming", "New World Computing", "Heroes of Might and Magic", "Font" };
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarGlyphWidth = 0;
			mvarGlyphHeight = 0;
			mvarGlyphCollection.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			FontObjectModel clone = (where as FontObjectModel);
			if (clone == null) return;

			clone.GlyphWidth = mvarGlyphWidth;
			clone.GlyphHeight = mvarGlyphHeight;
			foreach (PictureObjectModel picture in mvarGlyphCollection.Pictures)
			{
				clone.GlyphCollection.Pictures.Add(picture.Clone() as PictureObjectModel);
			}
		}

		private ushort mvarGlyphWidth = 0;
		public ushort GlyphWidth { get { return mvarGlyphWidth; } set { mvarGlyphWidth = value; } }

		private ushort mvarGlyphHeight = 0;
		public ushort GlyphHeight { get { return mvarGlyphHeight; } set { mvarGlyphHeight = value; } }

		private string mvarGlyphCollectionFileName = String.Empty;
		public string GlyphCollectionFileName
		{
			get { return mvarGlyphCollectionFileName; }
			set
			{
				mvarGlyphCollectionFileName = value;
				if (System.IO.File.Exists(mvarGlyphCollectionFileName))
				{
					mvarGlyphCollection = UniversalEditor.Common.Reflection.GetAvailableObjectModel<PictureCollectionObjectModel>(mvarGlyphCollectionFileName);
				}
			}
		}

		private PictureCollectionObjectModel mvarGlyphCollection = new PictureCollectionObjectModel();
		public PictureCollectionObjectModel GlyphCollection
		{
			get { return mvarGlyphCollection; }
		}
	}
}
