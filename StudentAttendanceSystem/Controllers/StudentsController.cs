using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace StudentAttendanceSystem.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentRepository studentRepository;

        public StudentsController(DatabaseDbContext context)
        {
            studentRepository = new StudentRepository(context);
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            try
            {

                var students = await studentRepository.GetStudentsAsync();

                return View(students);

                //return _context.Students != null ?
                //          View(await _context.Students.ToListAsync()) :
                //          Problem("Entity set 'DatabaseDbContext.Students'  is null.");
                //return View(await _context.Students.ToListAsync());
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View("Error");
            
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);

            if (id == 0 || student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.CreateStudentAsync(student);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);

            if (id == 0 || student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            studentRepository.UpdateStudentAsync(student);
            return RedirectToAction(nameof(Index));
        }


        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 )
            {
                return NotFound();
            }

            await studentRepository.DeleteStudentAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> StudentClassList(int? id)
        {
            // redirect to another action and controller with ID
            return RedirectToAction("Index", "StudentClasses", new { id = id });
        }

        //// POST: Students/Delete/5
        ////[HttpPost, ActionName("Delete")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (_context.Students == null)
        //    {
        //        return Problem("Entity set 'DatabaseDbContext.Students'  is null.");
        //    }
        //    var student = await _context.Students.FindAsync(id);
        //    if (student != null)
        //    {
        //        _context.Students.Remove(student);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool StudentExists(int id)
        //{
        //    return (_context.Students?.Any(e => e.StudentID == id)).GetValueOrDefault();
        //}
    }
}
