﻿using System;
using Backups.Tools;

namespace Backups.FileSystem.Implementations
{
    public class MemoryFileSystem : IFileSystem
    {
        private static MemoryFileSystem _instance;

        private readonly VirtualDirectory _rootDirectory = new MemoryDirectory(string.Empty, "C:");

        private MemoryFileSystem()
        {
        }

        public static MemoryFileSystem GetInstance()
        {
            return _instance ??= new MemoryFileSystem();
        }

        public static void Reset()
        {
            _instance = null;
        }

        public string GetRoot()
        {
            return _rootDirectory.GetPath();
        }

        public StorageObject GetStorageObject(string path)
        {
            return DepthFirstSearch(_rootDirectory, path);
        }

        public void CreateFile(string pathToFile)
        {
            if (GetStorageObject(pathToFile) != null)
                throw new StorageObjectAlreadyExists($"File {pathToFile} already exists");
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

        public void WriteToFile(string pathToFile, byte[] information)
        {
            var file = GetStorageObject(pathToFile) as MemoryFile;
            if (file == null)
            {
                CreateFile(pathToFile);
                file = GetStorageObject(pathToFile) as MemoryFile;
            }

            file.Write(information);
        }

        public byte[] ReadFile(string pathToFile)
        {
            var file = GetStorageObject(pathToFile) as MemoryFile;
            return file.Read();
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
            if (GetStorageObject(pathToNewDirectory) != null)
                throw new StorageObjectAlreadyExists($"Directory {pathToNewDirectory} already exists!");
            string pathToParentDirectory = GetParentDirectoryFromPath(pathToNewDirectory);
            var parentDirectory = GetStorageObject(pathToParentDirectory) as MemoryDirectory;
            parentDirectory.AddObject(new MemoryDirectory(pathToParentDirectory, GetNameFromPath(pathToNewDirectory)));
        }

        public void RemoveDirectory(string pathToDirectory)
        {
            var directory = GetStorageObject(GetParentDirectoryFromPath(pathToDirectory)) as MemoryDirectory;
            StorageObject storageObject = GetStorageObject(pathToDirectory);
            directory.RemoveObject(storageObject);
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