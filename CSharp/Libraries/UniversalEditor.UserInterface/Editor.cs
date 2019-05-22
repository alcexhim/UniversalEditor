using UniversalWidgetToolkit;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Provides an interface for custom editor implementations not using the Universal Widget Toolkit.
	/// </summary>
	public abstract class Editor : Control
	{

		/// <summary>
		/// Copies the selected content to the Universal Editor clipboard.
		/// </summary>
		public abstract void Copy();
		/// <summary>
		/// Pastes the content from the Universal Editor clipboard, overwriting any selected content.
		/// </summary>
		public abstract void Paste();
		/// <summary>
		/// Deletes the selected content.
		/// </summary>
		public abstract void Delete();

		/// <summary>
		/// Restores the previous object model in the stack.
		/// </summary>
		public abstract void Undo();
		/// <summary>
		/// Restores the previously-undone object model from the stack.
		/// </summary>
		public abstract void Redo();

		public abstract string Title { get; }

		public event ToolboxItemEventHandler ToolboxItemAdded;
		public event ToolboxItemEventHandler ToolboxItemSelected;

		public abstract bool SelectToolboxItem(ToolboxItem item);

		public abstract EditorReference MakeReference();
	}
}
