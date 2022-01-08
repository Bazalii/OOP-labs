using System;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace BackupsExtra.Algorithms.MergeAlgorithms.Implementations
{
    public class SingleStorageMerge : MergeAlgorithm
    {
        public SingleStorageMerge(IFileSystem fileSystem, string backupsDirectory)
        {
            FileSystem = fileSystem ??
                         throw new ArgumentNullException(
                             nameof(fileSystem), "FileSystem cannot be null!");
            BackupsDirectory = backupsDirectory ??
                               throw new ArgumentNullException(
                                   nameof(backupsDirectory), "Backup directory cannot be null!");
        }

        public override RestorePoint Merge(
            RestorePoint firstRestorePoint,
            RestorePoint secondRestorePoint)
        {
            return secondRestorePoint;
        }
    }
}