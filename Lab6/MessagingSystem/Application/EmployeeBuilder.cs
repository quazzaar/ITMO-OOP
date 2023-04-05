using MessagingSystem.Application.Exceptions;
using MessagingSystem.DataAccess;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.Application;

public class EmployeeBuilder
{
    public Employee Build(string name, string surname, Employee.EmployeePosition position)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
        {
            throw new EmployeeException("Name and surname must be set");
        }

        return new Employee(name, surname, position);
    }

    public void SetName(string name, Employee employee)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        employee.Name = name;
    }

    public void SetSurname(string surname, Employee employee)
    {
        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new EmployeeException("Surname cannot be empty");
        }

        employee.Surname = surname;
    }

    public void SetPosition(Employee.EmployeePosition position, Employee employee)
    {
         employee.Position = position;
    }
}