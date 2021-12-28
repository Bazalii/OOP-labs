using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace BackupsExtra.Algorithms.PointRemovalAlgorithms
{
    public interface IPointRemovalAlgorithm
    {
        int GetNumberOfPointsToRemove(List<RestorePoint> restorePoints);
    }
}