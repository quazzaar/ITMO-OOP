namespace Isu.Extra.Exceptions;

public class SameFacultyException : Exception
{
    public SameFacultyException()
        : base("Faculty of the ognp and faculty of the student must be different")
    {
    }
}