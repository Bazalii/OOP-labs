using System.Collections.Generic;
using Backups.BackupStructure;

namespace BackupsExtra.Algorithms.PointRemovalCalculators
{
    public interface IPointRemovalCalculator
    {
        int GetNumberOfPointsToRemove(List<RestorePoint> restorePoints);
    }
}