//
//  OptionPanel.cs
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

namespace MBS.Framework.UserInterface
{
	public class SettingsGroup : IComparable<SettingsGroup>
	{
		public class SettingsGroupCollection
			: System.Collections.ObjectModel.Collection<SettingsGroup>
		{
			public SettingsGroup Add(string path, Setting[] options)
			{
				string[] paths = new string[0];
				if (!String.IsNullOrEmpty (path)) {
					paths = path.Split (new char[] { ':' });
				}
				return Add (paths, options);
			}
			public SettingsGroup Add(string[] path, Setting[] options)
			{
				SettingsGroup grp = new SettingsGroup();
				grp.Path = path;
				if (options != null) {
					foreach (Setting option in options) {
						grp.Settings.Add (option);
					}
				}
				Add (grp);
				return grp;
			}
		}

		public SettingsGroup()
		{
		}
		public SettingsGroup(string path, Setting[] options)
		{
			string[] paths = new string[0];
			if (!String.IsNullOrEmpty (path)) {
				paths = path.Split (new char[] { ':' });
			}
			Path = paths;
			foreach (Setting option in options)
			{
				Settings.Add (option);
			}
		}
		public SettingsGroup(string[] paths, Setting[] options)
		{
			Path = paths;
			foreach (Setting option in options)
			{
				Settings.Add (option);
			}
		}

		public int CompareTo(SettingsGroup other)
		{
			string xpath = String.Join (":", this.GetPath ());
			string ypath = String.Join (":", other.GetPath ());
			return xpath.CompareTo (ypath);
		}

		public string[] GetPath()
		{
			if (Path == null) return new string[0];
			return Path;
		}

		public string[] Path { get; set; } = null;
		public string Title
		{
			get
			{
				if (Path.Length > 0) return Path[Path.Length - 1];
				return null;
			}
		}
		public Setting.SettingCollection Settings { get; } = new Setting.SettingCollection();

		public override string ToString ()
		{
			return String.Join (":", Path);
		}
	}
}

