using System;
using System.Collections.Generic;

namespace Backups.BackupStructure
{
    public class RestorePoint
    {
        private readonly List<JobObject> _backupedFiles = new ();

        public RestorePoint(DateTime timeOfBackup, IEnumerable<JobObject> backupedFiles)
        {
            TimeOfBackup = timeOfBackup;
            _backupedFiles.AddRange(backupedFiles);
        }

        public IReadOnlyList<JobObject> BackupedFiles => _backupedFiles;
        public DateTime TimeOfBackup { get; }
    }
}