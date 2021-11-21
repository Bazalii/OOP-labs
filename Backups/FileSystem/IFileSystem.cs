namespace Backups.FileSystem
{
    public interface IFileSystem
    {
        string GetRoot();

        void CreateFile(string pathToFile);

        void RemoveFile(string pathToFile);

        void WriteToFile(string pathToFile, string textToWrite);

        void CopyFile(string oldPath, string newPath);

        void CreateDirectory(string pathToNewDirectory);

        void RemoveDirectory(string pathToDirectory);

        void AddToArchive(string directoryToArchivePath, string archivePath);

        void ExtractFromArchive(string archivePath, string directoryToExtract);

        string GetFullNameFromPath(string path);

        string GetNameFromPath(string path);

        string GetParentDirectoryFromPath(string path);
    }
}