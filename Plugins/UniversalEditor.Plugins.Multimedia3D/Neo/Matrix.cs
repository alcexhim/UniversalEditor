//
//  Matrix.cs - provides various matrix operations that work with Matrix objects or two-dimensional double arrays
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//       Anas Abidi (2004-12-18 "Matrix Operations Library" v2.0)
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
using System.Text;

namespace Neo
{
	/// <summary>
	/// Provides various matrix operations that work with Matrix objects or two-dimensional double arrays. The
	/// '+','-','*' operators are overloaded to work with the Matrix objects.
	/// </summary>
	public class Matrix
	{
		private double[,] m_Array;

		private bool mvarSafeMode = true;
		/// <summary>
		/// When true, prevents the <see cref="Matrix" /> from being cleared during an attempt to change the <see cref="RowCount" /> or <see cref="ColumnCount" /> properties.
		/// </summary>
		public bool SafeMode { get { return mvarSafeMode; } set { mvarSafeMode = value; } }

		#region Class Constructor and Indexer
		/// <summary>
		/// Constructs an empty <see cref="Matrix" /> with the specified dimensions.
		/// </summary>
		/// <param name="rows">Number of rows in this matrix.</param>
		/// <param name="columns">Number of columns in this matrix.</param>
		public Matrix(int rows, int columns)
		{
			m_Array = new double[rows, columns];
		}
		/// <summary>
		/// Constructs a matrix from the specified array object.
		/// </summary>
		/// <param name="array">The array from which to generate the matrix.</param>
		public Matrix(double[,] array)
		{
			m_Array = (double[,])array.Clone();
		}
		/// <summary>
		/// Gets or sets the element at the specified position in the matrix.
		/// </summary>
		/// <param name="row">The row of the element to get or set.</param>
		/// <param name="column">The column of the element to get or set.</param>
		public double this[int row, int column]
		{
			get { return m_Array[row, column]; }
			set { m_Array[row, column] = value; }
		}
		#endregion
		#region Public Properties
		/// <summary>
		/// The number of rows in the matrix.
		/// </summary>
		/// <remarks>Setting this property will delete all of the elements of the matrix and set them to zero, if <see cref="SafeMode" /> is disabled.</remarks>
		public int RowCount
		{
			get { return m_Array.GetUpperBound(0) + 1; }
			set
			{
				if (!mvarSafeMode)
				{
					m_Array = new double[value, m_Array.GetUpperBound(0)];
				}
			}
		}
		/// <summary>
		/// The number of columns in the matrix.
		/// </summary>
		/// <remarks>Setting this property will delete all of the elements of the matrix and set them to zero, if <see cref="SafeMode" /> is disabled.</remarks>
		public int ColumnCount
		{
			get { return m_Array.GetUpperBound(1) + 1; }
			set
			{
				if (!mvarSafeMode)
				{
					m_Array = new double[m_Array.GetUpperBound(0), value];
				}
			}
		}
		/// <summary>
		/// Creates a two-dimensional <see cref="System.Double" /> array from this <see cref="Matrix" />.
		/// </summary>
		/// <returns>A two-dimensional <see cref="System.Double" /> array populated with the contents of this <see cref="Matrix" />.</returns>
		public double[,] ToArray()
		{
			return (double[,])m_Array.Clone();
		}
		#endregion

		/// <summary>
		/// Gets the next array index for a one-dimensional <see cref="System.Double" /> array.
		/// </summary>
		/// <param name="array">The <see cref="System.Double" /> array for which to find the array index.</param>
		/// <param name="row">The next available row index.</param>
		private static void GetNextArrayIndex(double[] array, out int row)
		{
			row = array.GetUpperBound(0);
		}
		/// <summary>
		/// Gets the next array indices for a two-dimensional <see cref="System.Double" /> array.
		/// </summary>
		/// <param name="array">The <see cref="System.Double" /> array for which to find the array indices.</param>
		/// <param name="row">The next available row index (upper bound of dimension 0).</param>
		/// <param name="column">The next available column index (upper bound of dimension 1).</param>
		private static void GetNextArrayIndex(double[,] array, out int row, out int column)
		{
			row = array.GetUpperBound(0);
			column = array.GetUpperBound(1);
		}

		#region "Change 1D array ([n]) to/from 2D array [n,1]"

		/// <summary>
		/// Returns the 2D form of a 1D array. i.e. array with dimension[n] is returned as an array with dimension [n,1].
		/// </summary>
		/// <param name="Mat">the array to convert, with dimesion [n]</param>
		/// <returns>the same array but with [n,1] dimension</returns>
		public static double[,] ConvertArray(double[] Mat)
		{
			int Rows;
			try
			{
				GetNextArrayIndex(Mat, out Rows);
			}
			catch
			{
				throw new MatrixNullException();
			}

			double[,] Sol = new double[Rows + 1, 1];

			for (int i = 0; i <= Rows; i++)
			{
				Sol[i, 0] = Mat[i];
			}

			return Sol;
		}

		/// <summary>
		/// Returns the one-dimensional form of a two-dimensional array.
		/// </summary>
		/// <param name="Mat">The two-dimensional array to convert</param>
		/// <returns>The one-dimensional form of the given two-dimensional <see cref="System.Double" /> array</returns>
		public static double[] ConvertArray(double[,] Mat)
		{
			int Rows;
			int Cols;
			//Find The dimensions !!
			try { GetNextArrayIndex(Mat, out Rows, out Cols); }
			catch { throw new MatrixNullException(); }

			if (Cols != 0) throw new MatrixDimensionException();

			double[] Sol = new double[Rows + 1];

			for (int i = 0; i <= Rows; i++)
			{
				Sol[i] = Mat[i, 0];
			}
			return Sol;
		}
		#endregion

		#region "Identity Matrix"
		/// <summary>
		/// Returns an Identity matrix with dimensions [n,n] in the from of an array.
		/// </summary>
		/// <param name="n">the no. of rows or no. cols in the matrix</param>
		/// <returns>An identity Matrix with dimensions [n,n] in the form of an array</returns>
		public static double[,] Identity(int n)
		{
			double[,] temp = new double[n, n];
			for (int i = 0; i < n; i++) temp[i, i] = 1;
			return temp;
		}
		#endregion

		#region Add Matrices
		/// <summary>
		/// Returns the summation of two matrices represented as two-dimensional <see cref="System.Double" /> arrays. The arrays must have compatible dimensions.
		/// </summary>
		/// <param name="array1">First <see cref="System.Double" /> array in the summation.</param>
		/// <param name="array2">Second <see cref="System.Double" /> array in the summation.</param>
		/// <returns>The sum of the two <see cref="System.Double" /> arrays.</returns>
		/// <exception cref="MatrixNullException"></exception>
		/// <exception cref="MatrixDimensionException"></exception>
		public static double[,] Add(double[,] array1, double[,] array2)
		{
			double[,] sum;
			int i, j;
			int Rows1, Cols1;
			int Rows2, Cols2;

			try
			{
				GetNextArrayIndex(array1, out Rows1, out Cols1);
				GetNextArrayIndex(array2, out Rows2, out Cols2);
			}
			catch
			{
				throw new MatrixNullException();
			}

			if (Rows1 != Rows2 || Cols1 != Cols2) throw new MatrixDimensionException();

			sum = new double[Rows1 + 1, Cols1 + 1];

			for (i = 0; i <= Rows1; i++)
			{
				for (j = 0; j <= Cols1; j++)
				{
					sum[i, j] = array1[i, j] + array2[i, j];
				}
			}
			return sum;
		}

		/// <summary>
		/// Returns the summation of two <see cref="Matrix" /> objects with compatible dimensions.
		/// </summary>
		/// <param name="matrix1">First matrix in the summation</param>
		/// <param name="matrix2">Second matrix in the summation</param>
		/// <returns>A <see cref="Matrix" /> object representing the sum of the two <see cref="Matrix" /> objects.</returns>
		public static Matrix Add(Matrix matrix1, Matrix matrix2)
		{
			return new Matrix(Add(matrix1.m_Array, matrix2.m_Array));
		}

		/// <summary>
		/// Returns the summation of two matrices with compatible
		/// dimensions.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat1">First Matrix object in the summation</param>
		/// <param name="Mat2">Second Matrix object in the summation</param>
		/// <returns>Sum of Mat1 and Mat2 as a Matrix object</returns>
		public static Matrix operator +(Matrix Mat1, Matrix Mat2)
		{
			return new Matrix(Add(Mat1.m_Array, Mat2.m_Array));
		}
		#endregion

		#region Subtract Matrices
		/// <summary>
		/// Returns the difference of two matrices represented as two-dimensional <see cref="System.Double" /> arrays. The arrays must have compatible dimensions.
		/// </summary>
		/// <param name="array1">First array in the subtraction</param>
		/// <param name="array2">Second array in the subtraction</param>
		/// <returns>Difference of Mat1 and Mat2 as an array</returns>
		/// <exception cref="MatrixNullException">Occurs when the given matrix array is null.</exception>
		/// <exception cref="MatrixDimensionException">Occurs when the dimensions of the two matrix arrays do not match.</exception>
		public static double[,] Subtract(double[,] array1, double[,] array2)
		{
			double[,] sol;
			int i, j;
			int Rows1, Cols1;
			int Rows2, Cols2;

			try
			{
				GetNextArrayIndex(array1, out Rows1, out Cols1);
				GetNextArrayIndex(array2, out Rows2, out Cols2);
			}
			catch
			{
				throw new MatrixNullException();
			}

			if (Rows1 != Rows2 || Cols1 != Cols2) throw new MatrixDimensionException();

			sol = new double[Rows1 + 1, Cols1 + 1];

			for (i = 0; i <= Rows1; i++)
				for (j = 0; j <= Cols1; j++)
				{
					sol[i, j] = array1[i, j] - array2[i, j];
				}

			return sol;
		}
		/// <summary>
		/// Subtracts the given <see cref="Matrix" /> from the current <see cref="Matrix" />. The matrices must have compatible dimensions.
		/// </summary>
		/// <param name="Mat2">The <see cref="Matrix" /> to subtract from the current <see cref="Matrix" />.</param>
		/// <returns>A <see cref="Matrix" /> representing the difference of <see cref="Mat2" /> and the current <see cref="Matrix" />.</returns>
		public Matrix Subtract(Matrix Mat2)
		{
			return new Matrix(Matrix.Subtract(m_Array, Mat2.m_Array));
		}
		/// <summary>
		/// Returns the difference of two <see cref="Matrix" /> objects with compatible dimensions.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix operator -(Matrix left, Matrix right)
		{
			return new Matrix(Subtract(left.m_Array, right.m_Array));
		}
		#endregion
		#region Multiply Matrices
		/// <summary>
		/// Returns the multiplication of two matrices represented as two-dimensional <see cref="System.Double" /> arrays. The arrays must have compatible dimensions.
		/// </summary>
		/// <param name="array1">The first array to multiply.</param>
		/// <param name="array2">The second array to multiply.</param>
		/// <returns>A two-dimensional <see cref="System.Double" /> array representing the product of <see cref="array1" /> and <see cref="array2" />.</returns>
		public static double[,] Multiply(double[,] array1, double[,] array2)
		{
			int Rows1, Cols1;
			int Rows2, Cols2;
			double MulAdd = 0;

			try
			{
				GetNextArrayIndex(array1, out Rows1, out Cols1);
				GetNextArrayIndex(array2, out Rows2, out Cols2);
			}
			catch
			{
				throw new MatrixNullException();
			}
			if (Cols1 != Rows2) throw new MatrixDimensionException();

			double[,] product = new double[Rows1 + 1, Cols2 + 1];

			for (int i = 0; i <= Rows1; i++)
			{
				for (int j = 0; j <= Cols2; j++)
				{
					for (int l = 0; l <= Cols1; l++)
					{
						MulAdd = MulAdd + array1[i, l] * array2[l, j];
					}
					product[i, j] = MulAdd;
					MulAdd = 0;
				}
			}
			return product;
		}

		/// <summary>
		/// Returns the multiplication of two matrices with compatible dimensions OR the cross-product of two vectors.
		/// </summary>
		/// <param name="Mat1">First matrix or vector (i.e: dimension [3,1]) object in multiplication</param>
		/// <param name="Mat2">Second matrix or vector (i.e: dimension [3,1]) object in multiplication</param>
		/// <returns>Mat1 multiplied by Mat2 as a Matrix object</returns>
		public static Matrix Multiply(Matrix Mat1, Matrix Mat2)
		{
			if ((Mat1.RowCount == 3) && (Mat2.RowCount == 3) &&
				(Mat1.ColumnCount == 1) && (Mat1.ColumnCount == 1))
			{ return new Matrix(CrossProduct(Mat1.m_Array, Mat2.m_Array)); }
			else
			{ return new Matrix(Multiply(Mat1.m_Array, Mat2.m_Array)); }
		}

		/// <summary>
		/// Returns the multiplication of two matrices with compatible
		/// dimensions OR the cross-product of two vectors.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat1">
		/// First matrix or vector (i.e: dimension [3,1]) object in
		/// multiplication
		/// </param>
		/// <param name="Mat2">
		/// Second matrix or vector (i.e: dimension [3,1]) object in
		/// multiplication
		/// </param>
		/// <returns>Mat1 multiplied by Mat2 as a Matrix object</returns>
		public static Matrix operator *(Matrix Mat1, Matrix Mat2)
		{
			/*
			if ((Mat1.RowCount==3) && (Mat2.RowCount==3) &&
				(Mat1.ColumnCount==1) && (Mat1.ColumnCount==1))
			{
				return new Matrix(CrossProduct(Mat1.m_Array,Mat2.m_Array));
			}
			else
			{
				return new Matrix(Multiply(Mat1.m_Array,Mat2.m_Array));
			}
            */

			Matrix matrix = new Matrix(Mat1.RowCount, Mat1.ColumnCount);

			for (int i = 0; i < Mat1.RowCount; i++)
			{
				matrix[i, 0] = Mat1[i, 0] * Mat2[0, 0] + Mat1[i, 1] * Mat2[1, 0] + Mat1[i, 2] * Mat2[2, 0] + Mat1[i, 3] * Mat2[3, 0];
				matrix[i, 1] = Mat1[i, 0] * Mat2[0, 1] + Mat1[i, 1] * Mat2[1, 1] + Mat1[i, 2] * Mat2[2, 1] + Mat1[i, 3] * Mat2[3, 1];
				matrix[i, 2] = Mat1[i, 0] * Mat2[0, 2] + Mat1[i, 1] * Mat2[1, 2] + Mat1[i, 2] * Mat2[2, 2] + Mat1[i, 3] * Mat2[3, 2];
				matrix[i, 3] = Mat1[i, 0] * Mat2[0, 3] + Mat1[i, 1] * Mat2[1, 3] + Mat1[i, 2] * Mat2[2, 3] + Mat1[i, 3] * Mat2[3, 3];
			}
			return matrix;
		}
		#endregion

		#region Determinant of a Matrix
		/// <summary>
		/// Returns the determinant of a matrix represented as a two-dimensional <see cref="System.Double" /> array.
		/// </summary>
		/// <param name="array">The two-dimensional <see cref="System.Double" /> array whose determinant is to be found.</param>
		/// <returns>A <see cref="System.Double" /> representing the determinant of the array.</returns>
		public static double GetDeterminant(double[,] array)
		{
			int S, k, k1, i, j;
			double[,] DArray;
			double save, ArrayK, tmpDet;
			int Rows, Cols;

			try
			{
				DArray = (double[,])array.Clone();
				GetNextArrayIndex(array, out Rows, out Cols);
			}
			catch
			{
				throw new MatrixNullException();
			}

			if (Rows != Cols) throw new MatrixNotSquareException();

			S = Rows;
			tmpDet = 1;

			for (k = 0; k <= S; k++)
			{
				if (DArray[k, k] == 0)
				{
					j = k;
					while ((j < S) && (DArray[k, j] == 0)) j = j + 1;
					if (DArray[k, j] == 0) return 0;
					else
					{
						for (i = k; i <= S; i++)
						{
							save = DArray[i, j];
							DArray[i, j] = DArray[i, k];
							DArray[i, k] = save;
						}
					}
					tmpDet = -tmpDet;
				}
				ArrayK = DArray[k, k];
				tmpDet = tmpDet * ArrayK;
				if (k < S)
				{
					k1 = k + 1;
					for (i = k1; i <= S; i++)
					{
						for (j = k1; j <= S; j++)
							DArray[i, j] = DArray[i, j] - DArray[i, k] * (DArray[k, j] / ArrayK);
					}
				}
			}
			return tmpDet;
		}

		/// <summary>
		/// Returns the determinant of the current <see cref="Matrix"/>.
		/// </summary>
		/// <returns>A <see cref="System.Double" /> representing the determinant of the current <see cref="Matrix" />.</returns>
		public double GetDeterminant()
		{
			return GetDeterminant(m_Array);
		}
		#endregion

		#region "Inverse of a Matrix"
		/// <summary>
		/// Returns the inverse of a matrix with [n,n] dimension
		/// and whose determinant is not zero.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">
		/// Array with [n,n] dimension whose inverse is to be found
		/// </param>
		/// <returns>Inverse of the array as an array</returns>
		public static double[,] Inverse(double[,] Mat)
		{
			double[,] AI, Mat1;
			double AIN, AF;
			int Rows, Cols;
			int LL, LLM, L1, L2, LC, LCA, LCB;

			try
			{
				GetNextArrayIndex(Mat, out Rows, out Cols);
				Mat1 = (double[,])Mat.Clone();
			}
			catch { throw new MatrixNullException(); }

			if (Rows != Cols) throw new MatrixNotSquareException();
			if (GetDeterminant(Mat) == 0) throw new MatrixDeterminentZeroException();

			LL = Rows;
			LLM = Cols;
			AI = new double[LL + 1, LL + 1];

			for (L2 = 0; L2 <= LL; L2++)
			{
				for (L1 = 0; L1 <= LL; L1++) AI[L1, L2] = 0;
				AI[L2, L2] = 1;
			}

			for (LC = 0; LC <= LL; LC++)
			{
				if (Math.Abs(Mat1[LC, LC]) < 0.0000000001)
				{
					for (LCA = LC + 1; LCA <= LL; LCA++)
					{
						if (LCA == LC) continue;
						if (Math.Abs(Mat1[LC, LCA]) > 0.0000000001)
						{
							for (LCB = 0; LCB <= LL; LCB++)
							{
								Mat1[LCB, LC] = Mat1[LCB, LC] + Mat1[LCB, LCA];
								AI[LCB, LC] = AI[LCB, LC] + AI[LCB, LCA];
							}
							break;
						}
					}
				}
				AIN = 1 / Mat1[LC, LC];
				for (LCA = 0; LCA <= LL; LCA++)
				{
					Mat1[LCA, LC] = AIN * Mat1[LCA, LC];
					AI[LCA, LC] = AIN * AI[LCA, LC];
				}

				for (LCA = 0; LCA <= LL; LCA++)
				{
					if (LCA == LC) continue;
					AF = Mat1[LC, LCA];
					for (LCB = 0; LCB <= LL; LCB++)
					{
						Mat1[LCB, LCA] = Mat1[LCB, LCA] - AF * Mat1[LCB, LC];
						AI[LCB, LCA] = AI[LCB, LCA] - AF * AI[LCB, LC];
					}
				}
			}
			return AI;
		}

		/// <summary>
		/// Returns the inverse of a matrix with [n,n] dimension
		/// and whose determinant is not zero.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">
		/// Matrix object with [n,n] dimension whose inverse is to be found
		/// </param>
		/// <returns>Inverse of the matrix as a Matrix object</returns>
		public static Matrix Inverse(Matrix Mat)
		{ return new Matrix(Inverse(Mat.m_Array)); }
		#endregion

		#region "Transpose of a Matrix"
		/// <summary>
		/// Returns the transpose of a matrix.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">Array whose transpose is to be found</param>
		/// <returns>Transpose of the array as an array</returns>
		public static double[,] Transpose(double[,] Mat)
		{
			double[,] Tr_Mat;
			int i, j, Rows, Cols;

			try { GetNextArrayIndex(Mat, out Rows, out Cols); }
			catch { throw new MatrixNullException(); }

			Tr_Mat = new double[Cols + 1, Rows + 1];

			for (i = 0; i <= Rows; i++)
				for (j = 0; j <= Cols; j++) Tr_Mat[j, i] = Mat[i, j];

			return Tr_Mat;
		}

		/// <summary>
		/// Returns the transpose of a matrix.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">Matrix object whose transpose is to be found</param>
		/// <returns>Transpose of the Matrix object as a Matrix object</returns>
		public static Matrix Transpose(Matrix Mat)
		{ return new Matrix(Transpose(Mat.m_Array)); }
		#endregion

		#region Singular Value Decomposition of a Matrix
		/// <summary>
		/// Evaluates the Singular Value Decomposition of a matrix,
		/// returns the  matrices S, U and V. Such that a given
		/// Matrix = U x S x V'.
		/// In case of an error the error is raised as an exception.
		/// Note: This method is based on the 'Singular Value Decomposition'
		/// section of Numerical Recipes in C by William H. Press,
		/// Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery,
		/// University of Cambridge Press 1992.
		/// </summary>
		/// <param name="array">Array whose SVD is to be computed</param>
		/// <param name="S_">An array where the S matrix is returned</param>
		/// <param name="U_">An array where the U matrix is returned</param>
		/// <param name="V_">An array where the V matrix is returned</param>
		public static void GetSingularValueDecomposition(double[,] array, out double[,] S_, out double[,] U_, out double[,] V_)
		{
			int Rows, Cols;
			int m, MP, n, NP;
			double[] w;
			double[,] A, v;

			try { GetNextArrayIndex(array, out Rows, out Cols); }
			catch { throw new MatrixNullException(); }

			m = Rows + 1;
			n = Cols + 1;

			if (m < n)
			{
				m = n;
				MP = NP = n;
			}
			else if (m > n)
			{
				n = m;
				NP = MP = m;
			}
			else
			{
				MP = m;
				NP = n;
			}

			A = new double[m + 1, n + 1];

			for (int row = 1; row <= Rows + 1; row++)
				for (int col = 1; col <= Cols + 1; col++)
				{ A[row, col] = array[row - 1, col - 1]; }

			const int NMAX = 100;
			v = new double[NP + 1, NP + 1];
			w = new double[NP + 1];

			int k, l, nm;
			int flag, i, its, j, jj;

			double[,] U_temp, S_temp, V_temp;
			double anorm, c, f, g, h, s, scale, x, y, z;
			double[] rv1 = new double[NMAX];

			l = 0;
			nm = 0;
			g = 0.0;
			scale = 0.0;
			anorm = 0.0;

			for (i = 1; i <= n; i++)
			{
				l = i + 1;
				rv1[i] = scale * g;
				g = s = scale = 0.0;
				if (i <= m)
				{
					for (k = i; k <= m; k++) scale += Math.Abs(A[k, i]);
					if (scale != 0)
					{
						for (k = i; k <= m; k++)
						{
							A[k, i] /= scale;
							s += A[k, i] * A[k, i];
						}
						f = A[i, i];
						g = -Sign(Math.Sqrt(s), f);
						h = f * g - s;
						A[i, i] = f - g;
						if (i != n)
						{
							for (j = l; j <= n; j++)
							{
								for (s = 0, k = i; k <= m; k++) s += A[k, i] * A[k, j];
								f = s / h;
								for (k = i; k <= m; k++) A[k, j] += f * A[k, i];
							}
						}
						for (k = i; k <= m; k++) A[k, i] *= scale;
					}
				}
				w[i] = scale * g;
				g = s = scale = 0.0;
				if (i <= m && i != n)
				{
					for (k = l; k <= n; k++) scale += Math.Abs(A[i, k]);
					if (scale != 0)
					{
						for (k = l; k <= n; k++)
						{
							A[i, k] /= scale;
							s += A[i, k] * A[i, k];
						}
						f = A[i, l];
						g = -Sign(Math.Sqrt(s), f);
						h = f * g - s;
						A[i, l] = f - g;
						for (k = l; k <= n; k++) rv1[k] = A[i, k] / h;
						if (i != m)
						{
							for (j = l; j <= m; j++)
							{
								for (s = 0.0, k = l; k <= n; k++) s += A[j, k] * A[i, k];
								for (k = l; k <= n; k++) A[j, k] += s * rv1[k];
							}
						}
						for (k = l; k <= n; k++) A[i, k] *= scale;
					}
				}
				anorm = Math.Max(anorm, (Math.Abs(w[i]) + Math.Abs(rv1[i])));
			}
			for (i = n; i >= 1; i--)
			{
				if (i < n)
				{
					if (g != 0)
					{
						for (j = l; j <= n; j++)
							v[j, i] = (A[i, j] / A[i, l]) / g;
						for (j = l; j <= n; j++)
						{
							for (s = 0.0, k = l; k <= n; k++) s += A[i, k] * v[k, j];
							for (k = l; k <= n; k++) v[k, j] += s * v[k, i];
						}
					}
					for (j = l; j <= n; j++) v[i, j] = v[j, i] = 0.0;
				}
				v[i, i] = 1.0;
				g = rv1[i];
				l = i;
			}
			for (i = n; i >= 1; i--)
			{
				l = i + 1;
				g = w[i];
				if (i < n)
					for (j = l; j <= n; j++) A[i, j] = 0.0;
				if (g != 0)
				{
					g = 1.0 / g;
					if (i != n)
					{
						for (j = l; j <= n; j++)
						{
							for (s = 0.0, k = l; k <= m; k++) s += A[k, i] * A[k, j];
							f = (s / A[i, i]) * g;
							for (k = i; k <= m; k++) A[k, j] += f * A[k, i];
						}
					}
					for (j = i; j <= m; j++) A[j, i] *= g;
				}
				else
				{
					for (j = i; j <= m; j++) A[j, i] = 0.0;
				}
				++A[i, i];
			}
			for (k = n; k >= 1; k--)
			{
				for (its = 1; its <= 30; its++)
				{
					flag = 1;
					for (l = k; l >= 1; l--)
					{
						nm = l - 1;
						if (Math.Abs(rv1[l]) + anorm == anorm)
						{
							flag = 0;
							break;
						}
						if (Math.Abs(w[nm]) + anorm == anorm) break;
					}
					if (flag != 0)
					{
						c = 0.0;
						s = 1.0;
						for (i = l; i <= k; i++)
						{
							f = s * rv1[i];
							if (Math.Abs(f) + anorm != anorm)
							{
								g = w[i];
								h = Pythagorean(f, g);
								w[i] = h;
								h = 1.0 / h;
								c = g * h;
								s = (-f * h);
								for (j = 1; j <= m; j++)
								{
									y = A[j, nm];
									z = A[j, i];
									A[j, nm] = y * c + z * s;
									A[j, i] = z * c - y * s;
								}
							}
						}
					}
					z = w[k];
					if (l == k)
					{
						if (z < 0.0)
						{
							w[k] = -z;
							for (j = 1; j <= n; j++) v[j, k] = (-v[j, k]);
						}
						break;
					}
					if (its == 30) Console.WriteLine("No convergence in 30 SVDCMP iterations");
					x = w[l];
					nm = k - 1;
					y = w[nm];
					g = rv1[nm];
					h = rv1[k];
					f = ((y - z) * (y + z) + (g - h) * (g + h)) / (2.0 * h * y);
					g = Pythagorean(f, 1.0);
					f = ((x - z) * (x + z) + h * ((y / (f + Sign(g, f))) - h)) / x;
					c = s = 1.0;
					for (j = l; j <= nm; j++)
					{
						i = j + 1;
						g = rv1[i];
						y = w[i];
						h = s * g;
						g = c * g;
						z = Pythagorean(f, h);
						rv1[j] = z;
						c = f / z;
						s = h / z;
						f = x * c + g * s;
						g = g * c - x * s;
						h = y * s;
						y = y * c;
						for (jj = 1; jj <= n; jj++)
						{
							x = v[jj, j];
							z = v[jj, i];
							v[jj, j] = x * c + z * s;
							v[jj, i] = z * c - x * s;
						}
						z = Pythagorean(f, h);
						w[j] = z;
						if (z != 0)
						{
							z = 1.0 / z;
							c = f * z;
							s = h * z;
						}
						f = (c * g) + (s * y);
						x = (c * y) - (s * g);
						for (jj = 1; jj <= m; jj++)
						{
							y = A[jj, j];
							z = A[jj, i];
							A[jj, j] = y * c + z * s;
							A[jj, i] = z * c - y * s;
						}
					}
					rv1[l] = 0.0;
					rv1[k] = f;
					w[k] = x;
				}
			}

			S_temp = new double[NP, NP];
			V_temp = new double[NP, NP];
			U_temp = new double[MP, NP];

			for (i = 1; i <= NP; i++) S_temp[i - 1, i - 1] = w[i];

			S_ = S_temp;

			for (i = 1; i <= NP; i++)
				for (j = 1; j <= NP; j++) V_temp[i - 1, j - 1] = v[i, j];

			V_ = V_temp;

			for (i = 1; i <= MP; i++)
				for (j = 1; j <= NP; j++) U_temp[i - 1, j - 1] = A[i, j];

			U_ = U_temp;
		}

		private static double Sign(double a, double b)
		{
			if (b >= 0.0) { return Math.Abs(a); }
			else { return -Math.Abs(a); }
		}

		private static double Pythagorean(double a, double b)
		{
			double absa, absb;

			absa = Math.Abs(a);
			absb = Math.Abs(b);
			if (absa > absb) return absa * Math.Sqrt(1.0 + Math.Pow((absb / absa), 2));
			else return (absb == 0.0 ? 0.0 : absb * Math.Sqrt(1.0 + Math.Pow((absa / absb), 2)));
		}

		/// <summary>
		/// Evaluates the Singular Value Decomposition of a matrix,
		/// returns the  matrices S, U and V. Such that a given
		/// Matrix = U x S x V'.
		/// In case of an error the error is raised as an exception.
		/// Note: This method is based on the 'Singular Value Decomposition'
		/// section of Numerical Recipes in C by William H. Press,
		/// Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery,
		/// University of Cambridge Press 1992.
		/// </summary>
		/// <param name="Mat">Matrix object whose SVD is to be computed</param>
		/// <param name="S">A Matrix object where the S matrix is returned</param>
		/// <param name="U">A Matrix object where the U matrix is returned</param>
		/// <param name="V">A Matrix object where the V matrix is returned</param>
		public static void SVD(Matrix Mat, out Matrix S, out Matrix U, out Matrix V)
		{
			double[,] s, u, v;
			GetSingularValueDecomposition(Mat.m_Array, out s, out u, out v);
			S = new Matrix(s);
			U = new Matrix(u);
			V = new Matrix(v);
		}
		#endregion

		#region LU Decomposition of a matrix
		/// <summary>
		/// Returns the LU Decomposition of a matrix.
		/// the output is: lower triangular matrix L, upper
		/// triangular matrix U, and permutation matrix P so that
		///	P*X = L*U.
		/// In case of an error the error is raised as an exception.
		/// Note: This method is based on the 'LU Decomposition and Its Applications'
		/// section of Numerical Recipes in C by William H. Press,
		/// Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery,
		/// University of Cambridge Press 1992.
		/// </summary>
		/// <param name="Mat">Array which will be LU Decomposed</param>
		/// <param name="L">An array where the lower traingular matrix is returned</param>
		/// <param name="U">An array where the upper traingular matrix is returned</param>
		/// <param name="P">An array where the permutation matrix is returned</param>
		public static void GetLUDecomposition(double[,] Mat, out double[,] L, out double[,] U, out double[,] P)
		{
			double[,] A;
			int i, j, k, Rows, Cols;

			try
			{
				A = (double[,])Mat.Clone();
				GetNextArrayIndex(Mat, out Rows, out Cols);
			}
			catch { throw new MatrixNullException(); }

			if (Rows != Cols) throw new MatrixNotSquareException();

			int IMAX = 0, N = Rows;
			double AAMAX, Sum, Dum, TINY = 1E-20;

			int[] INDX = new int[N + 1];
			double[] VV = new double[N * 10];
			double D = 1.0;

			for (i = 0; i <= N; i++)
			{
				AAMAX = 0.0;
				for (j = 0; j <= N; j++)
					if (Math.Abs(A[i, j]) > AAMAX) AAMAX = Math.Abs(A[i, j]);
				if (AAMAX == 0.0) throw new MatrixSingularException();
				VV[i] = 1.0 / AAMAX;
			}
			for (j = 0; j <= N; j++)
			{
				if (j > 0)
				{
					for (i = 0; i <= (j - 1); i++)
					{
						Sum = A[i, j];
						if (i > 0)
						{
							for (k = 0; k <= (i - 1); k++)
								Sum = Sum - A[i, k] * A[k, j];
							A[i, j] = Sum;
						}
					}
				}
				AAMAX = 0.0;
				for (i = j; i <= N; i++)
				{
					Sum = A[i, j];
					if (j > 0)
					{
						for (k = 0; k <= (j - 1); k++)
							Sum = Sum - A[i, k] * A[k, j];
						A[i, j] = Sum;
					}
					Dum = VV[i] * Math.Abs(Sum);
					if (Dum >= AAMAX)
					{
						IMAX = i;
						AAMAX = Dum;
					}
				}
				if (j != IMAX)
				{
					for (k = 0; k <= N; k++)
					{
						Dum = A[IMAX, k];
						A[IMAX, k] = A[j, k];
						A[j, k] = Dum;
					}
					D = -D;
					VV[IMAX] = VV[j];
				}
				INDX[j] = IMAX;
				if (j != N)
				{
					if (A[j, j] == 0.0) A[j, j] = TINY;
					Dum = 1.0 / A[j, j];
					for (i = j + 1; i <= N; i++)
						A[i, j] = A[i, j] * Dum;

				}
			}

			if (A[N, N] == 0.0) A[N, N] = TINY;

			int count = 0;
			double[,] l = new double[N + 1, N + 1];
			double[,] u = new double[N + 1, N + 1];

			for (i = 0; i <= N; i++)
			{
				for (j = 0; j <= count; j++)
				{
					if (i != 0) l[i, j] = A[i, j];
					if (i == j) l[i, j] = 1.0;
					u[N - i, N - j] = A[N - i, N - j];
				}
				count++;
			}

			L = l;
			U = u;

			P = Identity(N + 1);
			for (i = 0; i <= N; i++)
			{
				SwapRows(P, i, INDX[i]);
			}
		}

		private static void SwapRows(double[,] Mat, int Row, int toRow)
		{
			int N = Mat.GetUpperBound(0);
			double[,] dumArray = new double[1, N + 1];
			for (int i = 0; i <= N; i++)
			{
				dumArray[0, i] = Mat[Row, i];
				Mat[Row, i] = Mat[toRow, i];
				Mat[toRow, i] = dumArray[0, i];
			}
		}

		/// <summary>
		/// Returns the LU Decomposition of a matrix.
		/// the output is: lower triangular matrix L, upper
		/// triangular matrix U, and permutation matrix P so that
		///	P*X = L*U.
		/// In case of an error the error is raised as an exception.
		/// Note: This method is based on the 'LU Decomposition and Its Applications'
		/// section of Numerical Recipes in C by William H. Press,
		/// Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery,
		/// University of Cambridge Press 1992.
		/// </summary>
		/// <param name="Mat">Matrix object which will be LU Decomposed</param>
		/// <param name="L">A Matrix object where the lower traingular matrix is returned</param>
		/// <param name="U">A Matrix object where the upper traingular matrix is returned</param>
		/// <param name="P">A Matrix object where the permutation matrix is returned</param>
		public static void GetLUDecomposition(Matrix Mat, out Matrix L, out Matrix U, out Matrix P)
		{
			double[,] l, u, p;
			GetLUDecomposition(Mat.m_Array, out l, out u, out p);
			L = new Matrix(l);
			U = new Matrix(u);
			P = new Matrix(p);
		}
		#endregion

		#region Solve system of linear equations
		/// <summary>
		/// Solves a set of n linear equations A.X = B, and returns X, where A is [n,n] and B is [n,1].
		/// </summary>
		/// <param name="arrayA">The array 'A' on the left side of the equations A.X = B</param>
		/// <param name="arrayB">The array 'B' on the right side of the equations A.X = B</param>
		/// <returns>Array 'X' in the system of equations A.X = B</returns>
		/// <remarks>In the same manner if you need to compute: inverse(A).B, it is better to use this method instead, as it is much faster. This method is based on the 'LU Decomposition and Its Applications' section of Numerical Recipes in C by William H. Press, Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery, University of Cambridge Press 1992.</remarks>
		public static double[,] SolveLinearEquationSystem(double[,] arrayA, double[,] arrayB)
		{
			double[,] A;
			double[,] B;
			double SUM;
			int i, ii, j, k, ll, Rows, Cols;

			#region "LU Decompose"
			try
			{
				A = (double[,])arrayA.Clone();
				B = (double[,])arrayB.Clone();
				GetNextArrayIndex(A, out Rows, out Cols);
			}
			catch { throw new MatrixNullException(); }

			if (Rows != Cols) throw new MatrixNotSquareException();
			if ((B.GetUpperBound(0) != Rows) || (B.GetUpperBound(1) != 0))
				throw new MatrixDimensionException();

			int IMAX = 0, N = Rows;
			double AAMAX, Sum, Dum, TINY = 1E-20;

			int[] INDX = new int[N + 1];
			double[] VV = new double[N * 10];
			double D = 1.0;

			for (i = 0; i <= N; i++)
			{
				AAMAX = 0.0;
				for (j = 0; j <= N; j++)
					if (Math.Abs(A[i, j]) > AAMAX) AAMAX = Math.Abs(A[i, j]);
				if (AAMAX == 0.0) throw new MatrixSingularException();
				VV[i] = 1.0 / AAMAX;
			}
			for (j = 0; j <= N; j++)
			{
				if (j > 0)
				{
					for (i = 0; i <= (j - 1); i++)
					{
						Sum = A[i, j];
						if (i > 0)
						{
							for (k = 0; k <= (i - 1); k++)
								Sum = Sum - A[i, k] * A[k, j];
							A[i, j] = Sum;
						}
					}
				}
				AAMAX = 0.0;
				for (i = j; i <= N; i++)
				{
					Sum = A[i, j];
					if (j > 0)
					{
						for (k = 0; k <= (j - 1); k++)
							Sum = Sum - A[i, k] * A[k, j];
						A[i, j] = Sum;
					}
					Dum = VV[i] * Math.Abs(Sum);
					if (Dum >= AAMAX)
					{
						IMAX = i;
						AAMAX = Dum;
					}
				}
				if (j != IMAX)
				{
					for (k = 0; k <= N; k++)
					{
						Dum = A[IMAX, k];
						A[IMAX, k] = A[j, k];
						A[j, k] = Dum;
					}
					D = -D;
					VV[IMAX] = VV[j];
				}
				INDX[j] = IMAX;
				if (j != N)
				{
					if (A[j, j] == 0.0) A[j, j] = TINY;
					Dum = 1.0 / A[j, j];
					for (i = j + 1; i <= N; i++)
						A[i, j] = A[i, j] * Dum;

				}
			}
			if (A[N, N] == 0.0) A[N, N] = TINY;
			#endregion

			ii = -1;
			for (i = 0; i <= N; i++)
			{
				ll = INDX[i];
				SUM = B[ll, 0];
				B[ll, 0] = B[i, 0];
				if (ii != -1)
				{
					for (j = ii; j <= i - 1; j++) SUM = SUM - A[i, j] * B[j, 0];
				}
				else if (SUM != 0) ii = i;
				B[i, 0] = SUM;
			}
			for (i = N; i >= 0; i--)
			{
				SUM = B[i, 0];
				if (i < N)
				{
					for (j = i + 1; j <= N; j++) SUM = SUM - A[i, j] * B[j, 0];
				}
				B[i, 0] = SUM / A[i, i];
			}
			return B;
		}

		/// <summary>
		/// Solves a set of n linear equations A.X = B, and returns X, where A is [n,n] and B is [n,1].
		/// </summary>
		/// <param name="matrixA">The <see cref="Matrix" /> object 'A' on the left side of the equations A.X = B</param>
		/// <param name="matrixB">The <see cref="Matrix" /> object 'B' on the right side of the equations A.X = B</param>
		/// <returns>Matrix object 'X' in the system of equations A.X = B</returns>
		/// <remarks>In the same manner if you need to compute: inverse(A).B, it is better to use this method instead, as it is much faster. This method is based on the 'LU Decomposition and Its Applications' section of Numerical Recipes in C by William H. Press, Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery, University of Cambridge Press 1992.</remarks>
		public static Matrix SolveLinearEquationSystem(Matrix matrixA, Matrix matrixB)
		{
			return new Matrix(Matrix.SolveLinearEquationSystem(matrixA.m_Array, matrixB.m_Array));
		}
		#endregion

		#region "Rank of a matrix"
		/// <summary>
		/// Returns the rank of a matrix.
		/// </summary>
		/// <param name="Mat">The two-dimensional <see cref="System.Double" /> array whose rank is to be found</param>
		/// <returns>The rank of the array</returns>
		public static int Rank(double[,] Mat)
		{
			int r = 0;
			double[,] S, U, V;
			try
			{
				int Rows, Cols;
				GetNextArrayIndex(Mat, out Rows, out Cols);
			}
			catch { throw new MatrixNullException(); }
			double EPS = 2.2204E-16;
			GetSingularValueDecomposition(Mat, out S, out U, out V);

			for (int i = 0; i <= S.GetUpperBound(0); i++)
			{ if (Math.Abs(S[i, i]) > EPS) r++; }

			return r;
		}

		/// <summary>
		/// Returns the rank of a matrix.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">a Matrix object whose rank is to be found</param>
		/// <returns>The rank of the Matrix object</returns>
		public static int Rank(Matrix Mat)
		{ return Rank(Mat.m_Array); }
		#endregion

		#region "Pseudoinverse of a matrix"
		/// <summary>
		/// Returns the pseudoinverse of a matrix, such that
		/// X = PINV(A) produces a matrix 'X' of the same dimensions
		/// as A' so that A*X*A = A, X*A*X = X.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">An array whose pseudoinverse is to be found</param>
		/// <returns>The pseudoinverse of the array as an array</returns>
		public static double[,] PINV(double[,] Mat)
		{
			double[,] Matrix;
			int i, m, n, j, l;
			double[,] S, Part_I, Part_II;
			double EPS, MulAdd, Tol, Largest_Item = 0;

			double[,] svdU, svdS, svdV;

			try
			{
				Matrix = (double[,])Mat.Clone();
				GetNextArrayIndex(Mat, out m, out n);
			}
			catch { throw new MatrixNullException(); }

			GetSingularValueDecomposition(Mat, out svdS, out svdU, out svdV);

			EPS = 2.2204E-16;
			m++;
			n++;

			Part_I = new double[m, n];
			Part_II = new double[m, n];
			S = new Double[n, n];

			MulAdd = 0;
			for (i = 0; i <= svdS.GetUpperBound(0); i++)
			{
				if (i == 0) Largest_Item = svdS[0, 0];
				if (Largest_Item < svdS[i, i]) Largest_Item = svdS[i, i];
			}

			if (m > n) Tol = m * Largest_Item * EPS;
			else Tol = n * Largest_Item * EPS;

			for (i = 0; i < n; i++) S[i, i] = svdS[i, i];

			for (i = 0; i < m; i++)
			{
				for (j = 0; j < n; j++)
				{
					for (l = 0; l < n; l++)
					{
						if (S[l, j] > Tol) MulAdd += svdU[i, l] * (1 / S[l, j]);
					}
					Part_I[i, j] = MulAdd;
					MulAdd = 0;
				}
			}

			for (i = 0; i < m; i++)
			{
				for (j = 0; j < n; j++)
				{
					for (l = 0; l < n; l++)
					{
						MulAdd += Part_I[i, l] * svdV[j, l];
					}
					Part_II[i, j] = MulAdd;
					MulAdd = 0;
				}
			}
			return Transpose(Part_II);
		}

		/// <summary>
		/// Returns the pseudoinverse of a matrix, such that
		/// X = PINV(A) produces a matrix 'X' of the same dimensions
		/// as A' so that A*X*A = A, X*A*X = X.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">a Matrix object whose pseudoinverse is to be found</param>
		/// <returns>The pseudoinverse of the Matrix object as a Matrix Object</returns>
		public static Matrix PINV(Matrix Mat)
		{ return new Matrix(PINV(Mat.m_Array)); }
		#endregion

		#region Eigen Values and Vectors of Symmetric Matrix
		/// <summary>
		/// Returns the eigenvalues and eigenvectors of a real symmetric matrix defined as a two-dimensional <see cref="System.Double" /> array.
		/// </summary>
		/// <param name="array">The array whose eigenvalues and eigenvectors are to be found</param>
		/// <param name="eigenValues">An array where the eigenvalues are returned</param>
		/// <param name="eigenVectors">An array where the eigenvectors are returned</param>
		/// <remarks>This method is based on the 'Eigenvalues and Eigenvectors of a TridiagonalMatrix' section of "Numerical Recipes in C" by William H. Press, Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery, University of Cambridge Press 1992.</remarks>
		public static void GetEigen(double[,] array, out double[,] eigenValues, out double[,] eigenVectors)
		{
			double[,] a;
			int Rows, Cols;
			try
			{
				GetNextArrayIndex(array, out Rows, out Cols);
				a = (double[,])array.Clone();
			}
			catch { throw new MatrixNullException(); }

			if (Rows != Cols) throw new MatrixNotSquareException();

			int j, iq, ip, i, n, nrot;
			double tresh, theta, tau, t, sm, s, h, g, c;
			double[] b, z;

			n = Rows;
			eigenValues = new double[n + 1, 1];
			eigenVectors = new double[n + 1, n + 1];

			b = new double[n + 1];
			z = new double[n + 1];
			for (ip = 0; ip <= n; ip++)
			{
				for (iq = 0; iq <= n; iq++) eigenVectors[ip, iq] = 0.0;
				eigenVectors[ip, ip] = 1.0;
			}
			for (ip = 0; ip <= n; ip++)
			{
				b[ip] = eigenValues[ip, 0] = a[ip, ip];
				z[ip] = 0.0;
			}

			nrot = 0;
			for (i = 0; i <= 50; i++)
			{
				sm = 0.0;
				for (ip = 0; ip <= n - 1; ip++)
				{
					for (iq = ip + 1; iq <= n; iq++)
						sm += Math.Abs(a[ip, iq]);
				}
				if (sm == 0.0)
				{
					return;
				}
				if (i < 4)
					tresh = 0.2 * sm / (n * n);
				else
					tresh = 0.0;
				for (ip = 0; ip <= n - 1; ip++)
				{
					for (iq = ip + 1; iq <= n; iq++)
					{
						g = 100.0 * Math.Abs(a[ip, iq]);
						if (i > 4 && (double)(Math.Abs(eigenValues[ip, 0]) + g) == (double)Math.Abs(eigenValues[ip, 0])
							&& (double)(Math.Abs(eigenValues[iq, 0]) + g) == (double)Math.Abs(eigenValues[iq, 0]))
							a[ip, iq] = 0.0;
						else if (Math.Abs(a[ip, iq]) > tresh)
						{
							h = eigenValues[iq, 0] - eigenValues[ip, 0];
							if ((double)(Math.Abs(h) + g) == (double)Math.Abs(h))
								t = (a[ip, iq]) / h;
							else
							{
								theta = 0.5 * h / (a[ip, iq]);
								t = 1.0 / (Math.Abs(theta) + Math.Sqrt(1.0 + theta * theta));
								if (theta < 0.0) t = -t;
							}
							c = 1.0 / Math.Sqrt(1 + t * t);
							s = t * c;
							tau = s / (1.0 + c);
							h = t * a[ip, iq];
							z[ip] -= h;
							z[iq] += h;
							eigenValues[ip, 0] -= h;
							eigenValues[iq, 0] += h;
							a[ip, iq] = 0.0;
							for (j = 0; j <= ip - 1; j++)
							{
								ROT(g, h, s, tau, a, j, ip, j, iq);
							}
							for (j = ip + 1; j <= iq - 1; j++)
							{
								ROT(g, h, s, tau, a, ip, j, j, iq);
							}
							for (j = iq + 1; j <= n; j++)
							{
								ROT(g, h, s, tau, a, ip, j, iq, j);
							}
							for (j = 0; j <= n; j++)
							{
								ROT(g, h, s, tau, eigenVectors, j, ip, j, iq);
							}
							++(nrot);
						}
					}
				}
				for (ip = 0; ip <= n; ip++)
				{
					b[ip] += z[ip];
					eigenValues[ip, 0] = b[ip];
					z[ip] = 0.0;
				}
			}
			throw new OutOfMemoryException("Too many iterations");
		}

		private static void ROT(double g, double h, double s, double tau,
			double[,] a, int i, int j, int k, int l)
		{
			g = a[i, j]; h = a[k, l];
			a[i, j] = g - s * (h + g * tau);
			a[k, l] = h + s * (g - h * tau);
		}

		/// <summary>
		/// Returns the eigenvalues and eigenvectors of the current real symmetric <see cref="Matrix" /> object.
		/// </summary>
		/// <param name="eigenValues">A <see cref="Matrix" /> object where the eigenvalues are returned</param>
		/// <param name="eigenVectors">A <see cref="Matrix" /> object where the eigenvectors are returned</param>
		/// <remarks>This method is based on the 'Eigenvalues and Eigenvectors of a TridiagonalMatrix' section of Numerical Recipes in C by William H. Press, Saul A. Teukolsky, William T. Vetterling and Brian P. Flannery, University of Cambridge Press 1992.</remarks>
		public void GetEigen(out Matrix eigenValues, out Matrix eigenVectors)
		{
			double[,] values, vectors;
			GetEigen(m_Array, out values, out vectors);
			eigenValues = new Matrix(values);
			eigenVectors = new Matrix(vectors);
		}
		#endregion

		#region "Multiply a matrix or a vector with a scalar quantity"
		/// <summary>
		/// Returns the multiplication of a matrix or a vector (i.e
		/// dimension [3,1]) with a scalar quantity.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Value">The scalar value to multiply the array</param>
		/// <param name="Mat">Array which is to be multiplied by a scalar</param>
		/// <returns>The multiplication of the scalar and the array as an array</returns>
		public static double[,] ScalarMultiply(double Value, double[,] Mat)
		{
			int i, j, Rows, Cols;
			double[,] sol;

			try { GetNextArrayIndex(Mat, out Rows, out Cols); }
			catch { throw new MatrixNullException(); }
			sol = new double[Rows + 1, Cols + 1];

			for (i = 0; i <= Rows; i++)
				for (j = 0; j <= Cols; j++)
					sol[i, j] = Mat[i, j] * Value;

			return sol;
		}

		/// <summary>
		/// Returns the multiplication of a matrix or a vector (i.e
		/// dimension [3,1]) with a scalar quantity.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Value">The scalar value to multiply the array</param>
		/// <param name="Mat">Matrix which is to be multiplied by a scalar</param>
		/// <returns>The multiplication of the scalar and the array as an array</returns>
		public static Matrix ScalarMultiply(double Value, Matrix Mat)
		{ return new Matrix(ScalarMultiply(Value, Mat.m_Array)); }

		/// <summary>
		/// Returns the multiplication of a matrix or a vector (i.e
		/// dimension [3,1]) with a scalar quantity.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Mat">Matrix object which is to be multiplied by a scalar</param>
		/// <param name="Value">The scalar value to multiply the Matrix object</param>
		/// <returns>
		/// The multiplication of the scalar and the Matrix object as a
		/// Matrix object
		/// </returns>
		public static Matrix operator *(Matrix Mat, double Value)
		{ return new Matrix(ScalarMultiply(Value, Mat.m_Array)); }

		/// <summary>
		/// Returns the multiplication of a matrix or a vector (i.e
		/// dimension [3,1]) with a scalar quantity.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Value">The scalar value to multiply the Matrix object</param>
		/// <param name="Mat">Matrix object which is to be multiplied by a scalar</param>
		/// <returns>
		/// The multiplication of the scalar and the Matrix object as a
		/// Matrix object
		/// </returns>
		public static Matrix operator *(double Value, Matrix Mat)
		{ return new Matrix(ScalarMultiply(Value, Mat.m_Array)); }
		#endregion

		#region "Divide a matrix or a vector with a scalar quantity"
		/// <summary>
		/// Returns the division of a matrix or a vector (i.e
		/// dimension [3,1]) by a scalar quantity.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Value">The scalar value to divide the array with</param>
		/// <param name="Mat">Array which is to be divided by a scalar</param>
		/// <returns>The division of the array and the scalar as an array</returns>
		public static double[,] ScalarDivide(double Value, double[,] Mat)
		{
			int i, j, Rows, Cols;
			double[,] sol;

			try { GetNextArrayIndex(Mat, out Rows, out Cols); }
			catch { throw new MatrixNullException(); }

			sol = new double[Rows + 1, Cols + 1];

			for (i = 0; i <= Rows; i++)
				for (j = 0; j <= Cols; j++)
					sol[i, j] = Mat[i, j] / Value;

			return sol;
		}

		/// <summary>
		/// Returns the division of a matrix or a vector (i.e
		/// dimension [3,1]) by a scalar quantity.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Value">The scalar value to divide the array with</param>
		/// <param name="Mat">Matrix which is to be divided by a scalar</param>
		/// <returns>The division of the array and the scalar as an array</returns>
		public static Matrix ScalarDivide(double Value, Matrix Mat)
		{ return new Matrix(ScalarDivide(Value, Mat.m_Array)); }

		/// <summary>
		/// Returns the division of a matrix or a vector (i.e
		/// dimension [3,1]) by a scalar quantity.
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="Value">The scalar value to divide the Matrix object with</param>
		/// <param name="Mat">Matrix object which is to be divided by a scalar</param>
		/// <returns>
		/// The division of the Matrix object and the scalar as a Matrix object
		/// </returns>
		public static Matrix operator /(Matrix Mat, double Value)
		{ return new Matrix(ScalarDivide(Value, Mat.m_Array)); }
		#endregion

		#region "Vectors Cross Product"
		/// <summary>
		/// Returns the cross product of two vectors whose
		/// dimensions should be [3] or [3,1].
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="V1">First vector array (dimension [3]) in the cross product</param>
		/// <param name="V2">Second vector array (dimension [3]) in the cross product</param>
		/// <returns>Cross product of V1 and V2 as an array (dimension [3])</returns>
		public static double[] CrossProduct(double[] V1, double[] V2)
		{
			double i, j, k;
			double[] sol = new double[2];
			int Rows1;
			int Rows2;

			try
			{
				GetNextArrayIndex(V1, out Rows1);
				GetNextArrayIndex(V2, out Rows2);
			}
			catch
			{
				throw new MatrixNullException();
			}

			if (Rows1 != 2) throw new MatrixDimensionException();

			if (Rows2 != 2) throw new MatrixDimensionException();

			i = V1[1] * V2[2] - V1[2] * V2[1];
			j = V1[2] * V2[0] - V1[0] * V2[2];
			k = V1[0] * V2[1] - V1[1] * V2[0];

			sol[0] = i; sol[1] = j; sol[2] = k;

			return sol;
		}

		/// <summary>
		/// Returns the cross product of two vectors whose
		/// dimensions should be [3] or [3x1].
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="V1">First vector array (dimensions [3,1]) in the cross product</param>
		/// <param name="V2">Second vector array (dimensions [3,1]) in the cross product</param>
		/// <returns>Cross product of V1 and V2 as an array (dimension [3,1])</returns>
		public static double[,] CrossProduct(double[,] V1, double[,] V2)
		{
			double i, j, k;
			double[,] sol = new double[3, 1];
			int Rows1, Cols1;
			int Rows2, Cols2;

			try
			{
				GetNextArrayIndex(V1, out Rows1, out Cols1);
				GetNextArrayIndex(V2, out Rows2, out Cols2);
			}
			catch { throw new MatrixNullException(); }

			if (Rows1 != 2 || Cols1 != 0) throw new MatrixDimensionException();

			if (Rows2 != 2 || Cols2 != 0) throw new MatrixDimensionException();

			i = V1[1, 0] * V2[2, 0] - V1[2, 0] * V2[1, 0];
			j = V1[2, 0] * V2[0, 0] - V1[0, 0] * V2[2, 0];
			k = V1[0, 0] * V2[1, 0] - V1[1, 0] * V2[0, 0];

			sol[0, 0] = i; sol[1, 0] = j; sol[2, 0] = k;

			return sol;
		}

		/// <summary>
		/// Returns the cross product of two vectors whose
		/// dimensions should be [3] or [3x1].
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="V1">First Matrix (dimensions [3,1]) in the cross product</param>
		/// <param name="V2">Second Matrix (dimensions [3,1]) in the cross product</param>
		/// <returns>Cross product of V1 and V2 as a matrix (dimension [3,1])</returns>
		public static Matrix CrossProduct(Matrix V1, Matrix V2)
		{ return (new Matrix((CrossProduct(V1.m_Array, V2.m_Array)))); }
		#endregion

		#region Vectors Dot Product
		/// <summary>
		/// Returns the dot product of two vectors whose
		/// dimensions should be [3] or [3,1].
		/// </summary>
		/// <param name="V1">First vector array (dimension [3]) in the dot product</param>
		/// <param name="V2">Second vector array (dimension [3]) in the dot product</param>
		/// <returns>Dot product of V1 and V2</returns>
		public static double DotProduct(double[] V1, double[] V2)
		{
			int Rows1;
			int Rows2;

			int Cols1;
			int Cols2;

			try
			{
				GetNextArrayIndex(V1, out Rows1);
				GetNextArrayIndex(V2, out Rows2);
			}
			catch { throw new MatrixNullException(); }

			if (Rows1 != 2) throw new MatrixDimensionException();

			if (Rows2 != 2) throw new MatrixDimensionException();

			return (V1[0] * V2[0] + V1[1] * V2[1] + V1[2] * V2[2]);
		}

		/// <summary>
		/// Returns the dot product of two vectors whose
		/// dimensions should be [3] or [3,1].
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="V1">First vector array (dimension [3,1]) in the dot product</param>
		/// <param name="V2">Second vector array (dimension [3,1]) in the dot product</param>
		/// <returns>Dot product of V1 and V2</returns>
		public static double DotProduct(double[,] V1, double[,] V2)
		{
			int Rows1, Cols1;
			int Rows2, Cols2;

			try
			{
				GetNextArrayIndex(V1, out Rows1, out Cols1);
				GetNextArrayIndex(V2, out Rows2, out Cols2);
			}
			catch { throw new MatrixNullException(); }

			if (Rows1 != 2 || Cols1 != 0) throw new MatrixVectorDimensionException();

			if (Rows2 != 2 || Cols2 != 0) throw new MatrixVectorDimensionException();

			return (V1[0, 0] * V2[0, 0] + V1[1, 0] * V2[1, 0] + V1[2, 0] * V2[2, 0]);
		}

		/// <summary>
		/// Returns the dot product of two vectors whose
		/// dimensions should be [3] or [3,1].
		/// In case of an error the error is raised as an exception.
		/// </summary>
		/// <param name="V1">First Matrix object (dimension [3,1]) in the dot product</param>
		/// <param name="V2">Second Matrix object (dimension [3,1]) in the dot product</param>
		/// <returns>Dot product of V1 and V2</returns>
		public static double DotProduct(Matrix V1, Matrix V2)
		{ return (DotProduct(V1.m_Array, V2.m_Array)); }
		#endregion

		#region Vector Magnitude Calculation
		/// <summary>
		/// Returns the magnitude of a vector whose dimension is [3] or [3,1].
		/// </summary>
		/// <param name="V">The vector array (dimension [3]) whose magnitude is to be found</param>
		/// <returns>The magnitude of the vector array</returns>
		public static double GetVectorMagnitude(double[] V)
		{
			int Rows;

			try
			{
				GetNextArrayIndex(V, out Rows);
			}
			catch
			{
				throw new MatrixNullException();
			}

			if (Rows != 2) throw new MatrixDimensionException();

			return Math.Sqrt(V[0] * V[0] + V[1] * V[1] + V[2] * V[2]);
		}

		/// <summary>
		/// Returns the magnitude of a <see cref="Matrix" /> vector represented as a two-dimensional <see cref="System.Double" /> array whose dimension is [3] or [3,1].
		/// </summary>
		/// <param name="array">The two-dimensional <see cref="System.Double" /> array representing the vector of dimension [3,1] whose magnitude is to be found.</param>
		/// <returns>The magnitude of the <see cref="System.Double" /> vector array</returns>
		public static double GetVectorMagnitude(double[,] array)
		{
			int Rows, Cols;

			try
			{
				GetNextArrayIndex(array, out Rows, out Cols);
			}
			catch
			{
				throw new MatrixNullException();
			}

			if (Rows != 2 || Cols != 0) throw new MatrixVectorDimensionException();

			return Math.Sqrt(array[0, 0] * array[0, 0] + array[1, 0] * array[1, 0] + array[2, 0] * array[2, 0]);
		}

		/// <summary>
		/// Returns the magnitude of a <see cref="Matrix" /> vector whose dimension is [3] or [3,1].
		/// </summary>
		/// <returns>The magnitude of the <see cref="Matrix" /> vector</returns>
		public double GetVectorMagnitude()
		{
			return (Matrix.GetVectorMagnitude(m_Array));
		}
		#endregion

		#region Comparison
		/// <summary>
		/// Determines whether two specified two-dimensional <see cref="System.Double" /> arrays are equal in value.
		/// </summary>
		/// <param name="Mat1">First array in equality check</param>
		/// <param name="Mat2">Second array in equality check</param>
		/// <returns>True if both two-dimensional <see cref="System.Double" /> arrays are equal; false otherwise.</returns>
		public static bool IsEqual(double[,] Mat1, double[,] Mat2)
		{
			double eps = 1E-14;
			int Rows1, Cols1;
			int Rows2, Cols2;

			try
			{
				GetNextArrayIndex(Mat1, out Rows1, out Cols1);
				GetNextArrayIndex(Mat2, out Rows2, out Cols2);
			}
			catch
			{
				throw new MatrixNullException();
			}
			if (Rows1 != Rows2 || Cols1 != Cols2) throw new MatrixDimensionException();

			for (int i = 0; i <= Rows1; i++)
			{
				for (int j = 0; j <= Cols1; j++)
				{
					if (Math.Abs(Mat1[i, j] - Mat2[i, j]) > eps) return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Determines whether two <see cref="Matrix" /> objects of equal dimensions are equal in value.
		/// </summary>
		/// <param name="Mat1">First Matrix object in equality check</param>
		/// <param name="Mat2">Second Matrix object in equality check</param>
		/// <returns>True if the two <see cref="Matrix" /> objects are equal; false otherwise.</returns>
		public static bool operator ==(Matrix Mat1, Matrix Mat2)
		{
			return IsEqual(Mat1.m_Array, Mat2.m_Array);
		}

		/// <summary>
		/// Determines whether two <see cref="Matrix" /> objects of equal dimensions are not equal in value.
		/// </summary>
		/// <param name="Mat1">First Matrix object in equality check</param>
		/// <param name="Mat2">Second Matrix object in equality check</param>
		/// <returns>True if the two <see cref="Matrix" /> objects are not equal; false otherwise.</returns>
		public static bool operator !=(Matrix Mat1, Matrix Mat2)
		{
			return (!IsEqual(Mat1.m_Array, Mat2.m_Array));
		}

		/// <summary>
		/// Tests whether the specified object is a <see cref="Matrix" /> object and, if so, whether it is identical to the current <see cref="Matrix" /> object.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with the current <see cref="Matrix" /> object</param>
		/// <returns>True if <see cref="obj" /> is a <see cref="Matrix" /> object identical to the current <see cref="Matrix" /> object; otherwise, false.</returns>
		public override bool Equals(Object obj)
		{
			try
			{
				return (this == (Matrix)obj);
			}
			catch
			{
				return false;
			}
		}
		#endregion

		public static Matrix Identity()
		{
			Matrix matOut = new Matrix(4, 4);
			matOut[0, 1] = 0.0;
			matOut[0, 2] = 0.0;
			matOut[0, 3] = 0.0;
			matOut[1, 0] = 0.0;
			matOut[1, 2] = 0.0;
			matOut[1, 3] = 0.0;
			matOut[2, 0] = 0.0;
			matOut[2, 1] = 0.0;
			matOut[2, 3] = 0.0;
			matOut[3, 0] = 0.0;
			matOut[3, 1] = 0.0;
			matOut[3, 2] = 0.0;
			matOut[0, 0] = 1.0;
			matOut[1, 1] = 1.0;
			matOut[2, 2] = 1.0;
			matOut[3, 3] = 1.0;
			return matOut;
		}

		public static Matrix Lerp(Matrix input1, Matrix input2, float lerpValue)
		{
			Matrix matOut = new Matrix(input1.RowCount, input1.ColumnCount);
			float fT = 1.0f - lerpValue;

			for (int i = 0; i < matOut.RowCount; i++)
			{
				for (int j = 0; j < matOut.ColumnCount; j++)
				{
					matOut[i, j] = input1[i, j] * lerpValue + input2[i, j] * fT;
				}
			}
			return matOut;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{ ");
			for (int i = 0; i < RowCount; i++)
			{
				sb.Append("{ ");
				for (int j = 0; j < ColumnCount; j++)
				{
					sb.Append(this[i, j].ToString());
					if (j < ColumnCount - 1)
					{
						sb.Append(", ");
					}
				}
				sb.Append("}");
				if (i < RowCount - 1)
				{
					sb.Append(", ");
				}
			}
			sb.Append(" }");
			return sb.ToString();
		}
	}
}
