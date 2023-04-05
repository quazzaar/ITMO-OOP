namespace MessagingSystem.Application.Exceptions;

public class EmployeeException : Exception
{
    public EmployeeException(string message)
        : base(message) { }
}