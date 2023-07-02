//
//  PartitionGuiInfo.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.PartitionedFileSystem;

namespace UniversalEditor.Editors.PartitionedFileSystem
{
	public struct PartitionGuiInfo
	{
		public string Name { get; }
		public PartitionType PartitionType { get; }
		public Color Color { get; }
		public Image ColorImage { get; }

		public PartitionGuiInfo(PartitionType type, string name, Color color)
		{
			Name = name;
			PartitionType = type;
			Color = color;

			Image image = Image.Create(16, 16);
			Graphics g = Graphics.FromImage(image);
			//g.Clear(color);
			g.FillRectangle(new SolidBrush(color), new Rectangle(0, 0, 16, 16));

			ColorImage = image;
		}
	}
}
