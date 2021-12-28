using System;
using System.Collections.Generic;
using System.Linq;
using Backups.BackupStructure;
using Backups.FileSystem;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.PointRemovalAlgorithms.Implementations
{
    public class TimeAlgorithm : IPointRemovalAlgorithm
    {
        [JsonProperty]
        private int _maxDays;

        public TimeAlgorithm(int maxDays)
        {
            if (maxDays <= 0)
            {
                throw new ArgumentException("Number of points should be a positive number", nameof(maxDays));
            }

            _maxDays = maxDays;
        }

        public int GetNumberOfPointsToRemove(List<RestorePoint> restorePoints)
        {
            int numberOfPointsToDelete = restorePoints.Count(restorePoint =>
                restorePoint.TimeOfBackup.CompareTo(DateTime.Now.AddDays(-_maxDays)) == 1);
            if (numberOfPointsToDelete == restorePoints.Count)
            {
                throw new CannotDeleteAllPointsException("All restore points cannot be deleted!");
            }

            return numberOfPointsToDelete;
        }
    }
}