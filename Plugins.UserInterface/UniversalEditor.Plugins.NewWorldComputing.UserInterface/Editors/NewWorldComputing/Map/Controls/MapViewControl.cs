//
//  MapViewControl.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.FileSystem.NewWorldComputing.AGG;
using UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.ICN;
using UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.TIL;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

using UniversalEditor.Plugins.Multimedia.UserInterface;

namespace UniversalEditor.Plugins.NewWorldComputing.UserInterface.Editors.NewWorldComputing.Map.Controls
{
	public class MapViewControl : CustomControl
	{
		private int _TileSize = 32;
		public int TileSize { get { return _TileSize; } set { _TileSize = value; } }

		private MBS.Framework.UserInterface.Drawing.Pen pbk = new MBS.Framework.UserInterface.Drawing.Pen(SystemColors.HighlightBackground);

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			MapEditor ed = (Parent as MapEditor);
			if (ed == null) return;

			MapObjectModel map = (ed.ObjectModel as MapObjectModel);
			e.Graphics.DrawRectangle(pbk, new MBS.Framework.Drawing.Rectangle(0, 0, map.Width * TileSize, map.Height * TileSize));

			int x = 0, y = 0;
			for (int i = 0; i < map.Tiles.Count; i++)
			{
				if (x >= (TileSize * map.Width))
				{
					x = 0;
					y += TileSize;
				}

				if (!(x >= HorizontalAdjustment.Value + Size.Width || y >= VerticalAdjustment.Value + Size.Height))
				{
					DrawTile(e.Graphics, map.Tiles[i], x, y);
				}

				x += TileSize;
			}
		}

		PictureCollectionObjectModel spriteTile = null;

		/// <summary>
		/// Initializes the <see cref="spriteTile" /> <see cref="PictureCollectionObjectModel" /> with tile images from the game. This function should be called only once.
		/// </summary>
		private void InitSpriteTile()
		{
			if (spriteTile != null) return; // already done

			string aggpath = @"/opt/fheroes2/data/HEROES2.AGG";
			string tilepath = "GROUND32.TIL";

			FileSystemObjectModel fsomAgg = new FileSystemObjectModel();
			using (Document.Load(fsomAgg, new AGGDataFormat(), new FileAccessor(aggpath), false))
			{
				File fileTile = fsomAgg.Files[tilepath];
				if (fileTile == null) return;

				byte[] dataTile = fileTile.GetData();
				MemoryAccessor maTile = new MemoryAccessor(dataTile);

				TILDataFormat icndf = new TILDataFormat();

				spriteTile = new PictureCollectionObjectModel();
				Document.Load(spriteTile, icndf, maTile);
			}
		}

		private void DrawTile(Graphics graphics, MapTile tile, int x, int y)
		{
			InitSpriteTile();

			PictureObjectModel pic = spriteTile.Pictures[tile.IndexName2];
			graphics.DrawImage(pic.ToImage(), x, y);
		}
	}
}
