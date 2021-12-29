using System.Text;
using Backups.Algorithms.Implementations;
using Backups.BackupStructure;
using Backups.FileSystem;
using Backups.FileSystem.Implementations;
using BackupsExtra.Algorithms.MergeAlgorithms.Implementations;
using BackupsExtra.Algorithms.PointRemovalAlgorithms.Implementations;
using BackupsExtra.Algorithms.PointRemovalCalculators.Implementations;
using BackupsExtra.Algorithms.RestoreAlgorithms.Implementations;
using BackupsExtra.BackupExtraStructure;
using BackupsExtra.Logger.Implementations;
using BackupsExtra.Tools;
using NUnit.Framework;
using Serilog;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class BackupsExtraTest
    {
        private ModifiedBackupJob _modifiedBackupJob;

        private MemoryFileSystem _fileSystem;

        private IArchiver _archiver = new ZipArchiver();

        private MyLogger _logger;

        private LoggerStringsGenerator _generator;

        private string _backupsDirectory = "C:\\Backups";

        private readonly string _firstFileInformation = "Hi, I'm the first file!";
        
        private readonly string _secondFileInformation = "Hi, I'm the second file!";
        
        private readonly string _newFirstFileInformation = "Hi, I'm the new first file!";

        [SetUp]
        public void SetUp()
        {
            MemoryFileSystem.Reset();
            _fileSystem = MemoryFileSystem.GetInstance();
            _logger = new MyLogger(new LoggerConfiguration().WriteTo.Console().CreateLogger());
            _generator = new LoggerStringsGenerator();
            _fileSystem.CreateDirectory("C:\\Restoring");
            _archiver = new ZipArchiver();
            _fileSystem.CreateDirectory("C:\\One");
            _fileSystem.CreateDirectory("C:\\Two");
            _fileSystem.CreateFile("C:\\One\\First.txt");
            _fileSystem.CreateFile("C:\\Two\\Second.txt");
            _fileSystem.WriteToFile("C:\\One\\First.txt", Encoding.UTF8.GetBytes(_firstFileInformation));
            _fileSystem.WriteToFile("C:\\Two\\Second.txt", Encoding.UTF8.GetBytes(_secondFileInformation));
        }

        [Test]
        public void Restore_UseComplexStrategyForSplitStorageForRestoringOnePoint_CorrespondingFilesAreRestored()
        {
            _modifiedBackupJob =
                new ModifiedBackupJob(
                    new ComplexStrategy(new SplitStorage(_fileSystem, _archiver, _backupsDirectory),
                        new SplitStorageRestoring(_fileSystem, _archiver, _backupsDirectory, "C:\\Restoring"),
                        new NumberOfPointsCalculator(3),
                        new SplitStorageRemovalAlgorithm(_fileSystem, _backupsDirectory),
                        new SplitStorageMerge(_fileSystem, _backupsDirectory)),
                    _logger,
                    _generator,
                    _backupsDirectory);
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
            _modifiedBackupJob.Process();
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
            _modifiedBackupJob.Process();
            _fileSystem.RemoveFile("C:\\One\\First.txt");
            _fileSystem.RemoveFile("C:\\Two\\Second.txt");
            _modifiedBackupJob.Restore("RestorePoint1");
            Assert.IsTrue(Encoding.UTF8.GetString(_fileSystem.ReadFile("C:\\Restoring\\First.txt")) == _firstFileInformation);
            Assert.IsTrue(Encoding.UTF8.GetString(_fileSystem.ReadFile("C:\\Restoring\\Second.txt")) == _secondFileInformation);
        }
        
        [Test]
        public void Restore_UseComplexStrategyForSingeStorageForRestoringOnePoint_CorrespondingFilesAreRestored()
        {
            _modifiedBackupJob =
                new ModifiedBackupJob(
                    new ComplexStrategy(new SingleStorage(_fileSystem, _archiver, _backupsDirectory),
                        new SingleStorageRestoring(_fileSystem, _archiver, _backupsDirectory, "C:\\Restoring"),
                        new NumberOfPointsCalculator(3),
                        new SingleStorageRemovalAlgorithm(_fileSystem, _backupsDirectory),
                        new SingleStorageMerge(_fileSystem, _backupsDirectory)),
                    _logger,
                    _generator,
                    _backupsDirectory);
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
            _modifiedBackupJob.Process();
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
            _modifiedBackupJob.Process();
            _fileSystem.RemoveFile("C:\\One\\First.txt");
            _fileSystem.RemoveFile("C:\\Two\\Second.txt");
            _modifiedBackupJob.Restore("RestorePoint1");
            Assert.IsTrue(Encoding.UTF8.GetString(_fileSystem.ReadFile("C:\\Restoring\\First.txt")) == _firstFileInformation);
            Assert.IsTrue(Encoding.UTF8.GetString(_fileSystem.ReadFile("C:\\Restoring\\Second.txt")) == _secondFileInformation);
        }
        
        [Test]
        public void Clean_UseComplexStrategyForSplitStorageForCleaningOfThePoints_CorrespondingPointsAreDeleted()
        {
            _modifiedBackupJob =
                new ModifiedBackupJob(
                    new ComplexStrategy(new SplitStorage(_fileSystem, _archiver, _backupsDirectory),
                        new SplitStorageRestoring(_fileSystem, _archiver, _backupsDirectory, "C:\\Restoring"),
                        new NumberOfPointsCalculator(3),
                        new SplitStorageRemovalAlgorithm(_fileSystem, _backupsDirectory),
                        new SplitStorageMerge(_fileSystem, _backupsDirectory)),
                    _logger,
                    _generator,
                    _backupsDirectory);
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
            _modifiedBackupJob.Process();
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Clean();
            Assert.IsTrue(_modifiedBackupJob.GetRestorePointsNumber() == 3);
            Assert.Catch<RestorePointNotFoundException>(() =>
            {
                _modifiedBackupJob.GetRestorePoint("RestorePoint0");
            });
            Assert.Catch<RestorePointNotFoundException>(() =>
            {
                _modifiedBackupJob.GetRestorePoint("RestorePoint1");
            });
        }
        
        [Test]
        public void Clean_UseComplexStrategyForSingleStorageForCleaningOfThePoints_CorrespondingPointsAreDeleted()
        {
            _modifiedBackupJob =
                new ModifiedBackupJob(
                    new ComplexStrategy(new SplitStorage(_fileSystem, _archiver, _backupsDirectory),
                        new SplitStorageRestoring(_fileSystem, _archiver, _backupsDirectory, "C:\\Restoring"),
                        new NumberOfPointsCalculator(3),
                        new SplitStorageRemovalAlgorithm(_fileSystem, _backupsDirectory),
                        new SplitStorageMerge(_fileSystem, _backupsDirectory)),
                    _logger,
                    _generator,
                    _backupsDirectory);
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
            _modifiedBackupJob.Process();
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Clean();
            Assert.IsTrue(_modifiedBackupJob.GetRestorePointsNumber() == 3);
            Assert.Catch<RestorePointNotFoundException>(() =>
            {
                _modifiedBackupJob.GetRestorePoint("RestorePoint0");
            });
            Assert.Catch<RestorePointNotFoundException>(() =>
            {
                _modifiedBackupJob.GetRestorePoint("RestorePoint1");
            });
        }
        
        [Test]
        public void Merge_UseComplexForSplitStorageForMergingOfThePoints_CorrespondingPointsAreDeleted()
        {
            {
                _modifiedBackupJob =
                    new ModifiedBackupJob(
                        new ComplexStrategy(new SplitStorage(_fileSystem, _archiver, _backupsDirectory),
                            new SplitStorageRestoring(_fileSystem, _archiver, _backupsDirectory, "C:\\Restoring"),
                            new NumberOfPointsCalculator(3),
                            new SplitStorageRemovalAlgorithm(_fileSystem, _backupsDirectory),
                            new SplitStorageMerge(_fileSystem, _backupsDirectory)),
                        _logger,
                        _generator,
                        _backupsDirectory);
                _modifiedBackupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
                _modifiedBackupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
                _modifiedBackupJob.Process();
                _modifiedBackupJob.RemoveJobObject(new JobObject("C:\\Two\\Second.txt"));
                _modifiedBackupJob.Process();
                _modifiedBackupJob.Process();
                _modifiedBackupJob.Restore("RestorePoint1");
                _modifiedBackupJob.Merge("RestorePoint0", "RestorePoint1");
                Assert.IsTrue(_modifiedBackupJob.GetRestorePointsNumber() == 2);
                Assert.Catch<RestorePointNotFoundException>(() =>
                {
                    _modifiedBackupJob.GetRestorePoint("RestorePoint0");
                });
                Assert.IsTrue(_modifiedBackupJob.GetRestorePoint("RestorePoint1").BackupedFiles[1].PathToFile == "C:\\One\\First.txt");
                Assert.IsTrue(_modifiedBackupJob.GetRestorePoint("RestorePoint1").BackupedFiles[0].PathToFile == "C:\\Two\\Second.txt");
            }
        }
        
        [Test]
        public void Merge_UseComplexForSingleStorageForMergingOfThePoints_CorrespondingPointsAreDeleted()
        {
            _modifiedBackupJob =
                new ModifiedBackupJob(
                    new ComplexStrategy(new SingleStorage(_fileSystem, _archiver, _backupsDirectory),
                        new SingleStorageRestoring(_fileSystem, _archiver, _backupsDirectory, "C:\\Restoring"),
                        new NumberOfPointsCalculator(3),
                        new SingleStorageRemovalAlgorithm(_fileSystem, _backupsDirectory),
                        new SingleStorageMerge(_fileSystem, _backupsDirectory)),
                    _logger,
                    _generator,
                    _backupsDirectory);
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
            _modifiedBackupJob.Process();
            _fileSystem.WriteToFile("C:\\One\\First.txt", Encoding.UTF8.GetBytes(_newFirstFileInformation));
            _modifiedBackupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
            _modifiedBackupJob.Process();
            _modifiedBackupJob.Merge("RestorePoint0", "RestorePoint1");
            _modifiedBackupJob.Restore("RestorePoint1");
            Assert.IsTrue(_modifiedBackupJob.GetRestorePointsNumber() == 1);
            Assert.IsTrue(_modifiedBackupJob.GetRestorePoint("RestorePoint1").BackupedFiles[0].PathToFile == "C:\\One\\First.txt");
            Assert.IsTrue(_modifiedBackupJob.GetRestorePoint("RestorePoint1").BackupedFiles[1].PathToFile == "C:\\Two\\Second.txt");
            Assert.IsTrue(Encoding.UTF8.GetString(_fileSystem.ReadFile("C:\\Restoring\\First.txt")) == _newFirstFileInformation);
            Assert.IsTrue(Encoding.UTF8.GetString(_fileSystem.ReadFile("C:\\Restoring\\Second.txt")) == _secondFileInformation);
        }
    }
}