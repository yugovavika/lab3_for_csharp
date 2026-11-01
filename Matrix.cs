using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Matrix
    {
        private int[,] _data;

        public int Rows
        {
            get { return _data.GetLength(0); }
        }

        public int Cols
        {
            get { return _data.GetLength(1); }
        }

        public Matrix(int rows, int cols)
        {
            if (rows < 1 || cols < 1)
            {
                throw new ArgumentException(
                    "Размеры должны быть положительными.");
            }

            _data = new int[rows, cols];
        }

        public Matrix(int[,] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            _data = (int[,])data.Clone();
        }

        public int Get(int row, int column)
        {
            return _data[row, column];
        }

        public void Set(int row, int column, int value)
        {
            _data[row, column] = value;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int row = 0;
            int column = 0;

            while (row < Rows)
            {
                column = 0;

                while (column < Cols)
                {
                    result.Append(
                        _data[row, column].ToString().PadLeft(5));
                    column++;
                }

                result.AppendLine();
                row++;
            }

            return result.ToString();
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            CheckDimensions(left, right);

            Matrix result = new Matrix(left.Rows, left.Cols);
            int row = 0;
            int column = 0;

            while (row < left.Rows)
            {
                column = 0;

                while (column < left.Cols)
                {
                    result.Set(
                        row,
                        column,
                        left.Get(row, column) +
                        right.Get(row, column));

                    column++;
                }

                row++;
            }

            return result;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            CheckDimensions(left, right);

            Matrix result = new Matrix(left.Rows, left.Cols);
            int row = 0;
            int column = 0;

            while (row < left.Rows)
            {
                column = 0;

                while (column < left.Cols)
                {
                    result.Set(
                        row,
                        column,
                        left.Get(row, column) -
                        right.Get(row, column));

                    column++;
                }

                row++;
            }

            return result;
        }

        public static Matrix operator *(int scalar, Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Rows, matrix.Cols);
            int row = 0;
            int column = 0;

            while (row < matrix.Rows)
            {
                column = 0;

                while (column < matrix.Cols)
                {
                    result.Set(
                        row,
                        column,
                        matrix.Get(row, column) * scalar);

                    column++;
                }

                row++;
            }

            return result;
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix(Cols, Rows);
            int row = 0;
            int column = 0;

            while (row < Rows)
            {
                column = 0;

                while (column < Cols)
                {
                    result.Set(column, row, Get(row, column));
                    column++;
                }

                row++;
            }

            return result;
        }

        private static void CheckDimensions(
            Matrix left, Matrix right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(
                    "Матрицы не должны быть пустыми.");
            }

            if (left.Rows != right.Rows ||
                left.Cols != right.Cols)
            {
                throw new ArgumentException(
                    "Размеры матриц должны совпадать.");
            }
        }
    }
}