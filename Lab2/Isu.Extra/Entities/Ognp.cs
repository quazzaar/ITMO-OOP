namespace Isu.Extra.Entities;

public class Ognp
{
    public const int MaxNumberOfStreams = 5;
    private List<OgnpStream> _streams;
    public Ognp(string name, string faculty)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(faculty))
        {
            throw new ArgumentNullException();
        }

        Name = name;
        Faculty = faculty;
        _streams = new List<OgnpStream>();
    }

    public string Name { get; }
    public string Faculty { get; }
    public IReadOnlyCollection<OgnpStream> Streams => _streams;

    public OgnpStream CreateStream(int streamNumber)
    {
        if (_streams.Count >= MaxNumberOfStreams)
        {
            throw new ArgumentOutOfRangeException();
        }

        _streams.Add(new OgnpStream(this, streamNumber));
        return _streams.ToList().First();
    }
}