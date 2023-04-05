namespace Isu.Entities;

public class Student
{
    public Student(int id, string name, string surname, Group group)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Group = group;
    }

    public int Id { get; set; }
    public string Name { get; }
    public string Surname { get; }
    public Group Group { get; private set; }
}