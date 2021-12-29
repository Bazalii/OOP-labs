using System.Collections.Generic;
using System.Linq;
using Backups.BackupStructure;
using BackupsExtra.Logger.Implementations;
using BackupsExtra.Tools;
using Newtonsoft.Json;
using Serilog;

namespace BackupsExtra.BackupExtraStructure
{
    public class ModifiedBackupJob : BackupJob
    {
        private MyLogger _logger;

        private LoggerStringsGenerator _generator;

        public ModifiedBackupJob(ComplexStrategy strategy, MyLogger logger, LoggerStringsGenerator generator, string backupsDirectory)
            : base(strategy.SavingAlgorithm, backupsDirectory)
        {
            Strategy = strategy;
            _logger = logger ?? (_logger = new MyLogger(new LoggerConfiguration().WriteTo.Console().CreateLogger()));
            _generator = generator ?? (_generator = new LoggerStringsGenerator());
        }

        [JsonProperty]
        protected ComplexStrategy Strategy { get; set; }

        public new void Process()
        {
            _logger.Write(_generator.ToBackupLine(_generator.ToLogLines(JobObjects)));
            base.Process();
        }

        public void Restore(string restorePointName)
        {
            IReadOnlyList<JobObject> jobObjects = GetRestorePoint(restorePointName).BackupedFiles;
            _logger.Write(_generator.ToRestoringLine(_generator.ToLogLines(jobObjects)));
            Strategy.RestoreAlgorithm.Restore(jobObjects, restorePointName);
        }

        public void Clean()
        {
            int numberOfPointsToRemove = Strategy.RemovalCalculator.GetNumberOfPointsToRemove(RestorePoints);
            List<RestorePoint> restorePointsToRemove = RestorePoints.GetRange(0, numberOfPointsToRemove);
            _logger.Write(_generator.ToCleanLine(_generator.ToLogLines(restorePointsToRemove)));
            Strategy.RemovalAlgorithm.RemoveRestorePoints(restorePointsToRemove);
            DeleteFirstRestorePoints(numberOfPointsToRemove);
        }

        public void Merge(string firstRestorePointName, string secondRestorePointName)
        {
            RestorePoint firstRestorePoint = GetRestorePoint(firstRestorePointName);
            RestorePoint secondRestorePoint = GetRestorePoint(secondRestorePointName);
            _logger.Write(_generator.ToMergeLine(_generator.ToLogLines(
                new List<RestorePoint> { firstRestorePoint, secondRestorePoint })));
            RestorePoints[RestorePoints.IndexOf(secondRestorePoint)] = Strategy.MergeAlgorithm.Merge(firstRestorePoint, secondRestorePoint);
            Strategy.RemovalAlgorithm.RemoveRestorePoints(new List<RestorePoint> { firstRestorePoint });
            DeleteRestorePointByName(firstRestorePointName);
        }

        public RestorePoint GetRestorePoint(string restorePointName)
        {
            return RestorePoints.FirstOrDefault(point => point.Name == restorePointName) ??
                throw new RestorePointNotFoundException($"{restorePointName} wasn't found!");
        }

        public void SetLogger(Serilog.Core.Logger logger)
        {
            _logger.SetLogger(logger);
        }

        private void DeleteRestorePointByName(string restorePointName)
        {
            RestorePoints.RemoveAt(RestorePoints.IndexOf(GetRestorePoint(restorePointName)));
        }

        private void DeleteFirstRestorePoints(int numberOfPoints)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                RestorePoints.RemoveAt(0);
            }
        }
    }
}