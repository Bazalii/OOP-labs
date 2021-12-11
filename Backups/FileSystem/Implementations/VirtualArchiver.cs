using Backups.Tools;

namespace Backups.FileSystem.Implementations
{
    public class VirtualArchiver : IArchiver
    {
        public void CreateArchive(string pathToNewArchive)
        {
            try
            {
                MemoryFileSystem.GetInstance().GetStorageObject(pathToNewArchive);
                throw new StorageObjectAlreadyExists($"Directory {pathToNewArchive} already exists!");
            }
            catch (StorageObjectNotExistException)
            {
                string pathToParentDirectory = MemoryFileSystem.GetInstance().GetParentDirectoryFromPath(pathToNewArchive);
                var parentDirectory = MemoryFileSystem.GetInstance().GetStorageObject(pathToParentDirectory) as MemoryDirectory;
                parentDirectory.AddObject(new MemoryArchive(pathToParentDirectory, MemoryFileSystem.GetInstance().GetNameFromPath(pathToNewArchive)));
            }
        }

        public void AddToArchive(string directoryToArchivePath, string archivePath)
        {
            MemoryDirectory memoryDirectory = MemoryFileSystem.GetInstance().GetStorageObject(directoryToArchivePath) as MemoryDirectory ??
                                              throw new StorageObjectNotExistException(
                                                  $"Directory {directoryToArchivePath} doesn't exist! ");
            try
            {
                MemoryFileSystem.GetInstance().GetStorageObject(archivePath);
            }
            catch (StorageObjectNotExistException)
            {
                CreateArchive(archivePath);
            }

            foreach (StorageObject storageObject in memoryDirectory.GetObjects())
            {
                MemoryFileSystem.GetInstance().CopyFile(
                    storageObject.GetPath(),
                    archivePath + "\\" + MemoryFileSystem.GetInstance().GetFullNameFromPath(storageObject.GetPath()));
            }
        }

        public void ExtractFromArchive(string archivePath, string directoryToExtract)
        {
            try
            {
                MemoryFileSystem.GetInstance().GetStorageObject(directoryToExtract);
            }
            catch (StorageObjectNotExistException)
            {
                MemoryFileSystem.GetInstance().CreateDirectory(directoryToExtract);
            }

            var archive = MemoryFileSystem.GetInstance().GetStorageObject(archivePath) as MemoryArchive;
            foreach (StorageObject storageObject in archive.GetObjects())
            {
                MemoryFileSystem.GetInstance().CopyFile(
                    storageObject.GetPath(),
                    directoryToExtract + "\\" + MemoryFileSystem.GetInstance().GetFullNameFromPath(storageObject.GetPath()));
            }
        }
    }
}