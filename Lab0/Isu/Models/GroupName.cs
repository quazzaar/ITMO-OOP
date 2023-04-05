using System;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private static int _minimalCourseNumber = 1;
    private static int _maximumCourseNumber = 4;
    private static int _minimalGroupNumber = 0;
    private static int _maximumGroupNumber = 20;

    public GroupName(string groupName)
    {
        string codeOfFaculty = groupName.Substring(0, 1);
        groupName = groupName.Remove(0, 2);
        int numberOfCourse = Convert.ToInt32(groupName.Substring(0, 1));
        groupName = groupName.Remove(0, 1);
        int numberOfGroup = Convert.ToInt32(groupName.Substring(0, 2));

        if (numberOfCourse < _minimalCourseNumber || numberOfCourse > _maximumCourseNumber)
            throw new InvalidGroupNameException(groupName);
        if (numberOfGroup < _minimalGroupNumber || numberOfGroup > _maximumGroupNumber)
            throw new InvalidGroupNameException(groupName);

        CourseNumber = numberOfCourse;
        GroupNumber = numberOfGroup;
        CodeOfFaculty = codeOfFaculty;
    }

    public int CourseNumber { get; }
    public int GroupNumber { get; }
    public string CodeOfFaculty { get; }
}