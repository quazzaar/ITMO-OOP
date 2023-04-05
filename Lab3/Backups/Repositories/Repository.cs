using Backups.Entities;
using Backups.Exceptions;

namespace Backups.Repositories;

public class Repository : IRepository
{
    private readonly List<Backup> _backups = new ();

    public void AddBackup(Backup backup)
    {
        if (backup == null)
            throw new ArgumentNullException();
        _backups.Add(backup);
    }

    public Backup GetBackup(string name)
    {
        return _backups.Find(backup => backup.Name == name) ??
               throw new BackupNotFoundException(name);
    }

    public List<Backup> GetAllBackups()
    {
        return _backups;
    }

    public void MakeStorage(BackupTask backupTask, Backup backup)
    {
        backupTask.CreateRestorePoint(backup);
    }
}