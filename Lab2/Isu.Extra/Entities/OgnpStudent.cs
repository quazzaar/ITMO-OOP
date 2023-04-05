using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class OgnpStudent : Student
{
    public const int MaxNumberOfOgnp = 2;
    public const int MaxNumberOfLessons = 6;
    private readonly List<OgnpStudent> _studentsRegisteredOnOgnp = new ();

    public OgnpStudent(Student student)
        : base(student.Id, student.Name, student.Surname, student.Group)
    {
        Faculty = student.Group.GroupName.CodeOfFaculty;
    }

    public string Faculty { get; }

    public IReadOnlyCollection<OgnpStudent> StudentsRegisteredOnOgnp => _studentsRegisteredOnOgnp;

    public void RegisterStudentOnOgnp(OgnpStudent student, Ognp ognp)
    {
        if (_studentsRegisteredOnOgnp.Count >= MaxNumberOfOgnp)
            throw new ArgumentOutOfRangeException();

        if (student.Faculty == ognp.Faculty)
            throw new SameFacultyException();

        _studentsRegisteredOnOgnp.Add(student);
    }

    public void RemoveStudentFromOgnp(OgnpStudent removingStudent)
    {
        _studentsRegisteredOnOgnp.Remove(removingStudent);
    }
}