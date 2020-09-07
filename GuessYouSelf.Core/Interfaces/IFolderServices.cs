namespace GuessYouSelf.Core.Interfaces
{
    public interface IFolderServices
    {
        string PathToFolder { get; set; }
        void CreateFolder();
        void DeleteFolder();
    }
}
