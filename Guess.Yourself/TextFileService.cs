using Guess.Yourself.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Guess.Yourself
{
    public class TextFileService : IFileService
    {
        public IEnumerable<string> Open(string fileName)
        {
            using(StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        public void Save(string fileName, IEnumerable<string> str)
        {
            using(StreamWriter sr = new StreamWriter(fileName, true, Encoding.Default))
            {
                foreach(var st in str)
                {
                    sr.WriteLine(st);
                }
            }
        }
    }
}