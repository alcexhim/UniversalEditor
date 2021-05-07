//
//  MatrixException.cs - the exception(s) raised when an error occurs in matrix manipulation
//
//  Author:
//	   Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2004-2020 Mike Becker's Software
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

namespace Neo
{
	/// <summary>
	/// The exception raised when an error occurs in matrix manipulation.
	/// </summary>
	public class MatrixException : ApplicationException
	{
		public MatrixException (string message) : base(message)
		{
		}
	}
	/// <summary>
	/// The exception raised when a NULL matrix is passed into a function expecting a non-null value.
	/// </summary>
	public class MatrixNullException : MatrixException
	{
		public MatrixNullException() : base("Matrix cannot be null") { }
	}
	/// <summary>
	/// The exception raised when the dimension of multiple matrices is unsuitable for the desired matrix operation
	/// </summary>
	public class MatrixDimensionException : MatrixException
	{
		public MatrixDimensionException() : base("Dimension of the two matrices not suitable for this operation !") { }
	}
	/// <summary>
	/// The exception raised when a non-square matrix is passed into a function expecting a square matrix.
	/// </summary>
	public class MatrixNotSquareException : MatrixException
	{
		public MatrixNotSquareException() : base("Matrix must be a square matrix") { }
	}
	/// <summary>
	/// The exception raised when a matrix with a determinant of zero is passed into a function expecting a matrix with a non-zero determinant.
	/// </summary>
	public class MatrixDeterminentZeroException : MatrixException
	{
		public MatrixDeterminentZeroException() : base("Inverse cannot be found when determinant of matrix equals zero") { }
	}
	/// <summary>
	/// The exception raised when a matrix with unsuitable vector dimensions is passed into a function expecting otherwise.
	/// </summary>
	public class MatrixVectorDimensionException : MatrixException
	{
		public MatrixVectorDimensionException() : base("Dimension of matrix must be [3 , 1]") { }
	}
	/// <summary>
	/// The exception raised when a singular matrix is passed into a function expecting a non-singular matrix.
	/// </summary>
	public class MatrixSingularException : MatrixException
	{
		public MatrixSingularException() : base("Matrix must not be singular") { }
	}
}
