using System;
using System.Collections.Generic;
using System.Text;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace BackupsExtra.Algorithms.RestoreAlgorithms.Implementations
{
    public class SplitStorageRestoring : RestoreAlgorithm
    {
        public SplitStorageRestoring(IFileSystem fileSystem, IArchiver archiver, string backupsDirectory, string restoreDirectory)
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
            RestoreDirectory = restoreDirectory;
        }

        public override void Restore(IReadOnlyList<JobObject> jobObjects, string restorePointName)
        {
            if (RestoreDirectory == null)
            {
                foreach (JobObject jobObject in jobObjects)
                {
                    FileSystem.WriteToFile(
                        jobObject.PathToFile,
                        Archiver.Decompress(FileSystem.ReadFile(BackupsDirectory +
                                                                $"\\{restorePointName}{FileSystem.GetNameFromPath(jobObject.PathToFile)}")));
                }
            }

            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.WriteToFile(
                    RestoreDirectory + $"\\{FileSystem.GetFullNameFromPath(jobObject.PathToFile)}",
                    Archiver.Decompress(FileSystem.ReadFile(BackupsDirectory +
                                                            $"\\{restorePointName}{FileSystem.GetNameFromPath(jobObject.PathToFile)}")));
            }
        }
    }
}