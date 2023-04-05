namespace Backups.Exceptions;

public class BackupNotFoundException : Exception
{
    public BackupNotFoundException(string name)
        : base($"Backup {name} not found")
    {
    }
}