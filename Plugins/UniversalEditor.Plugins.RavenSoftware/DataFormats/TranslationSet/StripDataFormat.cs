//
//  StripDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework;
using MBS.Framework.Settings;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.ObjectModels.TranslationSet;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.RavenSoftware.DataFormats.TranslationSet
{
	public class StripDataFormat : UniversalEditor.DataFormats.PropertyList.ExtensibleConfiguration.ExtensibleConfigurationDataFormat
	{
		public StripDataFormat()
		{
			// apply settings for Strip format
			Settings.AllowTopLevelProperties = true;
			Settings.PropertySeparator = "\n";
			Settings.PropertyNameValueSeparator = " ";
		}

		public int Version { get; set; } = 1;
		public string ConfigurationFileName { get; set; } = String.Empty;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Clear();
				_dfr.Capabilities.Add(typeof(TranslationSetObjectModel), DataFormatCapabilities.All);
				_dfr.Title = "Raven Software Strip (String Package) translation set";

				_dfr.ImportOptions.SettingsGroups[0].Settings.Add(new TextSetting("ConfigurationFileName", "Configuration file name", ConfigurationFileName));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting("ConfigurationFileName", "Configuration file name", ConfigurationFileName));
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);

			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			TranslationSetObjectModel strip = (objectModels.Pop() as TranslationSetObjectModel);

			for (int i = 0; i < plom.Items.Count; i++)
			{
				if (plom.Items[i] is Property)
				{
					Property prop = ((Property)plom.Items[i]);
					switch (prop.Name)
					{
						case "VERSION":
						{
							Version = Int32.Parse(prop.Value?.ToString());
							break;
						}
						case "CONFIG":
						{
							ConfigurationFileName = prop.Value?.ToString();
							break;
						}
						case "ID":
						{
							strip.ID = Int32.Parse(prop.Value?.ToString());
							break;
						}
						case "REFERENCE":
						{
							strip.Reference = prop.Value?.ToString();
							break;
						}
						case "DESCRIPTION":
						{
							strip.Description = prop.Value?.ToString();
							break;
						}
						case "COUNT":
						{
							// unused - we just keep reading
							break;
						}
						case "INDEX":
						{
							if (i < plom.Items.Count - 1 && plom.Items[i + 1] is Group)
							{
								Group grp = (Group)plom.Items[i + 1];

								TranslationSetEntry entry = new TranslationSetEntry();

								for (int j = 0; j < grp.Items.Count; j++)
								{
									if (grp.Items[j] is Property)
									{
										Property pj = (Property)grp.Items[j];
										if (pj.Name == "REFERENCE")
										{
											string reff = (string)pj.Value;
											entry.Reference = reff;
										}
										else if (pj.Name.StartsWith("TEXT_LANGUAGE"))
										{
											TranslationSetValue value = new TranslationSetValue();

											value.LanguageIndex = Int32.Parse(pj.Name.Substring("TEXT_LANGUAGE".Length));
											value.Value = (string)pj.Value;

											entry.Values.Add(value);
										}
									}
								}

								strip.Entries.Add(entry);
							}
							else
							{
								((EditorApplication)Application.Instance).Messages.Add(HostApplicationMessageSeverity.Error, "bad .sp file - INDEX not followed by group");
								Console.WriteLine("ue: ravensoft: strip: bad .sp file - INDEX not followed by group");
								break;
							}
							break;
						}
					}
				}
			}



			// always remember to do this in case someone inherits from this file format , for whatever reason
			objectModels.Push(strip);
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			TranslationSetObjectModel strip = (objectModels.Pop() as TranslationSetObjectModel);
			PropertyListObjectModel plom = new PropertyListObjectModel();

			plom.Items.AddProperty("VERSION", "1");
			plom.Items.AddProperty("CONFIG", "W:\\bin\\striped.cfg");
			plom.Items.AddProperty("ID", strip.ID.ToString());
			plom.Items.AddProperty("REFERENCE", strip.Reference);
			plom.Items.AddProperty("DESCRIPTION", strip.Description);
			plom.Items.AddProperty("COUNT", strip.Entries.Count.ToString());

			for (int i = 0; i < strip.Entries.Count; i++)
			{
				TranslationSetEntry entry = strip.Entries[i];
				plom.Items.AddProperty("INDEX", i.ToString());

				Group g = new Group();
				g.Items.AddProperty("REFERENCE", entry.Reference);
				for (int j = 0; j < entry.Values.Count; j++)
				{
					g.Items.AddProperty(String.Format("TEXT_LANGUAGE{0}", entry.Values[j].LanguageIndex), entry.Values[j].Value);
				}
				plom.Items.Add(g);
			}

			objectModels.Push(plom);
		}
	}
}
