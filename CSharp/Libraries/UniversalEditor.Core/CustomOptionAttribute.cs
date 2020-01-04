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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class CustomOptionBooleanAttribute : CustomOptionAttribute
	{
		public CustomOptionBooleanAttribute(string title, bool defaultValue = false, bool enabled = true, bool visible = true)
			: base(title, enabled, visible)
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

	/// <summary>
	/// Defines how a custom option will appear on the options dialog
	/// when a file is saved or exported using a compatible
	/// implementation of UniversalEditor.UserInterface.
	/// </summary>
	public abstract class CustomOptionAttribute : Attribute
	{
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }

		public CustomOptionAttribute(string title, bool enabled = true, bool visible = true)
		{
			mvarTitle = title;
			mvarEnabled = enabled;
			mvarVisible = visible;
		}

		public abstract object GetValue();
	}

	public class CustomOptionTextAttribute : CustomOptionAttribute
	{
		/// <summary>
		/// Creates a custom option as a text box.
		/// </summary>
		/// <param name="title">The title of the export option.</param>
		public CustomOptionTextAttribute(string title, string defaultValue = "", bool enabled = true, bool visible = true)
			: base(title, enabled, visible)
		{
			mvarDefaultValue = defaultValue;
		}
		/// <summary>
		/// Creates a custom option as a text box.
		/// </summary>
		/// <param name="title">The title of the export option.</param>
		public CustomOptionTextAttribute(string title, string defaultValue, int maximumLength, bool enabled = true, bool visible = true)
			: base(title, enabled, visible)
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
	public class CustomOptionChoiceAttribute : CustomOptionAttribute
	{
		/// <summary>
		/// Creates a custom option as a drop-down list with the specified options.
		/// </summary>
		/// <param name="title">The title of the export option.</param>
		/// <param name="requireChoice"></param>
		/// <param name="choices"></param>
		public CustomOptionChoiceAttribute(string title, bool requireChoice = false, params CustomOptionFieldChoice[] choices)
			: base(title)
		{
			mvarIsRadioButton = false;
			mvarRequireChoice = requireChoice;
			foreach (CustomOptionFieldChoice choice in choices)
			{
				mvarChoices.Add(choice);
			}
		}
		public CustomOptionChoiceAttribute(string title, bool requireChoice = false, bool enabled = true, bool visible = true, params CustomOptionFieldChoice[] choices)
			: base(title, enabled, visible)
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
		public CustomOptionChoice(string title, params CustomOptionFieldChoice[] choices)
			: base(title)
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

	public class CustomOptionNumberAttribute : CustomOptionAttribute
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

		public CustomOptionNumberAttribute(string title, decimal defaultValue = 0, decimal? minimumValue = null, decimal? maximumValue = null, bool enabled = true, bool visible = true)
			: base(title, enabled, visible)
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
	public class CustomOptionMultipleChoiceAttribute : CustomOptionAttribute
	{
		public CustomOptionMultipleChoiceAttribute(string title, params CustomOptionFieldChoice[] choices)
			: base(title)
		{

		}
		public CustomOptionMultipleChoiceAttribute(string title, bool enabled = true, bool visible = true, params CustomOptionFieldChoice[] choices)
			: base(title, enabled, visible)
		{

		}

		public override object GetValue()
		{
			return null;
		}
	}

	public class CustomOptionFileAttribute : CustomOptionAttribute
	{
		private string mvarDefaultValue = String.Empty;
		public string DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private string mvarFilter = String.Empty;
		public string Filter { get { return mvarFilter; } set { mvarFilter = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public CustomOptionFileAttribute(string title, string defaultValue = "", string filter = "All Files|*.*", bool enabled = true, bool visible = true)
			: base(title, enabled, visible)
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
	public class CustomOptionVersionAttribute : CustomOptionAttribute
	{
		private Version mvarValue = null;
		public Version Value { get { return mvarValue; } set { mvarValue = value; } }

		public CustomOptionVersionAttribute(string title, Version value = null, bool enabled = true, bool visible = true)
			: base(title, enabled, visible)
		{
			mvarValue = value;
		}

		public override object GetValue()
		{
			return mvarValue;
		}
	}
}
