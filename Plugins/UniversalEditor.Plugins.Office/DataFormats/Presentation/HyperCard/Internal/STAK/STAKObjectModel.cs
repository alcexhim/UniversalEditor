//
//  STAKObjectModel.cs
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
namespace UniversalEditor.Plugins.Office.DataFormats.Presentation.HyperCard.Internal.STAK
{
	public class STAKObjectModel : ObjectModel
	{
		/// <summary>
		/// Gets or sets the script associated with this stack.
		/// </summary>
		/// <value>The script associated with this stack.</value>
		public HyperCardScript Script { get; set; } = null;

		/// <summary>
		/// Gets or sets the width, in pixels, of cards in this stack.
		/// </summary>
		/// <value>The width, in pixels, of cards in this stack.</value>
		public ushort Width { get; set; } = 320;
		/// <summary>
		/// Gets or sets the height, in pixels, of cards in this stack.
		/// </summary>
		/// <value>The height, in pixels, of cards in this stack.</value>
		public ushort Height { get; set; } = 240;

		/// <summary>
		/// Gets or sets the number of cards in this stack.
		/// </summary>
		/// <value>The number of cards in this stack.</value>
		public uint CardCount { get; set; } = 0;
		/// <summary>
		/// Gets or sets the user level setting under which to run this stack.
		/// </summary>
		/// <value>The user level setting under which to run this stack.</value>
		public HyperCardUserLevel UserLevel { get; set; } = HyperCardUserLevel.Scripting;

		public override void Clear()
		{
		}
		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
