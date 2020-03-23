using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.Drawing
{
	public class Font
	{
		private string mvarFamilyName = String.Empty;
		public string FamilyName { get { return mvarFamilyName; } set { mvarFamilyName = value; } }

		private string mvarFaceName = String.Empty;
		public string FaceName { get { return mvarFaceName; } set { mvarFaceName = value; } }

		private double mvarSize = 0.0;
		public double Size { get { return mvarSize; } set { mvarSize = value; } }

		private bool mvarItalic = false;
		public bool Italic { get { return mvarItalic; } set { mvarItalic = value; } }

		private double mvarWeight = FontWeights.Normal;
		public double Weight { get { return mvarWeight; } set { mvarWeight = value; } }

		public static Font FromFamily(string familyName, double size, double weight = 400)
		{
			Font font = new Font();
			font.FamilyName = familyName;
			font.Size = size;
			font.Weight = weight;
			return font;
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (mvarFamilyName);

			sb.Append (' ');
			sb.Append (mvarSize.ToString ());
			sb.Append (' ');
			sb.Append (mvarFaceName);
			return sb.ToString ();
		}
	}
}
