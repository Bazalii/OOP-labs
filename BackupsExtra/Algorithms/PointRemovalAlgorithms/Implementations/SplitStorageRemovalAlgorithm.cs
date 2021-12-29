using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace BackupsExtra.Algorithms.PointRemovalAlgorithms.Implementations
{
    public class SplitStorageRemovalAlgorithm : PointRemovalAlgorithm
    {
        public SplitStorageRemovalAlgorithm(IFileSystem fileSystem, string backupsDirectory)
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
                foreach (JobObject backupedFile in restorePoint.BackupedFiles)
                {
                    FileSystem.RemoveFile($"{BackupsDirectory}\\{restorePoint.Name}{FileSystem.GetNameFromPath(backupedFile.PathToFile)}");
                }
            }
        }
    }
}