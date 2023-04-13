using Domain.DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.ViewModels;

namespace StudentAttendanceSystem.Controllers
{
    public class ClassModelsController : Controller
    {
        private readonly DatabaseDbContext _context;

        public ClassModelsController(DatabaseDbContext context)
        {
            _context = context;
        }

        // GET: ClassModels
        public async Task<IActionResult> Index()
        {
            var databaseDbContext = _context.ClassModels.Include(c => c.Instructor).Include(j => j.Subject);
            return View(await databaseDbContext.ToListAsync());
        }

        // GET: ClassModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassModels == null)
            {
                return NotFound();
            }

            var classModel = await _context.ClassModels
                .Include(c => c.Instructor)
                .Include(j => j.Subject)
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // GET: ClassModels/Create
        public IActionResult Create()
        {
            var classViewModel = new ClassViewModel();
            classViewModel.Subjects = _context.Subjects.ToList();
            classViewModel.Instructors= _context.Instructors.ToList();

            return View(classViewModel);
        }

        // POST: ClassModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassViewModel classViewModel)
        {
            _context.Add(classViewModel.Class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ClassModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassModels == null)
            {
                return NotFound();
            }

            var classModel = await _context.ClassModels.FindAsync(id);
            if (classModel == null)
            {
                return NotFound();
            }
            var classViewModel = new ClassViewModel();
            classViewModel.Subjects = _context.Subjects.ToList();
            classViewModel.Instructors = _context.Instructors.ToList();
            classViewModel.Class = classModel;
            return View(classViewModel);
        }

        // POST: ClassModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClassViewModel classViewModel)
        {
            classViewModel.Subjects = _context.Subjects.ToList();
            classViewModel.Instructors = _context.Instructors.ToList();

            _context.Update(classViewModel.Class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ClassModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassModels == null)
            {
                return NotFound();
            }

            var classModel = await _context.ClassModels
                .Include(c => c.Instructor)
                .Include(j => j.Subject)
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // POST: ClassModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassModels == null)
            {
                return Problem("Entity set 'DatabaseDbContext.ClassModels'  is null.");
            }
            var classModel = await _context.ClassModels.FindAsync(id);
            if (classModel != null)
            {
                _context.ClassModels.Remove(classModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassModelExists(int id)
        {
          return (_context.ClassModels?.Any(e => e.ClassID == id)).GetValueOrDefault();
        }


        // Class Student List
        public async Task<IActionResult> StudentList(int? id)
        {
            // redirect to another action and controller with ID
            return RedirectToAction( "Index", "ClassStudents" , new { id = id });
        }
    }
}
