using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestConsole
{
    public class A
    {
        public virtual int Calc() => 10 * Gen();
        protected int Gen() => 10;
    }

    public class B : A
    {
        public override int Calc() => 20 * Gen();
        protected int Gen() => 20;
    }

    public class C: B
    {
        public override int Calc() => 30 * Gen();
        protected int Gen() => base.Gen();
    }
    class Program
    {
        public static void GetString()
        {
            //using var line = new StreamReader(@"C:\Users\gonzy\Desktop\Новый текстовый документ.txt");
            //var str2 = line.ReadToEnd();
            //var str3 = Regex.Replace(str2, @"([\p{P}\d])|([А-ЯЁа-яё]{20,})|([№<>|=])", "", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            //Console.WriteLine(str3);

        }
        static void Main(string[] args)
        {

            A a = new B();
            A a1 = new C();
            Console.WriteLine(a.Calc() + a1.Calc());
            Console.ReadKey();
            //Создание примера поиска текста при помощи регулярки
            //GetString();

            //Создание вордовского документа при помощи OpenXML
            //GeneratedClass generatedClass = new GeneratedClass();
            //generatedClass.CreatePackage(@"C:\Users\gonzy\Desktop\TestDoc1.docx");
        }
    }
}
