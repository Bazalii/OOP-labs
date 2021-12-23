using Backups.Algorithms;
using Backups.BackupStructure;

namespace BackupsExtra.Tests.BackupExtraStructure
{
    public class ModifiedBackupJob : BackupJob
    {
        public ModifiedBackupJob(SavingAlgorithm savingAlgorithm, string backupDirectoryPath)
            : base(savingAlgorithm, backupDirectoryPath)
        {
        }
    }
}