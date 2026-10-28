using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class MatrixTasks
    {
        private static int _instanceCount;

        static MatrixTasks()
        {
            _instanceCount = 0;
        }

        public MatrixTasks()
        {
            _instanceCount++;
        }

        public static int InstanceCount
        {
            get { return _instanceCount; }
        }

        public void RunTask1()
        {
            int n1 = Checked.ReadInt("Введите " +
                "количество строк n для 1-й матрицы: ", 1, 20);
            int m1 = Checked.ReadInt("Введите " +
                "количество столбцов m для 1-й " +
                "матрицы: ", 1, 20);

            int[,] arr1 = new int[n1, m1];
            Console.WriteLine($"\nЗаполнение ПЕРВОГО " +
                $"массива ({n1}x{m1}) по диагоналям:");

            int sumIdx = 0;
            while (sumIdx <= (n1 - 1) + (m1 - 1))
            {
                for (int i = 0; i < n1; i++)
                {
                    int j = sumIdx - i;
                    if (j >= 0 && j < m1)
                    {
                        arr1[i, j] = Checked.ReadInt($"Элем" +
                            $"ент [{i},{j}]: ");
                    }
                }
                sumIdx++;
            }
            Console.WriteLine("\nПервый массив:");
            PrintArray(arr1);

            int n2 = Checked.ReadInt("\nВведите размерность" +
                " n для 2-й матрицы (n x n): ", 1, 20);
            double[,] arr2 = new double[n2, n2];
            Random rnd = new Random();

            for (int i = 0; i < n2; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    if (i + j < n2 - 1)
                    {
                        arr2[i, j] = Math.Round(0.51 +
                            rnd.NextDouble() * 0.49, 2);
                    }
                    else
                    {
                        arr2[i, j] = Math.Round(rnd.
                            NextDouble() * 0.5, 2);
                    }
                }
            }
            Console.WriteLine("\nВторой массив " +
                "(случайные числа):");
            PrintDoubleArray(arr2);

            int n3 = Checked.ReadInt("\nВведите " +
                "размерность n для 3-й матрицы: ",1,20);

            int[,] arr3 = new int[n3, n3];

            int row;
            int column;
            int diagonal;
            int length;
            int value;
            int position;

            for (row = 0; row < n3; row++)
            {
                for (column = 0; column < n3; column++)
                {
                    arr3[row, column] = 0;
                }
            }

            for (diagonal = 0; diagonal < n3; diagonal++)
            {
                length = n3 - diagonal;

                if (diagonal == 0)
                {
                    for (position = 0; position < 
                        length; position++)
                    {
                        arr3[position, position] = position + 1;
                    }
                }
                else
                {
                    if (diagonal % 2 == 1)
                    {
                        value = length;

                        for (position = 0; position < 
                            length; position++)
                        {
                            row = diagonal + position;
                            column = position;

                            if (row < n3)
                            {
                                arr3[row, column] = value;
                                value--;
                            }
                        }
                    }
                    else
                    {
                        value = 1;

                        for (position = 0; position < 
                            length; position++)
                        {
                            row = diagonal + position;
                            column = position;

                            if (row < n3)
                            {
                                arr3[row, column] = value;
                                value++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\nТретий массив:");
            PrintArray(arr3);
        }

        private void PrintDoubleArray(double[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j],6:F2}");
                }
                Console.WriteLine();
            }
        }

        public void RunTask2()
        {
            int rows = Checked.ReadInt("Введите " +
                "количество строк матрицы: ", 1, 20);
            int cols = Checked.ReadInt("Введите " +
                "количество столбцов матрицы: ", 1, 20);

            int[,] arr = new int[rows, cols];
            Console.WriteLine("\nЗаполнение массива:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr[i, j] = Checked.ReadInt($"Элемент " +
                        $"[{i},{j}]: ");
                }
            }

            Console.WriteLine("\nВаш массив:");
            PrintArray(arr);

            int totalSum = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    totalSum += arr[i, j];
                }
            }

            int leftSum = 0;
            bool found = false;

            for (int j = 0; j < cols - 1; j++)
            {
                int colSum = 0;
                for (int i = 0; i < rows; i++)
                {
                    colSum += arr[i, j];
                }

                int rightSum = totalSum - leftSum - colSum;

                if (leftSum > rightSum)
                {
                    Console.WriteLine($"\nНайден " +
                        $"столбец {j}.");
                    Console.WriteLine($"Сумма слева: " +
                        $"{leftSum}");
                    Console.WriteLine($"Сумма справа: " +
                        $"{rightSum}");
                    found = true;
                    break;
                }

                leftSum += colSum;
            }

            if (!found)
            {
                Console.WriteLine("\nТакого столбца нет " +
                    "(ни один столбец не разбивает массив " +
                    "так, чтобы слева сумма была больше).");
            }
        }

        public void RunTask3()
        {
            Console.WriteLine("Выражение: A + (B - 3 * C^T)");

            int n = Checked.ReadInt("Введите количество" +
                " строк n (для матриц A и B): ", 1, 10);
            int m = Checked.ReadInt("Введите количество " +
                "столбцов m (для матриц A и B): ", 1, 10);
            Console.WriteLine($"\nМатрицы A и B будут" +
                $" размером {n}x{m}.");
            Console.WriteLine($"Матрица C будет размером " +
                $"{m}x{n} (чтобы после транспонирования " +
                $"получилось {n}x{m}).");

            int[,] dataA = new int[n, m];
            Console.WriteLine($"\nЗаполнение матрицы A " +
                $"({n}x{m}):");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    dataA[i, j] = Checked.ReadInt($"A" +
                        $"[{i},{j}]: ");
                }
            }

            int[,] dataB = new int[n, m];
            Console.WriteLine($"\nЗаполнение матрицы B " +
                $"({n}x{m}):");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    dataB[i, j] = Checked.ReadInt($"B" +
                        $"[{i},{j}]: ");
                }
            }

            int[,] dataC = new int[m, n];
            Console.WriteLine($"\nЗаполнение матрицы C " +
                $"({m}x{n}):");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataC[i, j] = Checked.ReadInt($"C" +
                        $"[{i},{j}]: ");
                }
            }

            Matrix A = new Matrix(dataA);
            Matrix B = new Matrix(dataB);
            Matrix C = new Matrix(dataC);

            Matrix CT = C.Transpose();
            Matrix result = A + (B - 3 * CT);

            Console.WriteLine("\nМатрица A:");
            Console.WriteLine(A);
            Console.WriteLine("Матрица B:");
            Console.WriteLine(B);
            Console.WriteLine("Матрица C:");
            Console.WriteLine(C);
            Console.WriteLine("Транспонированная матрица C" +
                " (C^T):");
            Console.WriteLine(CT);
            Console.WriteLine("Результат A + (B - 3*C^T):");
            Console.WriteLine(result);
        }

        private void PrintArray(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j],5}");
                }
                Console.WriteLine();
            }
        }
    }
}
