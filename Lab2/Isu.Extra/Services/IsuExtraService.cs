using System.Text.RegularExpressions;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Models;
using Isu.Services;
using Group = Isu.Entities.Group;

namespace Isu.Extra.Services;

public class IsuExtraService : IsuService, IIsuExtraService
{
    private readonly List<Ognp> _ognps = new ();
    private readonly List<OgnpStream> _ognpStreams = new ();
    private readonly List<GroupExt> _ognpGroups = new ();
    private readonly List<Teacher> _teachers = new ();
    private readonly List<OgnpStudent> _notRegisteredOnOgnpStudent = new ();

    public IReadOnlyCollection<Ognp> Ognps => _ognps;
    public IReadOnlyCollection<OgnpStream> OgnpStreams => _ognpStreams;
    public IReadOnlyCollection<GroupExt> OgnpGroups => _ognpGroups;
    public IReadOnlyCollection<Teacher> Teachers => _teachers;
    public IReadOnlyCollection<OgnpStudent> NotRegisteredOnOgnpStudent => _notRegisteredOnOgnpStudent;

    public Ognp CreateOgnp(string name, string faculty)
    {
        if (name == null || faculty == null)
        {
            throw new ArgumentNullException();
        }

        var newOgnp = new Ognp(name, faculty);
        _ognps.Add(newOgnp);
        return newOgnp;
    }

    public OgnpStream CreateOgnpStream(Ognp ognp, int streamNumber)
    {
        if (ognp == null || streamNumber <= 0)
        {
            throw new ArgumentNullException();
        }

        var newOgnpStream = ognp.CreateStream(streamNumber);
        _ognpStreams.Add(newOgnpStream);
        return newOgnpStream;
    }

    public GroupExt CreateOgnpGroup(OgnpStream ognpStream, Group group)
    {
        if (ognpStream == null || group == null)
        {
            throw new ArgumentNullException();
        }

        var newOgnpGroup = new GroupExt(group, ognpStream);
        _ognpGroups.Add(newOgnpGroup);
        return newOgnpGroup;
    }

    public Teacher CreateTeacher(string name, string surname)
    {
        if (name == null || surname == null)
        {
            throw new ArgumentNullException();
        }

        var newTeacher = new Teacher(name, surname);
        _teachers.Add(newTeacher);
        return newTeacher;
    }

    public Lesson CreateLesson(int audience, int lessonNumber, Teacher teacher)
    {
        if (audience < 0 || lessonNumber < 0 || teacher == null)
        {
            throw new ArgumentOutOfRangeException();
        }

        var newLesson = new Lesson(audience, lessonNumber, teacher);
        return newLesson;
    }

    public OgnpStudent CreateOgnpStudent(Student student)
    {
        if (student == null)
        {
            throw new ArgumentNullException();
        }

        var newOgnpStudent = new OgnpStudent(student);
        _notRegisteredOnOgnpStudent.Add(newOgnpStudent);
        return newOgnpStudent;
    }

    public void RegisterStudentOnOgnp(OgnpStudent student, Ognp ognp)
    {
        if (student == null || ognp == null)
        {
            throw new ArgumentNullException();
        }

        _notRegisteredOnOgnpStudent.Remove(student);
        student.RegisterStudentOnOgnp(student, ognp);
    }

    public void RemoveStudentFromOgnp(OgnpStudent student)
    {
        if (student == null)
        {
            throw new ArgumentNullException();
        }

        _notRegisteredOnOgnpStudent.Add(student);
        student.RemoveStudentFromOgnp(student);
    }

    public void AddStudentToOgnpGroup(GroupExt ognpGroup, OgnpStudent student)
    {
        if (ognpGroup == null || student == null)
        {
            throw new ArgumentNullException();
        }

        ognpGroup.AddStudentToOgnpGroup(student);
    }

    public void RemoveStudentFromOgnpGroup(GroupExt ognpGroup, OgnpStudent student)
    {
        if (ognpGroup == null || student == null)
        {
            throw new ArgumentNullException();
        }

        ognpGroup.RemoveStudentFromOgnpGroup(student);
    }

    public void AddLessonToGroup(GroupExt group, Lesson lesson)
    {
        if (group == null || lesson == null)
        {
            throw new ArgumentNullException();
        }

        group.AddLessonToGroup(group, lesson);
    }

    public void AddLessonToOgnp(GroupExt ognpGroup, Lesson lesson)
    {
        if (ognpGroup == null || lesson == null)
        {
            throw new ArgumentNullException();
        }

        ognpGroup.AddLessonToOgnp(ognpGroup, lesson);
    }

    public List<OgnpStream> GetOgnpStreamByOgnp(Ognp ognp)
    {
        if (ognp == null)
        {
            throw new ArgumentNullException();
        }

        return ognp.Streams.ToList();
    }

    public List<OgnpStudent> GetOgnpStudentsByOgnpGroup(GroupExt ognpGroup)
    {
        if (ognpGroup == null)
        {
            throw new ArgumentNullException();
        }

        return ognpGroup.OgnpStudent.ToList();
    }

    public List<OgnpStudent> FindNotRegisteredOnOgnpStudents()
    {
        return _notRegisteredOnOgnpStudent;
    }
}