namespace UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree
{
	public class FamilyTreeObjectModel : ObjectModel
	{
		public override void Clear ()
		{
		}

		public override void CopyTo (ObjectModel where)
		{
		}

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
