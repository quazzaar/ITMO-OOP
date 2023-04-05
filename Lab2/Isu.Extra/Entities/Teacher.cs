namespace Isu.Extra.Entities;

public class Teacher
{
    public Teacher(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        Name = name;
        Surname = surname;
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public string Surname { get; }
    public Guid Id { get; }
}