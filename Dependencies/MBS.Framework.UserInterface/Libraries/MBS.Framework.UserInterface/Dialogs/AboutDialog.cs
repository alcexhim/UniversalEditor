using System;

namespace MBS.Framework.UserInterface.Dialogs
{
	public class AboutDialog : CommonDialog
	{
		private string mvarProgramName = String.Empty;
		public string ProgramName { get { return mvarProgramName; } set { mvarProgramName = value; } }

		private Version mvarVersion = null;
		public Version Version { get { return mvarVersion; } set { mvarVersion = value; } }

		private string mvarCopyright = String.Empty;
		public string Copyright { get { return mvarCopyright; } set { mvarCopyright = value; } }

		private string mvarComments = String.Empty;
		public string Comments { get { return mvarComments; } set { mvarComments = value; } }

		private string mvarLicenseText = null;
		public string LicenseText {
			get { return mvarLicenseText; }
			set {
				mvarLicenseText = value;
				mvarLicenseType = LicenseType.Unknown;
			}
		}

		private LicenseType mvarLicenseType = LicenseType.Unknown;
		public LicenseType LicenseType {
			get { return mvarLicenseType; }
			set {
				mvarLicenseType = value;
				switch (mvarLicenseType) {
					case LicenseType.Artistic:
					{
						break;
					}
					case LicenseType.BSD:
					{
						break;
					}
					case LicenseType.GPL20:
					{
						break;
					}
					case LicenseType.GPL30:
					{
						break;
					}
					case LicenseType.LGPL21:
					{
						break;
					}
					case LicenseType.LGPL30:
					{
						break;
					}
					case LicenseType.MITX11:
					{
						break;
					}
				}
			}
		}

		public void LoadLicenseTextFromFile(string filename) {
			mvarLicenseText = System.IO.File.ReadAllText (filename);
			mvarLicenseType = LicenseType.Unknown;
		}

		private string mvarWebsite = null;
		public string Website { get { return mvarWebsite; } set { mvarWebsite = value; } }
	}
}

