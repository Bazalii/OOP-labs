using System;
using System.Collections.Generic;

namespace Backups.BackupStructure
{
    public class RestorePoint
    {
        private List<JobObject> _backupedFiles = new ();

        public RestorePoint(DateTime timeOfBackup, List<JobObject> backupedFiles)
        {
            TimeOfBackup = timeOfBackup;
            _backupedFiles.AddRange(backupedFiles);
        }

        public IReadOnlyList<JobObject> BackupedFiles => _backupedFiles;
        public DateTime TimeOfBackup { get; set; }
    }
}