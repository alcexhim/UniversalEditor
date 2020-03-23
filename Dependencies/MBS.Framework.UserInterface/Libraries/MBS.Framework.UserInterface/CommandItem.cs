using System;
using UniversalEditor.ObjectModels.Markup;

namespace MBS.Framework.UserInterface
{
	public abstract class CommandItem
	{
		public static CommandItem FromMarkup(MarkupTagElement tag)
		{
			CommandItem item = null;

			MarkupAttribute attInsertAfter = tag.Attributes["InsertAfter"];
			MarkupAttribute attInsertBefore = tag.Attributes["InsertBefore"];

			switch (tag.FullName)
			{
				case "CommandReference":
				{
					MarkupAttribute attCommandID = tag.Attributes["CommandID"];
					if (attCommandID != null)
					{
						item = new CommandReferenceCommandItem(attCommandID.Value);
					}
					break;
				}
				case "CommandPlaceholder":
				{
					MarkupAttribute attPlaceholderID = tag.Attributes["PlaceholderID"];
					if (attPlaceholderID != null)
					{
						item = new CommandPlaceholderCommandItem(attPlaceholderID.Value);
					}
					break;
				}
				case "Separator":
				{
					item = new SeparatorCommandItem();
					break;
				}
				case "Group":
				{
					item = new GroupCommandItem();

					MarkupTagElement tagItems = (tag.Elements["Items"] as MarkupTagElement);
					if (tagItems != null)
					{
						for (int i = 0; i < tagItems.Elements.Count; i++)
						{
							MarkupTagElement tag1 = (tagItems.Elements[i] as MarkupTagElement);
							if (tag1 == null) continue;

							CommandItem childItem = CommandItem.FromMarkup(tag1);
							(item as GroupCommandItem).Items.Add(childItem);
						}
					}
					break;
				}
				default:
				{
					if (System.Diagnostics.Debugger.IsAttached)
					{
						throw new ArgumentException(String.Format("unrecognized CommandBar Item type '{0}'", tag.FullName));
					}
					break;
				}
			}

			if (item != null)
			{
				if (attInsertAfter != null)
					item.InsertAfterID = attInsertAfter.Value;
				if (attInsertBefore != null)
					item.InsertBeforeID = attInsertBefore.Value;
			}
			return item;
		}

		public string InsertAfterID { get; set; } = null;
		public string InsertBeforeID { get; set; } = null;

		public class CommandItemCollection
			: System.Collections.ObjectModel.Collection<CommandItem>
		{
			public int IndexOf(string value)
			{
				for (int i = 0; i < Count; i++)
				{
					if (this[i] is CommandReferenceCommandItem)
					{
						if ((this[i] as CommandReferenceCommandItem).CommandID.Equals(value))
							return i;
					}
				}
				return -1;
			}
		}
	}
	public class CommandReferenceCommandItem : CommandItem
	{
		private string mvarCommandID = String.Empty;
		public string CommandID { get { return mvarCommandID; } set { mvarCommandID = value; } }
		
		public CommandReferenceCommandItem(string commandID)
		{
			mvarCommandID = commandID;
		}
	}
	public class CommandPlaceholderCommandItem : CommandItem
	{
		private string mvarPlaceholderID = String.Empty;
		public string PlaceholderID { get { return mvarPlaceholderID; } set { mvarPlaceholderID = value; } }

		public CommandPlaceholderCommandItem(string placeholderID)
		{
			mvarPlaceholderID = placeholderID;
		}
	}
	public class SeparatorCommandItem : CommandItem
	{
	}
	public class GroupCommandItem : CommandItem
	{
		public CommandItemCollection Items { get; } = new CommandItemCollection();
	}
}

