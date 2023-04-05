using Isu.Models;

namespace Isu.Exceptions;

public class GroupDoesNotExistException : Exception
{
    public GroupDoesNotExistException(GroupName groupName)
        : base($"Group {groupName} is not found")
    {
    }
}