using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.PointRemovalAlgorithms.Implementations
{
    public class NumberOfPointsAlgorithm : IPointRemovalAlgorithm
    {
        public NumberOfPointsAlgorithm(int maxPoints)
        {
            if (maxPoints <= 0)
            {
                throw new ArgumentException("Number of points should be a positive number", nameof(maxPoints));
            }

            MaxPoints = maxPoints;
        }

        [JsonProperty]
        protected int MaxPoints { get; set; }

        public int GetNumberOfPointsToRemove(List<RestorePoint> restorePoints)
        {
            int numberOfPointsToDelete = restorePoints.Count - MaxPoints;
            if (numberOfPointsToDelete == restorePoints.Count)
            {
                throw new CannotDeleteAllPointsException("All restore points cannot be deleted!");
            }

            return numberOfPointsToDelete > 0 ? numberOfPointsToDelete : 0;
        }
    }
}