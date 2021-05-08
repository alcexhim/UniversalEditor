//
//  ContactPhoto.cs - represents a photo in a ContactObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
	/// Represents a photo in a <see cref="ContactObjectModel" />.
	/// </summary>
	public class ContactPhoto : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactPhotoCollection
			: System.Collections.ObjectModel.Collection<ContactPhoto>
		{

		}

		#region IContactComplexType members
		public bool IsEmpty { get; set; } = false;
		public Guid ElementID { get; set; } = Guid.Empty;
		public DateTime? ModificationDate { get; set; } = null;
		#endregion

		public ContactLabel.ContactLabelCollection Labels { get; } = new ContactLabel.ContactLabelCollection();
		public string ContentType { get; set; } = String.Empty;
		public string ImageUrl { get; set; } = String.Empty;

		public object Clone()
		{
			ContactPhoto clone = new ContactPhoto();
			clone.ElementID = ElementID;
			clone.IsEmpty = IsEmpty;
			foreach (ContactLabel item in Labels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			clone.ModificationDate = ModificationDate;

			clone.ContentType = (ContentType.Clone() as string);
			clone.ImageUrl = (ImageUrl.Clone() as string);
			return clone;
		}
	}
}
