using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Provides an interface for custom editor implementations not using the Universal Widget Toolkit.
	/// </summary>
	public interface IEditorImplementation
	{
		/// <summary>
		/// Copies the selected content to the Universal Editor clipboard.
		/// </summary>
		void Copy();
		/// <summary>
		/// Pastes the content from the Universal Editor clipboard, overwriting any selected content.
		/// </summary>
		void Paste();
		/// <summary>
		/// Deletes the selected content.
		/// </summary>
		void Delete();

		/// <summary>
		/// Restores the previous object model in the stack.
		/// </summary>
		void Undo();
		/// <summary>
		/// Restores the previously-undone object model from the stack.
		/// </summary>
		void Redo();

		string Title { get; }
		ObjectModelReference.ObjectModelReferenceCollection SupportedObjectModels { get; }
		
		event ToolboxItemEventHandler ToolboxItemAdded;
		event ToolboxItemEventHandler ToolboxItemSelected;
		
		bool SelectToolboxItem(ToolboxItem item);
	}
}
