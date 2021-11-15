using System;
using System.Collections.Generic;
using Backups.Algorithms;

namespace Backups.BackupStructure
{
    public class BackupJob
    {
        private List<JobObject> _jobObjects = new ();

        private List<RestorePoint> _restorePoints = new ();

        public BackupJob(ISavingAlgorithm savingAlgorithm)
        {
            SavingAlgorithm = savingAlgorithm ??
                              throw new ArgumentNullException(
                                  nameof(savingAlgorithm), "SavingAlgorithm cannot be null!");
        }

        public ISavingAlgorithm SavingAlgorithm { get; set; }

        public void Process()
        {
            SavingAlgorithm.Backup(_jobObjects);
            _restorePoints.Add(new RestorePoint(DateTime.Now, _jobObjects));
        }

        public void AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }
    }
}