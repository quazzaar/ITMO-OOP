namespace MessagingSystem.DataAccess.Entities;

public class Employee
{
    public Employee(string name, string surname, EmployeePosition position)
    {
        Position = position;
        Name = name;
        Surname = surname;
        Id = Guid.NewGuid();
    }

    public enum EmployeePosition
    {
        Subordinate = 0,
        Manager = 1,
        Chief = 2,
        Anonymous = 3,
    }

    public EmployeePosition Position { get; set; }
    public string Name { get; internal set; }
    public string Surname { get; internal set; }
    public Guid Id { get; }
}