using GuessYouSelf.Core.Interfaces;

namespace GuessYouSelf.Core
{
    public class TextFileService : IFileService
    {
        public void Save(string fileName, StudentModel std)
        {
            WordDocument wordDocument = new WordDocument(std);
            wordDocument.CreatePackage(fileName);
        }

        public void SaveAs()
        {

        }
    }
}
