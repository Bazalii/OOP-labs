using Backups.Algorithms;
using Backups.BackupStructure;

namespace BackupsExtra.BackupExtraStructure
{
    public class ModifiedBackupJob : BackupJob
    {
        public ModifiedBackupJob(SavingAlgorithm savingAlgorithm, string backupDirectoryPath)
            : base(savingAlgorithm, backupDirectoryPath)
        {
        }
    }
}