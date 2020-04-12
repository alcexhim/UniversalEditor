//
//  CampaignObjectModel.cs - provides an ObjectModel for manipulating campaign game definitions in New World Computing (Heroes of Might and Magic II) games
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

namespace UniversalEditor.ObjectModels.NewWorldComputing.Campaign
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating campaign game definitions in New World Computing (Heroes of Might and Magic II) games.
	/// </summary>
	public class CampaignObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "New World Computing campaign";
			}
			return _omr;
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		public override void Clear()
		{
			mvarTitle = String.Empty;
			mvarDescription = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			CampaignObjectModel clone = (where as CampaignObjectModel);
			clone.Description = (mvarDescription.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
		}
	}
}
