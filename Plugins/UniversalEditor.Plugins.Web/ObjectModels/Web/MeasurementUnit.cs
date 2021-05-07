//
//  MeasurementUnit.cs - indicates the unit of a Measurement
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

namespace UniversalEditor.ObjectModels.Web
{
	/// <summary>
	/// Indicates the unit of a <see cref="Measurement" />.
	/// </summary>
	public enum MeasurementUnit
	{
		/// <summary>Measurement is in pixels.</summary>
		Pixel = 1,
		/// <summary>Measurement is in points. A point represents 1/72 of an inch.</summary>
		Point,
		/// <summary>Measurement is in picas. A pica represents 12 points.</summary>
		Pica,
		/// <summary>Measurement is in inches.</summary>
		Inch,
		/// <summary>Measurement is in millimeters.</summary>
		Mm,
		/// <summary>Measurement is in centimeters.</summary>
		Cm,
		/// <summary>Measurement is a percentage relative to the parent element.</summary>
		Percentage,
		/// <summary>Measurement is relative to the height of the parent element's font.</summary>
		Em,
		/// <summary>Measurement is relative to the height of the lowercase letter x of the parent element's font.</summary>
		Ex
	}
}
