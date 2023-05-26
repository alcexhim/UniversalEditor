//
//  ObjectModelConverter.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using System.Linq;

namespace UniversalEditor
{
	public abstract class ObjectModelConverter
	{
		protected abstract void ConvertInternal(ObjectModel source, ObjectModel destination);

		public void Convert(ObjectModel source, ObjectModel destination)
		{
			ConvertInternal(source, destination);
		}

		protected virtual ObjectModelConversion[] GetSupportedConversionsInternal()
		{
			return new ObjectModelConversion[0];
		}

		/// <summary>
		/// Gets a <see cref="Tuple{T1, T2}" /> indicating the <see cref="ObjectModel" />
		/// conversions supported by this <see cref="ObjectModelConverter" />.
		/// </summary>
		/// <returns>
		/// A <see cref="Tuple{T1, T2}" /> where the <see cref="Tuple{T1, T2}.Item1" />
		/// is the source <see cref="ObjectModelReference" /> and the <see cref="Tuple{T1, T2}.Item2" />
		/// is the destination <see cref="ObjectModelReference" />.
		/// </returns>
		public ObjectModelConversion[] GetSupportedConversions()
		{
			return GetSupportedConversionsInternal();
		}

		/// <summary>
		/// Determines if this <see cref="ObjectModelConverter" /> supports
		/// converting from the specified <paramref name="source" /> to the
		/// given <paramref name="destination" />.
		/// </summary>
		/// <returns><c>true</c>, if the conversion is supported; <c>false</c> otherwise.</returns>
		/// <param name="source">An <see cref="ObjectModelReference" /> indicating the type of <see cref="ObjectModel" /> being converted from.</param>
		/// <param name="destination">An <see cref="ObjectModelReference" /> indicating the type of <see cref="ObjectModel" /> being converted to.</param>
		public bool CanConvert(ObjectModelReference source, ObjectModelReference destination)
		{
			ObjectModelConversion tup = new ObjectModelConversion(source, destination);
			ObjectModelConversion[] tups = GetSupportedConversions();

			bool supported = tups.Contains(tup);
			return supported;
		}

		public bool CanConvertFrom(ObjectModelReference source)
		{
			ObjectModelConversion[] tups = GetSupportedConversions();
			foreach (ObjectModelConversion tup in tups)
			{
				if (tup.Source == source)
					return true;
			}
			return false;
		}

		public bool CanConvertTo(ObjectModelReference destination)
		{
			ObjectModelConversion[] tups = GetSupportedConversions();
			foreach (ObjectModelConversion tup in tups)
			{
				if (tup.Destination == destination)
					return true;
			}
			return false;
		}
	}
}
