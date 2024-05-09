using Microsoft.EntityFrameworkCore;
using Students.Common.Models;

namespace Students.Common.Data;

public class StudentsContext : DbContext
{
    public StudentsContext (DbContextOptions<StudentsContext> options)
        : base(options)
    {

    }

    public StudentsContext()
    {
    }

    public DbSet<Student> Student { get; set; } = default!;
    public DbSet<Subject> Subject { get; set; } = default!;
    public DbSet<StudentSubject> StudentSubject { get; set; } = default!;

    public DbSet<Animal> Animal { get; set; }
    public DbSet<Book> Book { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("StudentsContext");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentSubject>()
            .HasKey(ss => new { ss.StudentId, ss.SubjectId });

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.StudentId);
    }
}
