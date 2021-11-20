using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Backups.FileSystem.Implementations
{
    public class WindowsFileSystem : IFileSystem
    {
        public string GetRoot()
        {
            string path = Path.GetFullPath("BackupsTest.cs");
            return path.Substring(0, path.IndexOf("\\", StringComparison.Ordinal));
        }

        public void CreateFile(string pathToFile)
        {
            File.Create(pathToFile).Close();
        }

        public void RemoveFile(string pathToFile)
        {
            File.Delete(pathToFile);
        }

        public void WriteToFile(string pathToFile, string textToWrite)
        {
            FileStream file = File.Create(pathToFile);
            byte[] information = new UTF8Encoding(true).GetBytes(textToWrite);
            file.Write(information, 0, information.Length);
            file.Close();
        }

        public void CopyFile(string oldPath, string newPath)
        {
            File.Copy(oldPath, newPath);
        }

        public void CreateDirectory(string pathToNewDirectory)
        {
            Directory.CreateDirectory(pathToNewDirectory);
        }

        public void RemoveDirectory(string pathToDirectory)
        {
            Directory.Delete(pathToDirectory);
        }

        public void AddToArchive(string directoryToArchivePath, string archivePath)
        {
            ZipFile.CreateFromDirectory(directoryToArchivePath, archivePath);
        }

        public void ExtractFromArchive(string archivePath, string directoryToExtract)
        {
            ZipFile.ExtractToDirectory(archivePath, directoryToExtract);
        }

        public string GetFullNameFromPath(string path)
        {
            return path.Substring(
                path.LastIndexOf("\\", StringComparison.Ordinal) + 1,
                path.Length - path.LastIndexOf("\\", StringComparison.Ordinal) - 1);
        }

        public string GetNameFromPath(string path)
        {
            string fullFileName = GetFullNameFromPath(path);
            return fullFileName.Substring(0, fullFileName.IndexOf(".", StringComparison.Ordinal));
        }

        public string GetParentDirectoryFromPath(string path)
        {
            return path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
        }
    }
}