using System;
using System.Text;
using Backups.Tools;

namespace Backups.FileSystem.Implementations
{
    public class MemoryFileSystem : IFileSystem
    {
        private readonly VirtualDirectory _rootDirectory = new MemoryDirectory(string.Empty, "C:");

        public string GetRoot()
        {
            return _rootDirectory.GetPath();
        }

        public StorageObject GetStorageObject(string path)
        {
            return DepthFirstSearch(_rootDirectory, path) ??
                   throw new StorageObjectNotExistException($"Storage object {path} doesn't exist!");
        }

        public void CreateFile(string pathToFile)
        {
            try
            {
                GetStorageObject(pathToFile);
                throw new StorageObjectAlreadyExists($"File {pathToFile} already exists");
            }
            catch (StorageObjectNotExistException)
            {
                string parentDirectoryPath = GetParentDirectoryFromPath(pathToFile);
                var file = new MemoryFile(parentDirectoryPath, GetFullNameFromPath(pathToFile));
                var directory = GetStorageObject(parentDirectoryPath) as MemoryDirectory;
                directory.AddObject(file);
            }
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
            var newDirectory = GetStorageObject(GetParentDirectoryFromPath(newPath)) as VirtualDirectory;
            var fileToCopy = GetStorageObject(oldPath) as MemoryFile;
            var copyOfFile = new MemoryFile(GetParentDirectoryFromPath(newPath), GetFullNameFromPath(newPath));
            copyOfFile.Write(fileToCopy.Read());
            copyOfFile.SetPath(GetParentDirectoryFromPath(newPath));
            newDirectory.AddObject(copyOfFile);
        }

        public void CreateDirectory(string pathToNewDirectory)
        {
            try
            {
                GetStorageObject(pathToNewDirectory);
                throw new StorageObjectAlreadyExists($"Directory {pathToNewDirectory} already exists!");
            }
            catch (StorageObjectNotExistException)
            {
                string pathToParentDirectory = GetParentDirectoryFromPath(pathToNewDirectory);
                var parentDirectory = GetStorageObject(pathToParentDirectory) as MemoryDirectory;
                parentDirectory.AddObject(new MemoryDirectory(pathToParentDirectory, GetNameFromPath(pathToNewDirectory)));
            }
        }

        public void RemoveDirectory(string pathToDirectory)
        {
            var directory = GetStorageObject(GetParentDirectoryFromPath(pathToDirectory)) as MemoryDirectory;
            StorageObject storageObject = GetStorageObject(pathToDirectory);
            directory.RemoveObject(storageObject);
        }

        public void CreateArchive(string pathToNewArchive)
        {
            try
            {
                GetStorageObject(pathToNewArchive);
                throw new StorageObjectAlreadyExists($"Directory {pathToNewArchive} already exists!");
            }
            catch (StorageObjectNotExistException)
            {
                string pathToParentDirectory = GetParentDirectoryFromPath(pathToNewArchive);
                var parentDirectory = GetStorageObject(pathToParentDirectory) as MemoryDirectory;
                parentDirectory.AddObject(new MemoryArchive(pathToParentDirectory, GetNameFromPath(pathToNewArchive)));
            }
        }

        public void AddToArchive(string directoryToArchivePath, string archivePath)
        {
            MemoryDirectory memoryDirectory = GetStorageObject(directoryToArchivePath) as MemoryDirectory ??
                                              throw new StorageObjectNotExistException(
                                                  $"Directory {directoryToArchivePath} doesn't exist! ");
            try
            {
                GetStorageObject(archivePath);
            }
            catch (StorageObjectNotExistException)
            {
                CreateArchive(archivePath);
            }

            foreach (StorageObject storageObject in memoryDirectory.GetObjects())
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
            foreach (StorageObject storageObject in archive.GetObjects())
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
            return fullFileName[..nameLength];
        }

        public string GetParentDirectoryFromPath(string path)
        {
            return path[..path.LastIndexOf("\\", StringComparison.Ordinal)];
        }

        private StorageObject DepthFirstSearch(VirtualDirectory virtualDirectory, string path)
        {
            if (path == virtualDirectory.GetPath())
            {
                return virtualDirectory;
            }

            foreach (StorageObject storageObject in virtualDirectory.GetObjects())
            {
                if (storageObject is VirtualDirectory concreteDirectory)
                {
                    StorageObject output = DepthFirstSearch(concreteDirectory, path);
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