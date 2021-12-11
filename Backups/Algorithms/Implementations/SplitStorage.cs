﻿using System;
using System.Collections.Generic;
using Backups.BackupStructure;
using Backups.FileSystem;

namespace Backups.Algorithms.Implementations
{
    public class SplitStorage : SavingAlgorithm
    {
        public SplitStorage(IFileSystem fileSystem, IArchiver archiver, string backupsDirectoryPath)
        {
            FileSystem = fileSystem ??
                         throw new ArgumentNullException(
                             nameof(fileSystem), "FileSystem cannot be null!");
            Archiver = archiver ??
                       throw new ArgumentNullException(
                           nameof(archiver), "Archiver cannot be null!");
            BackupsDirectory = backupsDirectoryPath ??
                               throw new ArgumentNullException(
                                   nameof(backupsDirectoryPath), "Path cannot be null!");
            SwapDirectory = fileSystem.GetRoot() + "\\Swap";
            FileSystem.CreateDirectory(BackupsDirectory);
        }

        public override void Backup(List<JobObject> jobObjects, string backupName)
        {
            FileSystem.CreateDirectory(SwapDirectory);
            foreach (JobObject jobObject in jobObjects)
            {
                FileSystem.CopyFile(jobObject.PathToFile, SwapDirectory + "\\" + FileSystem.GetFullNameFromPath(jobObject.PathToFile));
                Archiver.AddToArchive(SwapDirectory, BackupsDirectory + $"\\{backupName + FileSystem.GetNameFromPath(jobObject.PathToFile)}");
                FileSystem.RemoveFile(SwapDirectory + "\\" + FileSystem.GetFullNameFromPath(jobObject.PathToFile));
            }

            FileSystem.RemoveDirectory(SwapDirectory);
        }

        public override void SetFileSystem(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public override void SetBackupsDirectoryPath(string path)
        {
            FileSystem.CreateDirectory(path);
            BackupsDirectory = path;
        }
    }
}