namespace Students.Common.Models;

public class StudentSubject
{
    public int StudentId { get; set; }

    public required Student Student { get; set; }

    public int SubjectId { get; set; }

    public required Subject Subject { get; set; }
}
