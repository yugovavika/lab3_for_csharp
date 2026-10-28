using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace lab3
{
    [Serializable]
    public struct Toy
    {
        public string Name;
        public double Price;
        public int MinAge;
        public int MaxAge;
    }

    public static class FileTasks
    {
        private static int _fileOpsCount;

        static FileTasks()
        {
            _fileOpsCount = 0;
        }

        public static int FileOpsCount
        {
            get { return _fileOpsCount; }
        }

        public static void RunAll()
        {
            RunTask4();
            RunTask5();
            RunTask6();
            RunTask7();
            RunTask8();
            Console.WriteLine($"Выполнено файловых операций: " +
                $"{FileOpsCount}");
        }

        private static void FillBinaryFile(string fileName)
        {
            int choice = Checked.ReadInt("Заполнить файл 1 - " +
                "случайными числами, 2 - вручную? ", 1, 2);

            using (BinaryWriter writer = new BinaryWriter
                (File.Create(fileName)))
            {
                int i = 0;

                if (choice == 1)
                {
                    Random rnd = new Random();
                    int count = Checked.ReadInt("Сколько " +
                        "случайных чисел сгенерировать? "
                        , 1, 100);
                    for (i = 0; i < count; i++)
                    {
                        writer.Write(rnd.Next(10, 1000));
                    }
                }
                else
                {
                    int count = Checked.ReadInt("Сколько " +
                        "чисел вы хотите ввести? ", 1, 100);
                    for (i = 0; i < count; i++)
                    {
                        int num = Checked.ReadInt($"Введите " +
                            $"{i + 1}-е число: ");
                        writer.Write(num);
                    }
                }
            }
        }

        public static void RunTask4()
        {
            _fileOpsCount++;
            string file1 = "input4.bin";
            string file2 = "output4.bin";

            FillBinaryFile(file1);

            using (BinaryReader reader = new BinaryReader
                (File.OpenRead(file1)))
            using (BinaryWriter writer = new BinaryWriter
                (File.Create(file2)))
            {
                while (reader.BaseStream.Position != 
                    reader.BaseStream.Length)
                {
                    int num = reader.ReadInt32();

                    int absNum = Math.Abs(num);

                    if (absNum >= 10)
                    {
                        string s = absNum.ToString();
                        if (s[0] == s[s.Length - 1])
                        {
                            writer.Write(num);
                        }
                    }
                }
            }
            Console.WriteLine("Файл обработан. " +
                "Результат записан в output4.bin");
        }

        public static void RunTask5()
        {
            _fileOpsCount++;
            string xmlFile = "toys.xml";

            int count = Checked.ReadInt("Сколько игрушек " +
                "вы хотите добавить? ", 1, 20);
            Toy[] toys = new Toy[count];

            int i = 0;
            for (i = 0; i < count; i++)
            {
                Console.WriteLine($"\nИгрушка №{i + 1}:");
                string? name = "";
                bool isValidName = false;
                while (!isValidName)
                {
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Ошибка. Название " +
                            "не может быть пустым.");
                    }
                    else
                    {
                        isValidName = true;
                    }
                }

                toys[i].Name = name;
                toys[i].Price = Checked.ReadInt("Цена: ", 0, 
                    1000000);
                toys[i].MinAge = Checked.ReadInt("Минимальный" +
                    " возраст: ", 0, 100);
                toys[i].MaxAge = Checked.ReadInt
                    ("Максимальный возраст: ", 0, 100);

                while (toys[i].MinAge > toys[i].MaxAge)
                {
                    Console.WriteLine("Ошибка. Минимальный " +
                        "возраст не может быть больше" +
                        " максимального.");
                    toys[i].MinAge = Checked.ReadInt("Введ" +
                        "ите минимальный возраст заново: ", 
                        0, 100);
                }
            }

            XmlSerializer serializer = new XmlSerializer
                (typeof(Toy[]));
            using (FileStream fs = new FileStream(xmlFile,
                FileMode.Create))
            {
                serializer.Serialize(fs, toys);
            }
            Console.WriteLine($"\nДанные сохранены в файл " +
                $"{xmlFile}");

            Toy[] loadedToys;
            using (FileStream fs = new FileStream(xmlFile,
                FileMode.Open))
            {
                loadedToys = (Toy[])serializer.Deserialize(fs);
            }
            Console.WriteLine("Данные успешно загружены из " +
                "файла.");

            double sum = 0;
            int cubeCount = 0;

            Console.WriteLine("\n Результат");
            foreach (Toy t in loadedToys)
            {
                if (t.Name.Equals("Кубики", StringComparison.
                    OrdinalIgnoreCase))
                {
                    sum += t.Price;
                    cubeCount++;
                    Console.WriteLine($"Найдены Кубики: " +
                        $"цена {t.Price}, возраст от " +
                        $"{t.MinAge} до {t.MaxAge} лет");
                }
            }

            if (cubeCount > 0)
            {
                double averagePrice = sum / cubeCount;
                Console.WriteLine($"\nСредняя стоимость " +
                    $"кубиков: {averagePrice:F2}");
            }
            else
            {
                Console.WriteLine("\nКубики не найдены.");
            }
        }

        private static void FillTextFileTask6(string fileName)
        {
            int choice = Checked.ReadInt("Заполнить файл 1 - " +
                "случайными числами, 2 - вручную? ", 1, 2);
            using (StreamWriter writer = new StreamWriter
                (fileName))
            {
                if (choice == 1)
                {
                    Random rnd = new Random();
                    int count = Checked.ReadInt("Сколько " +
                        "чисел сгенерировать? ", 1, 100);
                    for (int i = 0; i < count; i++)
                    {
                        writer.WriteLine(rnd.Next(1, 100));
                    }
                }
                else
                {
                    int count = Checked.ReadInt("Сколько " +
                        "чисел ввести? ", 1, 100);
                    int i = 0;
                    for (i = 0; i < count; i++)
                    {
                        int num = Checked.ReadInt($"Введите " +
                            $"{i + 1}-е число: ");
                        writer.WriteLine(num);
                    }
                }
            }
        }
        
        public static void RunTask6()
        {
            _fileOpsCount++;
            string file1 = "input6.txt";
            string file2 = "output6.txt";

            FillTextFileTask6(file1);

            int k = Checked.ReadInt("Введите делитель k " +
                "(не 0): ", 1, int.MaxValue);

            string[] lines = File.ReadAllLines(file1);

            Console.WriteLine("\nСодержимое исходного файла:");
            int i = 0;
            for ( i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }

            Console.WriteLine($"\nРезультат деления на {k} " +
                $"(записано в output6.txt):");
            using (StreamWriter writer = new StreamWriter
                (file2))
            {
                for (i = 0; i < lines.Length; i++)
                {
                    double val = double.Parse(lines[i]);
                    double result = val / k;
                    writer.WriteLine(Math.Round(result, 2));

                    Console.WriteLine(Math.Round(result, 2));
                }
            }
        }

        private static void FillTextFileTask7(string fileName)
        {
            int choice = Checked.ReadInt("Заполнить файл 1 - " +
                "случайными числами, 2 - вручную? ", 1, 2);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                if (choice == 1)
                {
                    Random rnd = new Random();
                    int rows = Checked.ReadInt("Сколько строк" +
                        " сгенерировать? ", 1, 10);
                    int i = 0;
                    int j = 0;
                    int cols = 0;
                    for ( i = 0; i < rows; i++)
                    {
                        cols = Checked.ReadInt($"Сколько " +
                            $"чисел в {i + 1}-й строке? ",
                            1, 10);
                        for ( j = 0; j < cols; j++)
                        {
                            writer.Write(rnd.Next(-50, 100) + 
                                " ");
                        }
                        writer.WriteLine();
                    }
                }
                else
                {
                    int i = 0;
                    int j = 0;
                    int cols = 0;
                    int rows = Checked.ReadInt("Сколько " +
                        "строк ввести? ", 1, 10);
                    for ( i = 0; i < rows; i++)
                    {
                        cols = Checked.ReadInt($"Сколько " +
                            $"чисел в {i + 1}-й строке? ", 
                            1, 10);
                        for ( j = 0; j < cols; j++)
                        {
                            int num = Checked.ReadInt
                                ($"Введите {j + 1}-е число" +
                                $" в {i + 1}-й строке: ");
                            writer.Write(num + " ");
                        }
                        writer.WriteLine();
                    }
                }
            }
        }

        public static void RunTask7()
        {
            _fileOpsCount++;
            string file = "input7.txt";

            FillTextFileTask7(file);

            string[] lines = File.ReadAllLines(file);
            Console.WriteLine("\nСодержимое исходного файла:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }

            int first = 0;
            int max = int.MinValue;
            bool isFirst = true;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ', 
                    StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < parts.Length; j++)
                {
                    int num = int.Parse(parts[j]);
                    if (isFirst)
                    {
                        first = num;
                        isFirst = false;
                    }
                    if (num > max)
                    {
                        max = num;
                    }
                }
            }

            Console.WriteLine("\nРезультат");
            Console.WriteLine($"Первый: {first}, " +
                $"Максимальный: {max}, Сумма: {first + max}");
        }

        private static void FillTextFileTask8(string fileName)
        {
            Console.WriteLine("Введите текст (каждое " +
                "новое слово/предложение пишите с " +
                "новой строки).");
            Console.WriteLine("Чтобы закончить ввод, " +
                "просто нажмите Enter на пустой строке:");

            using (StreamWriter writer = new 
                StreamWriter(fileName))
            {
                bool isInputting = true;
                while (isInputting)
                {
                    string? line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        isInputting = false;
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public static void RunTask8()
        {
            _fileOpsCount++;
            string file1 = "input8.txt";
            string file2 = "output8.txt";

            FillTextFileTask8(file1);

            string[] lines = File.ReadAllLines(file1);
            Console.WriteLine("\nСодержимое исходного файла:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }

            Console.WriteLine("\nРезультат (записано в " +
                "output8.txt)");
            using (StreamWriter writer = new 
                StreamWriter(file2))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();

                    if ((line.Length > 0 && (line[0] == 'б' 
                        || line[0] == 'Б')) ||
                        (line.Length > 1 && (line[1] == 'б'
                        || line[1] == 'Б')))
                    {
                        writer.WriteLine(line);
                        Console.WriteLine(line);
                    }
                }
            }
        }
    }
}