//
//  GrampsXMLDataFormat.cs
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
using System;
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree;

namespace UniversalEditor.Plugins.Genealogy.DataFormats.Gramps.XML
{
	public class GrampsXMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(FamilyTreeObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);

			byte[] hints = Accessor.Reader.ReadBytes(2);
			if (hints[0] == 0x1F && hints[1] == 0x8B)
			{
				Accessor.Seek(-2, IO.SeekOrigin.Current);
				byte[] data = Accessor.Reader.ReadToEnd();

				System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
				System.IO.MemoryStream msout = new System.IO.MemoryStream();
				Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(ms, msout);

				MemoryAccessor ma = new MemoryAccessor(msout.ToArray());

				Accessor = ma;
			}

			objectModels.Push(new MarkupObjectModel());
		}

		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			FamilyTreeObjectModel ftom = (objectModels.Pop() as FamilyTreeObjectModel);

			MarkupTagElement tagDatabase = mom.Elements["database"] as MarkupTagElement;
			if (tagDatabase == null) throw new InvalidDataFormatException();

			Dictionary<string, Event> _eventsByHandle = new Dictionary<string, Event>();
			Dictionary<string, Place> _placesByHandle = new Dictionary<string, Place>();
			Dictionary<string, Citation> _citationsByHandle = new Dictionary<string, Citation>();
			Dictionary<string, Person> _personsByHandle = new Dictionary<string, Person>();

			Dictionary<Event, string> _eventPlaceHandles = new Dictionary<Event, string>();
			Dictionary<CitableDatabaseObject, List<string>> _citationHandles = new Dictionary<CitableDatabaseObject, List<string>>();

			MarkupTagElement tagEvents = tagDatabase.Elements["events"] as MarkupTagElement;
			if (tagEvents != null)
			{
				foreach (MarkupElement elEvent in tagEvents.Elements)
				{
					MarkupTagElement tagEvent = (elEvent as MarkupTagElement);
					if (tagEvent == null) continue;
					if (tagEvent.Name != "event") continue;

					MarkupAttribute attEventID = tagEvent.Attributes["id"];
					MarkupAttribute attEventHandle = tagEvent.Attributes["handle"];
					MarkupAttribute attEventChangeTimestamp = tagEvent.Attributes["change"];

					Event evt = new Event();
					evt.ID = attEventID.Value;
					_eventsByHandle[attEventHandle.Value] = evt;

					foreach (MarkupElement el in tagEvent.Elements)
					{
						MarkupTagElement tag = (el as MarkupTagElement);
						if (tag == null) continue;

						switch (tag.Name)
						{
							case "type":
							{
								evt.Type = tag.Value;
								break;
							}
							case "dateval":
							{
								evt.Date = ParseDateReference(tag);
								break;
							}
							case "place":
							{
								_eventPlaceHandles[evt] = tag.Attributes["hlink"].Value;
								break;
							}
							case "description":
							{
								evt.Description = tag.Value;
								break;
							}
							case "attribute":
							{
								DatabaseAttribute att = new DatabaseAttribute();
								MarkupAttribute attType = tag.Attributes["type"];
								MarkupAttribute attValue = tag.Attributes["value"];
								if (attType == null || attValue == null)
									continue;

								att.Name = attType.Value;
								att.Value = attValue.Value;

								LoadCitations(_citationHandles, tag, att);

								evt.Attributes.Add(att);
								break;
							}
							case "noteref":
							{
								break;
							}
						}
					}

					LoadCitations(_citationHandles, tagEvent, evt);
				}
			}

			MarkupTagElement tagPeople = tagDatabase.Elements["people"] as MarkupTagElement;
			if (tagPeople != null)
			{
				foreach (MarkupElement elPerson in tagPeople.Elements)
				{
					MarkupTagElement tagPerson = (elPerson as MarkupTagElement);
					if (tagPerson == null) continue;
					if (tagPerson.Name != "person") continue;

					MarkupAttribute attHandle = tagPerson.Attributes["handle"];
					MarkupAttribute attChange = tagPerson.Attributes["change"];
					MarkupAttribute attID = tagPerson.Attributes["id"];

					Person person = new Person();
					person.ID = attID.Value;
					_personsByHandle[attHandle.Value] = person;

					foreach (MarkupElement el in tagPerson.Elements)
					{
						MarkupTagElement tag = el as MarkupTagElement;
						if (tag == null) continue;

						switch (tag.Name)
						{
							case "gender":
							{
								switch (tag.Value.ToLower())
								{
									case "m":
									{
										person.Gender = Gender.Male;
										break;
									}
								}
								break;
							}
							case "name":
							{
								PersonName name = ParseName(tag);
								if (name != null)
								{
									person.Names.Add(name);
								}
								if (person.Names.Count > 0)
								{
									person.DefaultName = person.Names[0];
								}
								break;
							}
						}
					}

					ftom.Persons.Add(person);
				}
			}

			MarkupTagElement tagPlaces = tagDatabase.Elements["places"] as MarkupTagElement;
			if (tagPlaces != null)
			{
				foreach (MarkupElement elPlaceObj in tagPlaces.Elements)
				{
					MarkupTagElement tagPlaceObj = (elPlaceObj as MarkupTagElement);
					if (tagPlaceObj == null) continue;
					if (tagPlaceObj.Name != "placeobj") continue;

					MarkupAttribute attHandle = tagPlaceObj.Attributes["handle"];
					MarkupAttribute attChange = tagPlaceObj.Attributes["change"];
					MarkupAttribute attID = tagPlaceObj.Attributes["id"];
					MarkupAttribute attType = tagPlaceObj.Attributes["type"];

					Place place = new Place();
					_placesByHandle[attHandle.Value] = place;
					// place.Type = PlaceType.Parse(attType.Value);
					foreach (MarkupElement el in tagPlaceObj.Elements)
					{
						MarkupTagElement tag = (el as MarkupTagElement);
						if (tag == null) continue;

						switch (tag.Name)
						{
							case "ptitle":
							{
								place.Title = tag.Value;
								break;
							}
							case "code":
							{
								place.Code = tag.Value;
								break;
							}
							case "pname":
							{
								place.Names.Add(ParsePlaceName(tag));
								break;
							}
						}
					}
					ftom.Places.Add(place);
				}
			}

			MarkupTagElement tagCitations = tagDatabase.Elements["citations"] as MarkupTagElement;
			if (tagCitations != null)
			{
				foreach (MarkupElement elCitation in tagCitations.Elements)
				{
					MarkupTagElement tagCitation = (elCitation as MarkupTagElement);
					if (tagCitation == null) continue;
					if (tagCitation.Name != "citation") continue;

					MarkupAttribute attHandle = tagCitation.Attributes["handle"];
					MarkupAttribute attChange = tagCitation.Attributes["change"];
					MarkupAttribute attID = tagCitation.Attributes["id"];

					Citation citation = new Citation();
					_citationsByHandle[attHandle.Value] = citation;

					ftom.Citations.Add(citation);
				}
			}

			// go through and apply all reference handles
			foreach (KeyValuePair<Event, string> eventPlaceHandle in _eventPlaceHandles)
			{
				eventPlaceHandle.Key.Place = _placesByHandle[eventPlaceHandle.Value];
			}
			foreach (KeyValuePair<CitableDatabaseObject, List<string>> citationHandle in _citationHandles)
			{
				foreach (string handle in citationHandle.Value)
				{
					citationHandle.Key.Citations.Add(_citationsByHandle[handle]);
				}
			}


			objectModels.Push(ftom);
		}

		private PlaceName ParsePlaceName(MarkupTagElement tag)
		{
			PlaceName name = new PlaceName();
			name.Value = tag.Value;
			if (tag.Attributes["lang"] != null)
			{
				name.Language = tag.Attributes["lang"].Value;
			}

			MarkupTagElement tagDateSpan = tag.Elements["datespan"] as MarkupTagElement;
			if (tagDateSpan != null)
			{
				name.DateSpan = new DateReference(Date.Parse(tagDateSpan.Attributes["start"]?.Value), Date.Parse(tagDateSpan.Attributes["stop"]?.Value), DateType.Range);
			}

			return name;
		}

		private PersonName ParseName(MarkupTagElement tag)
		{
			PersonName name = new PersonName();
			name.Type = PersonNameType.Parse(tag.Attributes["type"]?.Value);
			foreach (MarkupElement el in tag.Elements)
			{
				MarkupTagElement tag2 = (el as MarkupTagElement);
				if (tag2 == null) continue;
				switch (tag2.Name)
				{
					case "first":
					{
						name.CompleteGivenName = tag2.Value;
						break;
					}
					case "surname":
					{
						name.Surnames.Add(ParseSurname(tag2));
						break;
					}
				}
			}
			return name;
		}

		private Surname ParseSurname(MarkupTagElement tag)
		{
			Surname name = new Surname();
			name.Origin = SurnameOrigin.Parse(tag.Attributes["derivation"]?.Value);
			name.Name = tag.Value;
			return name;
		}

		private void LoadCitations(Dictionary<CitableDatabaseObject, List<string>> citationHandles, MarkupTagElement tag, CitableDatabaseObject obj)
		{
			foreach (MarkupElement el in tag.Elements)
			{
				MarkupTagElement tag2 = (el as MarkupTagElement);
				if (tag2 == null) continue;

				switch (tag2.Name)
				{
					case "citationref":
					{
						if (!citationHandles.ContainsKey(obj))
						{
							citationHandles[obj] = new List<string>();
						}
						citationHandles[obj].Add(tag2.Attributes["hlink"].Value);
						break;
					}
				}
			}
		}

		private DateReference ParseDateReference(MarkupTagElement tag)
		{
			MarkupAttribute attVal = tag.Attributes["val"];

			if (attVal == null) throw new FormatException("'val' attribute not found on dateval tag");
			DateReference date = DateReference.Parse(attVal.Value);

			MarkupAttribute attType = tag.Attributes["type"];
			if (attType != null)
			{
				switch (attType.Value.ToLower())
				{
					case "about":
					{
						date.Type = DateType.Approximate;
						break;
					}
				}
			}

			return date;
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			FamilyTreeObjectModel ftom = (objectModels.Pop() as FamilyTreeObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			objectModels.Push(mom);
		}


	}
}
