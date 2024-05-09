using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;

namespace Students.Web.Controllers;

public class StudentsController : Controller
{
    #region Ctor And Properties

    private readonly StudentsContext _context;
    private readonly ILogger _logger;
    private readonly ISharedResourcesService _sharedResourcesService;
    private readonly IDatabaseService _databaseService;

    public StudentsController(
        StudentsContext context, 
        ILogger<StudentsController> logger, 
        ISharedResourcesService sharedResourcesService,
        IDatabaseService databaseService)
    {
        _context = context;
        _logger = logger;
        _sharedResourcesService = sharedResourcesService;
        _databaseService = databaseService;
    }

    #endregion // Ctor And Properties

    #region Public Methods

    // GET: Students
    public async Task<IActionResult> Index(string? culture)
    {
        IActionResult result = View();
        try
        {
            var model = await _context.Student.ToListAsync();
            result = View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // GET: Students/Details/5
    public IActionResult Details(int? id)
    {
        IActionResult result = NotFound();

        try
        {
            var student = _databaseService.DisplayStudent(id);
            if (student != null)
            {
                result = View(student);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        IActionResult result = View();
        try
        {
            var listOfSubjects = _context.Subject
                .ToList();
            var newStudent = new Student();
            newStudent.AvailableSubjects = listOfSubjects;

             result = View(newStudent);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // POST: Students/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int id, string name, int age, string major, int[] subjectIdDst)
    {
        IActionResult result = View();
        try
        {
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();
            var availableSubjects = _context.Subject
                .Where(s => !subjectIdDst.Contains(s.Id))
                .ToList();
            var student = new Student()
            {
                Id = id,
                Name = name,
                Age = age,
                Major = major,
                AvailableSubjects = availableSubjects
            };
            foreach (var chosenSubject in chosenSubjects)
            {
                student.AddSubject(chosenSubject);
            }
            if (ModelState.IsValid)
            {
                _context.Add(student);
                var additionResult = await _context.SaveChangesAsync();
                if (additionResult == 0)
                {
                    throw new Exception("Error saving changes to the database.");
                }
                result = RedirectToAction(nameof(Index));
            }
            else
            {
                result = View(student);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        IActionResult result = NotFound();

        try
        {
            if (id != null)
            {
                var student = await _context.Student.FindAsync(id);
                if (student != null)
                {
                    var chosenSubjects = _context.StudentSubject
                        .Where(ss => ss.StudentId == id)
                        .Select(ss => ss.Subject)
                        .ToList();
                    var availableSubjects = _context.Subject
                        .Where(s => !chosenSubjects.Contains(s))
                        .ToList();
                    student.StudentSubjects = _context.StudentSubject
                        .Where(x => x.StudentId == id)
                        .ToList();
                    student.AvailableSubjects = availableSubjects;
                    result = View(student);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, string name, int age, string major, int[] subjectIdDst)
    {
        IActionResult result;

        try
        {
            bool saveResult = _databaseService.EditStudent(id, name, age, major, subjectIdDst);
            if (!saveResult)
            {
                throw new Exception("Error saving changes to the database.");
            }

            // Set the result to redirect to the Index action
            result = RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Log the exception and set the result to return the view with the current student
            _logger.LogError("Exception caught: " + ex.Message);
            var student = await _context.Student.FindAsync(id);
            result = View(student);
        }

        return result;
    }


    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        IActionResult result = View();
        try
        {
            if (id == null)
            {
                result = NotFound();
            }
            else
            {

                var student = await _context.Student
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (student == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = View(student);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }

        return result;
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        IActionResult result = View();
        try
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            result = RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }

        return result;
    }

    #endregion // Public Methods

    #region Private Methods

    private bool StudentExists(int id)
    {
        var result = _context.Student.Any(e => e.Id == id);
        return result;
    }

    #endregion // Private Methods
}
