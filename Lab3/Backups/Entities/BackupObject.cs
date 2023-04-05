using Backups.Exceptions;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException();

        Name = name;
        Path = path;
    }

    public string Name { get; set; }
    public string Path { get; set; }
}