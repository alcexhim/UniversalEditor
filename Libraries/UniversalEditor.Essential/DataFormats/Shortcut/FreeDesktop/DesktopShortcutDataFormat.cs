//
//  DesktopShortcutDataFormat.cs - provides a DataFormat for manipulating desktop shortcut files in FreeDesktop XML format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.ObjectModels.Shortcut;

namespace UniversalEditor.DataFormats.Shortcut.FreeDesktop
{
	public class DesktopShortcutDataFormat : WindowsConfigurationDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(ShortcutObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(ApplicationTitle), "_Application title: "));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(GenericTitle), "_Generic title: "));
				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(HideFromMenus), "_Do not display this entry in menus"));
				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(Deleted), "_Mark this shortcut as being deleted by the user"));
				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(DBusActivatable), "_Enable DBus activation"));
				_dfr.Sources.Add("http://standards.freedesktop.org/desktop-entry-spec/desktop-entry-spec-latest.html");
			}
			return _dfr;
		}
		/// <summary>
		/// Specific name of the application, for example "Mozilla". 
		/// </summary>
		public string ApplicationTitle { get; set; } = String.Empty;
		/// <summary>
		/// Generic name of the application, for example "Web Browser". 
		/// </summary>
		public string GenericTitle { get; set; } = String.Empty;
		public DesktopShortcutType Type { get; set; } = DesktopShortcutType.Application;
		/// <summary>
		/// NoDisplay means "this application exists, but don't display it in the menus". This
		/// can be useful to e.g. associate this application with MIME types, so that it gets
		/// launched from a file manager (or other apps), without having a menu entry for it
		/// (there are tons of good reasons for this, including e.g. the netscape -remote, or
		/// kfmclient openURL kind of stuff).
		/// </summary>
		public bool HideFromMenus { get; set; } = false;
		/// <summary>
		/// Hidden should have been called Deleted. It means the user deleted (at his level)
		/// something that was present (at an upper level, e.g. in the system dirs). It's strictly
		/// equivalent to the .desktop file not existing at all, as far as that user is concerned.
		/// This can also be used to "uninstall" existing files (e.g. due to a renaming) - by
		/// letting make install install a file with Hidden=true in it.
		/// </summary>
		public bool Deleted { get; set; } = false;
		/// <summary>
		/// <para>
		/// A list of strings identifying the desktop environments that should display/not
		/// display a given desktop entry.
		/// </para>
		/// <para>
		/// By default, a desktop file should be shown, unless an
		/// OnlyShowIn key is present, in which case, the default is for the file not to be
		/// shown.
		/// </para>
		/// <para>
		/// If $XDG_CURRENT_DIRECTORY is set then it contains a colon-separated list of strings.
		/// In order, each string is considered. If a matching entry is found in OnlyShowIn then
		/// the desktop file is shown. If an entry is found in NotShowIn then the desktop file is
		/// not shown. If none of the strings match then the default action is taken (as above).
		/// </para>
		/// <para>
		/// The same desktop name may not appear in both OnlyShowIn and NotShowIn of a group. 
		/// </para>
		/// </summary>
		public System.Collections.Specialized.StringCollection RestrictedEnvironments { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// <para>
		/// A list of strings identifying the desktop environments that should display/not
		/// display a given desktop entry.
		/// </para>
		/// <para>
		/// By default, a desktop file should be shown, unless an
		/// OnlyShowIn key is present, in which case, the default is for the file not to be
		/// shown.
		/// </para>
		/// <para>
		/// If $XDG_CURRENT_DIRECTORY is set then it contains a colon-separated list of strings.
		/// In order, each string is considered. If a matching entry is found in OnlyShowIn then
		/// the desktop file is shown. If an entry is found in NotShowIn then the desktop file is
		/// not shown. If none of the strings match then the default action is taken (as above).
		/// </para>
		/// <para>
		/// The same desktop name may not appear in both OnlyShowIn and NotShowIn of a group. 
		/// </para>
		/// </summary>
		public System.Collections.Specialized.StringCollection ExcludedEnvironments { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// A boolean value specifying if D-Bus activation is supported for this application. If
		/// this key is missing, the default value is false. If the value is true then
		/// implementations should ignore the Exec key and send a D-Bus message to launch the
		/// application. See D-Bus Activation for more information on how this works.
		/// Applications should still include Exec= lines in their desktop files for
		/// compatibility with implementations that do not understand the DBusActivatable key.
		/// </summary>
		public bool DBusActivatable { get; set; } = false;
		/// <summary>
		/// Path to an executable file on disk used to determine if the program is actually
		/// installed. If the path is not an absolute path, the file is looked up in the $PATH
		/// environment variable. If the file is not present or if it is not executable, the entry
		/// may be ignored (not be used in menus, for example).
		/// </summary>
		public string TryExec { get; set; } = String.Empty;
		/// <summary>
		/// Identifiers for application actions. This can be used to tell the application to make
		/// a specific action, different from the default behavior. The Application actions
		/// section describes how actions work.
		/// </summary>
		public DesktopShortcutAction.DesktopShortcutActionCollection Actions { get; } = new DesktopShortcutAction.DesktopShortcutActionCollection();
		/// <summary>
		/// The MIME type(s) supported by this application.
		/// </summary>
		public System.Collections.Specialized.StringCollection SupportedMimeTypes { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// Categories in which the entry should be shown in a menu (for possible values see the Desktop Menu Specification).
		/// </summary>
		public System.Collections.Specialized.StringCollection Categories { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// A list of strings which may be used in addition to other metadata to describe this
		/// entry. This can be useful e.g. to facilitate searching through entries. The values
		/// are not meant for display, and should not be redundant with the values of Name or
		/// GenericName.
		/// </summary>
		public System.Collections.Specialized.StringCollection Keywords { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// If set to <see cref="DesktopShortcutStartupNotifyBehavior.Supported" />, it is KNOWN
		/// that the application will send a "remove" message when started with the
		/// DESKTOP_STARTUP_ID environment variable set. If set to
		/// <see cref="DesktopShortcutStartupNotifyBehavior.Unsupported" />, it is KNOWN that the
		/// application does not work with startup notification at all (does not shown any window,
		/// breaks even when using StartupWMClass, etc.). If set to
		/// <see cref="DesktopShortcutStartupNotifyBehavior.Disabled" />, a reasonable handling is
		/// up to implementations (assuming false, using StartupWMClass, etc.). (See the Startup
		/// Notification Protocol Specification for more details).
		/// </summary>
		public DesktopShortcutStartupNotifyBehavior StartupNotify { get; set; } = DesktopShortcutStartupNotifyBehavior.Disabled;
		/// <summary>
		/// If specified, it is known that the application will map at least one window with the
		/// given string as its WM class or WM name hint (see the Startup Notification Protocol
		/// Specification for more details).
		/// </summary>
		public string StartupWindowClass { get; set; } = String.Empty;

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			ShortcutObjectModel shortcut = (objectModels.Pop() as ShortcutObjectModel);


		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			ShortcutObjectModel shortcut = (objectModels.Pop() as ShortcutObjectModel);
			PropertyListObjectModel plom = new PropertyListObjectModel();

			#region Desktop Entry
			{
				Group group = new Group("Desktop Entry");

				// Version of the Desktop Entry Specification that the desktop entry conforms
				// with. Entries that confirm with this version of the specification should use
				// 1.0. Note that the version field is not required to be present.
				group.Items.AddProperty("Version", "1.0");

				if (!String.IsNullOrEmpty(ApplicationTitle))
				{
					group.Items.AddProperty("Name", ApplicationTitle);
				}
				if (!String.IsNullOrEmpty(GenericTitle))
				{
					group.Items.AddProperty("GenericName", GenericTitle);
				}
				if (HideFromMenus)
				{
					group.Items.AddProperty("NoDisplay", "true");
				}
				if (!String.IsNullOrEmpty(shortcut.Comment))
				{
					group.Items.AddProperty("Comment", shortcut.Comment);
				}
				if (!String.IsNullOrEmpty(shortcut.IconFileName))
				{
					// Icon to display in file manager, menus, etc. If the name is an absolute
					// path, the given file will be used. If the name is not an absolute path, the
					// algorithm described in the Icon Theme Specification will be used to locate
					// the icon.
					group.Items.AddProperty("Icon", shortcut.IconFileName);
				}
				if (Deleted)
				{
					group.Items.AddProperty("Hidden", true);
				}
				if (RestrictedEnvironments.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string s in RestrictedEnvironments)
					{
						sb.Append(s);
						if (RestrictedEnvironments.IndexOf(s) < RestrictedEnvironments.Count - 1)
						{
							sb.Append(":");
						}
					}
					group.Items.AddProperty("OnlyShowIn", sb.ToString());
				}
				if (ExcludedEnvironments.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string s in ExcludedEnvironments)
					{
						sb.Append(s);
						if (ExcludedEnvironments.IndexOf(s) < ExcludedEnvironments.Count - 1)
						{
							sb.Append(":");
						}
					}
					group.Items.AddProperty("NotShowIn", sb.ToString());
				}

				if (DBusActivatable)
				{
					group.Items.AddProperty("DBusActivatable", true);
				}

				if (!String.IsNullOrEmpty(TryExec))
				{
					group.Items.AddProperty("TryExec", TryExec);
				}
				if (!String.IsNullOrEmpty(shortcut.ExecutableFileName))
				{
					// Program to execute, possibly with arguments. See the Exec key for details
					// on how this key works. The Exec key is required if DBusActivatable is not
					// set to true. Even if DBusActivatable is true, Exec should be specified for
					// compatibility with implementations that do not understand DBusActivatable.

					StringBuilder sb = new StringBuilder();
					sb.Append(shortcut.ExecutableFileName);
					foreach (string arg in shortcut.ExecutableArguments)
					{
						sb.Append(" ");
						sb.Append(arg);
					}
					group.Items.AddProperty("Exec", sb.ToString());
				}
				if (!String.IsNullOrEmpty(shortcut.WorkingDirectory))
				{
					// If entry is of type Application, the working directory in which to run the
					// program.
					group.Items.AddProperty("Path", shortcut.WorkingDirectory);
				}
				if (shortcut.RunInTerminal)
				{
					// Whether the program runs in a terminal window. 
					group.Items.AddProperty("Terminal", true);
				}
				#region Desktop Actions
				{
					// Identifiers for application actions. This can be used to tell the
					// application to make a specific action, different from the default
					// behavior. The Application actions section describes how actions work.
					StringBuilder sb = new StringBuilder();
					foreach (DesktopShortcutAction action in Actions)
					{
						sb.Append(action.Name);
						sb.Append(";");
					}
					group.Items.AddProperty("Actions", sb.ToString());
				}
				#endregion

				if (SupportedMimeTypes.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string s in SupportedMimeTypes)
					{
						sb.Append(s);
						sb.Append(";");
					}
					group.Items.AddProperty("MimeType", sb.ToString());
				}
				if (Categories.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string s in Categories)
					{
						sb.Append(s);
						sb.Append(";");
					}
					group.Items.AddProperty("Categories", sb.ToString());
				}
				if (Keywords.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string s in Keywords)
					{
						sb.Append(s);
						sb.Append(";");
					}
					group.Items.AddProperty("Keywords", sb.ToString());
				}

				switch (StartupNotify)
				{
					case DesktopShortcutStartupNotifyBehavior.Disabled:
					{
						break;
					}
					case DesktopShortcutStartupNotifyBehavior.Supported:
					{
						group.Items.AddProperty("StartupNotify", true);
						break;
					}
					case DesktopShortcutStartupNotifyBehavior.Unsupported:
					{
						group.Items.AddProperty("StartupNotify", false);
						break;
					}
				}

				if (!String.IsNullOrEmpty(StartupWindowClass))
				{
					group.Items.AddProperty("StartupWMClass", StartupWindowClass);
				}

				if (Type == DesktopShortcutType.InternetLink)
				{
					group.Items.AddProperty("URL", shortcut.ExecutableFileName);
				}


				// This specification defines 3 types of desktop entries: Application (type 1),
				// Link (type 2) and Directory (type 3). To allow the addition of new types in the
				// future, implementations should ignore desktop entries with an unknown type.
				// group.Items.AddProperty("Type");

				plom.Items.Add(group);


				#region Desktop Actions
				{
					// Identifiers for application actions. This can be used to tell the
					// application to make a specific action, different from the default
					// behavior. The Application actions section describes how actions work.
					foreach (DesktopShortcutAction action in Actions)
					{
						Group group1 = new Group("Desktop Action " + action.Name);

						StringBuilder sb = new StringBuilder();
						sb.Append(action.ExecutableFileName);
						foreach (string s in action.ExecutableArguments)
						{
							sb.Append(" ");
							sb.Append(s);
						}
						group1.Items.AddProperty("Exec", sb.ToString());

						group1.Items.AddProperty("Name", action.Title);
						group1.Items.AddProperty("Icon", action.IconFileName);

						plom.Items.Add(group1);
					}
				}
				#endregion
			}
			#endregion

			objectModels.Push(plom);
		}
	}
}
