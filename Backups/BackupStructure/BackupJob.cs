using System;
using System.Collections.Generic;
using Backups.Algorithms;
using Newtonsoft.Json;

namespace Backups.BackupStructure
{
    public class BackupJob
    {
        public BackupJob(SavingAlgorithm savingAlgorithm, string backupsDirectory)
        {
            SavingAlgorithm = savingAlgorithm ??
                              throw new ArgumentNullException(
                                  nameof(savingAlgorithm), "SavingAlgorithm cannot be null!");
            BackupsDirectory = backupsDirectory ??
                                   throw new ArgumentNullException(
                                       nameof(backupsDirectory), "Path cannot be null!");
        }

        [JsonProperty]
        protected List<JobObject> JobObjects { get; set; } = new ();

        [JsonProperty]
        protected SavingAlgorithm SavingAlgorithm { get; set; }

        [JsonProperty]
        protected string BackupsDirectory { get; set; }

        [JsonProperty]
        protected int RestorePointsCounter { get; set; }

        [JsonProperty]
        protected string RestorePointName { get; set; } = "RestorePoint";

        [JsonProperty]
        protected List<RestorePoint> RestorePoints { get; set; } = new ();

        public void Process()
        {
            SavingAlgorithm.Backup(JobObjects, RestorePointName + RestorePointsCounter);
            RestorePoints.Add(new RestorePoint(RestorePointName + RestorePointsCounter, DateTime.Now, JobObjects));
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
            BackupsDirectory = path;
            SavingAlgorithm.SetBackupsDirectoryPath(path);
        }

        public int GetRestorePointsNumber()
        {
            return RestorePointsCounter;
        }
    }
}