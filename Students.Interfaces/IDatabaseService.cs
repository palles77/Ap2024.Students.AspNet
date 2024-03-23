using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);

    Student? DisplayStudent(int? id);
}
