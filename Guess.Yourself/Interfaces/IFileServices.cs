using System.Collections.Generic;

namespace Guess.Yourself.Interfaces
{
    public interface IFileService
    {
        IEnumerable<string> Open(string fileName);
        void Save(string fileName, StudentModel std);

        void SaveAs();
    }
}