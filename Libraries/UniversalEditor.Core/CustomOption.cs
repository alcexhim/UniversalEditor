//
//  CustomOption.cs - UI-agnostic definition for options when loading/saving DFs, etc.
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class CustomOptionBoolean : CustomOption
	{
		public CustomOptionBoolean(string propertyName, string title, bool defaultValue = false, bool enabled = true, bool visible = true)
			: base(propertyName, title, enabled, visible)
		{
			mvarDefaultValue = defaultValue;
			mvarValue = defaultValue;
		}

		private bool mvarDefaultValue = false;
		public bool DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private bool mvarValue = false;
		public bool Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object GetValue()
		{
			return mvarValue;
		}
	}
	public class CustomOptionGroup : CustomOption
	{
		public class CustomOptionGroupCollection
			: System.Collections.ObjectModel.Collection<CustomOptionGroup>
		{
		}

		public CustomOptionGroup(string propertyName, string title = null, bool enabled = true, bool visible = true)
			: base(propertyName, title, enabled, visible)
		{
			if (title == null) base.Title = propertyName;
		}

		private CustomOption.CustomOptionCollection mvarOptions = new CustomOption.CustomOptionCollection();
		public CustomOption.CustomOptionCollection Options { get { return mvarOptions; } }

		public override object GetValue()
		{
			return null;
		}
	}

	public class CustomOptionFieldChoice
	{
		public class CustomOptionFieldChoiceCollection
			: System.Collections.ObjectModel.Collection<CustomOptionFieldChoice>
		{
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		private bool mvarIsDefault = false;
		public bool IsDefault { get { return mvarIsDefault; } set { mvarIsDefault = value; } }

		public CustomOptionFieldChoice(string title) : this(title, title, false)
		{
		}
		public CustomOptionFieldChoice(object value)
		{
			if (value != null)
			{
				mvarTitle = value.ToString();
			}
			mvarValue = value;
		}
		public CustomOptionFieldChoice(object value, bool isDefault)
		{
			if (value != null)
			{
				mvarTitle = value.ToString();
			}
			mvarValue = value;
			mvarIsDefault = isDefault;
		}
		public CustomOptionFieldChoice(string title, object value) : this(title, value, false)
		{
		}
		public CustomOptionFieldChoice(string title, object value, bool isDefault)
		{
			mvarTitle = title;
			mvarValue = value;
			mvarIsDefault = isDefault;
		}

		public override string ToString()
		{
			return mvarTitle;
		}
	}

	/// <summary>
	/// Defines how a custom option will appear on the options dialog
	/// when a file is saved or exported using a compatible
	/// implementation of UniversalEditor.UserInterface.
	/// </summary>
	public abstract class CustomOption
	{
		public class CustomOptionCollection
			: System.Collections.ObjectModel.Collection<CustomOption>
		{

			public CustomOption this[string propertyName]
			{
				get
				{
					foreach (CustomOption co in this)
					{
						if (co.PropertyName == propertyName) return co;
					}
					return null;
				}
			}

		}

		private string mvarPropertyName = String.Empty;
		public string PropertyName { get { return mvarPropertyName; } set { mvarPropertyName = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }

		public CustomOption(string propertyName, string title, bool enabled = true, bool visible = true)
		{
			mvarPropertyName = propertyName;
			mvarTitle = title;
			mvarEnabled = enabled;
			mvarVisible = visible;
		}

		public abstract object GetValue();
	}

	public class CustomOptionText : CustomOption
	{
		/// <summary>
		/// Creates a custom option as a text box.
		/// </summary>
		/// <param name="title">The title of the export option.</param>
		public CustomOptionText(string propertyName, string title, string defaultValue = "", int? maximumLength = null, bool enabled = true, bool visible = true)
			: base(propertyName, title, enabled, visible)
		{
			mvarDefaultValue = defaultValue;
			mvarMaximumLength = maximumLength;
		}

		private int? mvarMaximumLength = null;
		public int? MaximumLength { get { return mvarMaximumLength; } set { mvarMaximumLength = value; } }

		private string mvarDefaultValue = String.Empty;
		public string DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object GetValue()
		{
			return mvarValue;
		}
	}
	public class CustomOptionChoice : CustomOption
	{
		/// <summary>
		/// Creates a custom option as a drop-down list with the specified options.
		/// </summary>
		/// <param name="title">The title of the export option.</param>
		/// <param name="requireChoice"></param>
		/// <param name="choices"></param>
		public CustomOptionChoice(string propertyName, string title, bool requireChoice = false, params CustomOptionFieldChoice[] choices)
			: base(propertyName, title)
		{
			mvarIsRadioButton = false;
			mvarRequireChoice = requireChoice;
			foreach (CustomOptionFieldChoice choice in choices)
			{
				mvarChoices.Add(choice);
			}
		}
		public CustomOptionChoice(string propertyName, string title, bool requireChoice = false, bool enabled = true, bool visible = true, params CustomOptionFieldChoice[] choices)
			: base(propertyName, title, enabled, visible)
		{
			mvarIsRadioButton = false;
			mvarRequireChoice = requireChoice;
			foreach (CustomOptionFieldChoice choice in choices)
			{
				mvarChoices.Add(choice);
			}
		}

		/*
		/// <summary>
		/// Creates a custom option as a radio button list with the specified options.
		/// </summary>
		/// <param name="title">The title of the export option.</param>
		/// <param name="requireChoice"></param>
		/// <param name="choices"></param>
		public CustomOptionChoice(string propertyName, string title, params CustomOptionFieldChoice[] choices)
			: base(propertyName, title)
		{
			mvarIsRadioButton = true;
			foreach (CustomOptionFieldChoice choice in choices)
			{
				mvarChoices.Add(choice);
			}
		}
		*/

		private bool mvarIsRadioButton = false;
		public bool IsRadioButton { get { return mvarIsRadioButton; } set { mvarIsRadioButton = value; } }

		private bool mvarRequireChoice = false;
		public bool RequireChoice { get { return mvarRequireChoice; } set { mvarRequireChoice = value; } }

		private CustomOptionFieldChoice.CustomOptionFieldChoiceCollection mvarChoices = new CustomOptionFieldChoice.CustomOptionFieldChoiceCollection();
		public CustomOptionFieldChoice.CustomOptionFieldChoiceCollection Choices { get { return mvarChoices; } }

		private CustomOptionFieldChoice mvarDefaultValue = null;
		public CustomOptionFieldChoice DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private CustomOptionFieldChoice mvarValue = null;
		public CustomOptionFieldChoice Value { get { return mvarValue; } set { mvarValue = value; } }

		public override object GetValue()
		{
			if (mvarValue == null) return null;
			return mvarValue.Value;
		}
	}

	public class DataFormatOptionNumberSuggestedValue
	{
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private decimal mvarValue = 0;
		public decimal Value { get { return mvarValue; } set { mvarValue = value; } }

		public class DataFormatOptionNumberSuggestedValueCollection
			: System.Collections.ObjectModel.Collection<DataFormatOptionNumberSuggestedValue>
		{
			public DataFormatOptionNumberSuggestedValue Add(decimal value, string title = null)
			{
				if (title == null) title = value.ToString();
				
				DataFormatOptionNumberSuggestedValue item = new DataFormatOptionNumberSuggestedValue();
				item.Title = title;
				item.Value = value;
				Add(item);
				return item;
			}
		}
	}

	public class CustomOptionNumber : CustomOption
	{
		private DataFormatOptionNumberSuggestedValue.DataFormatOptionNumberSuggestedValueCollection mvarSuggestedValues = new DataFormatOptionNumberSuggestedValue.DataFormatOptionNumberSuggestedValueCollection();
		public DataFormatOptionNumberSuggestedValue.DataFormatOptionNumberSuggestedValueCollection SuggestedValues { get { return mvarSuggestedValues; } }

		private decimal? mvarMinimumValue = null;
		public decimal? MinimumValue { get { return mvarMinimumValue; } set { mvarMinimumValue = value; } }

		private decimal? mvarMaximumValue = null;
		public decimal? MaximumValue { get { return mvarMaximumValue; } set { mvarMaximumValue = value; } }

		private decimal mvarDefaultValue = 0;
		public decimal DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private decimal mvarValue = 0;
		public decimal Value { get { return mvarValue; } set { mvarValue = value; } }

		public CustomOptionNumber(string propertyName, string title, decimal defaultValue = 0, decimal? minimumValue = null, decimal? maximumValue = null, bool enabled = true, bool visible = true)
			: base(propertyName, title, enabled, visible)
		{
			mvarMinimumValue = minimumValue;
			mvarMaximumValue = maximumValue;
			mvarDefaultValue = defaultValue;
		}

		public override object GetValue()
		{
			return mvarValue;
		}
	}
	public class CustomOptionMultipleChoice : CustomOption
	{
		public CustomOptionMultipleChoice(string propertyName, string title, params CustomOptionFieldChoice[] choices)
			: base(propertyName, title)
		{
			
		}
		public CustomOptionMultipleChoice(string propertyName, string title, bool enabled = true, bool visible = true, params CustomOptionFieldChoice[] choices)
			: base(propertyName, title, enabled, visible)
		{

		}

		public override object GetValue()
		{
			return null;
		}
	}

	public enum CustomOptionFileDialogMode
	{
		Open,
		Save
	}
	public class CustomOptionFile : CustomOption
	{
		private string mvarDefaultValue = String.Empty;
		public string DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private string mvarFilter = String.Empty;
		public string Filter { get { return mvarFilter; } set { mvarFilter = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public CustomOptionFile(string propertyName, string title, string defaultValue = "", string filter = "All Files|*.*", bool enabled = true, bool visible = true)
			: base(propertyName, title, enabled, visible)
		{
			mvarDefaultValue = defaultValue;
			mvarFilter = filter;
		}

		private CustomOptionFileDialogMode mvarDialogMode = CustomOptionFileDialogMode.Open;
		public CustomOptionFileDialogMode DialogMode { get { return mvarDialogMode; } set { mvarDialogMode = value; } }

		public override object GetValue()
		{
			return mvarValue;
		}
	}
	public class CustomOptionVersion : CustomOption
	{
		private Version mvarValue = null;
		public Version Value { get { return mvarValue; } set { mvarValue = value; } }

		public CustomOptionVersion(string propertyName, string title, Version value = null, bool enabled = true, bool visible = true)
			: base(propertyName, title, enabled, visible)
		{
			mvarValue = value;
		}

		public override object GetValue()
		{
			return mvarValue;
		}
	}
}
