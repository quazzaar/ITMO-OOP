namespace Isu.Exceptions;

public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(int id)
        : base($"Student with {id} is not found")
    {
    }
}