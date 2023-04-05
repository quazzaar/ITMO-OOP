using Isu.Extra.Exceptions;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraServiceTests
{
    [Fact]
    public void CreateNewOgnpCourse_OgnpWasCreated()
    {
        var isuExtraService = new IsuExtraService();
        isuExtraService.CreateOgnp("Cyber Security", "K");
        Assert.True(isuExtraService.Ognps.Count == 1);
    }

    [Fact]
    public void AddStudentToOgnpGroup_StudentWasAdded()
    {
        var isuExtraService = new IsuExtraService();
        var isuService = new IsuService();
        var newTeacher = isuExtraService.CreateTeacher("Roman", "Makarevich");
        var newGroupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(newGroupName);
        var newStudent = isuService.AddStudent(newGroup, "Dima", "Dima");
        var ognpStudent = isuExtraService.CreateOgnpStudent(newStudent);
        var newOgnp = isuExtraService.CreateOgnp("Cyber Security", "K");
        var newOgnpStream = isuExtraService.CreateOgnpStream(newOgnp, 1);
        var newGroupExt = isuExtraService.CreateOgnpGroup(newOgnpStream, newGroup);
        isuExtraService.RegisterStudentOnOgnp(ognpStudent, newOgnp);
        isuExtraService.AddStudentToOgnpGroup(newGroupExt, ognpStudent);
        Assert.True(newGroupExt.OgnpStudent.Count == 1);
    }

    [Fact]
    public void StudentTryToRegisterOnOgnpWithTheSameFaculty_StudentWasNotAdded()
    {
        var isuExtraService = new IsuExtraService();
        var isuService = new IsuService();
        var newTeacher = isuExtraService.CreateTeacher("Roman", "Makarevich");
        var newGroupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(newGroupName);
        var newStudent = isuService.AddStudent(newGroup, "Dima", "Dima");
        var ognpStudent = isuExtraService.CreateOgnpStudent(newStudent);
        var newOgnp = isuExtraService.CreateOgnp("Cyber Security", "M");
        var newOgnpStream = isuExtraService.CreateOgnpStream(newOgnp, 1);
        var newGroupExt = isuExtraService.CreateOgnpGroup(newOgnpStream, newGroup);
        Assert.Throws<SameFacultyException>(() =>
                isuExtraService.RegisterStudentOnOgnp(ognpStudent, newOgnp));
    }

    [Fact]
    public void RemoveStudentFromOgnpGroup_StudentWasRemoved()
    {
        var isuExtraService = new IsuExtraService();
        var isuService = new IsuService();
        var newTeacher = isuExtraService.CreateTeacher("Roman", "Makarevich");
        var newGroupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(newGroupName);
        var newStudent = isuService.AddStudent(newGroup, "Dima", "Dima");
        var ognpStudent = isuExtraService.CreateOgnpStudent(newStudent);
        var newOgnp = isuExtraService.CreateOgnp("Cyber Security", "K");
        var newOgnpStream = isuExtraService.CreateOgnpStream(newOgnp, 1);
        var newGroupExt = isuExtraService.CreateOgnpGroup(newOgnpStream, newGroup);
        isuExtraService.RegisterStudentOnOgnp(ognpStudent, newOgnp);
        isuExtraService.AddStudentToOgnpGroup(newGroupExt, ognpStudent);
        Assert.True(newGroupExt.OgnpStudent.Count == 1);
        isuExtraService.RemoveStudentFromOgnpGroup(newGroupExt, ognpStudent);
        Assert.True(newGroupExt.OgnpStudent.Count == 0);
    }

    [Fact]
    public void GetAllOgnpStreamsByOgnp_OgnpStreamsWereReturned()
    {
        var isuExtraService = new IsuExtraService();
        var isuService = new IsuService();
        var newTeacher = isuExtraService.CreateTeacher("Roman", "Makarevich");
        var newGroupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(newGroupName);
        var newStudent = isuService.AddStudent(newGroup, "Dima", "Dima");
        var ognpStudent = isuExtraService.CreateOgnpStudent(newStudent);
        var newOgnp = isuExtraService.CreateOgnp("Cyber Security", "K");
        var newOgnpStream = isuExtraService.CreateOgnpStream(newOgnp, 1);
        var newGroupExt = isuExtraService.CreateOgnpGroup(newOgnpStream, newGroup);
        isuExtraService.GetOgnpStreamByOgnp(newOgnp);
        Assert.True(newOgnp.Streams.Count == 1);
    }

    [Fact]
    public void GetAllStudentsByOgnpGroup_StudentsWereReturned()
    {
        var isuExtraService = new IsuExtraService();
        var isuService = new IsuService();
        var newTeacher = isuExtraService.CreateTeacher("Roman", "Makarevich");
        var newGroupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(newGroupName);
        var newStudent = isuService.AddStudent(newGroup, "Dima", "Dima");
        var ognpStudent = isuExtraService.CreateOgnpStudent(newStudent);
        var newOgnp = isuExtraService.CreateOgnp("Cyber Security", "K");
        var newOgnpStream = isuExtraService.CreateOgnpStream(newOgnp, 1);
        var newGroupExt = isuExtraService.CreateOgnpGroup(newOgnpStream, newGroup);
        isuExtraService.RegisterStudentOnOgnp(ognpStudent, newOgnp);
        isuExtraService.AddStudentToOgnpGroup(newGroupExt, ognpStudent);
        isuExtraService.GetOgnpStudentsByOgnpGroup(newGroupExt);
        Assert.True(newGroupExt.OgnpStudent.Count == 1);
    }

    [Fact]
    public void GetAllStudentsNotRegisteredOnOgnp_StudentsWereReturned()
    {
        var isuExtraService = new IsuExtraService();
        var isuService = new IsuService();
        var newTeacher = isuExtraService.CreateTeacher("Roman", "Makarevich");
        var newGroupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(newGroupName);
        var newStudent = isuService.AddStudent(newGroup, "Dima", "Dima");
        var newStudent2 = isuService.AddStudent(newGroup, "Dimas", "Dimas");
        var ognpStudent = isuExtraService.CreateOgnpStudent(newStudent);
        var ognpStudent2 = isuExtraService.CreateOgnpStudent(newStudent2);
        var newOgnp = isuExtraService.CreateOgnp("Cyber Security", "K");
        var newOgnpStream = isuExtraService.CreateOgnpStream(newOgnp, 1);
        var newGroupExt = isuExtraService.CreateOgnpGroup(newOgnpStream, newGroup);
        isuExtraService.RegisterStudentOnOgnp(ognpStudent, newOgnp);
        isuExtraService.AddStudentToOgnpGroup(newGroupExt, ognpStudent);
        var list = isuExtraService.FindNotRegisteredOnOgnpStudents();
        Assert.True(list.Count == 1);
    }

    [Fact]
    public void TryToAddSameLessonToOgnpGroup_ExceptionWasThrown()
    {
        var isuExtraService = new IsuExtraService();
        var isuService = new IsuService();
        var newTeacher = isuExtraService.CreateTeacher("Roman", "Makarevich");
        var newGroupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(newGroupName);
        var newLesson = isuExtraService.CreateLesson(1, 2, newTeacher);
        var newLesson2 = isuExtraService.CreateLesson(2, 2, newTeacher);
        var newStudent = isuService.AddStudent(newGroup, "Dima", "Dima");
        var ognpStudent = isuExtraService.CreateOgnpStudent(newStudent);
        var newOgnp = isuExtraService.CreateOgnp("Cyber Security", "K");
        var newOgnpStream = isuExtraService.CreateOgnpStream(newOgnp, 1);
        var newGroupExt = isuExtraService.CreateOgnpGroup(newOgnpStream, newGroup);
        isuExtraService.RegisterStudentOnOgnp(ognpStudent, newOgnp);
        isuExtraService.AddStudentToOgnpGroup(newGroupExt, ognpStudent);
        isuExtraService.AddLessonToOgnp(newGroupExt, newLesson);
        Assert.Throws<LessonIntersectionException>(() =>
                isuExtraService.AddLessonToOgnp(newGroupExt, newLesson2));
    }
}