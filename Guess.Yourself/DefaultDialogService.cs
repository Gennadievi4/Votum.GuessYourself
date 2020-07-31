using Guess.Yourself.Interfaces;
using Microsoft.Win32;
using System;
using System.Windows;

namespace Guess.Yourself
{
    public class DefaultDialogService : IDialogServices
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public bool OpenDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Текстовый документ: (.txt)|*.txt";
            openFileDialog.Title = "Обзор";
            openFileDialog.Multiselect = false;
            openFileDialog.ValidateNames = true;

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                FileName = openFileDialog.SafeFileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            FolderService folderService = new FolderService();

            saveFileDialog.Filter = "Документ Word: (*.docx)|*.docx|Документ Word 97-2003: (*.doc)|*.doc";
            saveFileDialog.DefaultExt = ".docx";
            saveFileDialog.AddExtension = true;

            folderService.CreateFolder();
            saveFileDialog.InitialDirectory = folderService.PathToFolder;

            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
