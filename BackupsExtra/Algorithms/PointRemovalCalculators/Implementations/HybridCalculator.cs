using System;
using System.Collections.Generic;
using System.Linq;
using Backups.BackupStructure;
using Newtonsoft.Json;

namespace BackupsExtra.Algorithms.PointRemovalCalculators.Implementations
{
    public class HybridCalculator : IPointRemovalCalculator
    {
        public HybridCalculator(List<IPointRemovalCalculator> algorithms, bool all)
        {
            if (algorithms.Count < 2)
            {
                throw new ArgumentException(
                    "You should at at least two algorithms for usage of several criteria",
                    nameof(algorithms));
            }

            Algorithms = algorithms;
            All = all;
        }

        [JsonProperty]
        protected List<IPointRemovalCalculator> Algorithms { get; set; }

        [JsonProperty]
        protected bool All { get; set; }

        public int GetNumberOfPointsToRemove(List<RestorePoint> restorePoints)
        {
            int numberOfPointsToRemove = Algorithms[0].GetNumberOfPointsToRemove(restorePoints);
            foreach (int outputNumber in Algorithms.Select(algorithm => algorithm.GetNumberOfPointsToRemove(restorePoints)))
            {
                if (All)
                {
                    if (outputNumber < numberOfPointsToRemove)
                    {
                        numberOfPointsToRemove = outputNumber;
                    }
                }
                else
                {
                    if (outputNumber > numberOfPointsToRemove)
                    {
                        numberOfPointsToRemove = outputNumber;
                    }
                }
            }

            return numberOfPointsToRemove;
        }
    }
}