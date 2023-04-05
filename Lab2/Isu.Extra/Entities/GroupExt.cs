using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class GroupExt : Group
{
    private List<OgnpStudent> _studentsRegisteredOnOgnp = new ();
    private List<Lesson> _lessonsOfGroup = new ();
    private List<Lesson> _lessonsOfOgnp = new ();

    public GroupExt(Group group, OgnpStream ognpStream)
        : base(group.GroupName)
    {
        OgnpStream = ognpStream;
    }

    public OgnpStream OgnpStream { get; }
    public IReadOnlyCollection<OgnpStudent> OgnpStudent => _studentsRegisteredOnOgnp;
    public IReadOnlyCollection<Lesson> LessonsOfGroup => _lessonsOfGroup;
    public IReadOnlyCollection<Lesson> LessonsOfOgnp => _lessonsOfOgnp;

    public void AddStudentToOgnpGroup(OgnpStudent student)
    {
        _studentsRegisteredOnOgnp.Add(student);
    }

    public void RemoveStudentFromOgnpGroup(OgnpStudent removingStudent)
    {
        _studentsRegisteredOnOgnp.Remove(removingStudent);
    }

    public void AddLessonToGroup(GroupExt group, Lesson lesson)
    {
        foreach (var checkLesson in group.LessonsOfGroup)
        {
            if (checkLesson.LessonNumber == lesson.LessonNumber)
                throw new LessonIntersectionException(lesson);
        }

        _lessonsOfGroup.Add(lesson);
    }

    public void AddLessonToOgnp(GroupExt ognpGroup, Lesson lesson)
    {
        foreach (var checkLesson in ognpGroup.LessonsOfOgnp)
        {
            if (checkLesson.LessonNumber == lesson.LessonNumber)
                throw new LessonIntersectionException(checkLesson);
        }

        _lessonsOfOgnp.Add(lesson);
    }
}