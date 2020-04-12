//
//  PMDExtensionObjectModel.cs - represents an extension to the AniMiku PMD model format
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

namespace UniversalEditor.ObjectModels.AniMiku.PMDExtension
{
	/// <summary>
	/// Represents an extension to the AniMiku PMD model format.
	/// </summary>
	internal class PMDExtensionObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null)
            {
                _omr = base.MakeReferenceInternal();
                _omr.Title = "AniMiku PMD model extension";
            }
            return _omr;
        }
        public override void Clear()
        {
            mvarArchiveFiles.Clear();
        }
        public override void CopyTo(ObjectModel where)
        {
            PMDExtensionObjectModel clone = (where as PMDExtensionObjectModel);
            if (clone == null) return;

            foreach (PMDExtensionTextureGroup file in mvarArchiveFiles)
            {
                clone.ArchiveFiles.Add(file.Clone() as PMDExtensionTextureGroup);
            }
        }

        private PMDExtensionTextureGroup.PMDExtensionArchiveFileCollection mvarArchiveFiles = new PMDExtensionTextureGroup.PMDExtensionArchiveFileCollection();
        public PMDExtensionTextureGroup.PMDExtensionArchiveFileCollection ArchiveFiles
        {
            get { return mvarArchiveFiles; }
        }
    }
}
