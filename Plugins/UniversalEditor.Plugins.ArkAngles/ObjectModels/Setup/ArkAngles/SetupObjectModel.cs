//
//  SetupObjectModel.cs - contains the instructions and data needed to process an Ark Angles software installation
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Setup.ArkAngles
{
	/// <summary>
	/// Contains the instructions and data needed to process an Ark Angles software installation.
	/// </summary>
	public class SetupObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Setup", "Ark Angles", "Installation script" };
			}
			return _omr;
		}

		private Action.ActionCollection mvarActions = new Action.ActionCollection();
		/// <summary>
		/// The actions taken during the installation process.
		/// </summary>
		public Action.ActionCollection Actions { get { return mvarActions; } }

		private AutoStartCommand.AutoStartCommandCollection mvarAutoStartCommands = new AutoStartCommand.AutoStartCommandCollection();
		/// <summary>
		/// The commands to execute automatically when the setup program is launched.
		/// </summary>
		public AutoStartCommand.AutoStartCommandCollection AutoStartCommands { get { return mvarAutoStartCommands; } }

		private string mvarCatalogExecutableFileName = String.Empty;
		/// <summary>
		/// The file name of the catalog executable to launch via the "Catalog" button. If this value is empty, the "Catalog" button is not displayed.
		/// </summary>
		public string CatalogExecutableFileName { get { return mvarCatalogExecutableFileName; } set { mvarCatalogExecutableFileName = value; } }

		private string mvarDefaultInstallationDirectory = String.Empty;
		/// <summary>
		/// The default installation directory for this application.
		/// </summary>
		public string DefaultInstallationDirectory { get { return mvarDefaultInstallationDirectory; } set { mvarDefaultInstallationDirectory = value; } }

		private bool mvarDeleteFromSFX = false;
		/// <summary>
		/// Determines whether the Setup program and its related files are deleted when the installation exits, regardless of success or failure.
		/// </summary>
		public bool DeleteFromSFX { get { return mvarDeleteFromSFX; } set { mvarDeleteFromSFX = value; } }

		private string mvarDocumentationFileName = String.Empty;
		/// <summary>
		///
		/// </summary>
		public string DocumentationFileName { get { return mvarDocumentationFileName; } set { mvarDocumentationFileName = value; } }

		private int? mvarFooterFontSize = null; // default ??
		/// <summary>
		/// The size of the font used for the footer of the installation program.
		/// </summary>
		public int? FooterFontSize { get { return mvarFooterFontSize; } set { mvarFooterFontSize = value; } }

		private string mvarFooterText = String.Empty;
		/// <summary>
		/// The text to display at the bottom of the installer background window.
		/// </summary>
		public string FooterText { get { return mvarFooterText; } set { mvarFooterText = value; } }

		private int? mvarHeaderFontSize = null; // default 23, KCHESS uses 48
		/// <summary>
		/// The size of the font used for the header of the installation program.
		/// </summary>
		public int? HeaderFontSize { get { return mvarHeaderFontSize; } set { mvarHeaderFontSize = value; } }

		private string mvarLogFileName = String.Empty;
		/// <summary>
		/// The file name of the installation log to create upon installation.
		/// </summary>
		public string LogFileName { get { return mvarLogFileName; } set { mvarLogFileName = value; } }

		private bool mvarRestartAfterInstallation = false;
		/// <summary>
		/// Determines whether the Restart command should be presented after installation (or the installer
		/// should automatically restart the operating system if the AutoStartCommand.Restart is present).
		/// </summary>
		public bool RestartAfterInstallation { get { return mvarRestartAfterInstallation; } set { mvarRestartAfterInstallation = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override void Clear()
		{
			mvarActions.Clear();
			mvarAutoStartCommands.Clear();
			mvarCatalogExecutableFileName = String.Empty;
			mvarDefaultInstallationDirectory = String.Empty;
			mvarDeleteFromSFX = false;
			mvarDocumentationFileName = String.Empty;
			mvarFooterFontSize = null;
			mvarFooterText = String.Empty;
			mvarHeaderFontSize = null;
			mvarLogFileName = String.Empty;
			mvarRestartAfterInstallation = false;
			mvarTitle = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			SetupObjectModel clone = (where as SetupObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();
			foreach (Action action in mvarActions)
			{
				clone.Actions.Add(action.Clone() as Action);
			}
			foreach (AutoStartCommand cmd in mvarAutoStartCommands)
			{
				clone.AutoStartCommands.Add(cmd);
			}
			clone.CatalogExecutableFileName = (mvarCatalogExecutableFileName.Clone() as string);
			clone.DefaultInstallationDirectory = (mvarDefaultInstallationDirectory.Clone() as string);
			clone.DeleteFromSFX = mvarDeleteFromSFX;
			clone.DocumentationFileName = (mvarDocumentationFileName.Clone() as string);
			clone.FooterFontSize = mvarFooterFontSize;
			clone.FooterText = (mvarFooterText.Clone() as string);
			clone.HeaderFontSize = mvarHeaderFontSize;
			clone.LogFileName = (mvarLogFileName.Clone() as string);
			clone.RestartAfterInstallation = mvarRestartAfterInstallation;
			clone.Title = (mvarTitle.Clone() as string);
		}
	}
}
