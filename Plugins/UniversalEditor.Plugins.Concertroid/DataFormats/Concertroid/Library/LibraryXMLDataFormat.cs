//
//  LibraryXMLDataFormat.cs - provides a DataFormat for manipulating Concertroid asset libraries in XML format
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
using System.Collections.Generic;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Concertroid.Library;
using UniversalEditor.ObjectModels.Concertroid;

namespace UniversalEditor.DataFormats.Concertroid.Library
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Concertroid asset libraries in XML format.
	/// </summary>
	public class LibraryXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(LibraryObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			LibraryObjectModel library = (objectModels.Pop() as LibraryObjectModel);
			if (library == null) throw new ObjectModelNotSupportedException();

			MarkupTagElement tagConcertroidLibrary = (mom.Elements["ConcertroidLibrary"] as MarkupTagElement);
			if (tagConcertroidLibrary == null) throw new InvalidDataFormatException("File does not contain top-level tag \"ConcertroidLibrary\"");

			MarkupTagElement tagProducers = (tagConcertroidLibrary.Elements["Producers"] as MarkupTagElement);
			if (tagProducers != null)
			{
				foreach (MarkupElement elProducer in tagProducers.Elements)
				{
					MarkupTagElement tagProducer = (elProducer as MarkupTagElement);
					if (tagProducer == null) continue;
					if (tagProducer.FullName != "Producer") continue;

					MarkupAttribute attProducerID = tagProducer.Attributes["ID"];
					if (attProducerID == null) continue;

					Producer p = new Producer();
					p.ID = new Guid(attProducerID.Value);

					MarkupTagElement tagInformation = (tagProducer.Elements["Information"] as MarkupTagElement);
					if (tagInformation != null)
					{
						MarkupTagElement tagGivenName = (tagInformation.Elements["GivenName"] as MarkupTagElement);
						if (tagGivenName != null) p.FirstName = tagGivenName.Value;

						MarkupTagElement tagFamilyName = (tagInformation.Elements["FamilyName"] as MarkupTagElement);
						if (tagFamilyName != null) p.LastName = tagFamilyName.Value;
					}
					library.Producers.Add(p);
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			LibraryObjectModel library = (objectModels.Pop() as LibraryObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagConcertroid = new MarkupTagElement();
			tagConcertroid.FullName = "Concertroid";
			tagConcertroid.Attributes.Add("Version", "1.0");



			mom.Elements.Add(tagConcertroid);
			objectModels.Push(mom);
		}
	}
}
