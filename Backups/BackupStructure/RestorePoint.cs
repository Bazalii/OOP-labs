using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backups.BackupStructure
{
    public class RestorePoint
    {
        [JsonProperty]
        private readonly List<JobObject> _backupedFiles = new ();

        public RestorePoint(string name, DateTime timeOfBackup, IEnumerable<JobObject> backupedFiles)
        {
            Name = name ??
                   throw new ArgumentNullException(
                       nameof(name), "Name cannot be null!");
            TimeOfBackup = timeOfBackup;
            _backupedFiles.AddRange(backupedFiles);
        }

        public IReadOnlyList<JobObject> BackupedFiles => _backupedFiles;

        public DateTime TimeOfBackup { get; }

        public string Name { get; }
    }
}