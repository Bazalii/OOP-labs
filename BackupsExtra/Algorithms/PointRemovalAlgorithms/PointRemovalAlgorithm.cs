using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.PointRemovalAlgorithms
{
    public abstract class PointRemovalAlgorithm
    {
        [JsonProperty]
        protected IFileSystem FileSystem { get; set; }

        [JsonProperty]
        protected string BackupsDirectory { get; set; }

        public abstract void RemoveRestorePoints(List<RestorePoint> restorePoints);
    }
}