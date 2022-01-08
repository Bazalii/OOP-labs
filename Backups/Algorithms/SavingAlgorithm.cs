using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;
using Newtonsoft.Json;

namespace Backups.Algorithms
{
    public abstract class SavingAlgorithm
    {
        [JsonProperty]
        protected IFileSystem FileSystem { get; set; }

        [JsonProperty]
        protected IArchiver Archiver { get; set; }

        [JsonProperty]
        protected string BackupsDirectory { get; set; }

        protected string SwapDirectory { get; set; }

        public abstract void Backup(List<JobObject> jobObjects, string backupName);

        public abstract void SetFileSystem(IFileSystem fileSystem);

        public abstract void SetBackupsDirectoryPath(string path);
    }
}