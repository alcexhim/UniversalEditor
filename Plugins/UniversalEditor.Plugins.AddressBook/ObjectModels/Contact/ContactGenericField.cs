//
//  ContactGenericField.cs - represents a generic field in a ContactObjectModel
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

namespace UniversalEditor.ObjectModels.Contact
{
	/// <summary>
	/// Represents a generic field in a <see cref="ContactObjectModel" />.
	/// </summary>
	public struct ContactGenericField<T>
	{
		private DateTime mvarModificationDate;
		public DateTime ModificationDate { get { return mvarModificationDate; } set { mvarModificationDate = value; } }

		private T mvarValue;
		public T Value { get { return mvarValue; } set { mvarValue = value; } }

		public static readonly ContactGenericField<T> Empty = new ContactGenericField<T>(default(T));

		public ContactGenericField(T value, DateTime? modificationDate = null)
		{
			mvarValue = value;
			if (modificationDate != null)
			{
				mvarModificationDate = modificationDate.Value;
			}
			else
			{
				mvarModificationDate = DateTime.Now;
			}
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
