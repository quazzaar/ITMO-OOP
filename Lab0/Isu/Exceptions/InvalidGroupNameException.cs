using Isu.Models;

namespace Isu.Exceptions;

public class InvalidGroupNameException : Exception
{
    public InvalidGroupNameException(string groupName)
        : base($"Group name {groupName} is invalid")
    {
    }
}