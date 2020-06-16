using GuessYouSelf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace GuessYouSelf.Core
{
    public class TextFileService : IFileService
    {
        public IEnumerable<string> Open(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                var line = sr.ReadToEnd().Trim();
                line = Regex.Replace(line, @"([\p{P}\d])|([А-ЯЁа-яё]{20,})|([№<>|=])", "", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                var str = line.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var item in str)
                {
                    yield return item;
                }
            }
        }

        public void Save(string fileName, IEnumerable<string> str)
        {
            using (StreamWriter sr = new StreamWriter(fileName, true, Encoding.Default))
            {
                foreach (var st in str)
                {
                    sr.WriteLine(st);
                }
            }
        }
    }
}
