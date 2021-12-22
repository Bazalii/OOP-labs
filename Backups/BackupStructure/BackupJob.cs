using System;
using System.Collections.Generic;
using Backups.Algorithms;

namespace Backups.BackupStructure
{
    public class BackupJob
    {
        public BackupJob(SavingAlgorithm savingAlgorithm, string backupsDirectoryPath)
        {
            SavingAlgorithm = savingAlgorithm ??
                              throw new ArgumentNullException(
                                  nameof(savingAlgorithm), "SavingAlgorithm cannot be null!");
            BackupsDirectoryPath = backupsDirectoryPath ??
                                   throw new ArgumentNullException(
                                       nameof(backupsDirectoryPath), "Path cannot be null!");
        }

        public SavingAlgorithm SavingAlgorithm { get; set; }

        protected string BackupsDirectoryPath { get; set; }

        protected int RestorePointsCounter { get; set; }

        protected string RestorePointName { get; set; } = "RestorePoint";

        protected List<JobObject> JobObjects { get; set; } = new ();

        protected List<RestorePoint> RestorePoints { get; set; } = new ();

        public void Process()
        {
            SavingAlgorithm.Backup(JobObjects, RestorePointName + RestorePointsCounter);
            RestorePoints.Add(new RestorePoint(DateTime.Now, JobObjects));
            RestorePointsCounter += 1;
        }

        public void AddJobObject(JobObject jobObject)
        {
            JobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObjectToRemove)
        {
            JobObjects.Remove(jobObjectToRemove);
        }

        public void SetBackupPath(string path)
        {
            BackupsDirectoryPath = path;
            SavingAlgorithm.SetBackupsDirectoryPath(path);
        }

        public int GetRestorePointsNumber()
        {
            return RestorePointsCounter;
        }
    }
}