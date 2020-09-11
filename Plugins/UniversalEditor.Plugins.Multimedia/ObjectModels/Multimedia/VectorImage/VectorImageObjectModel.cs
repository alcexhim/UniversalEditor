//
//  VectorImageObjectModel.cs - provides an ObjectModel for manipulating vector images
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

using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.VectorImage
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating vector images.
	/// </summary>
	public class VectorImageObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;

		public int Width { get; set; } = 0;
		public int Height { get; set; } = 0;

		public Rectangle ViewBox { get; set; } = Rectangle.Empty;

		public VectorItem.VectorItemCollection Items { get; } = new VectorItem.VectorItemCollection();

		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Multimedia", "Picture", "Vector image" };
			}
			return _omr;
		}

		public override void Clear()
		{
			Items.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			VectorImageObjectModel clone = (where as VectorImageObjectModel);
			if (clone == null) return;

			for (int i = 0; i < Items.Count; i++)
			{
				clone.Items.Add(Items[i].Clone() as VectorItem);
			}
		}
	}
}
