﻿//
//  SceneObjectGuids.cs
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
namespace UniversalEditor.ObjectModels.NWCSceneLayout
{
	public class SceneObjectGuids
	{
		public static Guid Button { get; } = new Guid("{621e6323-5b10-4fb4-843b-9aa607751a7b}");
		public static Guid Image { get; } = new Guid("{6f8d562e-8473-4872-be57-9fc04e575e9b}");
		public static Guid Label { get; } = new Guid("{061ded85-99fd-43b0-b23d-e31c904ba397}");
	}
}
