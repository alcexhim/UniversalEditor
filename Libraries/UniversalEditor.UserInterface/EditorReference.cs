using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Logic;
using MBS.Framework.UserInterface;
using UniversalEditor.ObjectModels.Markup;

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

		public MarkupTagElement Configuration { get; set; } = null;

		public CommandBar MenuBar { get; } = new CommandBar();
		public Command.CommandCollection Commands { get; } = new Command.CommandCollection();
		public KeyBinding.KeyBindingCollection KeyBindings { get; } = new KeyBinding.KeyBindingCollection();

		public EditorView.EditorViewCollection Views { get; } = new EditorView.EditorViewCollection();
		public Variable.VariableCollection Variables { get; } = new Variable.VariableCollection();

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
