using Backups.Entities;

namespace Backups.Repositories;

public interface IRepository
{
    void AddBackup(Backup backup);
    Backup GetBackup(string name);
    List<Backup> GetAllBackups();
}