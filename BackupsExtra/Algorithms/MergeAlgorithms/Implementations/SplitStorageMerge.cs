using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace BackupsExtra.Algorithms.MergeAlgorithms.Implementations
{
    public class SplitStorageMerge : MergeAlgorithm
    {
        public SplitStorageMerge(IFileSystem fileSystem, string backupsDirectory)
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
            List<JobObject> jobObjects = new ();
            foreach (JobObject firstJobObject in firstRestorePoint.BackupedFiles)
            {
                bool flag = false;
                string firstFileName = FileSystem.GetNameFromPath(firstJobObject.PathToFile);
                foreach (JobObject secondJobObject in secondRestorePoint.BackupedFiles)
                {
                    if (firstJobObject.PathToFile == secondJobObject.PathToFile)
                    {
                        flag = true;
                        FileSystem.RemoveFile(BackupsDirectory +
                                              $"{firstRestorePoint.Name}{firstFileName}");
                        break;
                    }
                }

                if (flag) continue;
                byte[] information = FileSystem.ReadFile(
                    BackupsDirectory + $"\\{firstRestorePoint.Name}{firstFileName}");
                FileSystem.WriteToFile(
                    BackupsDirectory + $"\\{secondRestorePoint.Name}{firstFileName}",
                    information);
                FileSystem.RemoveFile(
                    BackupsDirectory + $"\\{firstRestorePoint.Name}{firstFileName}");
                jobObjects.Add(firstJobObject);
            }

            jobObjects.AddRange(secondRestorePoint.BackupedFiles);
            return new RestorePoint(secondRestorePoint.Name, secondRestorePoint.TimeOfBackup, jobObjects);
        }
    }
}