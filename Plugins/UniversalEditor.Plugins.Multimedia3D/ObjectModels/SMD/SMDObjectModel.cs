//
//  SMDObjectModel.cs - provides an ObjectModel for manipulating StudioMDL (SMD) files
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

using System;

namespace UniversalEditor.ObjectModels.SMD
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating StudioMDL (SMD) files.
	/// </summary>
	public class SMDObjectModel : ObjectModel
	{
		public SMDSection.SMDSectionCollection Sections { get; } = new SMDSection.SMDSectionCollection();

		public override void Clear()
		{
			throw new NotImplementedException();
		}
		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Multimedia", "3D Multimedia", "Model", "StudioMDL container" };
			}
			return _omr;
		}
	}
}
