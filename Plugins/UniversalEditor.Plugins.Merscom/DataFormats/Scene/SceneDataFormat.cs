//
//  SceneDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using System.Collections.Generic;
using UniversalEditor.DataFormats.Chunked.RIFF;
using UniversalEditor.ObjectModels.Chunked;

namespace UniversalEditor.Plugins.Merscom.DataFormats.Scene
{
	public class SceneDataFormat : SceneChunkDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);

			ChunkedObjectModel chunked = new ChunkedObjectModel();
			objectModels.Push(chunked);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = (objectModels.Pop() as ChunkedObjectModel);

		}
	}
}
