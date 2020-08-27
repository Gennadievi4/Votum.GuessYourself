using System.Collections.Generic;

namespace Guess.Yourself.Interfaces
{
    public interface IFileService
    {
        void Save(string fileName, StudentModel std);

        void SaveAs();
    }
}