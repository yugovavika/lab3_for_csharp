using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public static class Checked
    {
        public static int ReadInt(string prompt)
        {
            int result = 0;
            bool isParsed = false;

            while (!isParsed)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                isParsed = int.TryParse(input, out result);

                if (!isParsed)
                {
                    Console.WriteLine("Ошибка. " +
                        "Введите целое число.");
                }
            }
            return result;
        }

        public static int ReadInt(string prompt, 
            int min, int max)
        {
            int result = 0;
            bool isParsed = false;

            while (!isParsed)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                isParsed = int.TryParse(input, out result);

                if (!isParsed)
                {
                    Console.WriteLine("Ошибка. " +
                        "Введите целое число.");
                }
                else if (result < min || result > max)
                {
                    isParsed = false;
                    Console.WriteLine($"Ошибка. " +
                        $"Число должно быть " +
                        $"от {min} до {max}.");
                }
            }
            return result;
        }
    }
}