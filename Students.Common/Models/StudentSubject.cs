using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Models;

public class StudentSubject
{
    public int StudentId { get; set; }
    public required Student Student { get; set; }

    public int SubjectId { get; set; }
    public required Subject Subject { get; set; }
}
