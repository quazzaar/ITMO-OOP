using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class LessonIntersectionException : Exception
{
    public LessonIntersectionException(Lesson lesson)
        : base($"{lesson.LessonNumber} lesson is already exist in schedule")
    {
    }
}