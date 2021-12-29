using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace BackupsExtra.Algorithms.PointRemovalAlgorithms.Implementations
{
    public class SingleStorageRemovalAlgorithm : PointRemovalAlgorithm
    {
        public SingleStorageRemovalAlgorithm(IFileSystem fileSystem, string backupsDirectory)
        {
            FileSystem = fileSystem ??
                         throw new ArgumentNullException(
                             nameof(fileSystem), "FileSystem cannot be null!");
            BackupsDirectory = backupsDirectory ??
                               throw new ArgumentNullException(
                                   nameof(backupsDirectory), "Backup directory cannot be null!");
        }

        public override void RemoveRestorePoints(List<RestorePoint> restorePoints)
        {
            foreach (RestorePoint restorePoint in restorePoints)
            {
                FileSystem.RemoveFile($"{BackupsDirectory}\\{restorePoint.Name}");
            }
        }
    }
}