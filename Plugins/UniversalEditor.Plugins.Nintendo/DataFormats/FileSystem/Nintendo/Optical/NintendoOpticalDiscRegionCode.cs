//
//  NintendoOpticalDiscRegionCode.cs - provides metadata information about region codes for Nintendo optical disc images
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
using System.Reflection;

namespace UniversalEditor.DataFormats.FileSystem.Nintendo.Optical
{
	/// <summary>
	/// Provides metadata information about region codes for Nintendo optical disc images.
	/// </summary>
	/// <completionlist cref="NintendoOpticalDiscRegionCodes" />
	public class NintendoOpticalDiscRegionCode
	{
		private char mvarValue = '\0';
		public char Value { get { return mvarValue; } set { mvarValue = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override string ToString()
		{
			 return mvarTitle + " [" + mvarValue.ToString() + "]";
		}

		public NintendoOpticalDiscRegionCode(string title, char value)
		{
			mvarTitle = title;
			mvarValue = value;
		}


		/// <summary>
		/// Gets the <see cref="NintendoOpticalDiscRegionCode" /> with the given code if valid.
		/// </summary>
		/// <param name="value">The code to search on.</param>
		/// <returns>If the code is known, returns an instance of the associated <see cref="NintendoOpticalDiscRegionCode" />. Otherwise, returns null.</returns>
		public static NintendoOpticalDiscRegionCode FromCode(char value)
		{
			Type t = typeof(NintendoOpticalDiscRegionCodes);

			MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.Static;
			PropertyInfo[] properties = t.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType == typeof(NintendoOpticalDiscRegionCode))
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (getMethod != null && (getMethod.Attributes & methodAttributes) == methodAttributes)
					{
						object[] index = null;
						NintendoOpticalDiscRegionCode val = (NintendoOpticalDiscRegionCode)propertyInfo.GetValue(null, index);

						if (val.Value == value) return val;
					}
				}
			}
			return null;
		}
	}
	public class NintendoOpticalDiscRegionCodes
	{
		private static NintendoOpticalDiscRegionCode mvarGerman = new NintendoOpticalDiscRegionCode("German", 'D');
		public static NintendoOpticalDiscRegionCode German { get { return mvarGerman; } }
		private static NintendoOpticalDiscRegionCode mvarUnitedStates = new NintendoOpticalDiscRegionCode("United States", 'E');
		public static NintendoOpticalDiscRegionCode UnitedStates { get { return mvarUnitedStates; } }
		private static NintendoOpticalDiscRegionCode mvarFrance = new NintendoOpticalDiscRegionCode("France", 'F');
		public static NintendoOpticalDiscRegionCode France { get { return mvarFrance; } }
		private static NintendoOpticalDiscRegionCode mvarItaly = new NintendoOpticalDiscRegionCode("Italy", 'I');
		public static NintendoOpticalDiscRegionCode Italy { get { return mvarItaly; } }
		private static NintendoOpticalDiscRegionCode mvarJapan = new NintendoOpticalDiscRegionCode("Japan", 'J');
		public static NintendoOpticalDiscRegionCode Japan { get { return mvarJapan; } }
		private static NintendoOpticalDiscRegionCode mvarKorea = new NintendoOpticalDiscRegionCode("Korea", 'K');
		public static NintendoOpticalDiscRegionCode Korea { get { return mvarKorea; } }
		private static NintendoOpticalDiscRegionCode mvarPAL = new NintendoOpticalDiscRegionCode("PAL", 'P');
		public static NintendoOpticalDiscRegionCode PAL { get { return mvarPAL; } }
		private static NintendoOpticalDiscRegionCode mvarRussia = new NintendoOpticalDiscRegionCode("Russia", 'R');
		public static NintendoOpticalDiscRegionCode Russia { get { return mvarRussia; } }
		private static NintendoOpticalDiscRegionCode mvarSpanish = new NintendoOpticalDiscRegionCode("Spanish", 'S');
		public static NintendoOpticalDiscRegionCode Spanish { get { return mvarSpanish; } }
		private static NintendoOpticalDiscRegionCode mvarTaiwan = new NintendoOpticalDiscRegionCode("Taiwan", 'T');
		public static NintendoOpticalDiscRegionCode Taiwan { get { return mvarTaiwan; } }
		private static NintendoOpticalDiscRegionCode mvarAustralia = new NintendoOpticalDiscRegionCode("Australia", 'U');
		public static NintendoOpticalDiscRegionCode Australia { get { return mvarAustralia; } }
	}
}
