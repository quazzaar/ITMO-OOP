using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class OgnpStream
{
    public const int MaxNumberOfOgnpGroups = 2;
    private readonly List<GroupExt> _groupExt;
    public OgnpStream(Ognp ognp, int streamNumber)
    {
        if (ognp == null || streamNumber <= 0)
        {
            throw new ArgumentNullException();
        }

        Ognp = ognp;
        StreamNumber = streamNumber;
        _groupExt = new List<GroupExt>();
    }

    public Ognp Ognp { get; }
    public int StreamNumber { get; }
    public IReadOnlyCollection<GroupExt> GroupExt => _groupExt;
}