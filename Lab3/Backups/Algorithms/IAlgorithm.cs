using Backups.Entities;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    void CreateStorage(Backup backup, RestorePoint restorePoint);
}