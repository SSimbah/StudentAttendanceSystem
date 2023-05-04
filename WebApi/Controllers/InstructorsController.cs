using Domain.DataAccess;
using Domain.Entities;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private IInstructorRepository instructorRepository;

        public InstructorsController(DatabaseDbContext context)
        {
            instructorRepository = new InstructorRepository(context);
        }

        //// GET: Instructors
        //public async Task<IActionResult> Index()
        //{
        //    var instructors = await instructorRepository.GetInstructorsAsync();

        //    return View(instructors);
        //}

        // API - Get All Instructors / Index
        [HttpGet]
        public async Task<ActionResult> GetAllInstructors()
        {
            try
            {
                return Ok(await instructorRepository.GetInstructorsAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        //// GET: Instructors/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    var instructor = await instructorRepository.GetInstructorByIdAsync(id);

        //    if (id == 0 || instructor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(instructor);
        //}

        // API - Get Instructor Details / Details
        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetInstructor(int id)
        {
            var instructor = await instructorRepository.GetInstructorByIdAsync(id);

            if (id == 0 || instructor == null)
            {
                return NotFound();
            }

            return instructor;
        }

        //// GET: Instructors/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Instructors/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Instructor instructor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        instructorRepository.CreateInstructortAsync(instructor);
        //        //await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(instructor);
        //}
        // API - Post Student / Create
        [HttpPost]
        public async Task<ActionResult<Instructor>> PostInstructor(Instructor instructor)
        {
            await instructorRepository.CreateInstructortAsync(instructor);

            if (instructor == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetInstructor),
                    new { id = instructor.InstructorID }, instructor);
        }

        //// GET: Instructors/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var instructor = await instructorRepository.GetInstructorByIdAsync(id);

        //    if (id == 0 || instructor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(instructor);
        //}

        //// POST: Instructors/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Instructor instructor)
        //{
        //    instructorRepository.UpdateInstructorAsync(instructor);
        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // API - PUT Instructor / Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Instructor instructor)
        {
            if (id != instructor.InstructorID)
            {
                return BadRequest();
            }

            await instructorRepository.CheckInputAsync(instructor);
            await instructorRepository.UpdateInstructorAsync(instructor);


            return Ok();
        }

        //// GET: Instructors/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    await instructorRepository.DeleteInstructorAsync(id);

        //    return RedirectToAction(nameof(Index));
        //}

        // API - DELETE Instructor / Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instructor>> DeleteInstructor (int id)
        {
            try
            {
                var instructor = await instructorRepository.GetInstructorByIdAsync(id);

                if (instructor == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                await instructorRepository.DeleteInstructorAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        //public async Task<IActionResult> InstructorClassList(int id)
        //{
        //    var instructor = await instructorRepository.GetInstructorByIdAsync(id);
        //    ViewBag.InstructorName = instructor.FullName;
        //    var instructorClasses = await instructorRepository.GetInstructorClassesAsync(id);
        //    ViewBag.StudentID = id;
        //    return View(instructorClasses);
        //}
    }
}
