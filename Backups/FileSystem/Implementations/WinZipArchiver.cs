using System.IO.Compression;

namespace Backups.FileSystem.Implementations
{
    public class WinZipArchiver : IArchiver
    {
        public void AddToArchive(string directoryToArchivePath, string archivePath)
        {
            ZipFile.CreateFromDirectory(directoryToArchivePath, archivePath);
        }

        public void ExtractFromArchive(string archivePath, string directoryToExtract)
        {
            ZipFile.ExtractToDirectory(archivePath, directoryToExtract);
        }
    }
}