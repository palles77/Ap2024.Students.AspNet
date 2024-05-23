using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Students.Common.Models;

namespace Students.Common.Data
{
    public class StudentsContext : DbContext
    {
        public StudentsContext(DbContextOptions<StudentsContext> options)
            : base(options)
        {
            LoadDataFromJson();
        }

        public StudentsContext()
        {
            LoadDataFromJson();
        }

        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Subject> Subject { get; set; } = default!;
        public DbSet<StudentSubject> StudentSubject { get; set; } = default!;
        public DbSet<Animal> Animal { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("StudentsContext");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }

        public override int SaveChanges()
        {
            SaveDataToJson();
            return base.SaveChanges();
        }

        private void LoadDataFromJson()
        {
            if (File.Exists("data.json"))
            {
                var jsonData = File.ReadAllText("data.json");
                var data = JsonConvert.DeserializeObject<Data>(jsonData);

                if (data != null)
                {
                    Student.AddRange(data.Students);
                    Subject.AddRange(data.Subjects);
                    StudentSubject.AddRange(data.StudentSubjects);
                    Animal.AddRange(data.Animals);
                    Book.AddRange(data.Books);
                }
            }
        }

        private void SaveDataToJson()
        {
            var data = new Data
            {
                Students = Student.Local,
                Subjects = Subject.Local,
                StudentSubjects = StudentSubject.Local,
                Animals = Animal.Local,
                Books = Book.Local
            };

            var jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText("data.json", jsonData);
        }
    }

    public class Data
    {
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<Subject> Subjects { get; set; } = new List<Subject>();
        public IEnumerable<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
        public IEnumerable<Animal> Animals { get; set; } = new List<Animal>();
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
