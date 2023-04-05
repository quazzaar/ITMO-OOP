using MessagingSystem.DataAccess;
using MessagingSystem.DataAccess.Entities;

namespace MessagingSystem.Application;

public interface IMessageSystemService
{
    Employee CreateEmployee(string name, string surname, Employee.EmployeePosition position);
    void ChangeNameOfEmployee(string newName, Employee employee);
    void ChangeSurnameOfEmployee(string newSurname, Employee employee);
    void ChangePositionOfEmployee(Employee.EmployeePosition newPosition, Employee employee);
    Employee GetEmployeeById(Guid id);
    Employee CreateChief(string name, string surname);
    Account CreateAccount(string username, string password, Employee owner);
    void RecieveMessage(Employee employee, Message message);
    void ProcessMessage(Employee employee, Message message);
    void CreateReport(Employee employee);
}