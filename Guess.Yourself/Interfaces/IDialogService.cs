namespace Guess.Yourself.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string message);
        string FilePath { get; set; }
        string FileName { get; set; }
        bool OpenDialog();
        bool SaveDialog();
    }
}
