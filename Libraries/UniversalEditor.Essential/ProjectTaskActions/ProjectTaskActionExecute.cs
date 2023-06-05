//
//  ProjectTaskActionExecute.cs - represents a ProjectTaskAction that executes a command line
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
using MBS.Framework;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.ProjectTaskActions
{
	/// <summary>
	/// Represents a <see cref="ProjectTaskAction" /> that executes a command line.
	/// </summary>
	public class ProjectTaskActionExecute : ProjectTaskAction
	{
		public override string Title
		{
			get { return "Execute: " + mvarCommandLine.ToString(); }
		}

		private static ProjectTaskActionReference _ptar = null;
		protected override ProjectTaskActionReference MakeReferenceInternal()
		{
			if (_ptar == null)
			{
				_ptar = base.MakeReferenceInternal();
				_ptar.ProjectTaskActionTypeID = new Guid("{EE505E05-F125-4718-BA0A-879C72B5125A}");
				_ptar.ProjectTaskActionTypeName = "UniversalEditor.ProjectTaskActionExecute";
			}
			return _ptar;
		}

		private ExpandedString mvarCommandLine = ExpandedString.Empty;
		public ExpandedString CommandLine
		{
			get { return mvarCommandLine; }
			set
			{
				if (value == null)
				{
					mvarCommandLine = ExpandedString.Empty;
					return;
				}
				mvarCommandLine = value;
			}
		}

		protected override void ExecuteInternal(ExpandedStringVariableStore variables)
		{
			string fileNameWithArguments = mvarCommandLine.ToString(variables);
			if (String.IsNullOrEmpty(fileNameWithArguments)) return;

			string[] fileNameArgumentsSplit = fileNameWithArguments.Split(new char[] { ' ' }, "\"", StringSplitOptions.None, 2);
			string fileName = fileNameArgumentsSplit[0];
			string arguments = fileNameArgumentsSplit[1];

			if (!System.IO.File.Exists(fileName)) throw new System.IO.FileNotFoundException(fileName);

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo = new System.Diagnostics.ProcessStartInfo(fileName, arguments);
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.Start();
			p.WaitForExit();

			string error = p.StandardError.ReadToEnd();
			string output = p.StandardOutput.ReadToEnd();

			if (!String.IsNullOrEmpty(error))
			{
				throw new Exception(error);
			}
		}

		protected override void LoadFromMarkupInternal(MarkupTagElement tag)
		{
			MarkupTagElement tagCommandLine = (tag.Elements["CommandLine"] as MarkupTagElement);
			if (tagCommandLine != null)
			{
				mvarCommandLine = ExpandedString.FromMarkup(tagCommandLine);
			}
		}
	}
}
