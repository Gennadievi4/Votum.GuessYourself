﻿using System.Collections.Generic;

namespace Guess.Yourself.Interfaces
{
    public interface IFileService
    {
        IEnumerable<string> Open(string fileName);
        void Save(string fileName, IEnumerable<AnswerLog> str);

        void SaveAs();
    }
}