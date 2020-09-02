//
//  ModelReference.cs
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
namespace UniversalEditor.Plugins.RavenSoftware.ObjectModels.Carcass
{
	public class ModelReference : ICloneable
	{

		public class ModelReferenceCollection
			: System.Collections.ObjectModel.Collection<ModelReference>
		{

		}

		public string FileName { get; set; } = null;
		public int LoopCount { get; set; } = -1;
		public bool GenerateLoopFrame { get; set; } = false;
		public int FrameSpeed { get; set; } = DEFAULT_FRAMESPEED;
		public string EnumName { get; set; } = null;

		public CarcassFrame.CarcassFrameCollection AdditionalFrames { get; } = new CarcassFrame.CarcassFrameCollection();

		public const int MAX_FRAMESPEED = 26083348;

		public const int DEFAULT_FRAMESPEED = MAX_FRAMESPEED;
		public const int DEFAULT_FRAMESPEED_XSI = 20;

		public object Clone()
		{
			ModelReference clone = new ModelReference();
			clone.FileName = (FileName.Clone() as string);
			clone.LoopCount = LoopCount;
			clone.GenerateLoopFrame = GenerateLoopFrame;
			clone.FrameSpeed = FrameSpeed;
			clone.EnumName = (EnumName?.Clone() as string);
			for (int i = 0; i < AdditionalFrames.Count; i++)
			{
				clone.AdditionalFrames.Add(AdditionalFrames[i].Clone() as CarcassFrame);
			}
			return clone;
		}
	}
}
