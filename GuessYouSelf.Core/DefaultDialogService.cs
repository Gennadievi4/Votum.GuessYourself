using GuessYouSelf.Core.Interfaces;
using Microsoft.Win32;
using System.Windows;

namespace GuessYouSelf.Core
{
    internal class DefaultDialogService : IDialogService
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
            if (saveFileDialog.ShowDialog() == false)
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
