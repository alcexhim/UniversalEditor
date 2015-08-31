using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct IntStringSplitterResult
{
    private int mvarIntegerPart;
    public int IntegerPart { get { return mvarIntegerPart; } }

    private string mvarStringPart;
    public string StringPart { get { return mvarStringPart; } }

    public IntStringSplitterResult(int integerPart, string stringPart)
    {
        mvarIntegerPart = integerPart;
        mvarStringPart = stringPart;
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
