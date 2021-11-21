using System;
using System.Text;
using Backups.Tools;

namespace Backups.FileSystem.Implementations
{
    public class MemoryFileSystem : IFileSystem
    {
        private IDirectory _rootDirectory = new MemoryDirectory(string.Empty, "C:");

        public string GetRoot()
        {
            return _rootDirectory.GetPath();
        }

        public IStorageObject GetStorageObject(string path)
        {
            return DepthFirstSearch(_rootDirectory, path) ??
                   throw new StorageObjectNotExistException($"Storage object {path} doesn't exist!");
        }

        public void CreateFile(string pathToFile)
        {
            string parentDirectoryPath = GetParentDirectoryFromPath(pathToFile);
            var file = new MemoryFile(parentDirectoryPath, GetFullNameFromPath(pathToFile));
            var directory = GetStorageObject(parentDirectoryPath) as MemoryDirectory;
            directory.AddObject(file);
        }

        public void RemoveFile(string pathToFile)
        {
            var directory = GetStorageObject(GetParentDirectoryFromPath(pathToFile)) as MemoryDirectory;
            directory.RemoveObject(GetStorageObject(pathToFile));
        }

        public void WriteToFile(string pathToFile, string textToWrite)
        {
            var file = GetStorageObject(pathToFile) as MemoryFile;
            byte[] information = new UTF8Encoding(true).GetBytes(textToWrite);
            file.Write(information);
        }

        public void CopyFile(string oldPath, string newPath)
        {
            var newDirectory = GetStorageObject(GetParentDirectoryFromPath(newPath)) as IDirectory;
            var fileToCopy = GetStorageObject(oldPath) as MemoryFile;
            MemoryFile copyOfFile = new MemoryFile(GetParentDirectoryFromPath(newPath), GetFullNameFromPath(newPath));
            copyOfFile.Write(fileToCopy.Read());
            copyOfFile.SetPath(GetParentDirectoryFromPath(newPath));
            newDirectory.AddObject(copyOfFile);
        }

        public void CreateDirectory(string pathToNewDirectory)
        {
            string pathToParentDirectory = GetParentDirectoryFromPath(pathToNewDirectory);
            var parentDirectory = GetStorageObject(pathToParentDirectory) as MemoryDirectory;
            parentDirectory.AddObject(new MemoryDirectory(pathToParentDirectory, GetNameFromPath(pathToNewDirectory)));
        }

        public void RemoveDirectory(string pathToDirectory)
        {
            var directory = GetStorageObject(pathToDirectory) as MemoryDirectory;
            IStorageObject storageObject = GetStorageObject(pathToDirectory);
            directory.RemoveObject(storageObject);
        }

        public void CreateArchive(string pathToNewArchive)
        {
            string pathToParentDirectory = GetParentDirectoryFromPath(pathToNewArchive);
            var parentDirectory = GetStorageObject(pathToParentDirectory) as MemoryDirectory;
            parentDirectory.AddObject(new MemoryArchive(pathToParentDirectory, GetNameFromPath(pathToNewArchive)));
        }

        public void AddToArchive(string directoryToArchivePath, string archivePath)
        {
            MemoryDirectory memoryDirectory = GetStorageObject(directoryToArchivePath) as MemoryDirectory ??
                                              throw new StorageObjectNotExistException(
                                                  $"Directory {directoryToArchivePath} doesn't exist! ");
            CreateArchive(archivePath);
            foreach (IStorageObject storageObject in memoryDirectory.GetObjects())
            {
                CopyFile(storageObject.GetPath(), archivePath + "\\" + GetFullNameFromPath(storageObject.GetPath()));
            }
        }

        public void ExtractFromArchive(string archivePath, string directoryToExtract)
        {
            try
            {
                GetStorageObject(directoryToExtract);
            }
            catch (StorageObjectNotExistException)
            {
                CreateDirectory(directoryToExtract);
            }

            var archive = GetStorageObject(archivePath) as MemoryArchive;
            foreach (IStorageObject storageObject in archive.GetObjects())
            {
                CopyFile(storageObject.GetPath(), directoryToExtract + "\\" + GetFullNameFromPath(storageObject.GetPath()));
            }
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
            int dotIndex = fullFileName.IndexOf(".", StringComparison.Ordinal);
            int nameLength = dotIndex < 0 ? fullFileName.Length : dotIndex;
            return fullFileName.Substring(0, nameLength);
        }

        public string GetParentDirectoryFromPath(string path)
        {
            return path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
        }

        private IStorageObject DepthFirstSearch(IDirectory directory, string path)
        {
            if (path == directory.GetPath())
            {
                return directory;
            }

            foreach (IStorageObject storageObject in directory.GetObjects())
            {
                if (storageObject is IDirectory concreteDirectory)
                {
                    IStorageObject output = DepthFirstSearch(concreteDirectory, path);
                    if (output != null)
                    {
                        return output;
                    }
                }

                if (storageObject.GetPath() == path)
                {
                    return storageObject;
                }
            }

            return null;
        }
    }
}