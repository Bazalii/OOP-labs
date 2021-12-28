using System;
using System.Collections.Generic;
using System.Text;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace BackupsExtra.Algorithms.RestoreAlgorithms.Implementations
{
    public class SingleStorageRestoring : RestoreAlgorithm
    {
        public SingleStorageRestoring(IFileSystem fileSystem, IArchiver archiver, string backupsDirectory, string restoreDirectory = null)
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
            string uncompressedFiles = Encoding.UTF8.GetString(
                Archiver.Decompress(FileSystem.ReadFile(BackupsDirectory + $"\\{restorePointName}")));
            if (RestoreDirectory == null)
            {
                foreach (JobObject jobObject in jobObjects)
                {
                    FileSystem.WriteToFile(
                        jobObject.PathToFile,
                        Encoding.UTF8.GetBytes(
                            uncompressedFiles[..uncompressedFiles.IndexOf("$", StringComparison.Ordinal)]));
                    uncompressedFiles = uncompressedFiles.Remove(0, uncompressedFiles.IndexOf("|", StringComparison.Ordinal) + 2);
                }
            }

            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.WriteToFile(
                    RestoreDirectory + $"\\{FileSystem.GetFullNameFromPath(jobObject.PathToFile)}",
                    Encoding.UTF8.GetBytes(
                        uncompressedFiles[..uncompressedFiles.IndexOf("$", StringComparison.Ordinal)]));
                uncompressedFiles = uncompressedFiles.Remove(0, uncompressedFiles.IndexOf("|", StringComparison.Ordinal) + 2);
            }
        }
    }
}