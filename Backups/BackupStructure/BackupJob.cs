using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms;

namespace Backups.BackupStructure
{
    public class BackupJob
    {
        private string _backupsDirectoryPath;

        private int _restorePointsCounter;

        private string _restorePointName = "RestorePoint";

        private List<JobObject> _jobObjects = new ();

        private List<RestorePoint> _restorePoints = new ();

        public BackupJob(SavingAlgorithm savingAlgorithm, string backupsDirectoryPath)
        {
            SavingAlgorithm = savingAlgorithm ??
                              throw new ArgumentNullException(
                                  nameof(savingAlgorithm), "SavingAlgorithm cannot be null!");
            _backupsDirectoryPath = backupsDirectoryPath;
        }

        public SavingAlgorithm SavingAlgorithm { get; set; }

        public void Process()
        {
            SavingAlgorithm.Backup(_jobObjects, _restorePointName + _restorePointsCounter);
            _restorePoints.Add(new RestorePoint(DateTime.Now, _jobObjects));
            _restorePointsCounter += 1;
        }

        public void AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObjectToRemove)
        {
            _jobObjects.Remove(jobObjectToRemove);
        }

        public void SetBackupPath(string path)
        {
            _backupsDirectoryPath = path;
            SavingAlgorithm.SetBackupsDirectoryPath(path);
        }

        public int GetRestorePointsNumber()
        {
            return _restorePointsCounter;
        }
    }
}