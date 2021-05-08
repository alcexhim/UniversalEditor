//
//  CodeDataType.cs - represents a declaration of a data type
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

namespace UniversalEditor.ObjectModels.SourceCode
{
	/// <summary>
	/// Represents a declaration of a data type.
	/// </summary>
	public struct CodeDataType : IEquatable<CodeDataType>
	{
		private string[] mvarNames;
		public string[] Names
		{
			get { return mvarNames; }
			set { mvarNames = value; }
		}

		private bool mvarIsArray;
		public bool IsArray
		{
			get { return mvarIsArray; }
			set { mvarIsArray = value; }
		}

		private int mvarArrayLength;
		public int ArrayLength
		{
			get { return mvarArrayLength; }
			set { mvarArrayLength = value; }
		}

		private bool mvarIsArrayLengthDefined;
		public bool IsArrayLengthDefined
		{
			get { return mvarIsArrayLengthDefined; }
			set { mvarIsArrayLengthDefined = value; }
		}

		public CodeDataType(params string[] names)
		{
			mvarNames = names;
			mvarIsArray = false;
			mvarIsArrayLengthDefined = false;
			mvarArrayLength = 0;
		}
		public CodeDataType(bool isArray, params string[] names)
		{
			mvarNames = names;
			mvarIsArray = isArray;
			mvarIsArrayLengthDefined = false;
			mvarArrayLength = 0;
		}
		public CodeDataType(bool isArray, int arrayLength, params string[] names)
		{
			mvarNames = names;
			mvarIsArray = isArray;
			mvarIsArrayLengthDefined = true;
			mvarArrayLength = arrayLength;
		}

		public static readonly CodeDataType Empty = new CodeDataType(null);
		public static readonly CodeDataType Void = new CodeDataType("System", "Void");
		public static readonly CodeDataType Boolean = new CodeDataType("System", "Boolean");
		public static readonly CodeDataType Object = new CodeDataType("System", "Object");
		public static readonly CodeDataType Int16 = new CodeDataType("System", "Int16");
		public static readonly CodeDataType Int32 = new CodeDataType("System", "Int32");
		public static readonly CodeDataType Int64 = new CodeDataType("System", "Int64");
		public static readonly CodeDataType UInt16 = new CodeDataType("System", "UInt16");
		public static readonly CodeDataType UInt32 = new CodeDataType("System", "UInt32");
		public static readonly CodeDataType UInt64 = new CodeDataType("System", "UInt64");
		public static readonly CodeDataType Single = new CodeDataType("System", "Single");
		public static readonly CodeDataType Double = new CodeDataType("System", "Double");
		public static readonly CodeDataType Decimal = new CodeDataType("System", "Decimal");
		public static readonly CodeDataType DateTime = new CodeDataType("System", "DateTime");
		public static readonly CodeDataType Char = new CodeDataType("System", "Char");
		public static readonly CodeDataType String = new CodeDataType("System", "String");

		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<DataType>" declaration.

		public override bool Equals(object obj)
		{
			if (obj is CodeDataType)
				return Equals((CodeDataType)obj); // use Equals method below
			else
				return false;
		}

		public bool Equals(CodeDataType other)
		{
			// add comparisions for all members here
			return
			(
				(this.Names == other.Names)
				&& (this.IsArray == other.IsArray)
				&& (this.ArrayLength == other.ArrayLength)
				&& (this.IsArrayLengthDefined == other.IsArrayLengthDefined)
			);
		}

		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return
			(
				(this.Names.GetHashCode())
				^ (this.IsArray.GetHashCode())
				^ (this.ArrayLength.GetHashCode())
				^ (this.IsArrayLengthDefined.GetHashCode())
			);
		}

		public string GetFullName(string separator = ".")
		{
			return System.String.Join(separator, this.Names);
		}

		public override string ToString()
		{
			if (mvarNames == null)
			{
				return null;
			}
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(string.Join(".", mvarNames));
			if (mvarIsArray)
			{
				sb.Append("[");
				if (mvarIsArrayLengthDefined)
				{
					sb.Append(mvarArrayLength);
				}
				sb.Append("]");
			}
			return sb.ToString();
		}


		public static bool operator ==(CodeDataType left, CodeDataType right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(CodeDataType left, CodeDataType right)
		{
			return !left.Equals(right);
		}

		public static implicit operator CodeDataType(string value)
		{
			return new CodeDataType(value);
		}
		public static implicit operator string(CodeDataType value)
		{
			return value.ToString();
		}
		public static implicit operator CodeDataType(string[] value)
		{
			return new CodeDataType(value);
		}
		public static implicit operator string[] (CodeDataType value)
		{
			return value.Names;
		}
		#endregion
	}
}
