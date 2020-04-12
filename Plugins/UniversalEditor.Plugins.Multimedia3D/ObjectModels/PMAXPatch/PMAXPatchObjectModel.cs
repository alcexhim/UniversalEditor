//
//  PMAXPatchObjectModel.cs - provides an ObjectModel for manipulating Concertroid PMAX patch files
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

namespace UniversalEditor.ObjectModels.PMAXPatch
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Concertroid PMAX patch files.
	/// </summary>
	public class PMAXPatchObjectModel : ObjectModel
	{
		public override void Clear()
		{
			Patches.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			PMAXPatchObjectModel clone = (where as PMAXPatchObjectModel);
			if (clone == null) return;

			foreach (PMAXPatch patch in Patches)
			{
				clone.Patches.Add(patch.Clone() as PMAXPatch);
			}
		}

		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "PMAX patch";
			omr.Path = new string[] { "Multimedia", "3D Multimedia", "Concertroid Extensions" };
			omr.Description = "Extensions that enhance the 3D model rendered with Concertroid";
			return omr;
		}

		public PMAXPatch.PMAXPatchCollection Patches { get; } = new PMAXPatch.PMAXPatchCollection();

	}
}
