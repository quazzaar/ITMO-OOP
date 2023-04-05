using Backups.Entities;

namespace Backups.Exceptions;

public class BackupObjectNotFoundException : Exception
{
    public BackupObjectNotFoundException(BackupObject backupObject)
        : base($"Backup object {backupObject} not found")
    {
    }
}