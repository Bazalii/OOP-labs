namespace Backups.FileSystem
{
    public interface IArchiver
    {
        void AddToArchive(string directoryToArchivePath, string archivePath);

        void ExtractFromArchive(string archivePath, string directoryToExtract);
    }
}