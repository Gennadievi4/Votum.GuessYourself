using Guess.Yourself.Interfaces;
using System;
using System.IO;

namespace Guess.Yourself
{
    public class FolderService : IFolderServices
    {
        public string PathToFolder { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "Архив вопросов";

        public void CreateFolder()
        {
            if (!Directory.Exists(PathToFolder))
            {
                Directory.CreateDirectory(PathToFolder);
            }
        }

        public void DeleteFolder()
        {
            
        }
    }
}
