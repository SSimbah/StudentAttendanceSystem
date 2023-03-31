using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentAttendanceSystem.Controllers
{
    public class InstructorsController : Controller
    {
        //private readonly DatabaseDbContext _context;

        //public InstructorsController(DatabaseDbContext context)
        //{
        //    _context = context;
        //}
        private IInstructorRepository instructorRepository;

        public InstructorsController(DatabaseDbContext context)
        {
            instructorRepository = new InstructorRepository(context);
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
              //return _context.Instructors != null ? 
              //            View(await _context.Instructors.ToListAsync()) :
              //            Problem("Entity set 'DatabaseDbContext.Instructors'  is null.");

            var instructors = await instructorRepository.GetInstructorsAsync();

            return View(instructors);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var instructor = await instructorRepository.GetInstructorByIdAsync(id);

            if (id == 0 || instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
            //if (id == null || _context.Instructors == null)
            //{
            //    return NotFound();
            //}

            //var instructor = await _context.Instructors
            //    .FirstOrDefaultAsync(m => m.InstructorID == id);
            //if (instructor == null)
            //{
            //    return NotFound();
            //}

            //return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstructorNum,InstructorPassword,FirstName,LastName,Gender,Age")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.CreateInstructortAsync(instructor);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await instructorRepository.GetInstructorByIdAsync(id);

            if (id == 0 || instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Instructor instructor)
        {
            instructorRepository.UpdateInstructorAsync(instructor);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (id != instructor.InstructorID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(instructor);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!InstructorExists(instructor.InstructorID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            await instructorRepository.DeleteInstructorAsync(id);

            return RedirectToAction(nameof(Index));
            //if (id == null || _context.Instructors == null)
            //{
            //    return NotFound();
            //}

            //var instructor = await _context.Instructors
            //    .FirstOrDefaultAsync(m => m.InstructorID == id);
            //if (instructor == null)
            //{
            //    return NotFound();
            //}

            //return View(instructor);
        }

        // POST: Instructors/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Instructors == null)
        //    {
        //        return Problem("Entity set 'DatabaseDbContext.Instructors'  is null.");
        //    }
        //    var instructor = await _context.Instructors.FindAsync(id);
        //    if (instructor != null)
        //    {
        //        _context.Instructors.Remove(instructor);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool InstructorExists(int id)
        //{
        //  return (_context.Instructors?.Any(e => e.InstructorID == id)).GetValueOrDefault();
        //}
    }
}
