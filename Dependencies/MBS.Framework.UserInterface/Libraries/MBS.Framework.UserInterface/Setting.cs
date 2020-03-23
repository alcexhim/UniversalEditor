//
//  Option.cs
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
	public class RangeSetting : Setting
	{
		public double MinimumValue { get; set; } = 0.0;
		public double MaximumValue { get; set; } = 0.0;

		public RangeSetting(string title, double defaultValue = 0.0, double minimumValue = 0.0, double maximumValue = 0.0) : base(title, defaultValue) {
			MinimumValue = minimumValue;
			MaximumValue = maximumValue;
		}
	}
	public class GroupSetting : Setting
	{
		public Setting.SettingCollection Options { get; } = new Setting.SettingCollection();

		public GroupSetting(string title, Setting[] options = null)
		{
			Title = title;
			if (options != null) {
				foreach (Setting option in options) {
					Options.Add (option);
				}
			}
		}
	}
	public class BooleanSetting : Setting
	{
		public BooleanSetting(string title, bool defaultValue = false) : base(title, defaultValue)
		{
		}

		public override void SetValue (object value)
		{
			bool val = false;
			if (value != null) {
				val = (value.ToString ().ToLower ().Equals ("true"));
			}
			base.SetValue (val);
		}
	}
	public class ChoiceSetting : Setting
	{
		public ChoiceSetting(string title, ChoiceSettingValue defaultValue = null, ChoiceSettingValue[] values = null) : base (title, null)
		{
			if (values == null) {
				values = new ChoiceSettingValue[0];
			}
			if (defaultValue != null) {
				if (defaultValue.Value != null) {
					base.DefaultValue = defaultValue.Value.ToString ();
				} else {
					base.DefaultValue = null;
				}
			}
			
			foreach (ChoiceSettingValue value in values) {
				ValidValues.Add (value);
			}
		}

		public class ChoiceSettingValue
		{
			public class ChoiceSettingValueCollection
				: System.Collections.ObjectModel.Collection<ChoiceSettingValue>
			{
			}

			public string Title { get; set; } = String.Empty;
			public object Value { get; set; } = null;

			public ChoiceSettingValue(string title, object value) 
			{
				Title = title;
				Value = value;
			}
		}

		public ChoiceSettingValue.ChoiceSettingValueCollection ValidValues { get; } = new ChoiceSettingValue.ChoiceSettingValueCollection();
		public ChoiceSettingValue SelectedValue { get; set; } = null;

		public bool RequireSelectionFromList { get; set; } = true;
	}
	public class TextSetting : Setting
	{
		public TextSetting(string title, string defaultValue = "") : base(title, defaultValue)
		{
		}
	}
	public abstract class Setting
	{
		public Setting(string title, object defaultValue = null) {
			Title = title;
			DefaultValue = defaultValue;
			mvarValue = defaultValue;
		}
		public string Title { get; set; } = String.Empty;

		public class SettingCollection
			: System.Collections.ObjectModel.Collection<Setting>
		{

			public Setting this[string name]
			{
				get
				{
					foreach (Setting item in this) {
						if (item.Title.Replace("_", String.Empty).Replace (' ', '_').Equals (name)) { 
							return item;
						}
					}
					return null;
				}
			}
		}

		protected Setting()
		{
		}

		public object DefaultValue { get; set; } = null;

		private object mvarValue = null;

		public virtual object GetValue() {
			return mvarValue;
		}
		public virtual void SetValue(object value) {
			mvarValue = value;
		}
		public T GetValue<T>(T defaultValue = default(T)) {
			try {
				return (T)GetValue();
			}
			catch {
				return defaultValue;
			}
		}
		public void SetValue<T>(T value) {
			mvarValue = value;
		}
	}
}

