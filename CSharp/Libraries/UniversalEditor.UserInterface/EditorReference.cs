using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public class EditorReference
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Type mvarEditorType = null;
		public Type EditorType { get { return mvarEditorType; } set { mvarEditorType = value; } }

		private ObjectModelReference.ObjectModelReferenceCollection mvarSupportedObjectModels = new ObjectModelReference.ObjectModelReferenceCollection();
		public ObjectModelReference.ObjectModelReferenceCollection SupportedObjectModels { get { return mvarSupportedObjectModels; } }

		public EditorReference(Type type)
		{
			mvarEditorType = type;
		}

		public Editor Create()
		{
			if (mvarEditorType != null)
			{
				return (mvarEditorType.Assembly.CreateInstance(mvarEditorType.FullName) as Editor);
			}
			return null;
		}
	}
}
