using System;
using System.Collections.Generic;
using Backups.Algorithms;

namespace Backups.BackupStructure
{
    public class BackupJob
    {
        private int _restorePointsCounter;

        private string _restorePointName = "RestorePoint";

        private List<JobObject> _jobObjects = new ();

        private List<RestorePoint> _restorePoints = new ();

        public BackupJob(SavingAlgorithm savingAlgorithm)
        {
            SavingAlgorithm = savingAlgorithm ??
                              throw new ArgumentNullException(
                                  nameof(savingAlgorithm), "SavingAlgorithm cannot be null!");
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
    }
}