using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace lab3
{
    public struct Toy
    {
        private string _name;
        private double _price;
        private int _minAge;
        private int _maxAge;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int MinAge
        {
            get { return _minAge; }
            set { _minAge = value; }
        }

        public int MaxAge
        {
            get { return _maxAge; }
            set { _maxAge = value; }
        }
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

            Console.WriteLine(
                "Выполнено операций: " + _fileOpsCount);
        }

        private static bool IsFileAvailable(string path)
        {
            try
            {
                using (FileStream stream = File.OpenRead(path))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка открытия файла.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
            }

            return false;
        }

        private static void FillBinaryFile(string path)
        {
            int count = Checked.ReadInt(
                "Количество чисел: ", 1, 100);

            int choice = Checked.ReadInt(
                "Заполнить файл 1 - Вручную, 2 - Случайными числами? ", 1, 2);

            Random random = new Random();

            try
            {
                using (BinaryWriter writer = new BinaryWriter(
                    File.Create(path)))
                {
                    int index = 0;
                    int number = 0;

                    Console.WriteLine("Вводимые числа:");

                    while (index < count)
                    {
                        if (choice == 1)
                        {
                            number = Checked.ReadInt(
                                "Введите число: ",
                                int.MinValue, int.MaxValue);
                        }
                        else
                        {
                            number = random.Next(10, 1000);
                        }

                        writer.Write(number);
                        Console.WriteLine(number);
                        index++;
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine(
                    "Ошибка при работе с файлом.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(
                    "Нет доступа к файлу.");
            }
        }

        public static void RunTask4()
        {
            string source = "input4.bin";
            string result = "output4.bin";

            FillBinaryFile(source);

            if (!IsFileAvailable(source))
            {
                return;
            }

            try
            {
                using (BinaryReader reader = new BinaryReader(
                    File.OpenRead(source)))
                using (BinaryWriter writer = new BinaryWriter(
                    File.Create(result)))
                {
                    while (reader.BaseStream.Position <
                        reader.BaseStream.Length)
                    {
                        int number = reader.ReadInt32();
                        long absNumber = Math.Abs((long)number);
                        string text = absNumber.ToString();

                        if (absNumber >= 10 &&
                            text[0] == text[text.Length - 1])
                        {
                            writer.Write(number);
                        }
                    }
                }

                _fileOpsCount++;
                Console.WriteLine(
                    "Результат сохранён в output4.bin.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка обработки файла.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
            }
        }

        private static void SerializeToys(
            string path, Toy[] toys)
        {
            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(Toy[]));

                using (FileStream stream = File.Create(path))
                {
                    serializer.Serialize(stream, toys);
                }

                Console.WriteLine("Данные сохранены.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Ошибка сериализации.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка записи XML-файла.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
            }
        }

        private static Toy[] DeserializeToys(string path)
        {
            Toy[] toys = new Toy[0];

            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(Toy[]));

                using (FileStream stream = File.OpenRead(path))
                {
                    toys = (Toy[])serializer.Deserialize
                        (stream);
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Ошибка чтения XML.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка открытия XML-файла.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
            }

            return toys;
        }

        public static void RunTask5()
        {
            string path = "toys.xml";

            int count = Checked.ReadInt(
                "Количество игрушек: ", 1, 20);

            Toy[] toys = new Toy[count];
            int index = 0;

            while (index < count)
            {
                Toy toy = new Toy();

                Console.WriteLine("Игрушка " + (index + 1));
                string name = "";

                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Название: ");
                    name = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine(
                            "Название не должно быть пустым.");
                    }
                }

                toy.Name = name;
                toy.Price = Checked.ReadInt(
                    "Цена: ", 0, 1000000);
                toy.MinAge = Checked.ReadInt(
                    "Минимальный возраст: ", 0, 100);
                toy.MaxAge = Checked.ReadInt(
                    "Максимальный возраст: ", 0, 100);

                while (toy.MinAge > toy.MaxAge)
                {
                    Console.WriteLine(
                        "Минимальный возраст больше " +
                        "максимального.");

                    toy.MinAge = Checked.ReadInt(
                        "Введите минимум заново: ", 0, 100);
                }

                toys[index] = toy;
                index++;
            }

            SerializeToys(path, toys);

            if (!IsFileAvailable(path))
            {
                return;
            }

            Toy[] loadedToys = DeserializeToys(path);
            double sum = 0;
            int cubeCount = 0;
            index = 0;

            while (index < loadedToys.Length)
            {
                Toy toy = loadedToys[index];

                if (string.Equals(
                    toy.Name, "Кубики",
                    StringComparison.OrdinalIgnoreCase))
                {
                    sum += toy.Price;
                    cubeCount++;

                    Console.WriteLine(
                        "Кубики: возраст от " +
                        toy.MinAge + " до " +
                        toy.MaxAge + ", цена " + toy.Price);
                }

                index++;
            }

            if (cubeCount > 0)
            {
                Console.WriteLine(
                    "Средняя цена кубиков: " +
                    (sum / cubeCount).ToString("F2"));
            }
            else
            {
                Console.WriteLine("Кубики не найдены.");
            }

            _fileOpsCount++;
        }

        private static void FillTextFile(
            string path, int count)
        {
            Random random = new Random();

            using (StreamWriter writer = new StreamWriter(path))
            {
                int index = 0;

                while (index < count)
                {
                    int number = random.Next(1, 101);
                    writer.WriteLine(number);
                    Console.WriteLine(number);
                    index++;
                }
            }
        }

        public static void RunTask6()
        {
            string source = "input6.txt";
            string result = "output6.txt";

            int count = Checked.ReadInt(
                "Количество чисел: ", 1, 100);

            try
            {
                FillTextFile(source, count);
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка записи файла.");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
                return;
            }

            int divisor = Checked.ReadInt(
                "Введите делитель k: ", 1, int.MaxValue);

            if (!IsFileAvailable(source))
            {
                return;
            }

            try
            {
                using (StreamReader reader =
                    new StreamReader(source))
                using (StreamWriter writer =
                    new StreamWriter(result))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        int number;

                        if (int.TryParse(line, out number))
                        {
                            double value = 
                                (double)number / divisor;
                            writer.WriteLine(value);
                        }
                        else
                        {
                            Console.WriteLine(
                                "Пропущена некорректная " +
                                "строка.");
                        }

                        line = reader.ReadLine();
                    }
                }

                _fileOpsCount++;
                Console.WriteLine(
                    "Результат сохранён в output6.txt.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка обработки файла.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
            }
        }

        private static void FillNumberLines(string path)
        {
            int rows = Checked.ReadInt(
                "Количество строк: ", 1, 10);

            Random random = new Random();

            using (StreamWriter writer = new StreamWriter(path))
            {
                int row = 0;

                while (row < rows)
                {
                    int count = Checked.ReadInt(
                        "Количество чисел в строке: ", 1, 10);
                    int column = 0;

                    while (column < count)
                    {
                        if (column > 0)
                        {
                            writer.Write(" ");
                        }

                        writer.Write(random.Next(-50, 100));
                        column++;
                    }

                    writer.WriteLine();
                    row++;
                }
            }
        }

        public static void RunTask7()
        {
            string path = "input7.txt";

            try
            {
                FillNumberLines(path);
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка записи файла.");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
                return;
            }

            if (!IsFileAvailable(path))
            {
                return;
            }

            bool hasNumber = false;
            int first = 0;
            int maximum = int.MinValue;

            try
            {
                using (StreamReader reader =
                    new StreamReader(path))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        string[] parts = line.Split(
                            new char[] { ' ', '\t' },
                            StringSplitOptions.
                            RemoveEmptyEntries);

                        int column = 0;

                        while (column < parts.Length)
                        {
                            int number;

                            if (int.TryParse(
                                parts[column], out number))
                            {
                                if (!hasNumber)
                                {
                                    first = number;
                                    hasNumber = true;
                                }

                                if (number > maximum)
                                {
                                    maximum = number;
                                }
                            }

                            column++;
                        }

                        line = reader.ReadLine();
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка чтения файла.");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
                return;
            }

            if (hasNumber)
            {
                Console.WriteLine("Первый элемент: " + first);
                Console.WriteLine("Максимальный элемент: " + 
                    maximum);
                Console.WriteLine("Их сумма: " + ((long)first +
                    maximum));
                _fileOpsCount++;
            }
            else
            {
                Console.WriteLine("В файле нет чисел.");
            }
        }

        private static void FillTextLines(string path)
        {
            Console.WriteLine(
                "Введите строки. " +
                "Пустая строка завершает ввод.");

            using (StreamWriter writer = new StreamWriter(path))
            {
                bool reading = true;

                while (reading)
                {
                    string line = Console.ReadLine() ?? "";

                    if (line.Length == 0)
                    {
                        reading = false;
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
            string source = "input8.txt";
            string result = "output8.txt";

            try
            {
                FillTextLines(source);
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка записи файла.");
                return;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
                return;
            }

            if (!IsFileAvailable(source))
            {
                return;
            }

            try
            {
                using (StreamReader reader =
                    new StreamReader(source))
                using (StreamWriter writer =
                    new StreamWriter(result))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        string trimmed = line.Trim();

                        if (trimmed.Length > 0)
                        {
                            bool firstIsБ =
                                trimmed[0] == 'б' ||
                                trimmed[0] == 'Б';

                            bool secondIsБ =
                                trimmed.Length > 1 &&
                                (trimmed[1] == 'б' ||
                                 trimmed[1] == 'Б');

                            if (firstIsБ || secondIsБ)
                            {
                                writer.WriteLine(line);
                                Console.WriteLine(line);
                            }
                        }

                        line = reader.ReadLine();
                    }
                }

                _fileOpsCount++;
                Console.WriteLine(
                    "Результат сохранён в output8.txt.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка обработки файла.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
            }
        }
    }
}