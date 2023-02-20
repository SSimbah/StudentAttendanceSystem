using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Data;
using StudentAttendanceSystem.Models;

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
            var databaseDbContext = _context.ClassModels.Include(c => c.Instructor);
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
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "InstructorID", "FullName");
            List<SelectListItem> ScheduleDay = new()
            {
                new SelectListItem { Value = "Monday", Text = "Monday" },
                new SelectListItem { Value = "Tuesday", Text = "Tuesday" },
                new SelectListItem { Value = "Wednesday", Text = "Wednesday" },
                new SelectListItem { Value = "Thursday", Text = "Thursday" },
                new SelectListItem { Value = "Friday", Text = "Friday" },
                new SelectListItem { Value = "Saturday", Text = "Saturday" },
                new SelectListItem { Value = "Sunday", Text = "Sunday" }
            };
            ViewBag.ScheduleDay = ScheduleDay;
            return View();
        }

        // POST: ClassModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassModel classModel)
        {
            _context.Add(classModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    _context.Add(classModel);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["InstructorID"] = new SelectList(_context.Instructors, "InstructorID", "FirstName", classModel.InstructorID);
            //return View(classModel);
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
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "InstructorID", "FullName", classModel.InstructorID);
            List<SelectListItem> ScheduleDay = new()
            {
                new SelectListItem { Value = "Monday", Text = "Monday" },
                new SelectListItem { Value = "Tuesday", Text = "Tuesday" },
                new SelectListItem { Value = "Wednesday", Text = "Wednesday" },
                new SelectListItem { Value = "Thursday", Text = "Thursday" },
                new SelectListItem { Value = "Friday", Text = "Friday" },
                new SelectListItem { Value = "Saturday", Text = "Saturday" },
                new SelectListItem { Value = "Sunday", Text = "Sunday" }
            };

            //assigning SelectListItem to view Bag
            ViewBag.ScheduleDay = ScheduleDay;
            return View(classModel);
        }

        // POST: ClassModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClassModel classModel)
        {
            _context.Update(classModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (id != classModel.ClassID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(classModel);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ClassModelExists(classModel.ClassID))
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
            //ViewData["InstructorID"] = new SelectList(_context.Instructors, "InstructorID", "FirstName", classModel.InstructorID);
            //return View(classModel);
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
    }
}
