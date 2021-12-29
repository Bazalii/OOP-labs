using System;
using System.Collections.Generic;
using System.Linq;
using Backups.BackupStructure;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.PointRemovalCalculators.Implementations
{
    public class TimeCalculator : IPointRemovalCalculator
    {
        public TimeCalculator(int maxDays)
        {
            if (maxDays <= 0)
            {
                throw new ArgumentException("Number of points should be a positive number", nameof(maxDays));
            }

            MaxDays = maxDays;
        }

        [JsonProperty]
        protected int MaxDays { get; set; }

        public int GetNumberOfPointsToRemove(List<RestorePoint> restorePoints)
        {
            int numberOfPointsToRemove = restorePoints.Count(restorePoint =>
                restorePoint.TimeOfBackup.CompareTo(DateTime.Now.AddDays(-MaxDays)) == 1);
            if (numberOfPointsToRemove == restorePoints.Count)
            {
                throw new CannotDeleteAllPointsException("All restore points cannot be deleted!");
            }

            return numberOfPointsToRemove;
        }
    }
}