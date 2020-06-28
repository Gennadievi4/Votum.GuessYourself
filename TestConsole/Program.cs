using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TestConsole
{
    class Program
    {
        public static void GetString()
        {
            using var line = new StreamReader(@"C:\Users\gonzy\Desktop\Новый текстовый документ.txt");
            var str2 = line.ReadToEnd();
            var str3 = Regex.Replace(str2, @"([\p{P}\d])|([А-ЯЁа-яё]{20,})|([№<>|=])", "", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            Console.WriteLine(str3);

        }
        static void Main(string[] args)
        {
            GetString();
        }
    }
}
