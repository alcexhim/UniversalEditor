//
//  BVHDataFormat.cs - provides a DataFormat for manipulating animation data in BioVision Hierarchy (BVH) and QAvimator (AVM) format
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

using UniversalEditor.ObjectModels.Multimedia3D.Motion;

namespace UniversalEditor.DataFormats.Multimedia3D.Motion.Biovision
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating animation data in BioVision Hierarchy (BVH) and QAvimator (AVM) format.
	/// </summary>
	public class BVHDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(MotionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader tr = base.Accessor.Reader;

		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer tw = base.Accessor.Writer;


		}
	}
}
