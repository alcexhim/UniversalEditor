//
//  SettingsParser.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using System.Diagnostics.Contracts;
using System.Linq;
using MBS.Framework;
using MBS.Framework.Settings;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.UEPackage
{
	/// <summary>
	/// Provides utility functions for parsing <see cref="Setting" /> objects from
	/// XML definitions.
	/// </summary>
	public class SettingsParser
	{
		/// <summary>
		/// Converts an XML settings path definition to a <see cref="String" />
		/// array.
		/// </summary>
		/// <returns>The <see cref="String" /> array containing the path information.</returns>
		/// <param name="tag">The <see cref="MarkupTagElement" /> containing the XML to parse.</param>
		public static string[] ParsePath(MarkupTagElement tag)
		{
			if (tag == null) return null;
			if (tag.FullName != "Path") return null;

			List<string> path = new List<string>();
			foreach (MarkupElement el in tag.Elements)
			{
				MarkupTagElement tag2 = (el as MarkupTagElement);
				if (tag2 == null) continue;
				if (tag2.FullName != "Part") continue;

				path.Add(tag2.Value);
			}
			return path.ToArray();
		}

		/// <summary>
		/// Converts an XML settings definition into a <see cref="Setting" />
		/// object of the appropriate type.
		/// </summary>
		/// <returns>The parsed setting.</returns>
		/// <param name="tag">The <see cref="MarkupTagElement" /> containing the XML to parse.</param>
		public static Setting ParseSetting(MarkupTagElement tag)
		{
			if (tag == null) return null;

			MarkupAttribute attSettingID = tag.Attributes["ID"];
			MarkupAttribute attSettingName = tag.Attributes["Name"];
			MarkupAttribute attSettingTitle = tag.Attributes["Title"];
			MarkupAttribute attSettingDescription = tag.Attributes["Description"];

			MarkupAttribute attDefaultValue = tag.Attributes["DefaultValue"];

			Setting s = null;
			switch (tag.FullName)
			{
				case SettingsXMLSchema.BooleanSetting:
				{
					s = new BooleanSetting(attSettingName?.Value, attSettingTitle?.Value);
					if (attDefaultValue != null)
						s.DefaultValue = bool.Parse(attDefaultValue.Value);
					break;
				}
				case SettingsXMLSchema.CustomSetting:
				{
					MarkupAttribute attSettingTypeName = tag.Attributes["TypeName"];
					s = new CustomSetting(attSettingName?.Value, attSettingTitle?.Value, attSettingTypeName?.Value);
					break;
				}
				case SettingsXMLSchema.TextSetting:
				{
					s = new TextSetting(attSettingName?.Value, attSettingTitle?.Value);
					if (attDefaultValue != null)
						s.DefaultValue = attDefaultValue.Value;
					break;
				}
				case SettingsXMLSchema.FileSetting:
				{
					s = new FileSetting(attSettingName?.Value, attSettingTitle?.Value);

					MarkupAttribute attType = tag.Attributes["Type"];
					if (attType != null)
					{
						if (attType.Value.ToLower() == "folder")
						{
							((FileSetting)s).Mode = FileSettingMode.SelectFolder;
						}
						else
						{
						}
					}

					((FileSetting)s).FileNameFilter = tag.Attributes["FileNameFilter"]?.Value;
					if (attDefaultValue != null)
						s.DefaultValue = attDefaultValue.Value;
					break;
				}
				case SettingsXMLSchema.CommandSetting:
				{
					MarkupAttribute attCommandID = tag.Attributes["CommandID"];
					MarkupAttribute attStylePreset = tag.Attributes["StylePreset"];
					s = new CommandSetting(attSettingName?.Value, attSettingTitle?.Value, attCommandID?.Value);
					if (attStylePreset != null)
					{
						((CommandSetting)s).StylePreset = (CommandStylePreset)Enum.Parse(typeof(CommandStylePreset), attStylePreset.Value);
					}
					break;
				}
				case SettingsXMLSchema.RangeSetting:
				{
					s = new RangeSetting(attSettingName?.Value, attSettingTitle?.Value);
					MarkupAttribute attMinimumValue = tag.Attributes["MinimumValue"];
					if (attMinimumValue != null)
						((RangeSetting)s).MinimumValue = decimal.Parse(attMinimumValue.Value);

					MarkupAttribute attMaximumValue = tag.Attributes["MaximumValue"];
					if (attMaximumValue != null)
						((RangeSetting)s).MaximumValue = decimal.Parse(attMaximumValue.Value);

					if (attDefaultValue != null)
						((RangeSetting)s).DefaultValue = decimal.Parse(attDefaultValue.Value);
					break;
				}
				case SettingsXMLSchema.ChoiceSetting:
				{
					s = new ChoiceSetting(attSettingName?.Value, attSettingTitle?.Value);
					MarkupTagElement tagChoices = tag.Elements["Choices"] as MarkupTagElement;
					if (tagChoices != null)
					{
						foreach (MarkupTagElement tagChoice in tagChoices.Elements.OfType<MarkupTagElement>())
						{
							ChoiceSetting.ChoiceSettingValue value = ParseChoiceValue(tagChoice);
							((ChoiceSetting)s).ValidValues.Add(value);
						}
					}
					if (attDefaultValue != null)
						((ChoiceSetting)s).DefaultValue = decimal.Parse(attDefaultValue.Value);
					break;
				}
				case SettingsXMLSchema.GroupSetting:
				{
					s = new GroupSetting(attSettingName?.Value, attSettingTitle?.Value);
					MarkupTagElement tagSettings = tag.Elements["Settings"] as MarkupTagElement;
					if (tagSettings != null)
					{
						foreach (MarkupElement el in tagSettings.Elements)
						{
							Setting s2 = ParseSetting(el as MarkupTagElement);
							if (s2 != null)
							{
								(s as GroupSetting).Options.Add(s2);
							}
						}
					}
					MarkupTagElement tagHeaderSettings = tag.Elements["HeaderSettings"] as MarkupTagElement;
					if (tagHeaderSettings != null)
					{
						foreach (MarkupElement el in tagHeaderSettings.Elements)
						{
							Setting s2 = ParseSetting(el as MarkupTagElement);
							if (s2 != null)
							{
								(s as GroupSetting).HeaderSettings.Add(s2);
							}
						}
					}
					break;
				}
			}

			if (s != null)
			{
				s.ID = new Guid(attSettingID.Value);
				if (attSettingDescription != null)
					s.Description = attSettingDescription.Value;
			}
			return s;
		}

		private static ChoiceSetting.ChoiceSettingValue ParseChoiceValue(MarkupTagElement tag)
		{
			Contract.Assert(tag != null, String.Format("{0} must not be null", nameof(tag)));
			Contract.Assert(tag.FullName == "Choice", String.Format("{0} must be a 'Choice' tag", nameof(tag)));

			MarkupAttribute attID = tag.Attributes["ID"];
			Contract.Assert(attID?.Value != null, String.Format("{0} must be non-null and have a Value", nameof(attID)));

			Guid id = new Guid(attID.Value);
			MarkupAttribute attName = tag.Attributes["Name"];
			MarkupAttribute attTitle = tag.Attributes["Title"];
			MarkupAttribute attValue = tag.Attributes["Value"];
			return new ChoiceSetting.ChoiceSettingValue(attName?.Value, attTitle?.Value, attValue?.Value) { ID = id };
		}

		/// <summary>
		/// Loads a settings provider from the given XML tag definition into a caller-initialized
		/// <see cref="SettingsProvider" />. The caller of this method MUST initialize the value passed
		/// into the <paramref name="sp" /> parameter.
		/// </summary>
		/// <param name="tag">The <see cref="MarkupTagElement" /> containing the settings provider definition.</param>
		/// <param name="sp">The <see cref="SettingsProvider" /> to initialize.</param>
		public static void ParseSettingsProvider(MarkupTagElement tag, ref SettingsProvider sp)
		{
			if (sp == null)
				throw new ArgumentNullException(nameof(sp));

			if (tag == null)
			{
				sp = null;
				return;
			}
			if (tag.FullName != "SettingsProvider" && tag.FullName != "Settings")
			{
				sp = null;
				return;
			}

			if (tag.FullName == "SettingsProvider")
			{
				// expect an ID for settings provider; no ID for just "settings"
				MarkupAttribute attID = tag.Attributes["ID"];
				if (attID == null)
				{
					sp = null;
					return;
				}

				sp.ID = new Guid(attID.Value);
			}

			foreach (MarkupElement el in tag.Elements)
			{
				MarkupTagElement tag2 = (el as MarkupTagElement);
				if (tag2 == null) continue;
				if (tag2.FullName == "SettingsGroup")
				{
					Guid id2 = new Guid(tag2.Attributes["ID"].Value);

					if (sp.SettingsGroups.Contains(id2))
					{
						continue;
					}

					SettingsGroup sg = new SettingsGroup();
					sg.ID = id2;
					sg.Path = SettingsParser.ParsePath(tag2.Elements["Path"] as MarkupTagElement);

					MarkupTagElement tagSettings = (tag2.Elements["Settings"] as MarkupTagElement);
					if (tagSettings != null)
					{
						foreach (MarkupElement el2 in tagSettings.Elements)
						{
							Setting s = SettingsParser.ParseSetting(el2 as MarkupTagElement);
							if (s != null)
							{
								if (sg.Settings.Contains(s.ID))
									continue;

								sg.Settings.Add(s);
							}
						}
					}
					sp.SettingsGroups.Add(sg);
				}
			}
		}
	}
}
