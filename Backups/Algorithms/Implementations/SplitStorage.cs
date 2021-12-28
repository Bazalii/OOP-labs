using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace Backups.Algorithms.Implementations
{
    public class SplitStorage : SavingAlgorithm
    {
        public SplitStorage(IFileSystem fileSystem, IArchiver archiver, string backupsDirectory)
        {
            FileSystem = fileSystem ??
                         throw new ArgumentNullException(
                             nameof(fileSystem), "FileSystem cannot be null!");
            Archiver = archiver ??
                       throw new ArgumentNullException(
                           nameof(archiver), "Archiver cannot be null!");
            BackupsDirectory = backupsDirectory ??
                               throw new ArgumentNullException(
                                   nameof(backupsDirectory), "Path cannot be null!");
            SwapDirectory = fileSystem.GetRoot() + "\\Swap";
            FileSystem.CreateDirectory(BackupsDirectory);
        }

        public override void Backup(List<JobObject> jobObjects, string backupName)
        {
            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.WriteToFile(
                    BackupsDirectory + $"\\{backupName + FileSystem.GetNameFromPath(jobObject.PathToFile)}",
                    Archiver.Compress(FileSystem.ReadFile(jobObject.PathToFile)));
            }
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