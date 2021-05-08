//
//  ConcertXMLDataFormat.cs - provides a DataFormat for manipulating Concertroid concert information in XML format
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

using System.Collections.Generic;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Concertroid.Concert;

namespace UniversalEditor.DataFormats.Concertroid.Concert
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Concertroid concert information in XML format.
	/// </summary>
	public class ConcertXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(ConcertObjectModel), DataFormatCapabilities.All);
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
			ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);


		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			ConcertObjectModel concert = (objectModels.Pop() as ConcertObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagConcertroid = new MarkupTagElement();
			tagConcertroid.FullName = "Concertroid";
			tagConcertroid.Attributes.Add("Version", "1.0");



			mom.Elements.Add(tagConcertroid);
			objectModels.Push(mom);
		}
	}
}
