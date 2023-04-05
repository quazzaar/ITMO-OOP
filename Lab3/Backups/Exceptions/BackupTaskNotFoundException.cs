using Backups.Entities;

namespace Backups.Exceptions;

public class BackupTaskNotFoundException : Exception
{
    public BackupTaskNotFoundException(BackupTask backupTask)
        : base($"Backup task {backupTask} not found")
    {
    }
}