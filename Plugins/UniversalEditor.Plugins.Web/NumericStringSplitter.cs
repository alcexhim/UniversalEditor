//
//  NumericStringSplitter.cs - provides functions for splitting a string containing numbers into string and number components
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

public struct IntStringSplitterResult
{
	public int IntegerPart { get; }
	public string StringPart { get; }

	public IntStringSplitterResult(int integerPart, string stringPart)
	{
		IntegerPart = integerPart;
		StringPart = stringPart;
	}
}
public struct DoubleStringSplitterResult
{
	private double mvarDoublePart;
	public double DoublePart { get { return mvarDoublePart; } }

	private string mvarStringPart;
	public string StringPart { get { return mvarStringPart; } }

	public DoubleStringSplitterResult(double doublePart, string stringPart)
	{
		mvarDoublePart = doublePart;
		mvarStringPart = stringPart;
	}
}
public static class NumericStringSplitter
{
	public static IntStringSplitterResult SplitIntStringParts(this string value)
	{
		return SplitIntStringParts(value, 0);
	}
	public static IntStringSplitterResult SplitIntStringParts(this string value, int start)
	{
		string intval = String.Empty;
		string strval = String.Empty;

		int i = start;
		for (i = start; i < value.Length; i++)
		{
			if (value[i] >= '0' && value[i] <= '9')
			{
				intval += value[i];
			}
			else
			{
				break;
			}
		}
		strval = value.Substring(i);

		int realintval = Int32.Parse(intval);
		return new IntStringSplitterResult(realintval, strval);
	}
	public static DoubleStringSplitterResult SplitDoubleStringParts(this string value)
	{
		return SplitDoubleStringParts(value, 0);
	}
	public static DoubleStringSplitterResult SplitDoubleStringParts(this string value, int start)
	{
		string intval = String.Empty;
		string strval = String.Empty;

		int i = start;
		for (i = start; i < value.Length; i++)
		{
			if (value[i] >= '0' && value[i] <= '9')
			{
				intval += value[i];
			}
			else
			{
				break;
			}
		}
		strval = value.Substring(i);

		double realintval = Double.Parse(intval);
		return new DoubleStringSplitterResult(realintval, strval);
	}
}
