using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace Backups.Algorithms.Implementations
{
    public class SingleStorage : SavingAlgorithm
    {
        public SingleStorage(IFileSystem fileSystem, string backupsDirectoryPath)
        {
            FileSystem = fileSystem ??
                         throw new ArgumentNullException(
                             nameof(fileSystem), "FileSystem cannot be null!");
            BackupsDirectory = backupsDirectoryPath ??
                               throw new ArgumentNullException(
                                   nameof(backupsDirectoryPath), "Path cannot be null!");
            SwapDirectory = fileSystem.GetRoot() + "\\Swap";
            FileSystem.CreateDirectory(BackupsDirectory);
        }

        public override void Backup(List<JobObject> jobObjects, string backupName)
        {
            FileSystem.CreateDirectory(SwapDirectory);
            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.CopyFile(jobObject.PathToFile, SwapDirectory + "\\" + FileSystem.GetFullNameFromPath(jobObject.PathToFile));
            }

            FileSystem.AddToArchive(SwapDirectory, BackupsDirectory + $"\\{backupName}");
            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.RemoveFile(SwapDirectory + "\\" + FileSystem.GetFullNameFromPath(jobObject.PathToFile));
            }

            FileSystem.RemoveDirectory(SwapDirectory);
        }

        public override void SetFileSystem(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public override void SetBackupsDirectoryPath(string path)
        {
            FileSystem.CreateDirectory(path);
            BackupsDirectory = path;
        }
    }
}