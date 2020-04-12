//
//  AssociationCriteria.cs - criteria used when looking up Associations
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

namespace UniversalEditor
{
	public class AssociationCriteria
	{
		private Accessor mvarAccessor = null;
		/// <summary>
		/// The accessor to use for MagicByteSequence comparisons.
		/// </summary>
		public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

		private DataFormatReference mvarDataFormat = null;
		/// <summary>
		/// The <see cref="DataFormatReference" /> which points to the <see cref="DataFormat" /> to search for.
		/// </summary>
		public DataFormatReference DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

		private ObjectModelReference mvarObjectModel = null;
		/// <summary>
		/// The <see cref="ObjectModelReference" /> which points to the <see cref="ObjectModel" /> to search for.
		/// </summary>
		public ObjectModelReference ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }
		
		private string mvarFileName = null;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }
	}
}
