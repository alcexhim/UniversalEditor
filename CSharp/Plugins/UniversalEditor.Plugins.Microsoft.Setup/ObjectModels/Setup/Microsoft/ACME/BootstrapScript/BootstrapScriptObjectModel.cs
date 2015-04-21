using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript
{
	public class BootstrapScriptObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Microsoft ACME Setup Bootstrapper Script";
				_omr.Path = new string[]
				{
					"Setup",
					"Microsoft",
					"ACME Setup"
				};
			}
			return _omr;
		}

		private string mvarWindowTitle = "Your Application Setup";
		/// <summary>
		/// The title of the setup initialization dialog.
		/// </summary>
		public string WindowTitle { get { return mvarWindowTitle; } set { mvarWindowTitle = value; } }

		private string mvarWindowMessage = "Initializing Setup...";
		/// <summary>
		/// The message to display inside the setup initialization dialog.
		/// </summary>
		public string WindowMessage {  get { return mvarWindowMessage; } set { mvarWindowMessage = value; } }

		private int mvarTemporaryDirectorySize = 3200;
		/// <summary>
		/// The size of the directory in which to place the bootstrapped files.
		/// </summary>
		public int TemporaryDirectorySize { get { return mvarTemporaryDirectorySize; } set { mvarTemporaryDirectorySize = value; } }

		private string mvarTemporaryDirectoryName = "~msstfqf.t";
		/// <summary>
		/// The name of the directory in which to place the bootstrapped files.
		/// </summary>
		public string TemporaryDirectoryName { get { return mvarTemporaryDirectoryName; } set { mvarTemporaryDirectoryName = value; } }

		private string mvarCommandLine = String.Empty;
		/// <summary>
		/// The command to execute after bootstrapping, including any arguments.
		/// </summary>
		public string CommandLine { get { return mvarCommandLine; } set { mvarCommandLine = value; } }

		private string mvarWindowClassName = "Stuff-Shell";
		/// <summary>
		/// The name of the window class to register for the setup initialization dialog.
		/// </summary>
		public string WindowClassName { get { return mvarWindowClassName; } set { mvarWindowClassName = value; } }
		
		private string mvarRequire31Message = "This application requires a newer version of Microsoft Windows.";
		/// <summary>
		/// The message to display when a newer version of Microsoft Windows is required.
		/// </summary>
		public string Require31Message { get { return mvarRequire31Message; } set { mvarRequire31Message = value; } }

		private bool mvarRequire31Enabled = false;
		/// <summary>
		/// Determines whether a newer version of Microsoft Windows is required.
		/// </summary>
		public bool Require31Enabled { get { return mvarRequire31Enabled; } set { mvarRequire31Enabled = value; } }

		private BootstrapFile.BootstrapFileCollection mvarFiles = new BootstrapFile.BootstrapFileCollection();
		public BootstrapFile.BootstrapFileCollection Files { get { return mvarFiles; } }

		public override void Clear()
		{
			mvarWindowTitle = "Your Application Setup";
			mvarWindowMessage = "Initializing Setup...";
			mvarTemporaryDirectorySize = 3200;
			mvarTemporaryDirectoryName = "~msstfqf.t";
			mvarCommandLine = String.Empty;
			mvarWindowClassName = "Stuff-Shell";
			mvarRequire31Message = "This application requires a newer version of Microsoft Windows.";
			mvarRequire31Enabled = false;
			mvarFiles.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			BootstrapScriptObjectModel clone = (where as BootstrapScriptObjectModel);
			clone.WindowTitle = (mvarWindowTitle.Clone() as string);
			clone.WindowMessage = (mvarWindowMessage.Clone() as string);
			clone.TemporaryDirectorySize = mvarTemporaryDirectorySize;
			clone.TemporaryDirectoryName = (mvarTemporaryDirectoryName.Clone() as string);
			clone.CommandLine = (mvarCommandLine.Clone() as string);
			clone.WindowClassName = (mvarWindowClassName.Clone() as string);
			clone.Require31Message = (mvarRequire31Message.Clone() as string);
			clone.Require31Enabled = mvarRequire31Enabled;
			foreach (BootstrapFile file in mvarFiles)
			{
				clone.Files.Add(file.Clone() as BootstrapFile);
			}
		}
	}
}
