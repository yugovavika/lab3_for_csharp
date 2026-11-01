using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class MatrixTasks
    {
        private double[,] _data;

        public MatrixTasks()
        {
            _data = new double[1, 1];
        }

        public MatrixTasks(int rows, int columns)
        {
            if (rows < 1 || columns < 1)
            {
                throw new ArgumentException(
                    "Размеры должны быть положительными.");
            }

            _data = new double[rows, columns];

            int diagonal = 0;
            int row = 0;
            int column = 0;

            while (diagonal < rows + columns - 1)
            {
                row = 0;

                while (row < rows)
                {
                    column = diagonal - row;

                    if (column >= 0 && column < columns)
                    {
                        _data[row, column] = Checked.ReadInt(
                            $"Элемент [{row},{column}]: ");
                    }

                    row++;
                }

                diagonal++;
            }
        }

        public MatrixTasks(int size, bool randomMatrix)
        {
            if (size < 1)
            {
                throw new ArgumentException(
                    "Размер должен быть положительным.");
            }

            _data = new double[size, size];

            int row = 0;
            int column = 0;

            if (randomMatrix)
            {
                Random generator = new Random();

                while (row < size)
                {
                    column = 0;

                    while (column < size)
                    {
                        if (row + column < size - 1)
                        {
                            _data[row, column] =
                                0.51 + generator.NextDouble() * 0.49;
                        }
                        else
                        {
                            _data[row, column] =
                                generator.NextDouble() * 0.5;
                        }

                        _data[row, column] = Math.Round(
                            _data[row, column], 2);

                        column++;
                    }

                    row++;
                }
            }
            else
            {
                while (row < size)
                {
                    column = 0;

                    while (column < size)
                    {
                        if (column <= row)
                        {
                            int diagonal = row - column;
                            int length = size - diagonal;

                            if (diagonal % 2 == 0)
                            {
                                _data[row, column] = column + 1;
                            }
                            else
                            {
                                _data[row, column] =
                                    length - column;
                            }
                        }

                        column++;
                    }

                    row++;
                }
            }
        }

        public override string ToString()
        {
            string result = string.Empty;
            int row = 0;
            int column = 0;

            while (row < _data.GetLength(0))
            {
                column = 0;

                while (column < _data.GetLength(1))
                {
                    result += string.Format(
                        "{0,6}", _data[row, column]);
                    column++;
                }

                result += Environment.NewLine;
                row++;
            }

            return result;
        }

        public void RunTask1()
        {
            int rows = Checked.ReadInt(
                "Количество строк первой матрицы: ", 1, 20);
            int columns = Checked.ReadInt(
                "Количество столбцов первой матрицы: ", 1, 20);

            MatrixTasks first = new MatrixTasks(rows, columns);

            Console.WriteLine("\nПервая матрица:");
            Console.WriteLine(first);

            int size = Checked.ReadInt(
                "\nРазмер второй матрицы n x n: ", 1, 20);

            MatrixTasks second = new MatrixTasks(size, true);

            Console.WriteLine("\nВторая матрица:");
            Console.WriteLine(second);

            size = Checked.ReadInt(
                "\nРазмер третьей матрицы n x n: ", 1, 20);

            MatrixTasks third = new MatrixTasks(size, false);

            Console.WriteLine("\nТретья матрица:");
            Console.WriteLine(third);
        }

        public void RunTask2()
        {
            int rows = Checked.ReadInt(
                "Количество строк матрицы: ", 1, 20);
            int columns = Checked.ReadInt(
                "Количество столбцов матрицы: ", 1, 20);

            int[,] data = new int[rows, columns];

            int row = 0;
            int column = 0;

            Console.WriteLine("\nЗаполнение матрицы:");

            while (row < rows)
            {
                column = 0;

                while (column < columns)
                {
                    data[row, column] = Checked.ReadInt(
                        $"Элемент [{row},{column}]: ");
                    column++;
                }

                row++;
            }

            Console.WriteLine("\nВаша матрица:");
            PrintArray(data);

            int totalSum = 0;
            row = 0;

            while (row < rows)
            {
                column = 0;

                while (column < columns)
                {
                    totalSum += data[row, column];
                    column++;
                }

                row++;
            }

            int leftSum = 0;
            int columnSum = 0;
            int rightSum = 0;
            bool found = false;
            column = 0;

            while (column < columns - 1 && !found)
            {
                columnSum = 0;
                row = 0;

                while (row < rows)
                {
                    columnSum += data[row, column];
                    row++;
                }

                rightSum = totalSum - leftSum - columnSum;

                if (leftSum > rightSum)
                {
                    Console.WriteLine(
                        $"\nПодходящий столбец: {column}.");
                    Console.WriteLine($"Сумма слева: {leftSum}");
                    Console.WriteLine($"Сумма справа: {rightSum}");
                    found = true;
                }
                else
                {
                    leftSum += columnSum;
                }

                column++;
            }

            if (!found)
            {
                Console.WriteLine(
                    "\nПодходящий столбец не найден.");
            }
        }

        public void RunTask3()
        {
            Console.WriteLine("Выражение: A + (B - 3 * C^T)");

            int rows = Checked.ReadInt(
                "Количество строк матриц A и B: ", 1, 10);
            int columns = Checked.ReadInt(
                "Количество столбцов матриц A и B: ", 1, 10);

            int[,] dataA = new int[rows, columns];
            int[,] dataB = new int[rows, columns];
            int[,] dataC = new int[columns, rows];

            ReadMatrix(dataA, "A");
            ReadMatrix(dataB, "B");
            ReadMatrix(dataC, "C");

            Matrix matrixA = new Matrix(dataA);
            Matrix matrixB = new Matrix(dataB);
            Matrix matrixC = new Matrix(dataC);

            Matrix transposed = matrixC.Transpose();
            Matrix result = matrixA + (matrixB - 3 * transposed);

            Console.WriteLine("\nМатрица A:");
            Console.WriteLine(matrixA);

            Console.WriteLine("Матрица B:");
            Console.WriteLine(matrixB);

            Console.WriteLine("Матрица C:");
            Console.WriteLine(matrixC);

            Console.WriteLine("Транспонированная матрица C:");
            Console.WriteLine(transposed);

            Console.WriteLine("Результат выражения:");
            Console.WriteLine(result);
        }

        private void ReadMatrix(int[,] data, string name)
        {
            int row = 0;
            int column = 0;

            Console.WriteLine($"\nЗаполнение матрицы {name}:");

            while (row < data.GetLength(0))
            {
                column = 0;

                while (column < data.GetLength(1))
                {
                    data[row, column] = Checked.ReadInt(
                        $"{name}[{row},{column}]: ");
                    column++;
                }

                row++;
            }
        }

        private void PrintArray(int[,] data)
        {
            int row = 0;
            int column = 0;

            while (row < data.GetLength(0))
            {
                column = 0;

                while (column < data.GetLength(1))
                {
                    Console.Write($"{data[row, column],5}");
                    column++;
                }

                Console.WriteLine();
                row++;
            }
        }
    }
}