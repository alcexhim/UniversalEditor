namespace UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree
{
	public class FamilyTreeObjectModel : ObjectModel
	{
		public override void Clear ()
		{
			Events.Clear();
			Persons.Clear();
		}

		public override void CopyTo (ObjectModel where)
		{
			FamilyTreeObjectModel clone = (where as FamilyTreeObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			for (int i = 0; i < Events.Count; i++)
			{
				clone.Events.Add(Events[i].Clone() as Event);
			}
			for (int i = 0; i < Persons.Count; i++)
			{
				clone.Persons.Add(Persons[i].Clone() as Person);
			}
			for (int i = 0; i < Places.Count; i++)
			{
				clone.Places.Add(Places[i].Clone() as Place);
			}
		}

		public Event.EventCollection Events { get; } = new Event.EventCollection();
		public Person.PersonCollection Persons { get; } = new Person.PersonCollection();
		public Place.PlaceCollection Places { get; } = new Place.PlaceCollection();
		public Citation.CitationCollection Citations { get; } = new Citation.CitationCollection();

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal ()
		{
			if (_omr == null) {
				_omr = base.MakeReferenceInternal ();
				_omr.Path = new string[] { "Genealogy", "Family Tree" };
			}
			return _omr;
		}
	}
}
