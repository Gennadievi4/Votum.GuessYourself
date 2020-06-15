using Guess.Yourself.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Guess.Yourself
{
    public class TextFileService : IFileService
    {
        public IEnumerable<string> Open(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                //string pattern = @"\b[А-ЯЁа-яё][а-яё]+\b";
                //var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                //var line = sr
                //    .ReadToEnd()
                //    .Replace(",", "")
                //    .Replace('\r', ' ')
                //    .Replace('\n', ' ')
                //    .Trim()
                //    .Where(x => !char.IsDigit(x) && !char.IsControl(x) && !char.IsPunctuation(x) && !char.IsSymbol(x))
                //    .ToArray();

                var line = sr.ReadToEnd().Trim();
                line = Regex.Replace(line, @"([\p{P}\d])|([А-ЯЁа-яё]{20,})|([№<>|=])", "", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                var str = line.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in str)
                {
                    yield return item;
                }

                //while (!sr.EndOfStream)
                //{
                //    if (string.IsNullOrWhiteSpace(Regex.Match(line, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Value)) continue;
                //    if (Regex.IsMatch(line, pattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase))
                //        yield return Regex.Match(line, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Value;


                //    foreach (Match match in regex.Matches(line))
                //    {
                //        yield return match.Value;
                //    }
                //}
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