﻿using System;
using Backups.Algorithms;
using BackupsExtra.Algorithms.MergeAlgorithms;
using BackupsExtra.Algorithms.PointRemovalAlgorithms;
using BackupsExtra.Algorithms.RestoreAlgorithms;

namespace BackupsExtra.BackupExtraStructure
{
    public class ComplexStrategy
    {
        public ComplexStrategy(
            SavingAlgorithm savingAlgorithm,
            RestoreAlgorithm restoreAlgorithm,
            IPointRemovalAlgorithm removalAlgorithm,
            MergeAlgorithm mergeAlgorithm)
        {
            SavingAlgorithm = savingAlgorithm ??
                              throw new ArgumentNullException(
                                  nameof(savingAlgorithm), "SavingAlgorithm cannot be null!");
            RestoreAlgorithm = restoreAlgorithm ??
                               throw new ArgumentNullException(
                                   nameof(restoreAlgorithm), "RestoreAlgorithm cannot be null!");
            RemovalAlgorithm = removalAlgorithm ??
                               throw new ArgumentNullException(
                                   nameof(removalAlgorithm), "PointRemovalAlgorithm cannot be null!");
            MergeAlgorithm = mergeAlgorithm ??
                             throw new ArgumentNullException(
                                 nameof(mergeAlgorithm), "MergeAlgorithm cannot be null!");
        }

        public SavingAlgorithm SavingAlgorithm { get; }

        public RestoreAlgorithm RestoreAlgorithm { get; }

        public IPointRemovalAlgorithm RemovalAlgorithm { get; }

        public MergeAlgorithm MergeAlgorithm { get; }
    }
}