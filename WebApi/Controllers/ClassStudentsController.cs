using Domain.DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    public class ClassStudentsController : Controller
    {
        private readonly DatabaseDbContext _context;

        public ClassStudentsController(DatabaseDbContext context)
        {
            _context = context;
        }

        // GET: ClassStudents
        public async Task<IActionResult> Index(int? id)
        {
            var classSubject = from s in _context.ClassStudents.Include(c => c.Class).Include(c => c.Student).Include(c => c.Class.Subject)
                               select s;

            classSubject = classSubject.Where(s => s.ClassID == id);

            return View(await classSubject.AsNoTracking().ToListAsync());
        }

        // GET: ClassStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassStudents == null)
            {
                return NotFound();
            }

            var classStudent = await _context.ClassStudents
                .Include(c => c.Class)
                .Include(c => c.Student)
                .Include(c => c.Class.Subject)
                .FirstOrDefaultAsync(m => m.ClassStudentID == id);
            if (classStudent == null)
            {
                return NotFound();
            }

            return View(classStudent);
        }

        // GET: ClassStudents/Create
        public async Task<IActionResult> Create(int? id)
        {
            var classModel = await _context.ClassModels.FindAsync(id);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "FullName");

            return View(classModel);
        }

        // POST: ClassStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassStudent classStudent)
        {
            _context.Add(classStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ClassStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassStudents == null)
            {
                return NotFound();
            }

            var classStudent = await _context.ClassStudents.FindAsync(id);
            if (classStudent == null)
            {
                return NotFound();
            }
            var categorySelectListItems = _context.ClassModels.Select(c => new SelectListItem
            {
                Value = c.ClassID.ToString(),
                Text = $"{c.ClassName} | {c.Subject.SubjectCode}"
            });
            ViewData["ClassID"] = new SelectList(categorySelectListItems, "Value", "Text");
            //ViewData["ClassID"] = new SelectList(_context.ClassModels, "ClassID", "ClassSubject", classStudent.ClassID);
            ViewData["StudentID"] = new SelectList(_context.Students, "StudentID", "FullName", classStudent.StudentID);
            return View(classStudent);
        }

        // POST: ClassStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClassStudent classStudent)
        {
            _context.Update(classStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ClassStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassStudents == null)
            {
                return NotFound();
            }

            var classStudent = await _context.ClassStudents
                .Include(c => c.Class)
                .Include(c => c.Student)
                .Include(c => c.Class.Subject)
                .FirstOrDefaultAsync(m => m.ClassStudentID == id);
            if (classStudent == null)
            {
                return NotFound();
            }

            return View(classStudent);
        }

        // POST: ClassStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassStudents == null)
            {
                return Problem("Entity set 'DatabaseDbContext.ClassStudents'  is null.");
            }
            var classStudent = await _context.ClassStudents.FindAsync(id);
            if (classStudent != null)
            {
                _context.ClassStudents.Remove(classStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassStudentExists(int id)
        {
            return (_context.ClassStudents?.Any(e => e.ClassStudentID == id)).GetValueOrDefault();
        }
    }
}
