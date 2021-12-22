using System;
using System.Collections.Generic;
using System.Text;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace Backups.Algorithms.Implementations
{
    public class SingleStorage : SavingAlgorithm
    {
        public SingleStorage(IFileSystem fileSystem, IArchiver archiver, string backupsDirectoryPath)
        {
            FileSystem = fileSystem ??
                         throw new ArgumentNullException(
                             nameof(fileSystem), "FileSystem cannot be null!");
            Archiver = archiver ??
                       throw new ArgumentNullException(
                           nameof(archiver), "Archiver cannot be null!");
            BackupsDirectory = backupsDirectoryPath ??
                               throw new ArgumentNullException(
                                   nameof(backupsDirectoryPath), "Path cannot be null!");
            SwapDirectory = fileSystem.GetRoot() + "\\Swap";
            FileSystem.CreateDirectory(BackupsDirectory);
        }

        public override void Backup(List<JobObject> jobObjects, string backupName)
        {
            List<byte> archiveFiles = new ();
            foreach (JobObject jobObject in jobObjects)
            {
                archiveFiles.AddRange(FileSystem.ReadFile(jobObject.PathToFile));
                archiveFiles.AddRange(Encoding.UTF8.GetBytes("$|$"));
            }

            byte[] compressedFiles = Archiver.Compress(archiveFiles.ToArray());
            FileSystem.WriteToFile(BackupsDirectory + $"\\{backupName}", compressedFiles);
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