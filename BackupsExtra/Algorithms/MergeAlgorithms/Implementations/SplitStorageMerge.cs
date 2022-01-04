using System;
using System.Collections.Generic;
using System.Linq;
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
                string firstFileName = FileSystem.GetNameFromPath(firstJobObject.PathToFile);
                JobObject wantedJobObject =
                    secondRestorePoint.BackupedFiles.FirstOrDefault(jobObject =>
                        firstJobObject.PathToFile == jobObject.PathToFile);
                if (wantedJobObject != null)
                {
                    FileSystem.RemoveFile(BackupsDirectory +
                                          $"{firstRestorePoint.Name}{firstFileName}");
                    continue;
                }

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