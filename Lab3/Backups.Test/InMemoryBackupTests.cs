using System.IO.Compression;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Repositories;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;
using Xunit;

public class InMemoryBackupTests
{
    [Fact]
    public void MakeBackupTask_BackupTaskWasCreatedAndCreatedTwoRestorePointsAndThreeStoragesWasCreated()
    {
        var repository = new Repository();
        var storageAlgorithm = new SplitStorageAlgorithmInMemory();
        var backupObjectsToBackup = new List<BackupObject>();
        var backupConfig = new BackupConfig(storageAlgorithm, repository);
        var backupTask = new BackupTask("Default", backupConfig, backupObjectsToBackup);

        var backup = backupTask.CreateBackup("First", repository);
        var searchBackup = repository.GetBackup("First");
        Assert.Equal(searchBackup, backup);

        var backupA = new BackupObject("File A", "testPath");
        var backupB = new BackupObject("File B", "testPath");
        var backupC = new BackupObject("File C", "testPath");
        backupTask.AddBackupObject(backupA);
        backupTask.AddBackupObject(backupB);
        backupTask.AddBackupObject(backupC);

        backupTask.CreateRestorePoint(backup);
        Assert.Equal(1, backup.RestorePoints.Count);
        Assert.Equal(3, backup.RestorePoints.First().BackupObjects.Count());

        var storage = backup.RestorePoints.ToList()[0].Storages.First();
        Assert.Equal("File C(1).zip", storage.BackupObject.Name);

        backupTask.CreateRestorePoint(backup);
        Assert.Equal(2, backup.RestorePoints.Count);
        Assert.Equal(3, backup.RestorePoints.First().BackupObjects.Count());
        Assert.Equal("File C(2).zip", storage.BackupObject.Name);

        backupTask.RemoveBackupObject(backupA);
        backupTask.CreateRestorePoint(backup);
        Assert.Equal(3, backup.RestorePoints.Count);
        Assert.Equal(2, backup.RestorePoints.First().BackupObjects.Count());
        Assert.Equal("File C(2).zip", storage.BackupObject.Name);
    }

    [Fact]
    public void SingleStorageAlgorithmInFileSystem_FilesWereCreated()
    {
        /*
        var fileSystem = new PhysicalFileSystem();
        var repository = new Repository();
        var storageAlgorithm = new SingleStorageAlgorithmInFileSystem();
        var backupConfig = new BackupConfig(storageAlgorithm, repository);
        var backupObjectsToBackup = new List<BackupObject>();
        var backupTask = new BackupTask("Default", backupConfig, backupObjectsToBackup);

        var backup = backupTask.CreateBackup("First", repository);
        var searchBackup = repository.GetBackup("First");
        Assert.Equal(searchBackup, backup);

        var uPath = backupTask.SetPathToDirectory(
            "/Users/dmitrijnadezdin/Documents/GitHub/quazzaar/Lab3/Backups/Objects", fileSystem);
        var sPath = fileSystem.ConvertPathToInternal(uPath);
        backupTask.CreateBackupObject("A", sPath, fileSystem);

        Assert.True(fileSystem.FileExists(sPath + "/A"));
        backupTask.CreateBackupObject("B", sPath, fileSystem);
        backupTask.CreateRestorePoint(backup);
        */
    }

    [Fact]
    public void SplitStorageAlgorithmInFileSystem_FilesWereCreated()
    {
        /*
        var fileSystem = new PhysicalFileSystem();
        var repository = new Repository();
        var storageAlgorithm = new SplitStorageAlgorithmInFileSystem();
        var backupConfig = new BackupConfig(storageAlgorithm, repository);
        var backupObjectsToBackup = new List<BackupObject>();
        var backupTask = new BackupTask("Default", backupConfig, backupObjectsToBackup);

        var backup = backupTask.CreateBackup("First", repository);
        var searchBackup = repository.GetBackup("First");
        Assert.Equal(searchBackup, backup);

        var uPath = backupTask.SetPathToDirectory(
            "/Users/dmitrijnadezdin/Documents/GitHub/quazzaar/Lab3/Backups/Objects", fileSystem);
        var sPath = fileSystem.ConvertPathToInternal(uPath);
        backupTask.CreateBackupObject("C", sPath, fileSystem);

        Assert.True(fileSystem.FileExists(sPath + "/A"));
        backupTask.CreateBackupObject("D", sPath, fileSystem);
        backupTask.CreateRestorePoint(backup);
        */
    }
}