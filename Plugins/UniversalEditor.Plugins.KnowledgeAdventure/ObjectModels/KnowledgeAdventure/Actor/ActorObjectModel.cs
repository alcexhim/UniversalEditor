//
//  ActorObjectModel.cs - provides an ObjectModel to store data for Knowledge Adventure actor files
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

namespace UniversalEditor.ObjectModels.KnowledgeAdventure.Actor
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> to store data for Knowledge Adventure actor files.
	/// </summary>
	public class ActorObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Knowledge Adventure", "Actor" };
			}
			return _omr;
		}

		private string mvarName = String.Empty;
		/// <summary>
		/// The name used in scripts to identify this <see cref="Actor" />.
		/// </summary>
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarImageFileName = String.Empty;
		/// <summary>
		/// The file name of the image file.
		/// </summary>
		public string ImageFileName { get { return mvarImageFileName; } set { mvarImageFileName = value; } }

		public override void Clear()
		{
			mvarName = String.Empty;
			mvarImageFileName = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			ActorObjectModel clone = (where as ActorObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			clone.Name = (mvarName.Clone() as string);
			clone.ImageFileName = (mvarImageFileName.Clone() as string);
		}
	}
}
