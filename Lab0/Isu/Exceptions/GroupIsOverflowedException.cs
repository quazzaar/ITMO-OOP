using Isu.Models;

namespace Isu.Exceptions;

public class GroupIsOverflowedException : Exception
{
    public GroupIsOverflowedException(GroupName groupName)
        : base($"Group {groupName} is overflowed")
    {
    }
}