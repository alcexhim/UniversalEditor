using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs.Internal.FolderBrowserDialog.V2
{
	/// <summary>
	/// Wraps System.Windows.Forms.OpenFileDialog to make it present
	/// a vista-style dialog.
	/// </summary>
	public class FolderSelectDialog
	{
		// Wrapped dialog
		System.Windows.Forms.OpenFileDialog ofd = null;

		public System.Windows.Forms.OpenFileDialog OpenFileDialog { get { return ofd; } }

		/// <summary>
		/// Default constructor
		/// </summary>
		public FolderSelectDialog()
		{
			ofd = new System.Windows.Forms.OpenFileDialog();

			ofd.Filter = "Folders|\n";
			ofd.AddExtension = false;
			ofd.CheckFileExists = false;
			ofd.DereferenceLinks = true;
			ofd.Multiselect = false;
		}

		#region Properties

		/// <summary>
		/// Gets/Sets the initial folder to be selected. A null value selects the current directory.
		/// </summary>
		public string InitialDirectory
		{
			get { return ofd.InitialDirectory; }
			set { ofd.InitialDirectory = value == null || value.Length == 0 ? Environment.CurrentDirectory : value; }
		}

		/// <summary>
		/// Gets/Sets the title to show in the dialog
		/// </summary>
		public string Title
		{
			get { return ofd.Title; }
			set { ofd.Title = value; }
		}

		/// <summary>
		/// Gets the selected folder
		/// </summary>
		public string FileName
		{
			get { return ofd.FileName; }
			set { ofd.FileName = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Shows the dialog
		/// </summary>
		/// <returns>True if the user presses OK else false</returns>
		public System.Windows.Forms.DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}

		/// <summary>
		/// Shows the dialog
		/// </summary>
		/// <param name="parent">Handle of the control to be parent</param>
		/// <returns><see cref="System.Windows.Forms.DialogResult.OK" /> if the user presses OK; otherwise, <see cref="System.Windows.Forms.DialogResult.Cancel" />.</returns>
		public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window parent)
		{
			bool flag = false;

			if (Environment.OSVersion.Version.Major >= 6)
			{
				var r = new Reflector("System.Windows.Forms");

				uint num = 0;
				Type typeIFileDialog = r.GetType("FileDialogNative.IFileDialog");
				object dialog = r.Call(ofd, "CreateVistaDialog");
				r.Call(ofd, "OnBeforeVistaDialog", dialog);

				uint options = (uint)r.CallAs(typeof(System.Windows.Forms.FileDialog), ofd, "GetOptions");
				options |= (uint)r.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS");
				r.CallAs(typeIFileDialog, dialog, "SetOptions", options);

				object pfde = r.New("FileDialog.VistaDialogEvents", ofd);
				object[] parameters = new object[] { pfde, num };
				r.CallAs2(typeIFileDialog, dialog, "Advise", parameters);
				num = (uint)parameters[1];
				try
				{
					int num2 = (int)r.CallAs(typeIFileDialog, dialog, "Show", (parent == null ? IntPtr.Zero : parent.Handle));
					flag = 0 == num2;
				}
				finally
				{
					r.CallAs(typeIFileDialog, dialog, "Unadvise", num);
					GC.KeepAlive(pfde);
				}
			}
			else
			{
				V1.FolderBrowserDialogOld f = new V1.FolderBrowserDialogOld();
				f.AutoUpgradeEnabled = true;
				f.SelectedPath = this.FileName;
				if (f.ShowDialog() == DialogResult.OK)
				{
					this.FileName = f.SelectedPath;
					return System.Windows.Forms.DialogResult.OK;
				}
				return System.Windows.Forms.DialogResult.Cancel;
			}

			if (flag) return System.Windows.Forms.DialogResult.OK;
			return System.Windows.Forms.DialogResult.Cancel;
		}

		#endregion
	}
}
