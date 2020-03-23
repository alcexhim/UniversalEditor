//
//  DragDropTargetFlags.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
namespace MBS.Framework.UserInterface.DragDrop
{
	[Flags()]
	public enum DragDropTargetFlags
	{
		None = 0,
		/// <summary>
		/// If this is set, the target will only be selected for drags within a single application.
		/// </summary>
		SameApplication,
		/// <summary>
		/// If this is set, the target will only be selected for drags within a single widget.
		/// </summary>
		SameWidget,
		/// <summary>
		/// If this is set, the target will not be selected for drags within a single application.
		/// </summary>
		OtherApplication,
		/// <summary>
		/// If this is set, the target will not be selected for drags withing a single widget.
		/// </summary>
		OtherWidget
	}
}
