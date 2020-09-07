namespace GuessYouSelf.Core.Interfaces
{
    public interface IFileService
    {
        void Save(string fileName, StudentModel std);

        void SaveAs();
    }
}