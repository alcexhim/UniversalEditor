using System;
using UniversalEditor.UserInterface;
using UniversalWidgetToolkit;

namespace UniversalEditor.Engines.UWT
{
	public class Editor : Control
	{
		public Editor()
		{
		}

		public string Title => throw new NotImplementedException();

		public event ToolboxItemEventHandler ToolboxItemAdded;
		public event ToolboxItemEventHandler ToolboxItemSelected;

		public void Copy()
		{
			throw new NotImplementedException();
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}

		public EditorReference MakeReference()
		{
			throw new NotImplementedException();
		}

		public void Paste()
		{
			throw new NotImplementedException();
		}

		public void Redo()
		{
			throw new NotImplementedException();
		}

		public bool SelectToolboxItem(ToolboxItem item)
		{
			throw new NotImplementedException();
		}

		public void Undo()
		{
			throw new NotImplementedException();
		}
	}
}
