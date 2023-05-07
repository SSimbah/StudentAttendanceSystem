using Domain.DataAccess;
using Domain.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class StudentsController : ControllerBase
    {
        private IStudentRepository studentRepository;

        public StudentsController(DatabaseDbContext context)
        {
            studentRepository = new StudentRepository(context);
        }

        // GET: Students
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {

        //        var students = await studentRepository.GetStudentsAsync();

        //        return View(students);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Error = e.Message;
        //    }
        //    return View("Error");

        //}

        // GET: Students/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    var student = await studentRepository.GetStudentByIdAsync(id);

        //    if (id == 0 || student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        // API - Get All Student / Index
        [HttpGet]
        public async Task<ActionResult> GetAllStudent()
        {
            try
            {
                return Ok(await studentRepository.GetStudentsAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        // API - Get Student Details / Details
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await studentRepository.GetStudentByIdAsync(id);

            if (id == 0 || student == null)
            {
                return NotFound();
            }

            return student;
        }
        

        // GET: Students/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        studentRepository.CreateStudentAsync(student);
        //        //await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(student);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Student>> PostStudent(Student student)
        //{
        //    await studentRepository.CreateStudentAsync(student);

        //    return CreatedAtAction(nameof(GetStudent),new { id = student.StudentID },student);
        //}

        // API - Post Student / Create
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await studentRepository.CreateStudentAsync(student);

            if (student == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetStudent),
                    new { id = student.StudentID}, student);
        }

        // GET: Students/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var student = await studentRepository.GetStudentByIdAsync(id);

        //    if (id == 0 || student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Student student)
        //{
        //    studentRepository.UpdateStudentAsync(student);
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        //{
        //    try
        //    {
        //        if (id != student.StudentID)
        //            return BadRequest("Student ID mismatch");

        //        var employeeToUpdate = await employeeRepository.GetEmployee(id);

        //        if (employeeToUpdate == null)
        //            return NotFound($"Employee with Id = {id} not found");

        //        return await employeeRepository.UpdateEmployee(employee);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error updating data");
        //    }
        //}

        // API - PUT Student / Update
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            await studentRepository.CheckInputAsync(student);
            await studentRepository.UpdateStudentAsync(student);
            

            return NoContent();
        }


        //// GET: Students/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    await studentRepository.DeleteStudentAsync(id);

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStudent(int id)
        //{
        //    var student = await studentRepository.GetStudentByIdAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    studentRepository.DeleteStudentAsync(id);

        //    return NoContent();
        //}

        // API - DELETE Student / Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            try
            {
                var student = await studentRepository.GetStudentByIdAsync(id);

                if (student == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }

                await studentRepository.DeleteStudentAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        //public async Task<IActionResult> StudentClassList(int? id)
        //{
        //    // redirect to another action and controller with ID
        //    return RedirectToAction("Index", "StudentClasses", new { id = id });
        //}
    }
}
