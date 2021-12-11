﻿using System.Text;
using Backups.Algorithms.Implementations;
using Backups.BackupStructure;
using Backups.FileSystem;
using Backups.FileSystem.Implementations;
using NUnit.Framework;

namespace Backups.Tests
{
    [TestFixture]
    public class BackupsTest
    {
        private BackupJob _backupJob;

        private MemoryFileSystem _fileSystem;

        private IArchiver _archiver;

        [SetUp]
        public void SetUp()
        {
            MemoryFileSystem.Reset();
            _fileSystem = MemoryFileSystem.GetInstance();
            _archiver = new VirtualArchiver();
            _fileSystem.CreateDirectory("C:\\One");
            _fileSystem.CreateDirectory("C:\\Two");
            _fileSystem.CreateFile("C:\\One\\First.txt");
            _fileSystem.CreateFile("C:\\Two\\Second.txt");
            _fileSystem.WriteToFile("C:\\One\\First.txt", "Hi, I'm the first file!");
            _fileSystem.WriteToFile("C:\\Two\\Second.txt", "Hi, I'm the second file!");
        }
        
        [Test]
        public void Process_UseSplitStorageAlgorithmToProcessBackupJobTwice_BackupJobHasTwoRestorePointsFileSystemHasThreeStorages()
        {
            _backupJob = new BackupJob(new SplitStorage(_fileSystem, _archiver, "C:\\Backups"), "C:\\Backups");
            _backupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
            _backupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
            _backupJob.Process();
            _backupJob.RemoveJobObject(new JobObject("C:\\One\\First.txt"));
            _fileSystem.WriteToFile("C:\\Two\\Second.txt", "Hi, I'm new second file!");
            _backupJob.Process();
            Assert.IsTrue(_backupJob.GetRestorePointsNumber() == 2);
            _archiver.ExtractFromArchive("C:\\Backups\\RestorePoint0First", "C:\\Extract");
            _archiver.ExtractFromArchive("C:\\Backups\\RestorePoint1Second", "C:\\Extract");
            Assert.IsTrue(((MemoryDirectory) _fileSystem.GetStorageObject("C:\\Backups")).GetObjects().Count == 3);
            Assert.IsTrue(
                ((MemoryDirectory) _fileSystem.GetStorageObject("C:\\Backups")).GetObjects()[0]
                .GetPath() == "C:\\Backups\\RestorePoint0First");
            
            Assert.IsTrue(
                Encoding.UTF8.GetString(
                    ((MemoryFile) _fileSystem.GetStorageObject("C:\\Backups\\RestorePoint0First\\First.txt")).Read()) ==
                "Hi, I'm the first file!");
            Assert.IsTrue(
                ((MemoryDirectory) _fileSystem.GetStorageObject("C:\\Backups")).GetObjects()[1]
                .GetPath() == "C:\\Backups\\RestorePoint0Second");
            Assert.IsTrue(
                Encoding.UTF8.GetString(
                    ((MemoryFile) _fileSystem.GetStorageObject("C:\\Backups\\RestorePoint0Second\\Second.txt")).Read()) ==
                "Hi, I'm the second file!");
            Assert.IsTrue(
                ((MemoryDirectory) _fileSystem.GetStorageObject("C:\\Backups")).GetObjects()[2]
                .GetPath() == "C:\\Backups\\RestorePoint1Second");
            Assert.IsTrue(
                Encoding.UTF8.GetString(
                    ((MemoryFile) _fileSystem.GetStorageObject("C:\\Backups\\RestorePoint1Second\\Second.txt")).Read()) ==
                "Hi, I'm new second file!");
        }
        
        [Test]
        public void Process_UseSingleStorageAlgorithmToProcessJob_BackupJobHasOneRestorePointFileSystemHasOneStorageWithFiles()
        {
            _backupJob = new BackupJob(new SingleStorage(_fileSystem, _archiver, "C:\\Backups"), "C:\\Backups");
            _backupJob.SetBackupPath("C:\\MyBackups");
            _backupJob.AddJobObject(new JobObject("C:\\One\\First.txt"));
            _backupJob.AddJobObject(new JobObject("C:\\Two\\Second.txt"));
            _backupJob.Process();
            Assert.IsTrue(_backupJob.GetRestorePointsNumber() == 1);
            Assert.IsTrue(((MemoryDirectory) _fileSystem.GetStorageObject("C:\\MyBackups")).GetObjects().Count == 1);
            Assert.IsTrue(
                ((MemoryArchive) _fileSystem.GetStorageObject("C:\\MyBackups\\RestorePoint0")).GetObjects()[0]
                .GetPath() == "C:\\MyBackups\\RestorePoint0\\First.txt");
            Assert.IsTrue(
                Encoding.UTF8.GetString(
                    ((MemoryFile) _fileSystem.GetStorageObject("C:\\MyBackups\\RestorePoint0\\First.txt")).Read()) ==
                "Hi, I'm the first file!");
            Assert.IsTrue(
                ((MemoryArchive) _fileSystem.GetStorageObject("C:\\MyBackups\\RestorePoint0")).GetObjects()[1]
                .GetPath() == "C:\\MyBackups\\RestorePoint0\\Second.txt");
            Assert.IsTrue(
                Encoding.UTF8.GetString(
                    ((MemoryFile) _fileSystem.GetStorageObject("C:\\MyBackups\\RestorePoint0\\Second.txt")).Read()) ==
                "Hi, I'm the second file!");
        }
    }
}