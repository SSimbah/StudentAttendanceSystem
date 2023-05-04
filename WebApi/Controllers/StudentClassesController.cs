using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    public class StudentClassesController : Controller
    {
        //private IStudentClassRepository studentClassRepository;


        //public StudentClassesController(DatabaseDbContext context)
        //{
        //    studentClassRepository = new StudentClassRepository(context);
        //}

        private readonly DatabaseDbContext _context;

        public StudentClassesController(DatabaseDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var student = await _context.Students.FindAsync(id);
            ViewBag.StudentName = student.FullName;

            var studentClasses = _context.ClassStudents.Include(c => c.Class).Include(c => c.Student).Include(c => c.Class.Subject).Include(i => i.Class.Instructor).Where(s => s.StudentID == id);
            ViewBag.StudentID = id;
            return View(await studentClasses.AsNoTracking().ToListAsync());
        }


        public async Task<IActionResult> AddClass(int studentId, int classId)
        {
            //var studentId  = HttpContext.Session.GetInt32(studentId);
            ClassStudent classStudent = new ClassStudent();
            classStudent.StudentID = studentId;
            classStudent.ClassID = classId;
            _context.Add(classStudent);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "StudentClasses", new { id = studentId });
        }

        public async Task<IActionResult> DeleteClass(int classStudentId, int studentId)
        {
            if (_context.ClassStudents == null)
            {
                return Problem("Entity set 'DatabaseDbContext.ClassModels'  is null.");
            }
            var classStudentModel = await _context.ClassStudents.FindAsync(classStudentId);
            if (classStudentModel != null)
            {
                _context.ClassStudents.Remove(classStudentModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "StudentClasses", new { id = studentId });
        }

        public async Task<IActionResult> ClassesLookup(int studentId)
        {
            ViewBag.StudentID = studentId;
            var classes = _context.ClassModels.Include(s => s.Subject).Include(i => i.Instructor).ToListAsync();
            return View(classes);
        }
    }
}
