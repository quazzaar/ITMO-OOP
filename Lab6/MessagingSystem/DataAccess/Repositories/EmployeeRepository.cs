using System.Text.Json;
using MessagingSystem.Application.Exceptions;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.DataAccess;

public class EmployeeRepository<T> : IRepository<Employee>
{
    private string _path;
    private List<Employee> _employees;

    public EmployeeRepository(string path, List<Employee> employees)
    {
        _path = path;
        _employees = employees;
    }

    public void Add(Employee entity)
    {
        _employees.Add(entity);
    }

    public void Delete(Employee entity)
    {
        _employees.Remove(entity);
    }

    public Employee GetById(Guid id)
    {
        foreach (var employee in _employees)
        {
            if (employee.Id == id)
            {
                return employee;
            }
        }

        throw new EmployeeException("Employee not found");
    }

    public List<Employee> GetAll()
    {
        return _employees;
    }

    public void Serialize()
    {
        string jsonString = JsonSerializer.Serialize(_employees);
        File.AppendAllText(_path, jsonString);
    }

    public void Deserialize()
    {
        string jsonString = File.ReadAllText(_path);
        _employees = JsonSerializer.Deserialize<List<Employee>>(jsonString) !;
    }
}