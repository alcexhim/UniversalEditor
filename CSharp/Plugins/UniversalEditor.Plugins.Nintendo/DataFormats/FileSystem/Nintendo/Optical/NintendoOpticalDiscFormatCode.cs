using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Nintendo.Optical
{
	/// <summary>
	/// 
	/// </summary>
	/// <completionlist cref="NintendoOpticalDiscFormatCodes" />
	public class NintendoOpticalDiscFormatCode
	{
		private char mvarValue = '\0';
		public char Value { get { return mvarValue; } set { mvarValue = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override string ToString()
		{
			 return mvarTitle + " [" + mvarValue.ToString() + "]";
		}

		public NintendoOpticalDiscFormatCode(string title, char value)
		{
			mvarTitle = title;
			mvarValue = value;
		}


		/// <summary>
		/// Gets the <see cref="NintendoOpticalDiscRegionCode" /> with the given code if valid.
		/// </summary>
		/// <param name="value">The code to search on.</param>
		/// <returns>If the code is known, returns an instance of the associated <see cref="NintendoOpticalDiscRegionCode" />. Otherwise, returns null.</returns>
		public static NintendoOpticalDiscFormatCode FromCode(char value)
		{
			Type t = typeof(NintendoOpticalDiscFormatCodes);

			MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
			PropertyInfo[] properties = t.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType == typeof(NintendoOpticalDiscFormatCode))
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (getMethod != null && (getMethod.Attributes & methodAttributes) == methodAttributes)
					{
						object[] index = null;
						NintendoOpticalDiscFormatCode val = (NintendoOpticalDiscFormatCode)propertyInfo.GetValue(null, index);

						if (val.Value == value) return val;
					}
				}
			}
			return null;
		}
	}
	public class NintendoOpticalDiscFormatCodes
	{
		private static NintendoOpticalDiscFormatCode mvarRevolution = new NintendoOpticalDiscFormatCode("Revolution (Wii)", 'R');
		public static NintendoOpticalDiscFormatCode Revolution { get { return mvarRevolution; } }
		private static NintendoOpticalDiscFormatCode mvarWii = new NintendoOpticalDiscFormatCode("Wii", 'S');
		public static NintendoOpticalDiscFormatCode Wii { get { return mvarWii; } }
		private static NintendoOpticalDiscFormatCode mvarGameCube = new NintendoOpticalDiscFormatCode("GameCube", 'G');
		public static NintendoOpticalDiscFormatCode GameCube { get { return mvarGameCube; } }
		private static NintendoOpticalDiscFormatCode mvarUtility = new NintendoOpticalDiscFormatCode("Utility disc / GBA-Player", 'U');
		public static NintendoOpticalDiscFormatCode Utility { get { return mvarUtility; } }
		private static NintendoOpticalDiscFormatCode mvarGameCubeDemo = new NintendoOpticalDiscFormatCode("GameCube demo disc (?)", 'D');
		public static NintendoOpticalDiscFormatCode GameCubeDemo { get { return mvarGameCubeDemo; } }
		private static NintendoOpticalDiscFormatCode mvarGameCubePromotional = new NintendoOpticalDiscFormatCode("GameCube promotional disc (?)", 'P');
		public static NintendoOpticalDiscFormatCode GameCubePromotional { get { return mvarGameCubePromotional; } }
		private static NintendoOpticalDiscFormatCode mvarDiagnostic = new NintendoOpticalDiscFormatCode("Diagnostic disc (auto boot)", '0');
		public static NintendoOpticalDiscFormatCode Diagnostic { get { return mvarDiagnostic; } }
		private static NintendoOpticalDiscFormatCode mvarDiagnostic1 = new NintendoOpticalDiscFormatCode("Diagnostic disc (?)", '1');
		public static NintendoOpticalDiscFormatCode Diagnostic1 { get { return mvarDiagnostic1; } }
		private static NintendoOpticalDiscFormatCode mvarWiiBackup = new NintendoOpticalDiscFormatCode("Wii Backup disc", '4');
		public static NintendoOpticalDiscFormatCode WiiBackup { get { return mvarWiiBackup; } }
		private static NintendoOpticalDiscFormatCode mvarWiiFitChanInstaller = new NintendoOpticalDiscFormatCode("WiiFit-chan installer", '_');
		public static NintendoOpticalDiscFormatCode WiiFitChanInstaller { get { return mvarWiiFitChanInstaller; } }
	}
}
