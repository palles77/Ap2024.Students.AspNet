using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Common.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Age { get; set; }

    [Required]
    [StringLength(100)]
    public string Major { get; set; } = string.Empty;

    public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    [NotMapped]
    public List<Subject> AvailableSubjects { get; set; } = new List<Subject>();

    public Student()
    {
    }

    public Student(string name, int age, string major)
    {
        Name = name;
        Age = age;
        Major = major;
    }

    public void AddSubject(Subject subject)
    { 
        var studentSubject = new StudentSubject
        {
            Student = this,
            Subject = subject
        };
        StudentSubjects.Add(studentSubject);
    }
}
