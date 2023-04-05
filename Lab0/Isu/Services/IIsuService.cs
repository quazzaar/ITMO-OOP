using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public interface IIsuService
{
    Group AddGroup(GroupName name);
    Student AddStudent(Group group, string name, string surname);

    Student GetStudent(int id);
    Student FindStudent(int id);
    List<Student> FindStudents(GroupName groupName);
    List<Student> FindStudents(int courseNumber);

    Group FindGroup(GroupName groupName);
    List<Group> FindGroups(int courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}