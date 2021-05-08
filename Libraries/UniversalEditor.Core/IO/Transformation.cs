//
//  Transformation.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker
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

namespace UniversalEditor.IO
{
	public abstract class Transformation
	{
		public class TransformationCollection
			: System.Collections.ObjectModel.Collection<Transformation>
		{
		}

		protected abstract byte[] TransformInternal(byte[] input);
		public byte[] Transform(byte[] input)
		{
			return TransformInternal(input);
		}
	}
}
