namespace UniversalEditor.ObjectModels.FamilyTree
{
	public class FamilyTreeObjectModel : ObjectModel
	{
		public override void Clear ()
		{
			throw new System.NotImplementedException ();
		}

		public override void CopyTo (ObjectModel where)
		{
			throw new System.NotImplementedException ();
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal ()
		{
			if (_omr == null) {
				_omr = base.MakeReferenceInternal ();
				_omr.Title = "Family Tree";
			}
			return _omr;
		}
	}
}
