namespace Isu.Extra.Entities;

public class Lesson
{
    public Lesson(int audience, int lessonNumber, Teacher teacher)
    {
        if (audience < 0 || lessonNumber < 0 || teacher == null)
        {
            throw new ArgumentOutOfRangeException();
        }

        Audience = audience;
        LessonNumber = lessonNumber;
        Teacher = teacher;
    }

    public int Audience { get; }
    public int LessonNumber { get; }
    public Teacher Teacher { get; }
}