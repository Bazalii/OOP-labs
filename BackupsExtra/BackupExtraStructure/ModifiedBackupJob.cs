using System;
using System.Linq;
using Backups.Algorithms;
using Backups.BackupStructure;
using BackupsExtra.Algorithms.RestoreAlgorithms;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.BackupExtraStructure
{
    public class ModifiedBackupJob : BackupJob
    {
        public ModifiedBackupJob(ComplexStrategy strategy, string backupsDirectory)
            : base(strategy.SavingAlgorithm, backupsDirectory)
        {
            Strategy = strategy;
        }

        [JsonProperty]
        protected ComplexStrategy Strategy { get; set; }

        public void Restore(string restorePointName)
        {
            Strategy.RestoreAlgorithm.Restore(GetRestorePoint(restorePointName).BackupedFiles, restorePointName);
        }

        public void Clean()
        {
            DeleteFirstRestorePoints(Strategy.RemovalAlgorithm.GetNumberOfPointsToRemove(RestorePoints));
        }

        public void Merge(string firstRestorePointName, string secondRestorePointName)
        {
            RestorePoint firstRestorePoint = GetRestorePoint(firstRestorePointName);
            RestorePoint secondRestorePoint = GetRestorePoint(secondRestorePointName);
            Strategy.MergeAlgorithm.Merge(firstRestorePoint, secondRestorePoint);
        }

        public RestorePoint GetRestorePoint(string restorePointName)
        {
            return RestorePoints.FirstOrDefault(point => point.Name == restorePointName) ??
                throw new RestorePointNotFoundException($"{restorePointName} wasn't found!");
        }

        public void DeleteFirstRestorePoints(int numberOfPoints)
        {
            RestorePoints.RemoveAt(numberOfPoints);
        }
    }
}