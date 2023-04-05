using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int CapacityOfGroup = 30;
    private readonly List<Student> _students;

    public Group(GroupName groupName)
    {
        _students = new List<Student>(CapacityOfGroup);
        GroupName = groupName;
    }

    public IReadOnlyCollection<Student> Students => _students;
    public GroupName GroupName { get; }

    public Student AddStudentToGroup(Student student)
    {
        if (Students.Count >= CapacityOfGroup)
            throw new GroupIsOverflowedException(GroupName);

        student.Id = _students.Count;
        _students.Add(student);
        return student;
    }

    public Student RemoveStudentFromGroup(Student removingStudent)
    {
        _students.Remove(removingStudent);
        return removingStudent;
    }
}
