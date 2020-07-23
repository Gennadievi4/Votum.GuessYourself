namespace Guess.Yourself.Interfaces
{
    public interface IDialogServices
    {
        void ShowMessage(string message);
        string FilePath { get; set; }
        string FileName { get; set; }
        bool OpenDialog();
        bool SaveDialog();
    }
}
