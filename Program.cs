using lab3;
using System;
using System.Text;

namespace lab3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nВыберите задание:");
                Console.WriteLine("1 - Задание 1 " +
                    "(Заполнение двумерного массива)");
                Console.WriteLine("2 - Задание 2 " +
                    "(Анализ двумерного массива)");
                Console.WriteLine("3 - Задание 3 " +
                    "(Матричные операции)");
                Console.WriteLine("4 - Задание 4 " +
                    "(Бинарные файлы)");
                Console.WriteLine("5 - Задание 5 " +
                    "(XML Сериализация структуры)");
                Console.WriteLine("6 - Задание 6 " +
                    "(Текстовые файлы)");
                Console.WriteLine("7 - Задание 7 " +
                    "(Текстовый файл, несколько чисел в строке)");
                Console.WriteLine("8 - Задание 8 " +
                    "(Текстовый файл, строки)");
                Console.WriteLine("9 - Запустить " +
                    "ВСЕ задания подряд");
                Console.WriteLine("0 - Выход");

                int choice = Checked.ReadInt("Выбор: ", 0, 9);

                switch (choice)
                {
                    case 1:
                        MatrixTasks matrixTasks1 = 
                            new MatrixTasks();
                        matrixTasks1.RunTask1();
                        break;
                    case 2:
                        MatrixTasks matrixTasks2 = 
                            new MatrixTasks();
                        matrixTasks2.RunTask2();
                        break;
                    case 3:
                        MatrixTasks matrixTasks3 = 
                            new MatrixTasks();
                        matrixTasks3.RunTask3();
                        break;
                    case 4:
                        FileTasks.RunTask4();
                        break;
                    case 5:
                        FileTasks.RunTask5();
                        break;
                    case 6:
                        FileTasks.RunTask6();
                        break;
                    case 7:
                        FileTasks.RunTask7();
                        break;
                    case 8:
                        FileTasks.RunTask8();
                        break;
                    case 9:
                        Console.WriteLine("\n Все задания: ");
                        MatrixTasks allTasks = new 
                            MatrixTasks();
                        allTasks.RunTask1();
                        allTasks.RunTask2();
                        allTasks.RunTask3();
                        FileTasks.RunAll();

                        Console.WriteLine($"\nСоздано " +
                            $"объектов MatrixTasks: " +
                            $"{MatrixTasks.InstanceCount}");
                        Console.WriteLine($"Выполнено " +
                            $"файловых операций: " +
                            $"{FileTasks.FileOpsCount}");
                        break;
                    case 0:
                        isRunning = false;
                        Console.WriteLine("Выход из программы");
                        break;
                    default:
                        Console.WriteLine("Неверный " +
                            "пункт меню.");
                        break;
                }
            }

            Console.WriteLine("\nНажмите любую клавишу " +
                "для закрытия окна.");
            Console.ReadKey();
        }
    }
}