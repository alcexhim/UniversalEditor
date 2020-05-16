//
//  ScenePropertyGuids.cs
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
	public static class ScenePropertyGuids
	{
		public static class Button
		{
			public static Guid BackgroundImageFileName { get; } = new Guid("{df3a8c45-a6bf-44d3-bf46-d8569e78bc71}");
			public static Guid BackgroundImageIndex { get; } = new Guid("{e4226ed5-0ffa-468a-972f-146c934b0aa2}");
		}
		public static class Image
		{
			public static Guid BackgroundImageFileName { get; } = new Guid("{df3a8c45-a6bf-44d3-bf46-d8569e78bc71}");
			public static Guid BackgroundImageIndex { get; } = new Guid("{e4226ed5-0ffa-468a-972f-146c934b0aa2}");
		}
		public static class Label
		{
			public static Guid Text { get; } = new Guid("{25abbdfa-5b9d-491c-9b96-33dc0fd25156}");
			public static Guid FontFileName { get; } = new Guid("{eca4baa1-9f92-4c80-82fa-490a758083d0}");
		}
	}
}
