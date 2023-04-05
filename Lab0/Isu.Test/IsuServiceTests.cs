using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var isuService = new IsuService();
        var groupName = new GroupName("M32101");
        Assert.Equal(2, groupName.CourseNumber);
        Assert.Equal(10, groupName.GroupNumber);
        var newGroup = isuService.AddGroup(groupName);
        var student = isuService.AddStudent(newGroup, "Dima", "Dima");
        var student2 = isuService.AddStudent(newGroup, "Dima", "Dima");
        Assert.Equal(1, student2.Id);

        Assert.Equal(newGroup, student.Group);
        Assert.Contains(student, newGroup.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isuService = new IsuService();
        var groupName = new GroupName("M32101");
        var newGroup = isuService.AddGroup(groupName);
        Assert.Throws<GroupIsOverflowedException>(() =>
        {
            for (int i = 0; i < 31; i++)
                isuService.AddStudent(newGroup, "Dimasik", "Dimasik");
        });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        var isuService = new IsuService();
        Assert.Throws<InvalidGroupNameException>(() => new GroupName("M35901"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var isuService = new IsuService();
        var nameOfFirstGroup = new GroupName("M32111");
        var nameOfSecondGroup = new GroupName("M31101");
        var firstGroup = isuService.AddGroup(nameOfFirstGroup);
        var secondGroup = isuService.AddGroup(nameOfSecondGroup);
        var student = isuService.AddStudent(firstGroup, "Dima", "Dima");
        Assert.Equal(firstGroup, student.Group);
        Assert.Contains(student, firstGroup.Students);

        isuService.ChangeStudentGroup(student, secondGroup);
        Assert.Contains(student, secondGroup.Students);
        Assert.DoesNotContain(student, firstGroup.Students);
    }
}