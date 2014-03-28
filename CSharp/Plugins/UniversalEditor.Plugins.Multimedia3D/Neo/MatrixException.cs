using System;
using System.Collections.Generic;
using System.Text;

namespace Neo
{
	public class MatrixException : ApplicationException
	{
        public MatrixException (string message) : base(message)
        {
        }
    }
	public class MatrixNullException : MatrixException
	{
		public MatrixNullException() : base("Matrix cannot be null") { }
	}
	public class MatrixDimensionException : MatrixException
	{
		public MatrixDimensionException() : base("Dimension of the two matrices not suitable for this operation !") { }
	}
	public class MatrixNotSquareException : MatrixException
	{
		public MatrixNotSquareException() : base("Matrix must be a square matrix") { }
	}
	public class MatrixDeterminentZeroException : MatrixException
	{
        public MatrixDeterminentZeroException() : base("Inverse cannot be found when determinant of matrix equals zero") { }
	}
	public class MatrixVectorDimensionException : MatrixException
	{
        public MatrixVectorDimensionException() : base("Dimension of matrix must be [3 , 1]") { }
	}
	public class MatrixSingularException : MatrixException
	{
		public MatrixSingularException() : base("Matrix must not be singular") { }
	}
}
