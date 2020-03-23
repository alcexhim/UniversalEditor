using System;
using System.Collections.Generic;

namespace MBS.Framework.Logic
{
	public class Variable
	{
		public class VariableCollection
			: System.Collections.ObjectModel.Collection<Variable>
		{
			private Dictionary<string, Variable> _itemsByName = new Dictionary<string, Variable>();
			public Variable this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name))
						return _itemsByName[name];
					return null;
				}
			}

			protected override void ClearItems()
			{
				base.ClearItems();
				_itemsByName.Clear();
			}
			protected override void InsertItem(int index, Variable item)
			{
				base.InsertItem(index, item);
				_itemsByName[item.Name] = item;
			}
			protected override void RemoveItem(int index)
			{
				_itemsByName.Remove(this[index].Name);
				base.RemoveItem(index);
			}
		}

		public string Name { get; set; } = null;
		public Expression Expression { get; set; } = null;

		public Variable(string name, Expression expression)
		{
			Name = name;
			Expression = expression;
		}
	}
}
