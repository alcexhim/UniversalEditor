using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.Targa
{
	public class TargaExtensionArea
	{
		private bool mvarEnabled = false;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private DateTime mvarDateCreated = DateTime.Now;
		public DateTime DateCreated { get { return mvarDateCreated; } set { mvarDateCreated = value; } }

		private TimeSpan mvarJobTime = TimeSpan.Zero;
		public TimeSpan JobTime { get { return mvarJobTime; } set { mvarJobTime = value; } }

		private string mvarSoftwareID = String.Empty;
		public string SoftwareID { get { return mvarSoftwareID; } set { mvarSoftwareID = value; } }

		private string mvarVersionString = String.Empty;
		public string VersionString { get { return mvarVersionString; } set { mvarVersionString = value; } }

		private System.Drawing.Color mvarColorKey = System.Drawing.Color.Empty;
		public System.Drawing.Color ColorKey { get { return mvarColorKey; } set { mvarColorKey = value; } }

		private int mvarPixelAspectRatioNumerator = 0;
		public int PixelAspectRatioNumerator { get { return mvarPixelAspectRatioNumerator; } set { mvarPixelAspectRatioNumerator = value; } }

		private int mvarPixelAspectRatioDenominator = 0;
		public int PixelAspectRatioDenominator { get { return mvarPixelAspectRatioDenominator; } set { mvarPixelAspectRatioDenominator = value; } }

		private int mvarGammaNumerator = 0;
		public int GammaNumerator { get { return mvarGammaNumerator; } set { mvarGammaNumerator = value; } }

		private int mvarGammaDenominator = 0;
		public int GammaDenominator { get { return mvarGammaDenominator; } set { mvarGammaDenominator = value; } }

		private List<int> mvarScanLineTable = new List<int>();
		public List<int> ScanLineTable { get { return mvarScanLineTable; } }

		private List<System.Drawing.Color> mvarColorCorrectionTable = new List<System.Drawing.Color>();
		public List<System.Drawing.Color> ColorCorrectionTable { get { return mvarColorCorrectionTable; } }

		private int mvarAttributesType = 0;
		public int AttributesType { get { return mvarAttributesType; } set { mvarAttributesType = value; } }
	}
}
