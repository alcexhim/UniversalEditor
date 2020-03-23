//
//  CommandLineOption.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface
{
	public class CommandLineOption
	{
		public class CommandLineOptionCollection
			: System.Collections.ObjectModel.Collection<CommandLineOption>
		{
			private Dictionary<char, CommandLineOption> _byAbbreviation = new Dictionary<char, CommandLineOption>();
			private Dictionary<string, CommandLineOption> _byName = new Dictionary<string, CommandLineOption>();

			public bool Contains(char abbreviation)
			{
				return _byAbbreviation.ContainsKey(abbreviation);
			}
			public bool Contains(string name)
			{
				return _byName.ContainsKey(name);
			}

			public CommandLineOption this[char abbreviation]
			{
				get
				{
					if (_byAbbreviation.ContainsKey(abbreviation))
						return _byAbbreviation[abbreviation];
					return null;
				}
			}
			public CommandLineOption this[string name]
			{
				get
				{
					if (_byName.ContainsKey(name))
						return _byName[name];
					return null;
				}
			}

			protected override void ClearItems()
			{
				base.ClearItems();

				_byName.Clear();
				_byAbbreviation.Clear();
			}
			protected override void InsertItem(int index, CommandLineOption item)
			{
				base.InsertItem(index, item);

				if (item.Name != null)
					_byName[item.Name] = item;

				if (item.Abbreviation != '\0')
					_byAbbreviation[item.Abbreviation] = item;
			}

			public CommandLineOption Add(string name, char abbreviation = '\0', object defaultValue = null, CommandLineOptionValueType type = CommandLineOptionValueType.None, string description = null)
			{
				CommandLineOption option = new CommandLineOption();
				option.Name = name;
				option.Abbreviation = abbreviation;
				option.DefaultValue = defaultValue;
				option.Type = type;
				option.Description = description;

				Add(option);
				return option;
			}

			protected override void RemoveItem(int index)
			{
				_byName.Remove(this[index].Name);
				_byAbbreviation.Remove(this[index].Abbreviation);

				base.RemoveItem(index);
			}

			public T GetValueOrDefault<T>(string name, T defaultValue = default(T))
			{
				if (_byName.ContainsKey(name))
				{
					return (T) _byName[name].Value;
				}
				return defaultValue;
			}
		}

		public string Name { get; set; } = null;
		public char Abbreviation { get; set; } = '\0';

		public object DefaultValue { get; set; } = null;

		/// <summary>
		/// The description displayed in the help text.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; } = null;
		public object Value { get; set; } = null;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:MBS.Framework.UserInterface.CommandLineOption"/> expects a value to be passed in after this option.
		/// </summary>
		/// <value><c>true</c> if a value is expected to be passed in after this option; otherwise, <c>false</c>.</value>
		public CommandLineOptionValueType Type { get; set; } = CommandLineOptionValueType.None;
	}
}
