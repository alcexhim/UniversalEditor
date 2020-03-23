//
//  DefaultOptionProvider.cs
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
	public class DefaultSettingsProvider : ApplicationSettingsProvider
	{
		protected override void InitializeInternal()
		{
			base.InitializeInternal();

			SettingsGroups[1].Settings.Clear();
			for (int i = 0; i < Application.Features.Count; i++)
			{
				Feature feature = Application.Features[i];

				Plugin[] availablePluginsForFeature = Plugin.Get(new Feature[] { feature });
				List<ChoiceSetting.ChoiceSettingValue> listValues = new List<ChoiceSetting.ChoiceSettingValue>();
				for (int j = 0; j < availablePluginsForFeature.Length; j++)
				{
					listValues.Add(new ChoiceSetting.ChoiceSettingValue(availablePluginsForFeature[j].Title, availablePluginsForFeature[j]));
				}
				SettingsGroups[1].Settings.Add(new ChoiceSetting(feature.Title, null, listValues.ToArray()));
			}
		}
		public DefaultSettingsProvider()
		{
			SettingsGroups.Add("Application:Language", new Setting[]
			{
			});
			SettingsGroups.Add("Application:Features", new Setting[]
			{
			});
			SettingsGroups.Add("Application:Keyboard", new Setting[]
			{
			});
			SettingsGroups.Add("Application:Theme", new Setting[]
			{
			});
			SettingsGroups.Add("Plugins:Security", new Setting[]
			{
			});
		}
	}
}

