using System.Collections.Generic;
using System.Linq;
using Backups.BackupStructure;

namespace BackupsExtra.Logger.Implementations
{
    public class LoggerStringsGenerator
    {
        public List<string> ToLogLines(IReadOnlyList<JobObject> jobObjects)
        {
            return jobObjects.Select(jobObject => "Job object " + jobObject.PathToFile).ToList();
        }

        public List<string> ToLogLines(List<RestorePoint> restorePoints)
        {
            return restorePoints.Select(restorePoint => restorePoint.Name).ToList();
        }

        public List<string> ToBackupLine(List<string> lines)
        {
            return lines.Select(line => line + " is being backuped").ToList();
        }

        public List<string> ToRestoringLine(List<string> lines)
        {
            return lines.Select(line => line + " is being restored").ToList();
        }

        public List<string> ToCleanLine(List<string> lines)
        {
            return lines.Select(line => line + " is being deleted").ToList();
        }

        public List<string> ToMergeLine(List<string> lines)
        {
            return lines.Select(line => line + " is being merged").ToList();
        }
    }
}