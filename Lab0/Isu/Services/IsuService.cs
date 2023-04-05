using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups = new ();
    private readonly List<Student> _students = new ();

    public Group AddGroup(GroupName name)
    {
        if (name == null)
            throw new ArgumentNullException();

        var group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name, string surname)
    {
        if (group == null || name == null)
            throw new ArgumentNullException(nameof(group));
        var id = _students.Count;
        var student = group.AddStudentToGroup(new Student(id, name, surname, group));
        return student;
    }

    public Student GetStudent(int id)
    {
        return _students.Find(student => student.Id == id)
               ?? throw new StudentNotFoundException(id);
    }

    public Student FindStudent(int id)
    {
        return _students.Find(student => student.Id == id)
            ?? throw new StudentNotFoundException(id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _students.FindAll(student => student.Group.GroupName == groupName);
    }

    public List<Student> FindStudents(int courseNumber)
    {
        return _students.FindAll(student => student.Group.GroupName.CourseNumber == courseNumber);
    }

    public Group FindGroup(GroupName groupName)
    {
        return _groups.Find(group => group.GroupName == groupName)
            ?? throw new GroupDoesNotExistException(groupName);
    }

    public List<Group> FindGroups(int courseNumber)
    {
        return _groups.FindAll(group => group.GroupName.CourseNumber == courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student == null || newGroup == null)
            throw new ArgumentNullException();
        var oldGroup = FindGroup(student.Group.GroupName);
        oldGroup.RemoveStudentFromGroup(student);
        newGroup.AddStudentToGroup(student);
    }
}