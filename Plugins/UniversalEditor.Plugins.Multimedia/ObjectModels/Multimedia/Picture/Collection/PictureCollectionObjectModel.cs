//
//  PictureCollectionObjectModel.cs - provides an ObjectModel for manipulating sprites (collections of images)
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

namespace UniversalEditor.ObjectModels.Multimedia.Picture.Collection
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating sprites (collections of images).
	/// </summary>
	public class PictureCollectionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Description = "Store multiple pictures in a single file";
				_omr.Path = new string[] { "Multimedia", "Picture", "Picture collection" };
			}
			return _omr;
		}

		/// <summary>
		/// Gets a collection of <see cref="PictureObjectModel" /> instances representing the pictures in this <see cref="PictureCollectionObjectModel" />.
		/// </summary>
		/// <value>The pictures in this <see cref="PictureCollectionObjectModel" />.</value>
		public PictureObjectModel.PictureObjectModelCollection Pictures { get; } = new PictureObjectModel.PictureObjectModelCollection();

		/// <summary>
		/// Gets the width of the picture with the largest width in this <see cref="PictureCollectionObjectModel" />.
		/// </summary>
		/// <value>The largest width of all the widths of the pictures in this <see cref="PictureCollectionObjectModel" />.</value>
		public int MaximumPictureWidth
		{
			get
			{
				int value = 0;
				foreach (PictureObjectModel pic in Pictures)
				{
					if (pic.Width > value) value = pic.Width;
				}
				return value;
			}
		}
		/// <summary>
		/// Gets the height of the picture with the largest height in this <see cref="PictureCollectionObjectModel" />.
		/// </summary>
		/// <value>The largest height of all the heights of the pictures in this <see cref="PictureCollectionObjectModel" />.</value>
		public int MaximumPictureHeight
		{
			get
			{
				int value = 0;
				foreach (PictureObjectModel pic in Pictures)
				{
					if (pic.Height > value) value = pic.Height;
				}
				return value;
			}
		}

		public override void Clear()
		{
			Pictures.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			PictureCollectionObjectModel clone = (where as PictureCollectionObjectModel);
			if (clone == null) return;

			foreach (PictureObjectModel pic in Pictures)
			{
				clone.Pictures.Add(pic.Clone() as PictureObjectModel);
			}
		}
	}
}
