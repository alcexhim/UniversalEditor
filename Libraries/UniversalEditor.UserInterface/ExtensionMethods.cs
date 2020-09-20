//
//  ExtensionMethods.cs
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
using System.Text;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Dialogs;

namespace UniversalEditor.UserInterface
{
	public static class ExtensionMethods
	{
		public static void AddFileNameFilterFromAssociations(this FileDialog dialog, string title, Association association)
		{
			AddFileNameFilterFromAssociations(dialog, title, new Association[] { association });
		}
		public static void AddFileNameFilterFromAssociations(this FileDialog dialog, string title, Association[] associations)
		{
			StringBuilder sb = new StringBuilder();
			foreach (Association assoc in associations)
			{
				for (int i = 0; i < assoc.Filters.Count; i++)
				{
					for (int j = 0; j < assoc.Filters[i].FileNameFilters.Count; j++)
					{
						sb.Append(assoc.Filters[i].FileNameFilters[j]);
						if (j < assoc.Filters[i].FileNameFilters.Count - 1)
							sb.Append("; ");
					}

					if (i < assoc.Filters.Count - 1)
						sb.Append("; ");
				}
			}
			dialog.FileNameFilters.Add(title, sb.ToString());
		}

		private static void AddCustomOptionToSettingsGroup(CustomSettingsProvider csp, CustomOption eo, SettingsGroup sg, string[] path = null)
		{
			if (eo is CustomOptionChoice)
			{
				CustomOptionChoice option = (eo as CustomOptionChoice);

				List<ChoiceSetting.ChoiceSettingValue> choices = new List<ChoiceSetting.ChoiceSettingValue>();
				foreach (CustomOptionFieldChoice choice in option.Choices)
				{
					choices.Add(new ChoiceSetting.ChoiceSettingValue(choice.Title, choice.Title, choice.Value));
				}
				sg.Settings.Add(new ChoiceSetting(option.PropertyName, option.Title, option.Value == null ? null : new ChoiceSetting.ChoiceSettingValue(option.Value.Title, option.Value.Title, option.Value.Value), choices.ToArray()));
			}
			else if (eo is CustomOptionNumber)
			{
				CustomOptionNumber option = (eo as CustomOptionNumber);
				sg.Settings.Add(new RangeSetting(option.PropertyName, option.Title, (decimal)option.GetValue(), option.MinimumValue, option.MaximumValue));
			}
			else if (eo is CustomOptionText)
			{
				CustomOptionText option = (eo as CustomOptionText);
				sg.Settings.Add(new TextSetting(option.PropertyName, option.Title, (string)option.GetValue()));
			}
			else if (eo is CustomOptionBoolean)
			{
				CustomOptionBoolean option = (eo as CustomOptionBoolean);
				sg.Settings.Add(new BooleanSetting(option.PropertyName, option.Title, (bool)option.GetValue()));
			}
			else if (eo is CustomOptionFile)
			{
				CustomOptionFile option = (eo as CustomOptionFile);
				// sg.Settings.Add(new FileSetting(option.Title, option.DefaultValue));
				sg.Settings.Add(new FileSetting(option.PropertyName, option.Title, (string)option.GetValue()));
			}
			else if (eo is CustomOptionGroup)
			{
				CustomOptionGroup cogrp = (eo as CustomOptionGroup);
				SettingsGroup sg1 = new SettingsGroup();
				if (path == null)
				{
					path = new string[] { cogrp.Title };
				}
				else
				{
					string[] path2 = new string[path.Length + 1];
					Array.Copy(path, 0, path2, 0, path.Length);
					path2[path2.Length - 1] = cogrp.Title;
					path = path2;
				}
				sg1.Path = path;
				for (int j = 0; j < cogrp.Options.Count; j++)
				{
					AddCustomOptionToSettingsGroup(csp, cogrp.Options[j], sg1, path);
				}
				csp.SettingsGroups.Add(sg1);
			}
			sg.Settings[sg.Settings.Count - 1].Description = eo.Description;
		}

		public static void AddCustomOptions(this SettingsGroup sg, IEnumerable<CustomOption> options, CustomSettingsProvider csp)
		{
			foreach (CustomOption eo in options)
			{
				// do not render the CustomOption if it's supposed to be invisible
				if (!eo.Visible) continue;

				AddCustomOptionToSettingsGroup(csp, eo, sg);
			}
		}
	}
}
