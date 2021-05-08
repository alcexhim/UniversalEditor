//
//  ContactPhysicalAddress.cs - represents a physical address in a ContactObjectModel
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
using System.Text;

namespace UniversalEditor.ObjectModels.Contact
{
	/// <summary>
	/// Represents a physical address in a <see cref="ContactObjectModel" />.
	/// </summary>
	public class ContactPhysicalAddress : ICloneable, IContactLabelContainer, IContactComplexType
	{
		public class ContactPhysicalAddressCollection
			: System.Collections.ObjectModel.Collection<ContactPhysicalAddress>
		{

		}

		#region IContactComplexType members
		public bool IsEmpty { get; set; } = false;
		public Guid ElementID { get; set; } = Guid.Empty;
		public DateTime? ModificationDate { get; set; } = null;
		#endregion

		public ContactGenericField<string> Country { get; set; } = ContactGenericField<string>.Empty;
		public ContactGenericField<string> PostalCode { get; set; } = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Region { get; set; } = ContactGenericField<string>.Empty;
		public ContactGenericField<string> Locality { get; set; } = ContactGenericField<string>.Empty;
		public ContactGenericField<string> StreetAddress { get; set; } = ContactGenericField<string>.Empty;
		public ContactLabel.ContactLabelCollection Labels { get; } = new ContactLabel.ContactLabelCollection();

		public object Clone()
		{
			ContactPhysicalAddress clone = new ContactPhysicalAddress();
			clone.Country = Country;
			clone.ElementID = ElementID;
			foreach (ContactLabel item in Labels)
			{
				clone.Labels.Add(item.Clone() as ContactLabel);
			}
			clone.IsEmpty = IsEmpty;
			clone.Locality = Locality;
			clone.ModificationDate = ModificationDate;
			clone.PostalCode = PostalCode;
			clone.Region = Region;
			clone.StreetAddress = StreetAddress;
			return clone;
		}

		public string ToString(bool multiline)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(StreetAddress);
			if (multiline)
			{
				sb.Append(Environment.NewLine);
			}
			else
			{
				sb.Append(", ");
			}
			sb.Append(Locality);
			sb.Append(", ");
			sb.Append(Region);
			sb.Append(' ');
			sb.Append(PostalCode);

			return sb.ToString();
		}
		public override string ToString()
		{
			return ToString(false);
		}
	}
}
