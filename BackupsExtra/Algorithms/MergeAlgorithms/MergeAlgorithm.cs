using System;
using Backups.BackupStructure;
using Backups.FileSystem;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.MergeAlgorithms
{
    public abstract class MergeAlgorithm
    {
        [JsonProperty]
        protected IFileSystem FileSystem { get; set; }

        [JsonProperty]
        protected string BackupsDirectory { get; set; }

        public abstract RestorePoint Merge(
            RestorePoint firstRestorePoint,
            RestorePoint secondRestorePoint);

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