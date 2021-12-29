using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using BackupsExtra.Tools;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.PointRemovalCalculators.Implementations
{
    public class NumberOfPointsCalculator : IPointRemovalCalculator
    {
        public NumberOfPointsCalculator(int maxPoints)
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
            int numberOfPointsToRemove = restorePoints.Count - MaxPoints;
            if (numberOfPointsToRemove == restorePoints.Count)
            {
                throw new CannotDeleteAllPointsException("All restore points cannot be deleted!");
            }

            return numberOfPointsToRemove > 0 ? numberOfPointsToRemove : 0;
        }
    }
}