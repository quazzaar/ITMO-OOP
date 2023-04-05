using System.Collections.ObjectModel;
using System.IO.Compression;
using Backups.Algorithms;
using Backups.Exceptions;
using Backups.Repositories;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _backupObjects = new ();
    public BackupTask(string name, BackupConfig backupConfig, List<BackupObject> backupObjects)
    {
        if (backupConfig.Algorithm == null)
            throw new ArgumentNullException();
        if (backupConfig.Repository == null)
            throw new ArgumentNullException();
        if (backupObjects == null)
            throw new ArgumentNullException();

        Name = name;
        Algorithm = backupConfig.Algorithm;
        Repository = backupConfig.Repository;
        _backupObjects = backupObjects;
    }

    public string Name { get; set; }
    public IAlgorithm Algorithm { get; set; }
    public IRepository Repository { get; set; }
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;

    public void SetNameBackupTask(string name)
    {
        Name = name;
    }

    public void SetAlgorithm(IAlgorithm algorithm)
    {
        Algorithm = algorithm;
    }

    public void SetRepository(IRepository repository)
    {
        Repository = repository;
    }

    public UPath SetPathToDirectory(string path, IFileSystem fileSystem)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException();
        }

        var uPath = fileSystem.ConvertPathFromInternal(path);
        fileSystem.CreateDirectory(uPath);
        return uPath;
    }

    public void AddBackupObject(BackupObject backupObject)
    {
        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(BackupObject backupObject)
    {
        if (!_backupObjects.Contains(backupObject))
            throw new BackupObjectNotFoundException(backupObject);
        _backupObjects.Remove(backupObject);
    }

    public Backup CreateBackup(string name, IRepository repository)
    {
        var backup = new Backup(name);
        repository.AddBackup(backup);
        return backup;
    }

    public void CreateRestorePoint(Backup backup)
    {
        var restorePoint = new RestorePoint(_backupObjects, DateTime.Now);
        backup.AddRestorePoint(restorePoint);
        Algorithm.CreateStorage(backup, restorePoint);
    }

    public void CreateBackupObject(string name, string path, FileSystem fileSystem)
    {
        var uPath = SetPathToDirectory(path, fileSystem);
        var backupObject = new BackupObject(name, path);
        _backupObjects.Add(backupObject);
        fileSystem.WriteAllText(uPath + "/" + name, "content");
    }
}