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
        }
        public async Task<IActionResult> InstructorClassList(int id)
        {
            var instructor = await instructorRepository.GetInstructorByIdAsync(id);
            ViewBag.InstructorName = instructor.FullName;
            var instructorClasses = await instructorRepository.GetInstructorClassesAsync(id);
            ViewBag.StudentID = id;
            return View(instructorClasses);
        }
    }
}
