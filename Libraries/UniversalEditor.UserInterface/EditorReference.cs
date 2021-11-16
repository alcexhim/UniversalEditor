using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework;
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

		public Context.ContextCollection Contexts { get; } = new Context.ContextCollection();

		private Type mvarEditorType = null;
		public Type EditorType { get { return mvarEditorType; } set { mvarEditorType = value; } }

		private ObjectModelReference.ObjectModelReferenceCollection mvarSupportedObjectModels = new ObjectModelReference.ObjectModelReferenceCollection();
		public event EventHandler ConfigurationLoaded;

		public ObjectModelReference.ObjectModelReferenceCollection SupportedObjectModels { get { return mvarSupportedObjectModels; } }

		public MarkupTagElement Configuration { get; set; } = null;

		public CommandBar MenuBar { get; } = new CommandBar();
		public CommandBar.CommandBarCollection CommandBars { get; } = new CommandBar.CommandBarCollection();
		public Command.CommandCollection Commands { get; } = new Command.CommandCollection();
		public PanelReference.PanelReferenceCollection Panels { get; } = new PanelReference.PanelReferenceCollection();
		public EditorView.EditorViewCollection Views { get; } = new EditorView.EditorViewCollection();
		public Variable.VariableCollection Variables { get; } = new Variable.VariableCollection();
		public Toolbox Toolbox { get; } = new Toolbox();

		private bool _ConfigurationInitialized = false;
		public void InitializeConfiguration()
		{
			if (_ConfigurationInitialized) return;
			ConfigurationLoaded?.Invoke(this, EventArgs.Empty);
			_ConfigurationInitialized = true;
		}

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
