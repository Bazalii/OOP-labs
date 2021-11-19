using System;
using System.Text;
using Backups.Tools;

namespace Backups.FileSystem.Implementations
{
    public class MemoryFileSystem : IFileSystem
    {
        private IDirectory _rootDirectory = new MemoryDirectory("C:\\", string.Empty);

        public IDirectory GetRoot()
        {
            return _rootDirectory;
        }

        public IStorageObject GetStorageObject(string path)
        {
            return DepthFirstSearch(GetRoot(), path);
        }

        public void CreateFile(string pathToFile)
        {
            var file = new MemoryFile(pathToFile);
            var directory = GetStorageObject(GetParentDirectoryFromPath(pathToFile)) as MemoryDirectory;
            directory?.AddObject(file);
        }

        public void RemoveFile(string pathToFile)
        {
            var directory = GetStorageObject(GetParentDirectoryFromPath(pathToFile)) as MemoryDirectory;
            directory?.RemoveObject(GetStorageObject(pathToFile));
        }

        public void WriteToFile(string pathToFile, string textToWrite)
        {
            var file = GetStorageObject(pathToFile) as MemoryFile;
            byte[] information = new UTF8Encoding(true).GetBytes(textToWrite);
            file?.Write(information);
        }

        public void CopyFile(string oldPath, string newPath)
        {
            var oldDirectory = GetStorageObject(GetParentDirectoryFromPath(oldPath)) as MemoryDirectory;
            var newDirectory = GetStorageObject(GetParentDirectoryFromPath(newPath)) as MemoryDirectory;
            IStorageObject fileToCopy = GetStorageObject(GetNameFromPath(oldPath));
            newDirectory?.AddObject(fileToCopy);
            oldDirectory?.RemoveObject(fileToCopy);
        }

        public void CreateDirectory(string pathToNewDirectory)
        {
            var parentDirectory = GetStorageObject(GetParentDirectoryFromPath(pathToNewDirectory)) as MemoryDirectory;
            parentDirectory?.AddObject(new MemoryDirectory(pathToNewDirectory, GetNameFromPath(pathToNewDirectory)));
        }

        public void RemoveDirectory(string pathToDirectory)
        {
            var directory = GetStorageObject(pathToDirectory) as MemoryDirectory;
            IStorageObject storageObject = GetStorageObject(pathToDirectory);
            directory?.RemoveObject(storageObject);
        }

        public void AddToArchive(string directoryToArchivePath, string archivePath)
        {
            MemoryDirectory memoryDirectory = GetStorageObject(directoryToArchivePath) as MemoryDirectory ??
                                              throw new DirectoryNotExistException(
                                                  $"Directory {directoryToArchivePath} doesn't exist! ");
            MemoryArchive memoryArchive = GetStorageObject(archivePath) as MemoryArchive;
            foreach (IStorageObject storageObject in memoryDirectory.GetObjects())
            {
                memoryArchive.AddObject(storageObject);
            }
        }

        private IStorageObject DepthFirstSearch(IDirectory directory, string path)
        {
            foreach (IStorageObject storageObject in directory.GetObjects())
            {
                if (storageObject is IDirectory concreteDirectory)
                {
                    DepthFirstSearch(concreteDirectory, path);
                }

                if (storageObject.GetPath() == path)
                {
                    return storageObject;
                }
            }

            throw new DirectoryNotExistException($"Directory {path} doesn't exist!");
        }

        private string GetNameFromPath(string path)
        {
            return path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal), path.Length);
        }

        private string GetParentDirectoryFromPath(string path)
        {
            return path.Substring(0, path.LastIndexOf("\\", StringComparison.Ordinal));
        }
    }
}