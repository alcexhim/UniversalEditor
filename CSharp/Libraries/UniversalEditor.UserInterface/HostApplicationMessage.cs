using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public enum HostApplicationMessageSeverity
	{
		None = 0,
		Notice = 1,
		Warning = 2,
		Error = 3
	}
	public delegate void HostApplicationMessageModifyingEventHandler(object sender, HostApplicationMessageModifyingEventArgs e);
	public class HostApplicationMessageModifyingEventArgs
		: CancelEventArgs
	{
		public HostApplicationMessageModifyingEventArgs(HostApplicationMessage message)
		{
			mvarMessage = message;
		}

		private HostApplicationMessage mvarMessage = null;
		public HostApplicationMessage Message { get { return mvarMessage; } }
	}
	public delegate void HostApplicationMessageModifiedEventHandler(object sender, HostApplicationMessageModifiedEventArgs e);
	public class HostApplicationMessageModifiedEventArgs
		: EventArgs
	{
		public HostApplicationMessageModifiedEventArgs(HostApplicationMessage message)
		{
			mvarMessage = message;
		}

		private HostApplicationMessage mvarMessage = null;
		public HostApplicationMessage Message { get { return mvarMessage; } }
	}

	public class HostApplicationMessage
	{
		public class HostApplicationMessageCollection
			: System.Collections.ObjectModel.Collection<HostApplicationMessage>
		{
			public event HostApplicationMessageModifyingEventHandler MessageAdding;
			public event HostApplicationMessageModifyingEventHandler MessageRemoving;

			public event HostApplicationMessageModifiedEventHandler MessageAdded;
			public event HostApplicationMessageModifiedEventHandler MessageRemoved;

			public HostApplicationMessage Add(HostApplicationMessageSeverity severity, string description, string fileName = null, int? lineNumber = null, int? columnNumber = null, string projectName = null)
			{
				HostApplicationMessage message = new HostApplicationMessage();
				message.Severity = severity;
				message.Description = description;
				message.FileName = fileName;
				message.LineNumber = lineNumber;
				message.ColumnNumber = columnNumber;
				message.ProjectName = projectName;
				Add(message);
				return message;
			}

			protected virtual void OnMessageAdding(HostApplicationMessageModifyingEventArgs e)
			{
				if (MessageAdding != null)
				{
					MessageAdding(this, e);
				}
			}
			protected virtual void OnMessageAdded(HostApplicationMessageModifiedEventArgs e)
			{
				if (MessageAdded != null)
				{
					MessageAdded(this, e);
				}
			}

			protected virtual void OnMessageRemoving(HostApplicationMessageModifyingEventArgs e)
			{
				if (MessageRemoving != null)
				{
					MessageRemoving(this, e);
				}
			}
			protected virtual void OnMessageRemoved(HostApplicationMessageModifiedEventArgs e)
			{
				if (MessageRemoved != null)
				{
					MessageRemoved(this, e);
				}
			}

			protected override void InsertItem(int index, HostApplicationMessage item)
			{
				HostApplicationMessage message = item;
				HostApplicationMessageModifyingEventArgs e = new HostApplicationMessageModifyingEventArgs(message);
				OnMessageAdding(e);
				if (e.Cancel) return;

				base.InsertItem(index, item);

				OnMessageAdded(new HostApplicationMessageModifiedEventArgs(message));
			}
			protected override void RemoveItem(int index)
			{
				HostApplicationMessage message = this[index];
				HostApplicationMessageModifyingEventArgs e = new HostApplicationMessageModifyingEventArgs(message);
				OnMessageRemoving(e);
				if (e.Cancel) return;

				base.RemoveItem(index);

				OnMessageRemoved(new HostApplicationMessageModifiedEventArgs(message));
			}
		}

		private HostApplicationMessageSeverity mvarSeverity = HostApplicationMessageSeverity.None;
		public HostApplicationMessageSeverity Severity { get { return mvarSeverity; } set { mvarSeverity = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private string mvarFileName = null;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		private int? mvarLineNumber = null;
		public int? LineNumber { get { return mvarLineNumber; } set { mvarLineNumber = value; } }

		private int? mvarColumnNumber = null;
		public int? ColumnNumber { get { return mvarColumnNumber; } set { mvarColumnNumber = value; } }

		private string mvarProjectName = null;
		public string ProjectName { get { return mvarProjectName; } set { mvarProjectName = value; } }

	}
}
