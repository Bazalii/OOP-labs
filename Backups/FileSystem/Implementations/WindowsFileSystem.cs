using System.IO;
using System.IO.Compression;
using System.Text;

namespace Backups.FileSystem.Implementations
{
    public class WindowsFileSystem : IFileSystem
    {
        public void WriteToFile(string pathToFile, string textToWrite)
        {
            FileStream file = File.Create(pathToFile);
            byte[] information = new UTF8Encoding(true).GetBytes(textToWrite);
            file.Write(information, 0, information.Length);
            file.Close();
        }

        public void AddToArchive(string directoryToArchivePath, string archivePath)
        {
            ZipFile.CreateFromDirectory(directoryToArchivePath, archivePath);
        }

        public void CreateDirectory(string pathToNewDirectory)
        {
            Directory.CreateDirectory(pathToNewDirectory);
        }

        public void RemoveDirectory(string pathToDirectory)
        {
            Directory.Delete(pathToDirectory);
        }

        public void CreateFile(string pathToFile)
        {
            File.Create(pathToFile).Close();
        }

        public void CopyFile(string oldPath, string newPath)
        {
            File.Copy(oldPath, newPath);
        }

        public void RemoveFile(string pathToFile)
        {
            File.Delete(pathToFile);
        }
    }
}