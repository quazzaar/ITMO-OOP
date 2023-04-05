using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Services;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    Ognp CreateOgnp(string name, string faculty);
    OgnpStream CreateOgnpStream(Ognp ognp, int streamNumber);
    GroupExt CreateOgnpGroup(OgnpStream ognpStream, Group group);
    Teacher CreateTeacher(string name, string surname);
    Lesson CreateLesson(int audience, int lessonNumber, Teacher teacher);
    OgnpStudent CreateOgnpStudent(Student student);
    void RegisterStudentOnOgnp(OgnpStudent student, Ognp ognp);
    void RemoveStudentFromOgnp(OgnpStudent student);
    void AddStudentToOgnpGroup(GroupExt ognpGroup, OgnpStudent student);
    void RemoveStudentFromOgnpGroup(GroupExt ognpGroup, OgnpStudent student);
    void AddLessonToGroup(GroupExt group, Lesson lesson);
    void AddLessonToOgnp(GroupExt ognpGroup, Lesson lesson);
    List<OgnpStream> GetOgnpStreamByOgnp(Ognp ognp);
    List<OgnpStudent> GetOgnpStudentsByOgnpGroup(GroupExt ognpGroup);
    List<OgnpStudent> FindNotRegisteredOnOgnpStudents();
}