using Guess.Yourself.Interfaces;

namespace Guess.Yourself
{
    public class TextFileService : IFileService
    {
        public void Save(string fileName, StudentModel std)
        {
            //using (StreamWriter sr = new StreamWriter(fileName, true, Encoding.Default))
            //{
            //    foreach (var st in str)
            //    {
            //        sr.WriteLine(st);
            //    }
            //}
            WordDocument wordDocument = new WordDocument(std);
            wordDocument.CreatePackage(fileName);
        }

        public void SaveAs()
        {

        }
    }
}