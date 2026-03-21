using System;

namespace Exceptions;

public class MatrixException : Exception
{
    public MatrixException()
    {
    }

    public MatrixException(string message)
        : base(message)
    {
    }

    public MatrixException(string message, Exception innerException)
    : base(message, innerException)
    {
    }
}

public class Matrix
{
    private readonly double[] matrix;
    private readonly int rows;
    private readonly int columns;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class.
    /// </summary>
    /// <param name="rows">num of row.</param>
    /// <param name="columns">num of column.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when row or column is zero or negative.</exception>
    public Matrix(int rows, int columns)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rows);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(columns);
        this.rows = rows;
        this.columns = columns;
        this.matrix = new double[rows * columns];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
    /// </summary>
    /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
    /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
    public Matrix(Array array)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array is double[,] validArray)
        {
            this.rows = validArray.GetLength(0);
            this.columns = validArray.GetLength(1);
            this.matrix = new double[this.rows * this.columns];
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    this.matrix[(i * this.columns) + j] = validArray[i, j];
                }
            }
        }
        else
        {
            throw new ArgumentException(string.Empty, nameof(array));
        }
    }

    /// <summary>
    /// Gets Number of rows.
    /// </summary>
    public int Rows
    {
        get
        {
             return this.rows;
        }
    }

    /// <summary>
    /// Gets Number of columns.
    /// </summary>
    public int Columns
    {
        get
        {
            return this.columns;
        }
    }

    /// <summary>
    /// Gets an array of floating-point values that represents the elements of this Matrix.
    /// </summary>
    public double[,] Array
    {
        get
        {
            double[,] result = new double[this.rows, this.columns];
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    result[i, j] = this.matrix[(i * this.columns) + j];
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Allows instances of a <see cref="Matrix"/> to be indexed just like arrays.
    /// </summary>
    /// <param name="row">num of row.</param>
    /// <param name="column">num of column.</param>
    /// <exception cref="ArgumentException">Thrown when index is out of range.</exception>
    public double this[int row, int column]
    {
        get
        {
            if (row < 0 || row >= this.Rows)
            {
                throw new ArgumentException(string.Empty, nameof(row));
            }
            else if (column < 0 || column >= this.Columns)
            {
                throw new ArgumentException(string.Empty, nameof(column));
            }

            return this.matrix[(row * this.columns) + column];
        }

        set
        {
            if (row < 0 || row >= this.Rows)
            {
                throw new ArgumentException(string.Empty, nameof(row));
            }
            else if (column < 0 || column >= this.Columns)
            {
                throw new ArgumentException(string.Empty, nameof(column));
            }

            this.matrix[(row * this.columns) + column] = value;
        }
    }

    /// <summary>
    /// Adds <see cref="Matrix"/> to the current matrix.
    /// </summary>
    /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
    /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
    /// <exception cref="MatrixException">Thrown when the matrix has the wrong dimensions for the operation.</exception>
    /// <returns><see cref="Matrix"/>.</returns>
    public Matrix Add(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
        {
            throw new MatrixException();
        }

        Matrix result = new Matrix(new double[this.Rows, this.Columns]);
        for (int i = 0; i < this.matrix.Length; i++)
        {
            result.matrix[i] = this.matrix[i] + matrix.matrix[i];
        }

        return result;
    }

    /// <summary>
    /// Subtracts <see cref="Matrix"/> from the current matrix.
    /// </summary>
    /// <param name="matrix"><see cref="Matrix"/> for subtracting.</param>
    /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
    /// <exception cref="MatrixException">Thrown when the matrix has the wrong dimensions for the operation.</exception>
    /// <returns><see cref="Matrix"/>.</returns>
    public Matrix Subtract(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
        {
            throw new MatrixException();
        }

        Matrix result = new Matrix(new double[this.Rows, this.Columns]);
        for (int i = 0; i < this.matrix.Length; i++)
        {
            result.matrix[i] = this.matrix[i] - matrix.matrix[i];
        }

        return result;
    }

    /// <summary>
    /// Multiplies <see cref="Matrix"/> on the current matrix.
    /// </summary>
    /// <param name="matrix"><see cref="Matrix"/> for multiplying.</param>
    /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
    /// <exception cref="MatrixException">Thrown when the matrix has the wrong dimensions for the operation.</exception>
    /// <returns><see cref="Matrix"/>.</returns>
    public Matrix Multiply(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        if (this.Columns != matrix.Rows)
        {
            throw new MatrixException();
        }

        Matrix result = new Matrix(new double[this.Rows, matrix.Columns]);
        for (int i = 0; i < this.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                double sum = 0;
                for (int k = 0; k < this.Columns; k++)
                {
                    sum += this[i, k] * matrix[k, j];
                }

                result[i, j] = sum;
            }
        }

        return result;
    }
}