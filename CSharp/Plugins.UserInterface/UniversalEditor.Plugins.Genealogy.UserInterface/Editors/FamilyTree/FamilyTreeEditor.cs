using System;
using UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.FamilyTree
{
	public class FamilyTreeEditor : Editor
	{
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}
		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference ()
		{
			if (_er == null) {
				_er = base.MakeReference ();
				_er.SupportedObjectModels.Add (typeof (FamilyTreeObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged (EventArgs e)
		{
			base.OnObjectModelChanged (e);

			FamilyTreeObjectModel ftom = (ObjectModel as FamilyTreeObjectModel);
		}
	}
}
