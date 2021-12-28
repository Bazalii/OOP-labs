using System;
using System.Collections.Generic;
using Backups.Algorithms;
using Backups.BackupStructure;
using Backups.FileSystem;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.RestoreAlgorithms
{
    public abstract class RestoreAlgorithm
    {
        [JsonProperty]
        protected IFileSystem FileSystem { get; set; }

        [JsonProperty]
        protected IArchiver Archiver { get; set; }

        [JsonProperty]
        protected string BackupsDirectory { get; set; }

        [JsonProperty]
        protected string RestoreDirectory { get; set; }

        public abstract void Restore(IReadOnlyList<JobObject> jobObjects, string restorePointName);

        public void SetFileSystem(IFileSystem fileSystem)
        {
            FileSystem = fileSystem ??
                         throw new ArgumentNullException(
                             nameof(fileSystem), "FileSystem cannot be null!");
        }

        public void SetBackupsDirectoryPath(string path)
        {
            BackupsDirectory = path ??
                               throw new ArgumentNullException(
                                   nameof(path), "Backup directory cannot be null!");
        }
    }
}