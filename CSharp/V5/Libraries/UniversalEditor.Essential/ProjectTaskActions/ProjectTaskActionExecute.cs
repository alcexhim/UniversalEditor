using System;
using System.Collections.Generic;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.ProjectTaskActions
{
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
