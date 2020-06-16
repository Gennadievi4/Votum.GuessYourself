using System.Collections.Generic;

namespace GuessYouSelf.Core.Interfaces
{
    public interface IFileService
    {
        IEnumerable<string> Open(string fileName);
        void Save(string fileName, IEnumerable<string> str);
    }
}